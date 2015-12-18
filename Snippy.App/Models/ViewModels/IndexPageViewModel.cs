using Snippy.App.Models.ViewModels.Comments;
using Snippy.App.Models.ViewModels.Labels;
using Snippy.App.Models.ViewModels.Snippets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippy.App.Models.ViewModels
{
    public class IndexPageViewModel
    {
        public IEnumerable<SnippetBasicInfoViewModel> Snippets { get; set; }
        public IEnumerable<CommentWithSnippetViewModel> Comments { get; set; }
        public IEnumerable<LabelIndexPageViewModel> Labels { get; set; }
    }
}