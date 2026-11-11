using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gharshare.Models
{
    public class SearchVM
    {
        public List<Property>? Propertylistings { get; set; }
        public List<PersonListing>? PersonListings { get; set; }
        public List<PersonListing>? Teamups { get; set; }
        public string? Searchtype { get; set; }
        public string? address { get; set; }
        public string? filter { get; set; }
        public string? city { get; set; }
        public string? cityImage { get; set; }
        public Int64 cityId { get; set; }

        /// For property search filtering        
        public Property? Property { get; set; }
        public PersonListing? PersonListing { get; set; }
        public List<PropertyType>? propTypes4Person { get; set; }
        public decimal? minBudget { get; set; }
        public decimal? maxBudget { get; set; }
        public Int32? minAge { get; set; }
        public Int32? maxAge { get; set; }
        public List<Employment>? Professions { get; set; }
        public List<Lifestyle>? Lifestyles { get; set; }
        public Room? Room { get; set; }
        public List<PropertyType>? propertyTypes { get; set; }        
        public List<Accepting>? Acceptings { get; set; }
        public List<Furnishing>? Furnishings { get; set; }
        public List<Property>? Properties { get; set; }
        public List<Feature>? Features { get; set; }         
        public decimal? minRent { get; set; }
        public decimal? maxRent { get; set; }
        public decimal? minFlatemateRent { get; set; }
        public decimal? maxFlatemateRent { get; set; }
        public Room.Bathroom bathType { get; set; }
        public bool billsIncluded { get; set; } = false;
        public Room.RType roomType { get; set; }
        public Property.RoomFurnishing roomfurnishing { get; set; }
        public Filter filterType { get; set; }
        public Tab selectedTab { get; set; }
        public enum Filter
        {
            Property = 1,
            Person = 2,
            Teamsup = 3,
            All = 4
        }
        public enum Tab
        {
            rooms = 1,
            flatmates = 2,
            teamups = 3,             
        }
        /// End for property search filtering

        public string? Searchphrase { get; set; }
        public List<City>? Cities { get; set; }
    }
}
