using Snippy.App.Models.ViewModels.Comments;
using Snippy.App.Models.ViewModels.Labels;
using Snippy.App.Models.ViewModels.Language;
using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Snippy.App.Models.ViewModels.Snippets
{
    public class SnippetViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LanguageBasicInfoViewModel Language { get; set; }
        public string AuthorUsername { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<CommentBasicInfoViewModel> Comments { get; set; }
        public IEnumerable<LabelBasicInfoViewModel> Labels { get; set; }

        public static Expression<Func<Snippet, SnippetViewModel>> Create
        {
            get
            {
                return s => new SnippetViewModel()
                {
                    Id = s.Id,
                    Title = s.Title,
                    Code = s.Code,
                    Description = s.Description,
                    CreatedAt = s.CreatedAt,
                    Language = new LanguageBasicInfoViewModel()
                    {
                        Id = s.Language.Id,
                        Name = s.Language.Name
                    },
                    AuthorUsername = s.Author.UserName,
                    Labels = s.Labels.Select(l => new LabelBasicInfoViewModel()
                    {
                        Id = l.Id,
                        Text = l.Text
                    }),
                    Comments = s.Comments.OrderByDescending(c => c.CreatedAt).Select(c => new CommentBasicInfoViewModel()
                    {
                        Id = c.Id,
                        Content = c.Content,
                        CreatedAt = c.CreatedAt,
                        AuthorUsername = c.Author.UserName
                    })
                };
            }
        }
    }
}