using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class EnquiryVM
    {
        public Property? Property { get; set; }
        public Room? Room { get; set; }
        public Enquiry? Enquiry { get; set; }

    }
}
