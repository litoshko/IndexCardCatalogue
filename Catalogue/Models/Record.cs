using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Catalogue.Models
{
    /// <summary>
    /// Record entity. Contains information about an idex-catalogue record.
    /// </summary>
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

        /// <summary>
        /// The reviews property is used for access to the collection of
        /// reviews associated with given record.
        /// </summary>
        public virtual ICollection<Review> Reviews { get; set; }
    }
}