using Snippy.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippy.App.Models.ViewModels.Snippets;
using Snippy.App.Extensions;
using Microsoft.AspNet.Identity;
using Snippy.App.Models.ViewModels.Language;
using Snippy.App.Models;
using Snippy.Models;

namespace Snippy.App.Controllers
{
    public class SnippetController : BaseController
    {
        private const int ItemPerPage = 5;
        public SnippetController(ISnippyData data)
            :base(data)
        {

        }

        // id is the page
        public ActionResult All(int id = 1)
        {
            var page = id < 1 ? 1 : id;
            ViewBag.allPages = Math.Ceiling((decimal)this.Data.Snippets.All().Count() / SnippetController.ItemPerPage);
            if (page > ViewBag.allPages)
            {
                return RedirectToAction("All", "Snippet", new { id = ViewBag.allPages });
            }

            page--;
            var snippets = this.Data.Snippets.All()
                .OrderByDescending(s => s.CreatedAt)
                .Skip(page * SnippetController.ItemPerPage)
                .Take(SnippetController.ItemPerPage)
                .Select(SnippetBasicInfoViewModel.Create);

            page++;
            ViewBag.currentPage = page;
            ViewBag.allPages = Math.Ceiling((decimal)this.Data.Snippets.All().Count() / SnippetController.ItemPerPage);
            return View(snippets);
        }

        public ActionResult GetSnippet(int id)
        {
            var snippet = this.Data.Snippets.All().Where(s => s.Id == id).Select(SnippetViewModel.Create).FirstOrDefault();
            if (snippet == null)
            {
                this.AddNotification("Invalid snippet id", NotificationType.ERROR);
                return RedirectToAction("Index", "Home");
            }

            return View(snippet);
        }

        [Authorize]
        public ActionResult My()
        {
            var userId = User.Identity.GetUserId();
            var snippets = this.Data.Snippets.All().Where(s => s.AuthorId == userId).Select(SnippetBasicInfoViewModel.Create).ToList();

            return View(snippets);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Add()
        {
            var languages = this.SetLanguages();

            var snippet = new SnippetInputModel()
            {
                Languages = languages
            };

            return View(snippet);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Add(SnippetInputModel snippet)
        {
            if (snippet != null && this.ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var snippetToInsert = new Snippet()
                {
                    AuthorId = userId,
                    Code = snippet.Code,
                    CreatedAt = DateTime.Now,
                    Description = snippet.Description,
                    Title = snippet.Title,
                    LanguageId = snippet.LanguageId
                };

                this.Data.Snippets.Add(snippetToInsert);
                this.Data.SaveChanges();

                var snippetFromDb = this.Data.Snippets.All().FirstOrDefault(s => s.Id == snippetToInsert.Id);
                if (snippet.Labels != null)
                {
                    var labels = snippet.Labels.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var l in labels)
                    {
                        this.AddLabelToSnippet(snippetFromDb, l);
                    }
                }

                this.AddNotification("The snippet was created", NotificationType.SUCCESS);
                return RedirectToAction("GetSnippet", "Snippet", new { id = snippetToInsert.Id });
            }

            var languages = this.SetLanguages();
            snippet.Languages = languages;
            this.AddNotification("Something is wrong", NotificationType.ERROR);
            return View(snippet);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var snippetFromDb = this.Data.Snippets.All().Where(s => s.Id == id && s.AuthorId == userId).FirstOrDefault();
            if (snippetFromDb == null)
            {
                return RedirectToAction("My", "Snippet");
            }

            var snippet = new SnippetInputModel()
            {
                Id = snippetFromDb.Id,
                Code = snippetFromDb.Code,
                Description = snippetFromDb.Description,
                LanguageId = snippetFromDb.LanguageId,
                Title = snippetFromDb.Title,
                Labels = string.Join(";", snippetFromDb.Labels.Select(l => l.Text).ToArray())
            };

            snippet.Languages = this.SetLanguages();
            return View(snippet);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(SnippetInputModel snippet, int id)
        {
            var userId = User.Identity.GetUserId();
            if (snippet != null && this.ModelState.IsValid)
            {
                var snippetFromDb = this.Data.Snippets.All().Where(s => s.Id == id && s.AuthorId == userId).FirstOrDefault();
                if (snippetFromDb != null)
                {
                    snippetFromDb.Code = snippet.Code;
                    snippetFromDb.Description = snippet.Description;
                    snippetFromDb.Title = snippet.Title;
                    snippetFromDb.LanguageId = snippet.LanguageId;
                    snippetFromDb.Labels.Clear();
                    if (snippet.Labels != null)
                    {
                        var labels = snippet.Labels.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var l in labels)
                        {
                            this.AddLabelToSnippet(snippetFromDb, l);
                        }
                    }

                    this.Data.SaveChanges();
                    this.AddNotification("The snippet was edited", NotificationType.SUCCESS);
                    return RedirectToAction("GetSnippet", "Snippet", new { id = id });
                }
            }

            this.AddNotification("Something is wrong", NotificationType.ERROR);
            snippet.Languages = this.SetLanguages();
            return View(snippet);
        }

        public ActionResult Search(string search)
        {
            var snippets = this.Data.Snippets.All()
                .Where(s => s.Title.Contains(search) || s.Labels.Any(l => l.Text.Contains(search)))
                .Select(SnippetBasicInfoViewModel.Create)
                .ToList();

            return View(snippets);
        }

        private void AddLabelToSnippet(Snippet snippet, string labelName)
        {
            var label = labelName.Trim();
            var labelFromDb = this.Data.Labels.All().FirstOrDefault(lb => lb.Text == label);
            if (labelFromDb == null)
            {
                var labelToInsert = new Label()
                {
                    Text = label
                };
                this.Data.Labels.Add(labelToInsert);
                this.Data.SaveChanges();
                labelFromDb = this.Data.Labels.All().FirstOrDefault(lb => lb.Id == labelToInsert.Id);
            }

            snippet.Labels.Add(labelFromDb);
            this.Data.SaveChanges();
        }

        private IList<LanguageBasicInfoViewModel> SetLanguages()
        {
            return this.Data.Languages.All().Select(l => new LanguageBasicInfoViewModel()
            {
                Id = l.Id,
                Name = l.Name
            }).ToList();
        }
    }
}