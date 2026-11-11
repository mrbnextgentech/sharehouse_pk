using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class PropertyListingVM
    {
        public Property? Property { get; set; } 
        public List<Room>?Room { get; set; }
        public List<Furnishing>?Furnishings { get; set; }         
        public List<List<RoomFurnishing>>? RoomFurnishings { get; set; }        
        public List<Feature>? Features { get; set; }
        public List<PropertyFeature>? PropertyFeatures { get; set; } = new List<PropertyFeature>();
        public List<Accepting>? Acceptings { get; set; }
        public List<PropertyAccepting>? PropertyAcceptings { get; set; }
        public Message Message { get; set; }
        public bool Preview { get; set; } = false;
        public List<PropertyInspection>? Inspections { get; set; }
        public Enquiry? Enquiry { get; set; }
        public List<SelectListItem>? RoomsSelectList { get; set; }
        public List<PropertyImage>? FloorImages { get; set; }
        public List<TopCategories>? TopCategories { get; set; }
        public List<Property>? TopProperties { get; set; }
        public List<Property>? PopularProperties { get; set; }
        public List<Property>? RelatedProperties { get; set; }
        public string? Searchphrase { get; set; }
        public List<RoomFurnishing>? RoomFurnishingList { get; set; }
        public List<SelectListItem> CityList { get; set; }        
    }
}
