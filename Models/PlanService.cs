using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Reflection;

namespace Gharshare.Models
{
    public class PlanService
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public Int64 PlanID { get; set; }
        public Plan Plan { get; set; }
        public Int64 ServiceID { get; set; }
        public Listingsize? listingsize { get; set; }
        public Topsearch? topsearch { get; set; }
        public Int16? livepropertylisting { get; set; }
        public Int16? livepersonlisting { get; set; }
        public bool? sendreceivemessage { get; set; }
        public bool? seemobilenumber { get; set; }
        public bool? thirtydayguarantee { get; set; }
        public Searchranking? searchranking { get; set; }        
        public enum Searchranking
        {
            [Display(Name = "Free")]
            free = 1,
            [Display(Name = "Basic")]
            basic = 2,
            [Display(Name = "Full")]
            full = 3,
            [Display(Name = "Pro")]
            pro = 4,
            [Display(Name = "N/A")]
            None = 5
        }
        public enum Topsearch
        {
            [Display(Name = "No")]
            no = 1,
            [Display(Name = "Every 5 days")]
            every5days = 2,
            [Display(Name = "Every 7 days")]
            every7days = 3
        }
        public enum Listingsize
        {
            [Display(Name = "Free")]
            free = 1,
            [Display(Name = "Basic")]
            basic = 2,
            [Display(Name = "Full")]
            full = 3,
            [Display(Name = "Pro")]
            pro = 4,
            [Display(Name = "N/A")]
            None = 5
        }
        public Service Service { get; set; }
        public bool? isActive { get; set; } = true;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedbyUserID { get; set; }
        public ApplicationUser? CreatedbyUser { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedbyUserID { get; set; }
        public ApplicationUser? UpdatedbyUser { get; set; }         
    }
}