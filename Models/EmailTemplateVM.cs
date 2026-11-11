using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class EmailTemplateVM
    {
        public EmailTemplate? emailTemplate { get; set; }
        public IEnumerable<SelectListItem>? templateTypes { get; set; }
    }
}
