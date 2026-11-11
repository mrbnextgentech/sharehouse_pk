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
    public class Message
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public string FromUserID { get; set; }
        public ApplicationUser FromUser { get; set; }
        public string ToUserID { get; set; }
        public ApplicationUser ToUser { get; set; }
        [Required]
        [Display(Name = "Message")]
        public string Text { get; set; }
        
        [Display(Name = "Date")]
        public DateTime SentOn { get; set; }=DateTime.Now;
        public bool Sent { get; set; }
        public bool Read { get; set; }
        public MessageType? Type { get; set; }
        public Int64? PropertyListingId { get; set; }
        public Int64? PersonListingId { get; set; }
        public Property? Property { get; set; } 
        public PersonListing? PersonListing { get; set; }
        public enum MessageType
        {
            WithUser = 1,
            WithAdmin = 2,
            PropertyEnquiry = 3
        }
    }
}
