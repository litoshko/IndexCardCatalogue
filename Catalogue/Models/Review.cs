using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage ="Rating must be between 1 and 10")]
        public int Rating { get; set; }
        [StringLength(80)]
        public string Comment { get; set; }
        public int RecordId { get; set; }
    }
}