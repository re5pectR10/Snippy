using Snippy.App.Models.ViewModels.Language;
using Snippy.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snippy.App.Controllers
{
    public class LanguageController : BaseController
    {
        public LanguageController(ISnippyData data)
            : base(data)
        {

        }

        public ActionResult GetLanguage(int id)
        {
            var language = this.Data.Languages.All().Where(l => l.Id == id).Select(LanguageViewModel.Create).FirstOrDefault();
            return View(language);
        }
    }
}