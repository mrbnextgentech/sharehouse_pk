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
    public class Testimonial
    {
        [Key]
        public Int64 Id { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [ValidateNever]
        public string? ImagePath { get; set; }
        public string? MiniImagePath { get; set; }
       // [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]         
        public bool IsShown { get; set; }
        [ValidateNever]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ValidateNever]
        public ApplicationUser CreatedbyUser { get; set; }
        public string? CreatedbyUserID { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        [ValidateNever]
        public ApplicationUser? UpdatedbyUser { get; set; }
        [ValidateNever]
        public string? UpdatedbyUserID { get; set; }
    }
}
