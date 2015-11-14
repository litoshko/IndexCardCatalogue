namespace Catalogue.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Catalogue.Models.CatalogueContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Catalogue.Models.CatalogueContext";
        }

        protected override void Seed(Catalogue.Models.CatalogueContext context)
        {
            context.Records.AddOrUpdate(r => r.Title,
                new Record
                {
                    Title = "CLR via C#",
                    Author = "Richter",
                    Description = "Programming book"
                },
                new Record
                {
                    Title = "C# 5.0 in a Nutshell",
                    Author = "Albahari",
                    Description = "Programming book"
                },
                new Record
                {
                    Title = "C# Yellow book",
                    Author = "Miles",
                    Description = "Programming book",
                    Reviews = new List<Review>
                    {
                        new Review { Rating = 9, Comment = "Great book for a start!" }
                    }
                });
        }
    }
}
