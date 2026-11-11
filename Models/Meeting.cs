using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Gharshare.Models
{
    public class Meeting
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }

        [Required]
        [Display(Name = "Landlord Name")]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email id.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Meeting Date & Time")]
       // [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime MeetingDate { get; set; }        

        [Required]
        [Column(TypeName = "nvarchar(255)")]        
        public string Medium { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(255)")]
        [Display(Name = "ID/Phone")]
        public string? MediumID { get; set; }
        
        [Column(TypeName = "nvarchar(MAX)")]        
        public string? Message { get; set; }                
        
        [Display(Name = "Sent Date")]
        public DateTime Created { get; set; }=DateTime.Now;

        

    }
}
