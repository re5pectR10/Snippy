using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Snippy.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Snippet> snippets;
        private ICollection<Comment> comments;

        public ApplicationUser()
        {
            this.snippets = new HashSet<Snippet>();
            this.comments = new HashSet<Comment>();
        }

        public virtual ICollection<Snippet> Snippets { get { return this.snippets; } set { this.snippets = value; } }
        public virtual ICollection<Comment> Comments { get { return this.comments; } set { this.comments = value; } }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
