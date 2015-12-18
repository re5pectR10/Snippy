using Snippy.App.Models.ViewModels.Labels;
using Snippy.App.Models.ViewModels.Snippets;
using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Snippy.App.Models.ViewModels.Comments
{
    public class CommentWithSnippetViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorUsername { get; set; }
        public DateTime CreatedAt { get; set; }
        public SnippetBasicInfoViewModel Snippet { get; set; }

        public static Expression<Func<Comment, CommentWithSnippetViewModel>> Create
        {
            get
            {
                return s => new CommentWithSnippetViewModel()
                {
                    Id = s.Id,
                    Content = s.Content,
                    AuthorUsername = s.Author.UserName,
                    CreatedAt = s.CreatedAt,
                    Snippet = new SnippetBasicInfoViewModel()
                    {
                        Id = s.Snippet.Id,
                        Title = s.Snippet.Title
                    }
                };
            }
        }
    }
}