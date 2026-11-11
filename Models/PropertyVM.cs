using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class PropertyVM
    {
        public Property? Property { get; set; }
        public IEnumerable<Property>? Properties { get; set; }=new List<Property>();
        public IEnumerable<SelectListItem>? Landlords { get; set; }
        public ApplicationUser? Landlord { get; set; }
        public List<PropertyImage>? PropertyImages { get; set; }
        public IEnumerable<SelectListItem>? States { get; set; }
        [ValidateNever]
        public State State { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }
        [ValidateNever]
        public City City { get; set; }
        public IEnumerable<SelectListItem>? Suburbs { get; set; }
        public List<Feature>? Features { get; set; }
        public List<PropertyFeature>? PropertyFeatures { get; set; } = new List<PropertyFeature>();
        public List<Accepting>? Acceptings { get; set; }
        public List<PropertyAccepting>? PropertyAcceptings { get; set; }
        public List<PropertyInspection>? Inspections { get; set; }
        public Enquiry? Enquiry { get; set; }
        public List<PropertyImage>? FloorImages { get; set; }
        public List<TopCategories>? TopCategories { get; set; }
        public List<Property>? TopProperties { get; set; }
        public List<Property>? PopularProperties { get; set; }
        public List<Property>? RelatedProperties { get; set; }
        public string? Searchphrase { get; set; }
    }
}
