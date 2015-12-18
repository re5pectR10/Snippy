using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Snippy.App.Models.ViewModels.Comments
{
    public class CommentBasicInfoViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string AuthorUsername { get; set; }
        public DateTime CreatedAt { get; set; }

        public static Expression<Func<Comment, CommentBasicInfoViewModel>> Create
        {
            get
            {
                return s => new CommentBasicInfoViewModel()
                {
                    Id = s.Id,
                    Content = s.Content,
                    AuthorUsername = s.Author.UserName,
                    CreatedAt = s.CreatedAt
                };
            }
        }
    }
}