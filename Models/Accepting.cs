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
    public class Accepting
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [Required]
        [ValidateNever]
        [Column(TypeName= "nvarchar(200)")]
        public string Title { get; set; }
        public bool isSelected { get; set; }
        public bool IsActive { get; set; }=true;
        [Column(TypeName = "nvarchar(500)")]
        public string? iconpath { get; set; }
        [ValidateNever]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
