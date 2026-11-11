using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class HomeVM
    {
        public List<Property>? Placelistings { get; set; }=new List<Property>();
        public List<PersonListing>? PersonListings { get; set; }
        public IEnumerable<StaticContent>? StaticContent { get; set; } = new List<StaticContent>();
        public List<Testimonial> Testimonials { get; set; }
        public DemoRequest? demoRequest { get; set; }
        public List<SelectListItem>? Cities { get; set; }         
    }
}
