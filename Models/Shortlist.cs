using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.Models
{
    public class Shortlist
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public type ShortlistType { get; set; }
        public string UserId { get; set; }
        public Int64? PropertyId { get; set; }
        public Int64? PersonlistingId { get; set; }         
        public DateTime DateCreated { get; set; }=DateTime.Now;
        public DateTime? UpdatedOn { get; set; }
        public ApplicationUser? User { get; set; }
        public Property? Property  { get; set; }
        public PersonListing? PersonListing { get; set; }
        public enum type
        {             
            [Display(Name = "Person Listing")]
            Personlisting =1,
            [Display(Name = "Property Listing")]
            Propertylisting=2
        }
    }
}
