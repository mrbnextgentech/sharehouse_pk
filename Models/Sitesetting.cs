using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class Sitesetting
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Email { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string? Phone { get; set; }         
        public string? Address { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Youtube { get; set; }
        public string? Linkedin { get; set; }
        public string? Insta { get; set; }
        public string? Tiktok { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? Supportemail { get; set; }
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        public ApplicationUser? CreatedbyUser { get; set; }
        public string? CreatedbyUserId { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        public ApplicationUser? UpdatedbyUser { get; set; }
        public string? UpdatedbyUserId { get; set; }
    }
}
