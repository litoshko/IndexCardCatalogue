using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int RecordId { get; set; }
    }
}