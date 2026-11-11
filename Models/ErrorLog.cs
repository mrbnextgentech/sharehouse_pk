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
    public class ErrorLog
    {
        [Key]
        public Int64 Id { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public string? ErrorDetail { get; set; }
        public string? ErrorSource { get; set; }
        public string? ErrorPath { get; set; }
        public string? StackTrace { get; set; }
        public DateTime? Date { get; set; }
        public ApplicationUser? LoggedInUser { get; set; }
        public string? LoggedInUserId { get; set; }
        public enum Status {
            [Display(Name = "Open")]
            Open =1,
            [Display(Name = "Fixing")]
            Fixing =2,
            [Display(Name = "Fixed")]
            Fixed =3,
            [Display(Name = "Closed")]
            Closed=4
        }
        public Status? status { get; set; }
        public ApplicationUser? FiexedByUser { get; set; }
        public string? FiexedByUserId { get; set; }
        public string? Version { get; set; }
        public string? EnvName { get; set; }
        public static string GetEnumDisplayName(Enum value)
        {
            return value
                .GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .Name;
        }
        public ErrorLog()
        {
            Date = DateTime.Now;
        }
    }
}
