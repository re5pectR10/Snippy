using Snippy.App.Models.ViewModels.Language;
using Snippy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Snippy.App.Models
{
    public class SnippetInputModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Code { get; set; }
        public int LanguageId { get; set; }
        public IEnumerable<LanguageBasicInfoViewModel> Languages { get; set; }
        public string Labels { get; set; }
    }
}