using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Snippy.Data.Contracts;

namespace Snippy.App.Controllers
{
    public class BaseController : Controller
    {
        protected BaseController(ISnippyData data)
        {
            this.Data = data;
        }

        protected ISnippyData Data { get; private set; }
    }
}