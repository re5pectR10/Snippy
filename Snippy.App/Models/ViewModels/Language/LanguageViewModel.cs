using Snippy.App.Models.ViewModels.Labels;
using Snippy.App.Models.ViewModels.Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Snippy.App.Models.ViewModels.Language
{
    public class LanguageViewModel : LanguageBasicInfoViewModel
    {
        public IEnumerable<SnippetBasicInfoViewModel> Snippets { get; set; }

        public static Expression<Func<Snippy.Models.Language, LanguageViewModel>> Create
        {
            get
            {
                return s => new LanguageViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Snippets = s.Snippets.Select(l => new SnippetBasicInfoViewModel()
                    {
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