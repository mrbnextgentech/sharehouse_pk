
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class PersonListingVM
    {
        public PersonListing? PersonListing { get; set; }
        public List<PropertyType>? PropertyTypes { get; set; }
        public List<Person>? Mates { get; set; }         
        public List<ListingEmployment>? Employments { get; set; }
        public List<ListingLifestyle>? Lifestyles { get; set; }
        public List<ListingPropertyType>? DesiredAccommodations { get; set; }
        public Person? Partner { get; set; }
        public Enquiry Enquiry { get; set; }
        public List<SelectListItem> CityList { get; set; }

    }
}
