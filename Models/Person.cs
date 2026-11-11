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
    public class Person
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public Int64 PersonListingId { get; set; }
        public string? Name { get; set; } 
        public Int32 Age { get; set; }
        public bool Partner { get; set; } = false;
        [ValidateNever]
        [JsonIgnore]
        public PersonListing? PersonListing { get; set; }
        public Gender gender { get; set; }
        public enum Gender
        {
            Man = 1,
            Woman = 2,
            Transgender = 3,
            Undisclosed = 4

        }
    }
}
