using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Snippy.App.Models.ViewModels.Labels
{
    public class LabelIndexPageViewModel : LabelBasicInfoViewModel
    {
        public int SnippetsCount { get; set; }

        public static Expression<Func<Label, LabelIndexPageViewModel>> Create
        {
            get
            {
                return s => new LabelIndexPageViewModel()
                {
                    Id = s.Id,
                    Text = s.Text,
                    SnippetsCount = s.Snippets.Count()
                };
            }
        }
    }
}