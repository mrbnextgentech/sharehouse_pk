using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Gharshare.Models
{
    public class PersonListing
    {
        [Key]
        public Int64 ID { get; set; }
        [MaxLength(36)]
        public string? UId { get; set; }
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
        public List<ListingPropertyType>? DesiredAccommodations { get; set; }         
        public bool TeamsUp { get; set; } = false;       
        public string? Suburb { get; set; }
        public decimal? MonthlyBudget { get; set; }
        public DateTime? MoveDate { get; set; }
        public Stay? Length { get; set; }
        public RoomFurnishing? FurnishingType { get; set; }
        public Internet? InternetStatus { get; set; }
        public Status? listingstatus { get; set; } = Status.Draft;
        public List<Shortlist> Shortlists { get; set; }
        public enum Status
        {
            [Display(Name = "Draft")]
            Draft = 1,
            [Display(Name = "Pending")]
            Pending = 2,
            [Display(Name = "Active")]
            Active = 3,
            [Display(Name = "Inactive")]
            Inactive = 4,
            [Display(Name = "Plan Expired")]
            expired = 5,
            [Display(Name = "Blocked")]
            blocked = 6,
        }
        public Bath? Type { get; set; }
        public Flatmates? MaxNo { get; set; }
        public TotalPersons? Totalpersons { get; set; }
        public Purpose? PlaceFor { get; set; }
        public string? Profilepic { get; set; }
        public string? FirstName { get; set; }
        public Int32? Age { get; set; }
        public Gender? gender { get; set; }        
        public Employment? EmpStatus { get; set; }
        public string? Introduction { get; set; }
        public Int64? UserClicks { get; set; } = 0;
        public Decimal? Rating { get; set; } = 0;
        public Int64? PlanId { get; set; }
        public DateTime? PlanStart { get; set; }
        public DateTime? PlanEnd { get; set; }
        public bool PlanExpired { get; set; } = false;
        public bool isActive { get; set; } = false;
        public Parking? ParkingType { get; set; }
        public Plan? Plan { get; set; }         
        [ValidateNever]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        [ValidateNever]
        public string? CreatedbyUserID { get; set; }
        [ValidateNever]
        public ApplicationUser CreatedbyUser { get; set; }
        [ValidateNever]
        public DateTime? UpdatedOn { get; set; }
        [ValidateNever]
        public string? UpdatedbyUserID { get; set; }
        [ValidateNever]
        public ApplicationUser UpdatedbyUser { get; set; }
        public List<Person> Mates { get; set; }
        public List<ListingEmployment> Employments { get; set; }
        public List<ListingLifestyle> Lifestyles { get; set; }

        public enum TotalPersons
        {
            [Display(Name = "One")]
            One = 1,
            [Display(Name = "Two")]
            Two = 2,
            [Display(Name = "Three")]
            Three = 3,
            [Display(Name = "Four")]
            Four = 4,
            [Display(Name = "Five")]
            Five = 5,
            [Display(Name = "Six")]
            Six = 6,
            [Display(Name = "Seven")]
            Seven = 7,
            [Display(Name = "Eight")]
            Eight = 8,
            [Display(Name = "None")]
            None =0
        }
        public enum PropertyType
        {
            [Display(Name = "House")]
            House = 1,
            [Display(Name = "Flat")]
            Flat = 2,
            [Display(Name = "2+ bedroom")]
            Twoplusbedroom = 3,
            [Display(Name = "1 bed")]
            Onebed = 4,
            [Display(Name = "Studio")]
            Studio = 5,
            [Display(Name = "Grannyflat")]
            Grannyflat = 6,
            [Display(Name = "Student accommodation")]
            Studentaccommodation = 7,
            [Display(Name = "Homestay")]
            Homestay = 8
        }
        public enum AccType
        {
            [Display(Name = "Room(s) in a sharehouse")]
            Room = 1,
            [Display(Name = "Whole property for rent")]
            WholeProperty = 2,
            [Display(Name = "Student accommodation")]
            StudentAccommodation = 3,
            [Display(Name = "Homestay")]
            Homestay = 4,
            [Display(Name = "Flat")]
            Flat = 5,
            [Display(Name = "Room in a Flatshare")]
            Flatshare = 6
        }

        public enum Gender
        {
            [Display(Name = "Man")]
            Man = 1,
            [Display(Name = "Woman")]
            Woman = 2,
            [Display(Name = "Transgender")]
            Transgender = 3,
            [Display(Name = "Undisclosed")]
            Undisclosed =4
        }
        public enum Purpose
        {
            [Display(Name = "Me")]
            Me = 1,
            [Display(Name = "Couple")]
            Couple = 2,
            [Display(Name = "Group")]
            Group = 3,
            [Display(Name = "Man")]
            Man = 4,
            [Display(Name = "Woman")]
            Woman = 5,
            [Display(Name = "No Couple")]
            nocouple = 6,
            [Display(Name = "Anyone inc. couple")]
            anyone = 7,
        }
        public enum Flatmates
        {             
            [Display(Name = "1 Other")]
            OneOther = 1,
            [Display(Name = "2 Others")]
            TwoOthers = 2,
            [Display(Name = "3 Others")]
            ThreeOthers = 3,
            [Display(Name = "4 Others")]
            FourOthers = 4,
            [Display(Name = "5 Others")]
            FiveOthers = 5,
            [Display(Name = "6 Others")]
            SixOthers = 6,
            [Display(Name = "7 Others")]
            Seven = 7,
            [Display(Name = "8 Others")]
            Eight =8,
            [Display(Name = "Flexible")]
            Flexible =9
        }
        public enum Internet
        {
            [Display(Name = "Flexible")]
            Flexible = 1,
            [Display(Name = "Required")]
            Required =2,
            [Display(Name = "Not rquired")]
            Notrquired =3
        }
        public enum Parking
        {
            [Display(Name = "Flexible")]
            Flexible = 1,
            [Display(Name = "Off-stree required")]
            Offstree = 2,
            [Display(Name = "On-stree required")]
            Onstree = 3
        }

        public enum Bath
        {
            [Display(Name = "Flexible")]
            Flexible = 1,
            [Display(Name = "Ensuite or own required")]
            Ensuite = 2
        }
        public enum RoomFurnishing
        {
            [Display(Name = "Flexible")]
            Flexible = 1,
            [Display(Name = "Furnished")]
            Furnished = 2,
            [Display(Name = "UnFurnished")]
            UnFurnished = 3
        }

        public enum Stay
        {
            [Display(Name = "1 month")]
            OneMonth = 1,
            [Display(Name = "2 months")]
            TwoMonths = 2,
            [Display(Name = "3 months")]
            ThreeMonths = 3,
            [Display(Name = "6 months")]
            SixMonths = 4,
            [Display(Name = "9 months")]
            NineMonths = 5,
            [Display(Name = "12+ Months")]
            Plus12Months = 6,
            [Display(Name = "unknown")]
            Unlimited = 7
        }
    }
}
