namespace Snippy.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Snippy.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Snippy.Data.SnippyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Snippy.Data.SnippyDbContext";
        }

        protected override void Seed(Snippy.Data.SnippyDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedLanguages(context);
            this.SeedLabels(context);
            this.SeedSinppets(context);
            this.SeedComments(context);
        }

        private void SeedComments(Snippy.Data.SnippyDbContext context)
        {
            if (!(context.Comments.Any()))
            {
                var Comments = new List<Comment>()
                {
                    new Comment()
                    { 
                        Content = "Now that's really funny! I like it!",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                        CreatedAt = new DateTime(2015, 10, 30, 11, 50, 38),
                        Snippet = context.Snippets.FirstOrDefault(u => u.Title == "Ternary Operator Madness")
                    },
                    new Comment()
                    { 
                        Content = "Here, have my comment!",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "newUser"),
                        CreatedAt = new DateTime(2015, 11, 01, 15, 52, 42),
                        Snippet = context.Snippets.FirstOrDefault(u => u.Title == "Ternary Operator Madness")
                    },
                    new Comment()
                    { 
                        Content = "I didn't manage to comment first :(",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "someUser"),
                        CreatedAt = new DateTime(2015, 11, 02, 05, 30, 20),
                        Snippet = context.Snippets.FirstOrDefault(u => u.Title == "Ternary Operator Madness")
                    },
                    new Comment()
                    { 
                        Content = "That's why I love Python - everything is so simple!",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "newUser"),
                        CreatedAt = new DateTime(2015, 10, 27, 15, 28, 14),
                        Snippet = context.Snippets.FirstOrDefault(u => u.Title == "Reverse a String")
                    },
                    new Comment()
                    { 
                        Content = "I have always had problems with Geometry in school. Thanks to you I can now do this without ever having to learn this damn subject",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "someUser"),
                        CreatedAt = new DateTime(2015, 10, 29, 15, 08, 42),
                        Snippet = context.Snippets.FirstOrDefault(u => u.Title == "Points Around A Circle For GameObject Placement")
                    },
                    new Comment()
                    { 
                        Content = "Thank you. However, I think there must be a simpler way to do this. I can't figure it out now, but I'll post it when I'm ready.",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "someUser"),
                        CreatedAt = new DateTime(2015, 11, 03, 12, 56, 20),
                        Snippet = context.Snippets.FirstOrDefault(u => u.Title == "Numbers only in an input field")
                    }
                };

                foreach (var c in Comments)
                {
                    context.Comments.Add(c);
                }

                context.SaveChanges();
            }
        }

        private void SeedSinppets(Snippy.Data.SnippyDbContext context)
        {
            if (!(context.Snippets.Any()))
            {
                var Snippets = new List<Snippet>()
                {
                    new Snippet() 
                    { 
                        Title = "Ternary Operator Madness",
                        Description = "This is how you DO NOT user ternary operators in C#!",
                        Code = "bool X = Glob.UserIsAdmin ? true : false;",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                        CreatedAt = new DateTime(2015, 10, 26, 17, 20, 33),
                        Language = context.Languages.FirstOrDefault(l => l.Name == "C#"),
                        Labels = context.Labels.Where(l => l.Text == "funny").ToList()
                    },
                    new Snippet() 
                    { 
                        Title = "Points Around A Circle For GameObject Placement",
                        Description = "Determine points around a circle which can be used to place elements around a central point",
                        Code = "private Vector3 DrawCircle(float centerX, float centerY, float radius, float totalPoints, float currentPoint){float ptRatio = currentPoint / totalPoints;float pointX = centerX + (Mathf.Cos(ptRatio * 2 * Mathf.PI)) * radius;float pointY = centerY + (Mathf.Sin(ptRatio * 2 * Mathf.PI)) * radius;Vector3 panelCenter = new Vector3(pointX, pointY, 0.0f);return panelCenter;}",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                        CreatedAt = new DateTime(2015, 10, 26, 20, 15, 30),
                        Language = context.Languages.FirstOrDefault(l => l.Name == "C#"),
                        Labels = context.Labels.Where(l => l.Text == "geometry" || l.Text == "games").ToList()
                    },
                    new Snippet() 
                    { 
                        Title = "forEach. How to break?",
                        Description = "Array.prototype.forEach You can't break forEach. So use \"some\" or \"every\". Array.prototype.some some is pretty much the same as forEach but it break when the callback returns true. Array.prototype.every every is almost identical to some except it's expecting false to break the loop.",
                        Code = "var ary = [\"JavaScript\", \"Java\", \"CoffeeScript\", \"TypeScript\"];ary.some(function (value, index, _ary) {console.log(index + \": \" + value);return value === \"CoffeeScript\";});// output: // 0: JavaScript// 1: Java// 2: CoffeeScriptary.every(function(value, index, _ary) {console.log(index + \": \" + value)return value.indexOf(\"Script\") > -1;});// output:// 0: JavaScript// 1: Java",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "newUser"),
                        CreatedAt = new DateTime(2015, 10, 27, 10, 53, 20),
                        Language = context.Languages.FirstOrDefault(l => l.Name == "JavaScript"),
                        Labels = context.Labels.Where(l => l.Text == "jquery" || l.Text == "useful" || l.Text == "web" || l.Text == "front-end").ToList()
                    },
                    new Snippet() 
                    { 
                        Title = "Numbers only in an input field",
                        Description = "Method allowing only numbers (positive / negative / with commas or decimal points) in a field",
                        Code = "$(\"#input\").keypress(function(event){var charCode = (event.which) ? event.which : window.event.keyCode;(charCode <= 13) { return true; } else {var keyChar = String.fromCharCode(charCode);var regex = new RegExp(\"[0-9,.-]\");return regex.test(keyChar); } });",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "someUser"),
                        CreatedAt = new DateTime(2015, 10, 28, 09, 00, 56),
                        Language = context.Languages.FirstOrDefault(l => l.Name == "JavaScript"),
                        Labels = context.Labels.Where(l => l.Text == "web" || l.Text == "front-end").ToList()
                    },
                    new Snippet() 
                    { 
                        Title = "Reverse a String",
                        Description = "Almost not worth having a function for...",
                        Code = "def reverseString(s):\"\"\"Reverses a string given to it.\"\"\"return s[::-1]",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                        CreatedAt = new DateTime(2015, 10, 26, 09, 35, 13),
                        Language = context.Languages.FirstOrDefault(l => l.Name == "Python"),
                        Labels = context.Labels.Where(l => l.Text == "useful").ToList()
                    },
                    new Snippet() 
                    { 
                        Title = "Create a link directly in an SQL query",
                        Description = "That's how you create links - directly in the SQL!",
                        Code = "SELECT DISTINCTb.Id,concat('<button type=\"\"button\"\" onclick=\"\"DeleteContact(', cast(b.Id as char), ')\"\">Delete...</button>') as lnkDeleteFROM tblContact   bWHERE ....",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                        CreatedAt = new DateTime(2015, 10, 30, 11, 20, 00),
                        Language = context.Languages.FirstOrDefault(l => l.Name == "SQL"),
                        Labels = context.Labels.Where(l => l.Text == "funny" || l.Text == "bug" || l.Text == "mysql").ToList()
                    },
                    new Snippet() 
                    { 
                        Title = "Pure CSS Text Gradients",
                        Description = "This code describes how to create text gradients using only pure CSS",
                        Code = "/* CSS text gradients */h2[data-text] {position: relative;}h2[data-text]::after {content: attr(data-text);z-index: 10;color: #e3e3e3;position: absolute;top: 0;left: 0;-webkit-mask-image: -webkit-gradient(linear, left top, left bottom, from(rgba(0,0,0,0)), color-stop(50%, rgba(0,0,0,1)), to(rgba(0,0,0,0)));",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "someUser"),
                        CreatedAt = new DateTime(2015, 10, 22, 19, 26, 42),
                        Language = context.Languages.FirstOrDefault(l => l.Name == "CSS"),
                        Labels = context.Labels.Where(l => l.Text == "web" || l.Text == "front-end").ToList()
                    },
                    new Snippet() 
                    { 
                        Title = "Check for a Boolean value in JS",
                        Description = "How to check a Boolean value - the wrong but funny way :D",
                        Code = "var b = true;if (b.toString().length < 5) {//...}",
                        Author = context.Users.FirstOrDefault(u => u.UserName == "admin"),
                        CreatedAt = new DateTime(2015, 10, 22, 05, 30, 04),
                        Language = context.Languages.FirstOrDefault(l => l.Name == "JavaScript"),
                        Labels = context.Labels.Where(l => l.Text == "funny").ToList()
                    }
                };

                foreach (var s in Snippets)
                {
                    context.Snippets.Add(s);
                }

                context.SaveChanges();
            }
        }

        private void SeedLabels(Snippy.Data.SnippyDbContext context)
        {
            if (!(context.Labels.Any()))
            {
                var Labels = new List<Label>()
                {
                    new Label() { Text = "bug" },
                    new Label() { Text = "funny" },
                    new Label() { Text = "jquery" },
                    new Label() { Text = "mysql" },
                    new Label() { Text = "useful" },
                    new Label() { Text = "web" },
                    new Label() { Text = "geometry" },
                    new Label() { Text = "back-end" },
                    new Label() { Text = "front-end" },
                    new Label() { Text = "games" }
                };

                foreach (var l in Labels)
                {
                    context.Labels.Add(l);
                }

                context.SaveChanges();
            }
        }

        private void SeedLanguages(Snippy.Data.SnippyDbContext context)
        {
            if (!(context.Languages.Any()))
            {
                var Languages = new List<Language>()
                {
                    new Language() { Name = "C#" },
                    new Language() { Name = "JavaScript" },
                    new Language() { Name = "Python" },
                    new Language() { Name = "CSS" },
                    new Language() { Name = "SQL" },
                    new Language() { Name = "PHP" }
                };

                foreach (var l in Languages)
                {
                    context.Languages.Add(l);
                }

                context.SaveChanges();
            }
        }

        private void SeedRoles(Snippy.Data.SnippyDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }
        }

        private void SeedUsers(Snippy.Data.SnippyDbContext context)
        {
            if (!(context.Users.Any()))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "admin", Email = "admin@snippy.softuni.com" };
                userManager.Create(userToInsert, "adminPass123");
                userManager.AddToRole(userToInsert.Id, "Admin");

                userToInsert = new ApplicationUser { UserName = "newUser", Email = "new_user@gmail.com" };
                userManager.Create(userToInsert, "userPass123");

                userToInsert = new ApplicationUser { UserName = "someUser", Email = "someUser@example.com" };
                userManager.Create(userToInsert, "someUserPass123");
            }
        }
    }
}
