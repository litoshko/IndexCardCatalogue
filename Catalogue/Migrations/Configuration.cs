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
            AutomaticMigrationsEnabled = true;
            ContextKey = "Catalogue.Models.ApplicationDbContext";
        }

        protected override void Seed(Catalogue.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "x@x.ua"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "x@x.ua" };

                manager.Create(user, "password");
            }
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
    }
}
