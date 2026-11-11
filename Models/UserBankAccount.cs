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
    public class UserBankAccount
    {
        [Key]
        public Int64 ID { get; set; }
        [ValidateNever]
        public string? UserId { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [ValidateNever]
        public string AccountType { get; set; } // 1-BA Bank Account, 2-CC
        [Column(TypeName = "nvarchar(200)")]
        public string? AccountTitle { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? AccountNo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? BranchCode { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? CardHolderName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? CardNo { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? CardType { get; set; }
        public bool IsSelectedForPayment { get; set; }
        [ValidateNever]
        public DateTime CreatedOn { get; set; } = DateTime.Now;         
        [ValidateNever]
        public string? CreatedbyUserId { get; set; }
        [ValidateNever]
        public ApplicationUser? CreatedbyUser { get; set; }
        [ValidateNever]
        public DateTime UpdatedOn { get; set; }
        [ValidateNever]
        public string? UpdatedbyUserId { get; set; }
        [ValidateNever]
        public ApplicationUser? UpdatedbyUser { get; set; }

    }
}
