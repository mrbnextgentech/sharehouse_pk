using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.Models
{
    public class UserPlan
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Int64 PlanId { get; set; }
        public Plan? Plan { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public status Status { get; set; }
        public enum status
        {
            [Display(Name ="Active")]
            Active=1,
            [Display(Name = "Expired")]
            Expired =2,
            [Display(Name = "Canceled")]
            Canceled =3             
        }
        public DateTime DateCreated { get; set; }=DateTime.Now;
        [ValidateNever]
        public ApplicationUser? CreatedbyUser { get; set; }
        public string? CreatedbyUserID { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        [ValidateNever]
        public ApplicationUser UpdatedbyUser { get; set; }
        public string? UpdatedbyUserID { get; set; }         
    }
}
