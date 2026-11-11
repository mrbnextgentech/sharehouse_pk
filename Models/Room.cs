using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gharshare.Models
{
    public class Room
    {
        [Key]
        public Int64 ID { get; set; }         
        public List<Enquiry> enquiries;
        [MaxLength(36)]
        public string? UId { get; set; }
        public Int64 PropertyId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }         
        public FurnishingType Furnishing { get; set; }
        public Bathroom BathroomType { get; set; }
        public Bed BedSize { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMMM, dd, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AvailableFrom { get; set; }

        [Display(Name = "Rent")]
        //[DisplayFormat(DataFormatString = "{#,##0}", ApplyFormatInEditMode = true)]
        public Decimal? Rent { get; set; }
        public MinimumStay? MinStay { get; set; }
        public MaximumStay? MaxStay { get; set; }
        public Preference? PreferredFlatmate { get; set; }
        public Bill Billstatus { get; set; }
        public Bond BondType { get; set; }
        public RType RoomType { get; set; }
        [JsonIgnore]
        public Property Property { get; set; }
        [ValidateNever]
        public List<RoomFurnishing>? RoomFurnishings { get; set; }
        public enum Bill
        {
            [Display(Name = "Bills not included in rent")]
            AdditionalToRent = 1,
            [Display(Name = "Some bills included in rent")]
            SomeIncludedinRent = 2,
            [Display(Name = "Bills included in the rent")]
            IncludedinRent = 3
        }
        public enum Bond
        {
            [Display(Name = "One Month")]
            OneMonth = 1,
            [Display(Name = "Two Months")]
            TwoMonth = 2,
            [Display(Name = "Three Months")]
            ThreeMonths = 3,
            [Display(Name = "Four Months")]
            FourMonths = 4,
            None = 0
        }
        public enum RType
        {
            [Display(Name = "Private")]
            Private = 1,
            [Display(Name = "Shared")]
            Shared = 2,
            [Display(Name = "Any")]
            Any = 3
        }
        public enum FurnishingType
        {
            [Display(Name = "Flexible")]
            Flexible =1,
            [Display(Name = "Furnished")]
            Furnished =2,
            [Display(Name = "UnFurnished")]
            UnFurnished =3
        }
        public enum Bathroom
        {
            [Display(Name = "Shared")]
            Shared = 1,
            [Display(Name = "Own")]
            Own = 2,
            [Display(Name = "Ensuite")]
            Ensuite = 3,
            [Display(Name = "Any")]
            Any = 4
        }
        public enum Bed
        {             
            [Display(Name = "Single")]
            Single = 1,
            [Display(Name = "Double")]
            Double = 2,
            [Display(Name = "Queen")]
            Queen = 3,
            [Display(Name = "King")]
            King =4,
            [Display(Name = "None")]
            None =5
        }
        public enum Preference
        {             
            [Display(Name = "Men only")]
            Menonly = 1,
            [Display(Name = "Women only")]
            Womenonly = 2,
            [Display(Name = "No couples")]
            Nocouples = 3,
            [Display(Name = "Anyone inc. couples")]
            AnyoneIncCouples = 4
        }
        public enum MinimumStay
        {
            [Display(Name = "1 month")]
            OneMonth = 1,
            [Display(Name = "2 months")]
            TwoMonth = 2,
            [Display(Name = "4 months")]
            FourMonth = 3,
            [Display(Name = "6 months")]
            Sixmonth = 4,
            [Display(Name = "12+ months")]
            Twelvemonth = 5,
            [Display(Name = "no limit")]
            None =6
        }
        public enum MaximumStay
        {
            [Display(Name = "1 Month")]
            OneMonth = 1,
            [Display(Name = "6 Months")]
            SixMonths = 2,
            [Display(Name = "1 Year")]
            OneYear = 3,
            [Display(Name = "2 Years")]
            TwoYears = 4,
            [Display(Name = "3 Years")]
            ThreeYears = 5,
            [Display(Name = "5 Years")]
            FiveYears = 6,
            [Display(Name = "no limit")]
            FivePlus = 7,
            [Display(Name = "None")]
            None = 8
        }
    }
}
