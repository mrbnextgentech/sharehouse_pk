using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class ShortlistVM
    {
        public List<Shortlist>? Placeshortlist { get; set; }
        public List<Shortlist>? Personshortlist { get; set; }
        public List<Property> Placelistings { get; set; }
        public List<PersonListing>? PersonListings { get; set; }
    }
}
