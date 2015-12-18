using Snippy.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippy.App.Models.ViewModels.Labels;

namespace Snippy.App.Controllers
{
    public class LabelController : BaseController
    {
        public LabelController(ISnippyData data)
            : base(data)
        {

        }
     
        public ActionResult GetLabel(int id)
        {
            var label = this.Data.Labels.All().Where(l => l.Id == id).Select(LabelViewModel.Create).FirstOrDefault();
            return View(label);
        }
    }
}