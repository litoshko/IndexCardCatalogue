using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{
    public class CatalogueContext : DbContext
    {
        public CatalogueContext() : base("name = DefaultConnection")
        {

        }
        public DbSet<Record> Records { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}