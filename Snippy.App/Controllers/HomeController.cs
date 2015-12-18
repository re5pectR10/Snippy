using Snippy.App.Models.ViewModels;
using Snippy.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippy.App.Models.ViewModels.Snippets;
using Snippy.App.Models.ViewModels.Comments;
using Snippy.App.Models.ViewModels.Labels;

namespace Snippy.App.Controllers
{
    public class HomeController : BaseController
    {
        private const int ItemsCount = 5;

        public HomeController(ISnippyData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            var data = new IndexPageViewModel()
            {
                Snippets = this.Data.Snippets.All()
                .OrderByDescending(s => s.CreatedAt)
                .Take(HomeController.ItemsCount)
                .Select(SnippetBasicInfoViewModel.Create),

                Comments = this.Data.Comments.All()
                .OrderByDescending(c=>c.CreatedAt)
                .Take(HomeController.ItemsCount)
                .Select(CommentWithSnippetViewModel.Create),

                Labels = this.Data.Labels.All()
                .OrderByDescending(l=>l.Snippets.Count())
                .Take(HomeController.ItemsCount)
                .Select(LabelIndexPageViewModel.Create)
            };

            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}