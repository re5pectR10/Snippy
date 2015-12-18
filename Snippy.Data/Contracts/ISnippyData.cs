using Snippy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Data.Contracts
{
    public interface ISnippyData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Label> Labels { get; }

        IRepository<Language> Languages { get; }

        IRepository<Snippet> Snippets { get; }

        int SaveChanges();
    }
}
