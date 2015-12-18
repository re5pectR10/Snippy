namespace Snippy.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Snippy.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SnippyDbContext : IdentityDbContext<ApplicationUser>
    {
        public SnippyDbContext()
            : base("name=SnippyDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SnippyDbContext, Migrations.Configuration>());
        }

        public virtual DbSet<Snippet> Snippets { get; set; }
        public virtual DbSet<Label> Labels { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public static SnippyDbContext Create()
        {
            return new SnippyDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Snippet>()
                .HasRequired<ApplicationUser>(x => x.Author)
                .WithMany(x => x.Snippets)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}