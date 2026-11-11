using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gharshare.Models
{
    public class ListingPropertyType
    {
        [Key]
        public Int64 id { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public Int64 personListingid { get; set; }
        public Int64 propertytypeid { get; set; }
        [ValidateNever]
        public bool isSelected { get; set; } = false;
        [JsonIgnore]
        public PersonListing PersonListing { get; set; }
        public PropertyType PropertyType { get; set; }
        [ValidateNever]
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        [ValidateNever]
        public string? CreatedbyUserID { get; set; }
        public ApplicationUser? CreatedbyUser { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        [ValidateNever]
        public string? UpdatedbyUserID { get; set; }
    }
}
