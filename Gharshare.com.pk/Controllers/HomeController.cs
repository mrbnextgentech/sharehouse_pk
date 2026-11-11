using Gharshare.CommonHelper;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.DataAccessLayer.Data.Infrastructure.Repository;
using Gharshare.Models;
using iText.Commons.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace Gharshare.com.pk.Controllers
{
    public class HomeController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        private readonly TwilioService _twilioService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public HomeController(IUnitofWork unitofWork, Utility utility, TwilioService twilioService,
            SignInManager<ApplicationUser> signInManager)
        {
            _unitofWork = unitofWork;
            _utility = utility;
            _twilioService = twilioService;
            _signInManager = signInManager;
        }
        public IActionResult test()
        {
            return View();
        }             
        public IActionResult signinfacebook(string code)
        {
            return View();
        }
        [HttpGet("/person/details")]
        public IActionResult persondetails(string listingId)
        {
            var personListing = _unitofWork.PersonListing.Get(p => p.UId == listingId,
                includeProperties: "User,Mates,Employments,Lifestyles,DesiredAccommodations");
            var id = personListing.ID;
            PersonListingVM personListingVm = new PersonListingVM
            {
                PersonListing= personListing,
                Employments = _unitofWork.ListingEmployment.GetAll(e => e.PersonListingID == id,
                includeProperties: "Employment").ToList(),
                Lifestyles = _unitofWork.ListingLifestyle.GetAll(l => l.PersonListingID == id,
                includeProperties: "Lifestyle").ToList(),
                Mates = _unitofWork.Person.GetAll(p => p.PersonListingId == id)
                .ToList(),
                DesiredAccommodations = _unitofWork.ListingPropertyType.GetAll(p => p.personListingid == id &&
                p.isSelected==true,includeProperties: "PropertyType").ToList(),
                Enquiry = new Enquiry()
            };
            return View(personListingVm);
        }
        [HttpGet("/property/details")]
        public IActionResult listplacedetail(string listingid)
        {
            var property = _unitofWork.Property.Get(p => p.UId == listingid, 
                includeProperties: "Images,Rooms,Owner,PropertyInspections");

            Int64 propertyId = property.ID;

            PropertyListingVM propertyListingVM = null;
            try
            {
                propertyListingVM = new PropertyListingVM
                {
                    Property = property,
                    PropertyAcceptings = _unitofWork.PropertyAccepting.GetAll(pa => pa.PropertyID == propertyId &&
                    pa.isSelected == true, includeProperties: "Accepting").ToList(),
                    PropertyFeatures = _unitofWork.PropertyFeature.GetAll(pa => pa.PropertyID == propertyId &&
                    pa.isSelected == true, includeProperties: "Feature").ToList(),
                    Room = _unitofWork.Room.GetAll(r => r.PropertyId == propertyId,
                    includeProperties: "RoomFurnishings").ToList(),
                    RoomFurnishingList = _unitofWork.RoomFurnishing.GetAll(rf => rf.PropertyID == propertyId,
                    includeProperties: "Furnishing").OrderBy(rf => rf.RoomID).ToList(),
                    Enquiry=new Enquiry(),
                    RoomsSelectList= GetRoomsSelectList(propertyId)
                };
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(propertyListingVM);
        }
        private List<SelectListItem> GetRoomsSelectList(Int64 propertyId)
        {
            IEnumerable<SelectListItem> rooms = _unitofWork.Room.GetAll(r => r.PropertyId == propertyId)
                .OrderBy(c => c.Name).ToList().Select(
                    x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = Convert.ToString(x.ID)
                    }
                    );
            return rooms.ToList();
        }
        [HttpGet]
        public bool checkCellNumberAsUsernameExist(string cellNo)
        {
            try
            {
                ApplicationUser user;
                if (!string.IsNullOrEmpty(cellNo))
                {
                    user = _unitofWork.User.Get(u => u.Mobile == cellNo.Trim() || u.UserName == cellNo.Trim());
                    if (user != null)
                        return true;
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "home/checkCellNumberAsUsernameExist");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return true;
            }
            return false;
        }
        [HttpGet]
        public bool checkUsername(string userName)
        {
            try 
            {
                ApplicationUser user;
                if (!string.IsNullOrEmpty(userName))
                {
                    user = _unitofWork.User.Get(u => u.Email == userName.Trim());
                    if (user != null)
                        return true;
                }
                
            }
            catch(Exception exp)
            {
                LogErrorDetail(exp, "home/checkUsername");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;                 
                return true;
            }
            return false;
        }
        [HttpGet]
        public IActionResult usernotificationsettings()
        {
            return View("hello world");
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult externallogins(string provider,string returnUrl)
        {
            var redirectUrl = Url.Action("externalLoginCallback", "home",
               new { ReturnUrl = returnUrl });
            //var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
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
        private string GenerateOtp()
        {
            // generate a random OTP
            return new Random().Next(1000, 9999).ToString(); //+15075333943
        }
        public JsonResult GetPersonListings(int pg = 1)
        {
            try
            {
                var personListings = _unitofWork.PersonListing.GetAll(p => p.isActive == true &&
                    p.listingstatus == PersonListing.Status.Active,
                    includeProperties: "User")
                    .OrderByDescending(p => p.CreatedOn).ToList();
                bool isLoggedIn = false;
                var userId = HttpContext.Session.GetString("_UserId");
                if (userId != null)
                    isLoggedIn = true;
                #region apply paging on properties
                int pageSize = Pager.GetPageSize();
                if (pg < 0)
                    pg = 1;
                int recsCount = personListings.Count();
                int recSkip = (pg - 1) * pageSize;
                personListings = personListings.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "home", "index", pageSize, null, "",
                    personListings.Count(), "getPersonListings");
                ViewBag.Pager = pager;
                var result = new
                {
                    personListings = personListings,
                    userId = userId,
                    isLoggedIn = isLoggedIn,
                };
                return Json(result);
                #endregion
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public JsonResult GetPlaceListings(int pg=1)
        {
            try
            {
                var placeListings = _unitofWork.Property.GetAll(p => p.IsActive == true &&
                    p.listingstatus == Property.Status.Active,
                    includeProperties: "Owner,Rooms,Images")
                    .OrderByDescending(p => p.CreatedOn).ToList();
                bool isLoggedIn = false;
                var userId = HttpContext.Session.GetString("_UserId");
                if(userId !=null)
                    isLoggedIn = true;
                #region apply paging on properties
                int pageSize = Pager.GetPageSize();
                if(pg<0)
                pg = 1;
                int recsCount = placeListings.Count();
                int recSkip = (pg - 1) * pageSize;
                placeListings = placeListings.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "home", "index", pageSize, null, "",
                    placeListings.Count(), "getPlaceListings");
                ViewBag.Pager = pager;
                var result = new
                {
                    placeListings= placeListings,
                    userId=userId,
                    isLoggedIn=isLoggedIn,                    
                };
                return Json(result);
                #endregion
            }
            catch (Exception ex)
            {

            }

            return null;
        }
        public IActionResult index()
        {            
 
            HomeVM homeVM = null;
            try
            {
                var placeListings = _unitofWork.Property.GetAll(p => p.IsActive == true &&
                    p.listingstatus == Property.Status.Active,
                    includeProperties: "Owner,Rooms,Images")
                    .OrderByDescending(p=>p.CreatedOn).ToList();
                #region apply paging on properties
                int pageSize = Pager.GetPageSize();                
                int pg = 1;
                int recsCount = placeListings.Count();
                int recSkip = (pg - 1) * pageSize;
                placeListings = placeListings.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "home", "index", pageSize, null, "",
                    placeListings.Count(), "getPlaceListings");
                ViewBag.Pager = pager;
                #endregion

                var personListings = _unitofWork.PersonListing.GetAll(p => p.isActive == true &&
                    p.listingstatus == PersonListing.Status.Active,
                    includeProperties: "User")
                    .OrderByDescending(p => p.CreatedOn).ToList();
                #region apply paging on PersonListings
                int pageSizePerson = Pager.GetPageSize();
                int pgPerson = 1;
                int recsCountPerson = personListings.Count();
                int recSkipPerson = (pg - 1) * pageSize;
                personListings = personListings.Skip(recSkip).Take(pageSize).ToList();
                var pagerPerson = new Pager(recsCountPerson, pgPerson, "home", "index", pageSize, null, "",
                    personListings.Count(), "getPersonListings");
                ViewBag.PagerPerson = pagerPerson;
                #endregion
                homeVM = new HomeVM
                {
                    Placelistings = placeListings,
                    PersonListings = personListings
                };
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("The action method"))
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;                
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 404 });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;                 
                return RedirectToAction("Error", "Error");
            }
            return View(homeVM);
        }
        private List<SelectListItem> LoadCities()
        {
            IEnumerable<SelectListItem> Cities = _unitofWork.City.GetAll()
                .OrderBy(c=>c.Name).ToList().Select(
                    x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = Convert.ToString(x.ID)
                    }
                    );
            return Cities.ToList();
        }
    }
}
