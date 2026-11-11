using Gharshare.CommonHelper;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace Gharshare.com.pk.Controllers
{
    public class searchController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        private readonly TwilioService _twilioService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public searchController(IUnitofWork unitofWork, Utility utility,
            SignInManager<ApplicationUser> signInManager)
        {
            _unitofWork = unitofWork;
            _utility = utility;
            _signInManager = signInManager;
        }
         
        [HttpPost]
        public IActionResult bycity(SearchVM searchVM)
        {
            var city = _unitofWork.City.Get(c => c.ID == searchVM.cityId);
            Int64 id = city.ID;
            SearchVM searchVm = null;
            try
            {
                #region Filter properties and rooms
                if (searchVM.filterType == SearchVM.Filter.Property)
                {
                    var searchedProperties = _unitofWork.Property.GetAll(p =>
                                p.IsActive == true && p.listingstatus == Property.Status.Active,
                                includeProperties: "Owner,Rooms,Images").ToList();


                    var address = searchVM.address;
                    var targetProperty = searchVM.Property;
                    var targetRoom = searchVM.Room;
                    var minRent = searchVM.minRent;
                    var maxRent = searchVM.maxRent;
                    var propertyType = searchVM.Property.Propertytype;
                    var acceptings = searchVM.Acceptings;
                    var propTypes = searchVM.propertyTypes.Where(p => p.isSelected).Select(pt => pt.ID).
                        ToList();
                    var roomFurnishing = searchVM.Property.FurnishingType;
                    var bed = searchVM.Property.Bed;
                    var bathType = searchVM.bathType;
                    var parkingType = searchVM.Property.Parkingtype;
                    var womenOnlyprop = searchVM.Property.WomenOnlyProperty;
                    var minStay = searchVM.Property.MinStay;
                    var billIncluded = searchVM.billsIncluded;
                    var avaiableFrom = searchVM.Property.AvailableFrom;
                    var roomType = searchVM.roomType;

                    if (address != null)
                        searchedProperties = searchedProperties.Where(p => p.Address.Contains(address)).
                            ToList();
                    if (minRent != null && maxRent != null)
                        searchedProperties = searchedProperties.Where(p => p.Rent >= minRent
                        && p.Rent <= maxRent || (p.Rooms.Any(r => r.Rent >= minRent && r.Rent <= maxRent))).ToList();
                    if (avaiableFrom != null)
                        searchedProperties = searchedProperties.Where(p => p.AvailableFrom == avaiableFrom
                        || (p.Rooms.Any(r => r.AvailableFrom == avaiableFrom))).ToList();
                    if (propertyType != null)
                        searchedProperties = searchedProperties.Where(p => p.Propertytype == propertyType).
                            ToList();
                    if (parkingType != null && parkingType != Property.Parking.Any)
                        searchedProperties = searchedProperties.Where(p => p.Parkingtype == parkingType).
                            ToList();
                    if (bed != null)
                        searchedProperties = searchedProperties.Where(p => p.Bed >= bed).ToList();
                    if (billIncluded)
                        searchedProperties = searchedProperties.Where(p =>
                        p.Bill == Property.Billstatus.IncludedinRent).ToList();
                    if (minStay != null)
                        searchedProperties = searchedProperties.Where(p => p.MinStay == minStay
                        || (p.Rooms.Any(r => r.MinStay == (Room.MinimumStay)minStay))).ToList();
                    if (bathType != null && bathType != Room.Bathroom.Any)
                        searchedProperties = searchedProperties.Where(p => p.Rooms.
                        Any(room => room.BathroomType == bathType)).ToList();
                    if (roomType != null && roomType != Room.RType.Any)
                        searchedProperties = searchedProperties.Where(p => p.Rooms.
                        Any(room => room.RoomType == roomType)).ToList();
                    if (womenOnlyprop != null)
                        searchedProperties = searchedProperties.Where(p => p.WomenOnlyProperty == womenOnlyprop).
                            ToList();

                    if (roomFurnishing != null && roomFurnishing != 0)
                        searchedProperties = searchedProperties.Where(p => p.FurnishingType == roomFurnishing).
                            ToList();
                    if (acceptings != null)
                    {
                        var selectedAcceptingIDs = acceptings.Where(a => a.isSelected == true)
                            .Select(a => a.ID).ToList();
                        if (selectedAcceptingIDs.Any())
                        {
                            searchedProperties = searchedProperties.Where(p => p.PropertyAcceptings.
                            Any(pa => pa.isSelected == true &&
                            selectedAcceptingIDs.Contains((long)pa.AcceptingID))).ToList();
                        }
                    }

                    if (propTypes.Any())
                    {
                        searchedProperties = searchedProperties.Where(p => propTypes.
                        Contains((long)p.Propertytype)).ToList();
                    }

                    searchVm = new SearchVM
                    {
                        Propertylistings = searchedProperties,
                        PersonListings = new List<PersonListing>(),
                        city = city.Name,
                        cityImage=city.Image,
                        cityId = city.ID,
                        selectedTab = SearchVM.Tab.rooms
                    };
                }
                #endregion

                #region Filter person listings
                else
                {
                    List<PersonListing> personListingList=null;
                    SearchVM.Tab selectedTab= SearchVM.Tab.flatmates;
                    if (searchVM.filterType == SearchVM.Filter.Person)
                    {
                        personListingList = _unitofWork.PersonListing.GetAll(p =>
                            p.isActive == true && p.listingstatus == PersonListing.Status.Active
                            && p.TeamsUp == false,
                            includeProperties: "User,DesiredAccommodations,Employments,Lifestyles").ToList();
                       // selectedTab = SearchVM.Tab.flatmates;
                    }
                    else if (searchVM.filterType == SearchVM.Filter.Teamsup)
                    {
                        personListingList = _unitofWork.PersonListing.GetAll(p =>
                            p.isActive == true && p.listingstatus == PersonListing.Status.Active
                            && p.TeamsUp == true && p.TeamsUp == true,
                            includeProperties: "User,DesiredAccommodations,Employments,Lifestyles").ToList();
                        selectedTab = SearchVM.Tab.teamups;
                    }
                    var minBudget = searchVM.minBudget;
                    var maxBudget = searchVM.maxBudget;
                    var moveDate = searchVM.PersonListing.MoveDate;
                    var preference = searchVM.PersonListing.PlaceFor;
                    var minAge = searchVM.minAge;
                    var maxAge = searchVM.maxAge;
                    var propertyTypes = searchVM.propTypes4Person.Where(p => p.isSelected == true).
                        Select(p=>p.ID).ToList();
                    var professions = searchVM.Professions.Where(p => p.isSelected == true).
                        Select(p => p.ID).ToList();
                    var lifeStyles = searchVM.Lifestyles.Where(p => p.isSelected == true).
                        Select(p => p.ID).ToList();

                    if (minBudget > 0)
                        personListingList = personListingList.Where(p => 
                        p.MonthlyBudget >= minBudget).ToList();
                    if (maxBudget > 0)
                        personListingList = personListingList.Where(p =>
                        p.MonthlyBudget <= maxBudget).ToList();
                    if (moveDate != null)
                        personListingList = personListingList.Where(p => p.MoveDate == moveDate)
                            .ToList();
                    if (minAge > 0)
                        personListingList = personListingList.Where(p =>
                        p.Age >= minAge).ToList();
                    if (maxAge > 0)
                        personListingList = personListingList.Where(p =>
                        p.Age <= maxAge).ToList();
                    if (propertyTypes.Any())
                        personListingList = personListingList.Where(p => p.DesiredAccommodations.Any(pa =>
                        pa.isSelected==true && propertyTypes.Contains(pa.propertytypeid))).ToList();
                    if (professions.Any())
                        personListingList = personListingList.Where(p => p.Employments.Any(pa =>
                        pa.isSelected==true && professions.Contains(pa.EmploymentID))).ToList();
                    if (lifeStyles.Any())
                        personListingList = personListingList.Where(p => p.Lifestyles.Any(pa =>
                        pa.isSelected==true && lifeStyles.Contains(pa.LifestyleID))).ToList();

                    searchVm = new SearchVM
                    {
                        Propertylistings = new List<Property>(),
                        PersonListings = personListingList,
                        city = city.Name,
                        cityImage = city.Image,
                        cityId = city.ID,
                        selectedTab = selectedTab
                    };
                }
                #endregion
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(searchVm);
        }

        [HttpPost]
        public IActionResult index(string txtsearchtype, string address, string? filter)
        {
            SearchVM searchVm = null;
            List<Property> propertyList = null;
            List<PersonListing> personListingList = null;
            SearchVM.Tab selectedTab;
            try
            {
                if (txtsearchtype == "rooms")
                {
                    propertyList = _unitofWork.Property.GetAll(p =>
                        p.IsActive == true && p.listingstatus == Property.Status.Active,
                        includeProperties: "Owner,Rooms,Images").ToList();
                    selectedTab = SearchVM.Tab.rooms;
                }
                else if (txtsearchtype == "flatmates")
                {
                    personListingList = _unitofWork.PersonListing.GetAll(p =>
                        p.isActive == true && p.listingstatus == PersonListing.Status.Active
                        && p.TeamsUp == false, includeProperties: "User").ToList();
                    selectedTab = SearchVM.Tab.flatmates;
                }
                else
                {
                    personListingList = _unitofWork.PersonListing.GetAll(p =>
                        p.isActive == true && p.listingstatus == PersonListing.Status.Active
                        && p.TeamsUp == true, includeProperties: "User").ToList();
                    selectedTab = SearchVM.Tab.teamups;
                }

                if (propertyList != null)
                {
                    if (address != null)
                    {
                        propertyList = propertyList.Where(p => p.Address.Contains(address)).ToList();
                    }
                    if (filter != null)
                    {
                        switch (filter)
                        {
                            case "newst":
                                propertyList = propertyList.OrderByDescending(p => p.CreatedOn).ToList();
                                break;
                            case "rentl2h":
                                propertyList = propertyList.OrderBy(p => p.Rent ?? p.Rooms.Min(r => r.Rent)).ToList();
                                break;
                            case "renth2l":
                                propertyList = propertyList.OrderByDescending(p => p.Rent ?? p.Rooms.Min(r => r.Rent)).ToList();
                                break;
                            case "earliestava":
                                propertyList = propertyList.OrderBy(p => p.AvailableFrom ?? p.Rooms.Min(r => r.AvailableFrom)).ToList();
                                break;
                        }
                    }

                    searchVm = new SearchVM
                    {
                        Propertylistings = propertyList,
                        Searchtype = txtsearchtype,
                        address = address,
                        filter = filter,
                        selectedTab = selectedTab
                    };
                }
                else if (personListingList != null)
                {

                    if (address != null)
                    {
                        personListingList = personListingList.Where(p => p.User.Address.Contains(address)).ToList();
                    }
                    if (filter != null)
                    {
                        switch (filter)
                        {
                            case "newst":
                                personListingList = personListingList.OrderByDescending(p => p.CreatedOn).ToList();
                                break;
                            case "rentl2h":
                                personListingList = personListingList.OrderBy(p => p.MonthlyBudget).ToList();
                                break;
                            case "renth2l":
                                personListingList = personListingList.OrderByDescending(p => p.MonthlyBudget).ToList();
                                break;
                            case "earliestava":
                                personListingList = personListingList.OrderBy(p => p.MoveDate).ToList();
                                break;
                        }
                    }

                    searchVm = new SearchVM
                    {
                        PersonListings = personListingList,
                        Searchtype = txtsearchtype,
                        address = address,
                        filter = filter
                    };
                }

            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(searchVm);
        }
        [HttpGet]
        public IActionResult bycity(string name)
        {
            var city = _unitofWork.City.Get(c => c.Shortname == name);
            Int64 id = city.ID;
            SearchVM searchVm = null;
            try
            {
                searchVm = new SearchVM
                {
                    Propertylistings = _unitofWork.Property.GetAll(p => p.IsActive == true &&
                    p.listingstatus == Property.Status.Active && p.CityID == id,
                    includeProperties: "Owner,Rooms,Images").ToList(),
                    PersonListings = _unitofWork.PersonListing.GetAll(p => p.isActive == true &&
                    p.listingstatus == PersonListing.Status.Active && p.User.CityID == id,
                    includeProperties: "User").ToList(),
                    city = city.Name,
                    cityImage=city.Image,
                    cityId = city.ID,
                    filterType = SearchVM.Filter.All
                };
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(searchVm);
        }
        private void LogErrorDetail(Exception ex, string errorPath)
        {
            try
            {
                var errorLog = new ErrorLog
                {
                    ErrorDetail = ex.Message,
                    ErrorSource = ex.Source,
                    StackTrace = ex.StackTrace,
                    ErrorPath = errorPath,
                    LoggedInUserId = HttpContext.Session.GetString("_UserId"),
                    status = ErrorLog.Status.Open,
                };
                _unitofWork.ErrorLog.Add(errorLog);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {

                throw;
            }
        }
    }
}
