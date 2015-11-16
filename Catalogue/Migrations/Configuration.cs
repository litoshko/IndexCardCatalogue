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

        protected override void Seed(Catalogue.Models.ApplicationDbContext context)
        {
            SeedIdentity(context);

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

        private static void SeedIdentity(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                context.Roles.Add(new IdentityRole("Admin"));
            }
            if (!context.Users.Any(u => u.UserName == "admin@x.ua"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@x.ua" };

                manager.Create(user, "password");

                manager.AddToRole(user.Id, "Admin");
            }

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
