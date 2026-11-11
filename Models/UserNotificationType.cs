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
    public class UserNotificationType
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }        
        public DateTime DateCreated { get; set; }=DateTime.Now;
        [ValidateNever]
        public ApplicationUser? CreatedbyUser { get; set; }
        public string? CreatedbyUserID { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        [ValidateNever]
        public ApplicationUser? UpdatedbyUser { get; set; }
        public string? UpdatedbyUserID { get; set; }
        public string? Label { get; set; }
    }
}
