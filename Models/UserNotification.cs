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
    public class UserNotification
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }        
        public ApplicationUser User { get; set; }
        public Int64 NotificationTypeId { get; set; }
        public UserNotificationType NotificationType { get; set; }         
        public NotificationStatus Status { get; set; }
        public enum NotificationStatus
        {
            Email=1,
            Nothanks=2,
            Instant=3,
            Daily=4,
            None=5,
            deactivate=6
        }
        public DateTime DateCreated { get; set; }=DateTime.Now;
        [ValidateNever]
        public ApplicationUser? CreatedbyUser { get; set; }
        public string? CreatedbyUserID { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        [ValidateNever]
        [NotMapped]
        public ApplicationUser UpdatedbyUser { get; set; }
        public string? UpdatedbyUserID { get; set; }         
    }
}
