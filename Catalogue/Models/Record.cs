using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{
    public class Record
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Title { get; set; }
        [StringLength(20)]
        public string Author { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [DisplayName("Owner name")]
        public string OwnerName { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}