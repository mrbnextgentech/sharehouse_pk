using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Gharshare.Models
{
    public class Property
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        [Display(Name = "Property ID")]
        [Column(TypeName = "nvarchar(30)")]
        public string? PropertyCode { get; set; }
        public string OwnerId { get; set; }
        [ValidateNever]
        public ApplicationUser Owner { get; set; } //Owner: Owns or authorizes property
        public string? Address { get; set; }
        public Int64? StateID { get; set; }
        [ValidateNever]
        public State State { get; set; }
        //[RegularExpression(@"(/^[0-9]+$/)$", ErrorMessage = "City is required")]         
        public Int64? CityID { get; set; }

        [ValidateNever]
        public City City { get; set; }

        [ValidateNever]
        [Column(TypeName = "nvarchar(255)")]
        public string? Suburb { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [ValidateNever]
        public string? Street { get; set; }
        [ValidateNever]
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }
        public AccType? Accommodationtype { get; set; }
        public PropertyType? Propertytype { get; set; }
        public string? TotalArea { get; set; }

        [RegularExpression(@"^(100|[1-9]?[0-9])$", ErrorMessage = "Only integer value between 1-100.")]
        public int? Bed { get; set; }

        [RegularExpression(@"^(100|[1-9]?[0-9])$", ErrorMessage = "Only integer value between 1-100.")]
        public int? RoomsForRent { get; set; }

        [RegularExpression(@"^(100|[1-9]?[0-9])$", ErrorMessage = "Only integer value between 1-100.")]
        public int? Bathroom { get; set; }
        public enum RoomFurnishing
        {
            [Display(Name = "Flexible")]
            Flexible = 1,
            [Display(Name = "Furnished")]
            Furnished = 2,
            [Display(Name = "UnFurnished")]
            UnFurnished = 3
        }
        public RoomFurnishing FurnishingType { get; set; }
        public int? Yearbuilt { get; set; }
        public string? VideoURL { get; set; }
        [Display(Name = "Popular Tags")]
        public string? PopularTags { get; set; }

        [ValidateNever]
        [RegularExpression(@"^[0-9]+(\.[0-9]+)?$", ErrorMessage = "Enter a valid number")]
        public Decimal? BondValue { get; set; }
        public BondType? Bond { get; set; }
        [Display(Name = "Rent")]
        [RegularExpression(@"^[0-9]+(\.[0-9]+)?$", ErrorMessage = "Enter a valid number")]
        //[DisplayFormat(DataFormatString = "{#,##0}", ApplyFormatInEditMode = true)]
        //[DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public Decimal? Rent { get; set; }
        
        [ValidateNever]
        public Int32? TotalParkings { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [RegularExpression(@"^[0-9]+x[0-9]+$", ErrorMessage = "Enter size in lenght x width format e.g 10x10")]
        public string? ParkingSize { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [ValidateNever]
        public string? AdTitle { get; set; }
        public bool HidePrice { get; set; } = false;

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Description { get; set; }
        public int? Totalflatmates { get; set; } = 0; // Once all rooms are rented

        [Column(TypeName = "nvarchar(MAX)")]
        public string? AboutFlatmates { get; set; }

        //[Column(TypeName = "nvarchar(MAX)")]
        // public string? Accepting { get; set; }  //Pets, Students,Children, Smokers, Women only flat,
        //40+ years,Backpackers,Retirees, Unemployed/Welfare

        [ValidateNever]
        [Column(TypeName = "nvarchar(255)")]
        public string? CoverImage { get; set; }

        [ValidateNever]
        [Column(TypeName = "nvarchar(1000)")]
        public string? GoogleMapEmbedAPIURL { get; set; }
        public bool HideAddress { get; set; } = false;
        [Display(Name = "Owner Name")]
        public string? OwnerName { get; set; }
        [ValidateNever]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Owner Mobile")]
        public string? Ownermobile { get; set; }
        [ValidateNever]
        [Column(TypeName = "nvarchar(200)")]
        [Display(Name = "Owner email")]
        public string? Owneremail { get; set; }
        [ValidateNever]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayFormat(DataFormatString = "{0:MMMM, dd, yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AvailableFrom { get; set; }
        public bool IsRented { get; set; } = false;
        public bool RoomAvailable { get; set; } = true;
        public bool WomenOnlyProperty { get; set; } = false;
        public bool IsOnwnerOrAuthorized { get; set; } = false;
        public bool ShowPhoneNo { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public Status? listingstatus { get; set; } = Status.Draft;
        public bool IsAdlive { get; set; } = false;
        public bool? SharePhone { get; set; }
        public string? ShortDescription { get; set; }
        public Int64? UserClicks { get; set; } = 0;
        public Decimal? Rating { get; set; } = 0;
        public Parking? Parkingtype { get; set; }
        public Preference? Flatmatepreference { get; set; }
        public MinimumStay? MinStay { get; set; }
        public MaximumStay? MaxStay { get; set; }
        public Internet? InternetStatus { get; set; }
        public Int64? PlanId { get; set; }
        public DateTime? PlanStart { get; set; }
        public DateTime? PlanEnd { get; set; }
        public bool PlanExpired { get; set; } = false;
        public Billstatus? Bill { get; set; }
        public Inspection? InspectionTime { get; set; } // InspectionTime is actually 'address sharing time'- time when user want to share their address
        [Column(TypeName = "nvarchar(50)")]
        public string? InspectionLength { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? InspectionBreak { get; set; }
        public string? NavigationInstructions { get; set; }
        [ValidateNever]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ValidateNever]
        public ApplicationUser CreatedbyUser { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        [ValidateNever]
        public ApplicationUser UpdatedbyUser { get; set; }
        public string? UpdatedbyUserId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AdCreatedOn { get; set; }
        [ValidateNever]
        //[JsonIgnore]
        public List<Room>? Rooms { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [ValidateNever]
        public Plan? Plan { get; set; }
        [ValidateNever]
        public IEnumerable<PropertyImage>? Images { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public List<PropertyFeature> Features { get; set; }
        public List<PropertyAccepting>? PropertyAcceptings { get; set; }

        [ValidateNever]
        public List<Enquiry> Enquiries { get; set; }
        
        [ValidateNever]
        public IEnumerable<PropertyInspection> PropertyInspections { get; set; }        
        public enum Preference
        {
            [Display(Name = "Men Only")]
            Menonly = 1,
            [Display(Name = "Women Only")]
            Womenonly = 2,
            [Display(Name = "No Couples")]
            Nocouples = 3,
            [Display(Name = "Couple")]
            couple = 4,
            [Display(Name = "Anyone Inc. Couples")]
            AnyoneIncCouples = 5
        }
        public enum Inspection
        {
            [Display(Name = "During booking – When someone is booking an inspection")]
            duringbooking = 1,
            [Display(Name = "After booking – After someone has booked an inspection")]
            afterbooking = 2,
            [Display(Name = "Don’t share – I will share my address when appropriate")]
            dontshare = 3             
        }
        public enum BondType
        {            
            [Display(Name = "1 month")]
            onemonth = 1,
            [Display(Name = "2 months")]
            twomonths = 2,
            [Display(Name = "3 months")]
            threemonths = 3,
            [Display(Name = "4 months")]
            fourmonths = 4,
            [Display(Name = "none")]
            none = 0,
        }
        public enum MinimumStay
        {
            [Display(Name = "1 Month")]
            OneMonth = 1,
            [Display(Name = "2 Months")]
            TwoMonth = 2,
            [Display(Name = "4 Months")]
            FourMonth = 3,
            [Display(Name = "6 Months")]
            Sixmonth = 4,
            [Display(Name = "12 Months")]
            Twelvemonth = 5,
            [Display(Name = "Unknown")]
            None = 6
        }

        public enum MaximumStay
        {
            [Display(Name = "1 month")]
            OneMonth = 1,
            [Display(Name = "6 months")]
            SixMonths = 2,
            [Display(Name = "1 year")]
            OneYear = 3,
            [Display(Name = "2 years")]
            TwoYears = 4,
            [Display(Name = "3 years")]
            ThreeYears = 5,
            [Display(Name = "5+ years")]
            FiveYears = 6,
            [Display(Name = "Unknown")]
            none = 6
        }
        public enum Billstatus
        {
            [Display(Name = "Bills not included in rent")]
            AdditionalToRent = 1,
            [Display(Name = "Some bills included in rent")]
            SomeIncludedinRent = 2,
            [Display(Name = "Bills included in the rent")]
            IncludedinRent = 3
        }
        public enum Internet
        {
            [Display(Name = "Internet not available")]
            Nointernet = 1,
            [Display(Name = "Internet available(not incl. in rent)")]
            AvailableNotincludedInRent = 2,
            [Display(Name = "Internet available(incl. in rent)")]
            IncludedInRent = 3,
            [Display(Name = "unlimited(incl. in rent)")]
            UnlimitedIncludedInRent = 4
        }
        public enum Parking
        {
            [Display(Name = "No Parking")]
            NoParking = 1,
            [Display(Name = "Off-stree")]
            Onstreet = 2,
            [Display(Name = "On-stree")]
            Offstree = 3,
            [Display(Name = "Any")]
            Any = 4
        }

        public enum PropertyType
        {
            [Display(Name = "House")]
            House = 1,
            [Display(Name = "Flat")]
            Flat = 2,
            [Display(Name = "2+ Bedroom Flat")]
            Twoplusbedroom =3,
            [Display(Name = "1 Bed Flat")]
            Onebed =4,
            [Display(Name = "Studio")]
            Studio =5,
            [Display(Name = "Granny Flat")]
            Grannyflat =6,
            [Display(Name = "Student Accommodation")]
            Studentaccommodation =7,
            [Display(Name = "Homestay")]
            Homestay =8
        }
        public enum AccType
        {
            [Display(Name = "Room in sharehouse")]
            Room = 1,
            [Display(Name = "Whole property for rent")]
            WholeProperty = 2,
            [Display(Name = "Hostel")]
            StudentAccommodation = 3,
            [Display(Name = "Homestay")]
            Homestay = 4,
            [Display(Name = "Flat")]
            Flat = 5,
            [Display(Name = "Room in Flatshare")]
            Flatshare = 6
        }
        public enum Status
        {
            [Display(Name = "Draft")]
            Draft = 1,
            [Display(Name = "Pending")]
            Pending = 2,
            [Display(Name = "Active")]
            Active = 3,
            [Display(Name = "In-active")]
            Inactive = 4,
            [Display(Name = "Expired")]
            expired = 5,
            [Display(Name = "Blocked")]
            blocked = 6,
        }

        }
}
