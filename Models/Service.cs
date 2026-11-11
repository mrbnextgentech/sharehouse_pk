using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.Models
{
    public class Service
    {
        [Key]
        public Int64 id { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [Required]
        public string title { get; set; }        
        public string? description { get; set; }
        public ServiceType? Servicetype { get; set; }
        public enum ServiceType
        {
            [Display(Name = "Listing size")]
            listingsize = 1,
            [Display(Name = "Search Ranking")]
            searchranking = 2,
            [Display(Name = "Top search")]
            topsearch = 3,
            [Display(Name = "Property Listing")]
            livepropertylisting = 4,
            [Display(Name = "Person Listing")]
            livepersonlisting = 5,
            [Display(Name = "Send Message")]
            sendreceivemessage = 6,
            [Display(Name = "See Mobile number")]
            seemobilenumber = 7,
            [Display(Name = "Guarantee")]
            thirtydayguarantee = 8,            
            [Display(Name = "Other")]
            other = 9
        }
        public Status status { get; set; } = Status.active;
        public enum Status
        {
            [Display(Name = "Active")]
            active = 1,
            [Display(Name = "Pending")]
            pending = 2,
            [Display(Name = "Canceled")]
            canceled = 3
        }
        public DateTime createdOn { get; set; } = DateTime.Now;
        public string? createdbyUserID { get; set; }
        public ApplicationUser? CreatedbyUser { get; set; }
        [ValidateNever]
        public DateTime? updatedOn { get; set; }
        public string? updatedbyUserID { get; set; }
        public ApplicationUser? UpdatedbyUser { get; set; }
        public List<PlanService>? planServices { get; set; }         
    }
}
