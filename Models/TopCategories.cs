using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class TopCategories
    {
        public Int32 CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public Int32 Total { get; set; }
    }
}
