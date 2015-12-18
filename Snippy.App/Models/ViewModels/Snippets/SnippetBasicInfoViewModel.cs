using Snippy.App.Models.ViewModels.Labels;
using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Snippy.App.Models.ViewModels.Snippets
{
    public class SnippetBasicInfoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<LabelBasicInfoViewModel> Labels { get; set; }

        public static Expression<Func<Snippet, SnippetBasicInfoViewModel>> Create
        {
            get
            {
                return s => new SnippetBasicInfoViewModel()
                {
                    Id = s.Id,
                    Title = s.Title,
                    Labels = s.Labels.Select(l => new LabelBasicInfoViewModel()
                    {
                        Id = l.Id,
                        Text = l.Text
                    })
                };
            }
        }
    }
}