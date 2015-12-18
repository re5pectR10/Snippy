using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Snippy.App.HtmlHelpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString Pagination(this HtmlHelper helper, int currnetPage, int allPages, string actionName, string controllerName)
        {
            TagBuilder nav = new TagBuilder("nav");
            TagBuilder ul = new TagBuilder("ul");
            ul.MergeAttribute("class", "pagination");
            TagBuilder li = new TagBuilder("li");
            if (currnetPage == 1)
            {
                li.AddCssClass("disabled");
            }

            li.InnerHtml = LinkExtensions.ActionLink(helper, "<<", actionName, controllerName, new { id = currnetPage - 1 }, htmlAttributes: null).ToString();
            ul.InnerHtml = li.ToString();
            for (int i = 1; i <= allPages; i++)
            {
                li = new TagBuilder("li");
                if (i == currnetPage)
                {
                    li.AddCssClass("active");
                }

                li.InnerHtml = LinkExtensions.ActionLink(helper, i.ToString(), actionName, controllerName, new { id = i }, htmlAttributes: null).ToString();
                ul.InnerHtml += li.ToString();
            }

            li = new TagBuilder("li");
            if (currnetPage == allPages)
            {
                li.AddCssClass("disabled");
            }

            li.InnerHtml = LinkExtensions.ActionLink(helper, ">>", actionName, controllerName, new { id = currnetPage + 1 }, htmlAttributes: null).ToString();
            ul.InnerHtml += li.ToString();
            nav.InnerHtml = ul.ToString();
            return new MvcHtmlString(nav.ToString());
        }
    }
}