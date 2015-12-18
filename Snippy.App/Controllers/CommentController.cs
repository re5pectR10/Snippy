using Snippy.App.Models.BindingModels;
using Snippy.Data.Contracts;
using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Snippy.App.Models.ViewModels.Comments;
using System.Net;
using Snippy.App.Extensions;

namespace Snippy.App.Controllers
{
    public class CommentController : BaseController
    {
        public CommentController(ISnippyData data)
            :base(data)
        {

        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post(CommentBindingModel comment, int id)
        {
            var snippet = this.Data.Snippets.All().FirstOrDefault(s => s.Id == id);
            if (snippet == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Snippet!");
            }

            var userId = User.Identity.GetUserId();
            var commentToInsert = new Comment()
            {
                Content = comment.Content,
                CreatedAt = DateTime.Now,
                SnippetId = snippet.Id,
                AuthorId = userId
            };

            this.Data.Comments.Add(commentToInsert);
            this.Data.SaveChanges();

            var commentFromDb = this.Data.Comments.All()
                .Where(c => c.Id == commentToInsert.Id)
                .Select(CommentBasicInfoViewModel.Create).FirstOrDefault();

            return PartialView("DisplayTemplates/CommentBasicInfoViewModel", commentFromDb);
        }

        [Authorize]
        public ActionResult Remove(int id)
        {
            var userId = User.Identity.GetUserId();
            var comment = this.Data.Comments.All().Where(c => c.Id == id && c.AuthorId == userId).Select(CommentWithSnippetViewModel.Create).FirstOrDefault();
            return View(comment);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var comment = this.Data.Comments.All().Where(c => c.Id == id && c.AuthorId == userId).FirstOrDefault();
            this.Data.Comments.Delete(comment);
            this.Data.SaveChanges();
            this.AddNotification("The comment was deleted", NotificationType.SUCCESS);
            return RedirectToAction("GetSnippet", "Snippet", new { id = comment.SnippetId });
        }
    }
}