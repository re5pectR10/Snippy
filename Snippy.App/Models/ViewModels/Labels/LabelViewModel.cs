using Snippy.App.Models.ViewModels.Snippets;
using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Snippy.App.Models.ViewModels.Labels
{
    public class LabelViewModel : LabelBasicInfoViewModel
    {
        public IEnumerable<SnippetBasicInfoViewModel> Snippets { get; set; }

        public static Expression<Func<Label, LabelViewModel>> Create
        {
            get
            {
                return s => new LabelViewModel()
                {
                    Id = s.Id,
                    Text = s.Text,
                    Snippets = s.Snippets.Select(l => new SnippetBasicInfoViewModel() {
                        Id = l.Id,
                        Title = l.Title,
                        Labels = l.Labels.Select(p => new LabelBasicInfoViewModel()
                        {
                            Id = p.Id,
                            Text = p.Text
                        })
                    })
                };
            }
        }
    }
}