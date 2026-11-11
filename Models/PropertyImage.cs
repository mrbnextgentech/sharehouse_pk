using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace Gharshare.Models
{
    public class PropertyImage
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [JsonIgnore]
        public Property? Property { get; set; }        
        public Int64 PropertyID { get; set; }
        [Required]
        public string ImagePath { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }=true;
        [ValidateNever]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public ApplicationUser CreatedbyUser { get; set; }
        public string? CreatedbyUserId { get; set; }
        [ValidateNever]
        public DateTime UpdatedOn { get; set; }
        public ApplicationUser UpdatedbyUser { get; set; }
        public string? UpdatedbyUserId { get; set; }
        public enum type 
        { 
            building=1,
            cover=2,
            floor=3
        }
        public type? ImageType { get; set; }
        public int? FloorNo { get; set; }
        public bool isCover { get; set; } = false;
        public int? order { get; set; }
    }
}
