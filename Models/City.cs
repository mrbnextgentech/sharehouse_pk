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
    public class City
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        [Required]
        public string Shortname { get; set; }
        //public string? Image { get; set; }
        [ValidateNever]
        public List<Property>? Propertylistings { get; set; }
        [ValidateNever]
        public List<Person>? Personlistings { get; set; }
        public bool Featured { get; set; } = false;
        [ValidateNever]
        public DateTime? CreatedOn { get; set; } = DateTime.Now;         
        [Required]
        [ValidateNever]
        public string Image { get; set; }
    }
}
