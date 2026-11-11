using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Gharshare.Models
{
    public class PropertyFeature
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public Property? Property { get; set; }        
        public Int64 PropertyID { get; set; }
        public Feature? Feature { get; set; }
        public Int64? FeatureID { get; set; }
        [ValidateNever]
        public bool isSelected { get; set; } = false;
        public string? Svgd { get; set; }
        public string? SvgIconPath { get; set; }

        [ValidateNever]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedbyUserId { get; set; }
        public string? UpdatedbyUserId { get; set; }
        public ApplicationUser? CreatedbyUser { get; set; }
        [ValidateNever]
        public ApplicationUser? UpdatedbyUser { get; set; }
    }
}
