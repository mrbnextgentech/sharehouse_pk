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
    public class PropertyInspection
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public Int64 PropertyID { get; set; }
        [ValidateNever]
        public Property Property { get; set; }
        [ValidateNever]
        public DateTime InspectionDate { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [ValidateNever]
        public string? From { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [ValidateNever]
        public string? To { get; set; }
        public bool isActive { get; set; } = true;
        [ValidateNever]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ValidateNever]
        public ApplicationUser CreatedbyUser { get; set; }
        public string? CreatedbyUserId { get; set; }
        [ValidateNever]
        public DateTime UpdatedOn { get; set; }
        [ValidateNever]
        public ApplicationUser UpdatedbyUser { get; set; }
        public string? UpdatedbyUserId { get; set; }
    }
}
