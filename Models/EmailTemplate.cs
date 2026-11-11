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
    public class EmailTemplate
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }         
        public TemplateType EmailTemplateType { get; set; }
        public enum TemplateType
        {
            [Display(Name = "Welcome")]
            welcome=1,
            [Display(Name = "Security")]
            security =2,
            [Display(Name = "Reminder")]
            reminder =3,
            [Display(Name = "Other")]
            other =4,
        }
        [Required]
        [ValidateNever]
        [Column(TypeName= "nvarchar(200)")]
        [Display(Name = "Name")]
        public string Title { get; set; }
        public string Template { get; set; }
        public bool IsActive { get; set; }=true;        
        //public EmailTemplateType EmailTemplateType { get; set; }
        [ValidateNever]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public ApplicationUser? CreatedbyUser { get; set; }
        public string? CreatedbyUserId { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        [ValidateNever]
        public ApplicationUser? UpdatedbyUser { get; set; }
        public string? UpdatedbyUserId { get; set; }
    }
}
