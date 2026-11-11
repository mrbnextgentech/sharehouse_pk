using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gharshare.Models
{
    public class PersonListingViewed
    {
        [Key]
        public Int64 Id { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public Int64 PersonlistingId { get; set; }
        public string UserId { get; set; }
        [ValidateNever]
        public DateTime ViewDate { get; set; } = DateTime.Now;
        [ValidateNever]
        public PersonListing Personlisting { get; set; }
        public bool Shortlist { get; set; } = false;
        [ValidateNever]
        public ApplicationUser User { get; set; }      
         
    }
}
