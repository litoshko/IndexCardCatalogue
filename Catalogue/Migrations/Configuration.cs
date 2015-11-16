namespace Catalogue.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Catalogue.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Catalogue.Models.ApplicationDbContext";
        }

        /// <summary>
        /// Seed method for EF migration. Used for initial seed of application database.
        /// </summary>
        /// <param name="context">Database context to seed into.</param>
        protected override void Seed(Catalogue.Models.ApplicationDbContext context)
        {
            // Seed Identity with users.
            SeedIdentity(context);

            // Add Records and Reviews to database.
            context.Records.AddOrUpdate(r => r.Title,
                new Record
                {
                    Title = "CLR via C#",
                    Author = "Richter",
                    Description = "Excellent programming book",
                    OwnerName = "x@x.ua"
                },
                new Record
                {
                    Title = "C# 5.0 in a Nutshell",
                    Author = "Albahari",
                    Description = "Decent language reference",
                    OwnerName = "x@x.ua"
                },
                new Record
                {
                    Title = "C# Yellow book",
                    Author = "Miles",
                    Description = "Programming book for beginners",
                    OwnerName = "x@x.ua",
                    Reviews = new List<Review>
                    {
                        new Review { Rating = 9, Comment = "Great book for a start!" }
                    }
                });
        }

        /// <summary>
        /// Adds two users to database. Makes one of them Admin.
        /// </summary>
        /// <param name="context">DbContext to add users to.</param>
        private static void SeedIdentity(ApplicationDbContext context)
        {
            // Create role Admin if it does not exist.
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                context.Roles.Add(new IdentityRole("Admin"));
            }

            // Add user to database if user with given username does not exist.
            // Add role Admin for freshly created user
            if (!context.Users.Any(u => u.UserName == "admin@x.ua"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@x.ua" };

                manager.Create(user, "password");

                manager.AddToRole(user.Id, "Admin");
            }

            // Add user to database if user with given username does not exist.
            if (!context.Users.Any(u => u.UserName == "x@x.ua"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "x@x.ua" };

                manager.Create(user, "password");
            }
        }
    }
}
