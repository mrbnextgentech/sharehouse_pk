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
    public class PropertyAccepting
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [JsonIgnore]
        public Property? Property { get; set; }        
        public Int64 PropertyID { get; set; }
        public Accepting? Accepting { get; set; }
        public Int64? AcceptingID { get; set; }
        [ValidateNever]
        public bool isSelected { get; set; } = false;

        [ValidateNever]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ValidateNever]
        public string? CreatedbyUserID { get; set; }
        public ApplicationUser? CreatedbyUser { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        [ValidateNever]
        public string? UpdatedbyUserID { get; set; }

        [ValidateNever]
        public ApplicationUser? UpdatedbyUser { get; set; }
    }
}
