using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.Models
{
    public class Toolkit
    {
        [Key]
        public Int64 ID { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [Required]        
        public string Type { get; set; } // Rental estimate, agent switch kit, e-book
        
        [Column(TypeName = "nvarchar(255)")]
        [Required]        
        [Display(Name = "Address")]
        [ValidateNever]
        public string PropertyAddress { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [Display(Name = "Name")]
        public string OwnerName { get; set; }

        [Column(TypeName = "nvarchar(255)")]         
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "*")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required]
        public string Phone { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string? IPAddress { get; set; }
        [ValidateNever]
        public bool IsReplied { get; set; }= false;

        [Column(TypeName = "nvarchar(255)")]
        [ValidateNever]
        //[Required(ErrorMessage = "* The {0} field is required.")]
        public string? NeedFor { get; set; }         
        [ValidateNever]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
         
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        public ApplicationUser? UpdatedbyUser { get; set; }
        
    }
}
