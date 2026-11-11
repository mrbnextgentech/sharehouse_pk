using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class DemoVM
    {

        public List<StaticContent> StaticContent { get; set; }
        public List<Testimonial> Testimonials { get; set; }
    }
}
