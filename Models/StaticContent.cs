using Gharshare.Models;
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
    public class StaticContent
    {
        [Required]
        public Int64 Id { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [Required]
        public Type ContentType { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(300)")]
        public string? Heading { get; set; }
        [Required(ErrorMessage = "*required")]
        public string? Topsection { get; set; }
        public string? Imagepath { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? LowersectionHeading { get; set; }         
        public string? LowerSection { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created on")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ValidateNever]        
        [Display(Name = "Created by")]
        public ApplicationUser CreatedbyUser { get; set; }
        public string? CreatedbyUserId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Updated on")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        [ValidateNever]
        public ApplicationUser UpdatedbyUser { get; set; }
        public string? UpdatedbyUserId { get; set; }
        public status Status { get; set; } = status.draft;
        public enum Type
        {            
            [Display(Name = "Latest articles ")]
            article=1 ,
            [Display(Name = "How to guides")]
            howtoguide=2,
            [Display(Name = "Legal guides")]
            legalguides=3,
            [Display(Name = "About us")]
            aboutus = 4,
            [Display(Name = "Privacy Policy")]
            privacypolicy = 5,
            [Display(Name = "Terms and Conditions")]
            termsandconidtions = 6,
            [Display(Name = "FAQs")]
            FAQs = 7,
            [Display(Name = "Local Guide")]
            localguide = 8,
            [Display(Name = "Claims")]
            claims = 9,
        }
        public enum status
        {
            [Display(Name = "Active")]
            active = 1,
            [Display(Name = "Draft")]
            draft = 2,
            [Display(Name = "Canceled")]
            Canceled = 3             
        }
    }
}
