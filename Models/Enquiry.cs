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
    public class Enquiry
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public Int64? ParentEnquiryId { get; set; }
        public bool isReplied { get; set; } = false;
        public Int64? PropertyID { get; set; }
        public Int64? RoomId { get; set; }
        public Int64? PersonlistingId { get; set; }
        [ValidateNever]
        public Property Property { get; set; }
        public PersonListing PersonListing { get; set; }
        public Room? Room { get; set; }
        [Required]
        public EnquriyType? Enquirytype { get; set; }
        public string Message { get; set; }         
        public string FromUserID { get; set; }
        public ApplicationUser FromUser { get; set; }
        public string ToUserID { get; set; }
        public ApplicationUser ToUser { get; set; }
        public DateTime DateReceived { get; set; } = DateTime.Now;        
        public EnquiryStatus Enquirystatus { get; set; }  
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        public ApplicationUser? UpdatedbyUser { get; set; }         

        public enum EnquriyType
        {
            [Display(Name = "About Property")]
            aboutproperty = 1,
            [Display(Name = "About Person")]
            aboutperson = 2,
            [Display(Name = "Inspection")]
            aboutinspection = 3,
            [Display(Name = "Message to Admin")]
            tosharehousepk = 4
        }
        public enum EnquiryStatus
        {
            [Display(Name ="Read")]
            read = 1,
            [Display(Name = "Unread")]
            unread = 2,
            [Display(Name = "Shortlist")]
            shortlist = 3,
            [Display(Name = "Notsuitable")]
            notsuitable
        }
    }
}
