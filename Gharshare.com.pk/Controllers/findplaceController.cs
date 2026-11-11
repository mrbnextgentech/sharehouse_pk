using Gharshare.CommonHelper;
using Microsoft.AspNetCore.Http;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;


namespace ShareHouse.pk.Controllers
{
    [Authorize(Roles = WebSiteRoles.Role_FreeUser)]
    public class findplaceController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        public findplaceController(IUnitofWork unitofWork, Utility utility)
        {
            _unitofWork = unitofWork;
            _utility = utility;
        }
        [HttpPost]
        public JsonResult updateemployments(PersonListingVM personListingVm)
        {
            var personListing = personListingVm.PersonListing;
            var employments = personListingVm.Employments;
            try
            {
                if (personListing != null && personListing.ID > 0)
                {
                    if (employments != null)
                    {
                        employments.ForEach(p =>
                        {
                            p.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                            p.UpdatedOn = DateTime.UtcNow;
                        });
                        _unitofWork.ListingEmployment.UpdateRange(employments);
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/updateemployments");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            employments = _unitofWork.ListingEmployment.GetAll(e => e.PersonListingID == personListing.ID
            && e.isSelected == true).ToList();
            return (new JsonResult(employments));
        }
        [HttpPost]
        public JsonResult updateaccommodationtypes(PersonListingVM personListingVm)
        {
            var personListing = personListingVm.PersonListing;
            var desiredAccommodations = personListingVm.DesiredAccommodations;
            try
            {
                if (personListing != null && personListing.ID > 0)
                {
                    PersonListing personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID);
                    personListingDB.TeamsUp = personListing.TeamsUp;

                    if (desiredAccommodations != null)
                    {
                        desiredAccommodations.ForEach(p =>
                        {
                            p.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                            p.UpdatedOn = DateTime.UtcNow;
                        });
                        _unitofWork.ListingPropertyType.UpdateRange(desiredAccommodations);
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/updateaccommodationtypes");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            desiredAccommodations = _unitofWork.ListingPropertyType.GetAll(a => a.personListingid == personListing.ID
            && a.isSelected == true).ToList();
            return (new JsonResult(desiredAccommodations));
        }
        [HttpPost]
        public JsonResult updatelocations(PersonListingVM personListingvm)
        {
            var personListing = personListingvm.PersonListing;
            PersonListing personListingDB = null;
            try
            {
                if (personListing != null && personListingvm.PersonListing.ID > 0)
                {
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID);
                    personListingDB.Suburb = personListing.Suburb;
                    personListingDB.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                    personListingDB.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.PersonListing.update(personListingDB);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/updatelocations");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(personListingDB));
        }
        [HttpPost]
        public JsonResult updatepreferences(PersonListingVM personListingvm)
        {
            PersonListing personListingDB = null;
            var personListing = personListingvm.PersonListing;
            try
            {
                if (personListing != null && personListing.ID > 0)
                {
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID);
                    personListingDB.FurnishingType = personListing.FurnishingType;
                    personListingDB.InternetStatus = personListing.InternetStatus;
                    personListingDB.Type = personListing.Type;
                    personListingDB.ParkingType = personListing.ParkingType;
                    personListingDB.MaxNo = personListing.MaxNo;
                    personListingDB.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                    personListingDB.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.PersonListing.update(personListingDB);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/savepreferencestep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(personListingDB));
        }
        [HttpPost]
        public JsonResult updaterent(PersonListingVM personListingvm)
        {
            var personListing = personListingvm.PersonListing;
            PersonListing personListingDB = null;
            try
            {
                if (personListing != null && personListingvm.PersonListing.ID > 0)
                {
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID);
                    personListingDB.MonthlyBudget = personListing.MonthlyBudget;
                    personListingDB.Length = personListing.Length;
                    personListingDB.MoveDate = personListing.MoveDate;
                    personListingDB.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                    personListingDB.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.PersonListing.update(personListingDB);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/updaterent");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(personListingDB));
        }

        [HttpPost]
        public JsonResult updatetenant(PersonListingVM personListingvm)
        {
            var personListing = personListingvm.PersonListing;
            var partner = personListingvm.Partner;
            var otherpersons = personListing.Mates;

            var oldMatesDb = _unitofWork.Person.GetAll(p => p.PersonListingId ==
                            personListing.ID && p.Partner == false).ToList();
            var oldPartnerDb = _unitofWork.Person.Get(p => p.PersonListingId == personListing.ID
                        && p.Partner == true);

            PersonListing personListingDB = null;
            try
            {
                if (personListing != null && personListing.ID > 0)
                {
                    //delete old partner or friends, if any
                    if (oldPartnerDb != null)
                        _unitofWork.Person.Delete(oldPartnerDb);
                    if (oldMatesDb != null)
                        _unitofWork.Person.DeleteRange(oldMatesDb);
                    _unitofWork.Save();

                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID);
                    personListingDB.FirstName = personListing.FirstName;
                    // personListingDB.Age = personListing.Age;
                    personListingDB.PlaceFor = personListing.PlaceFor;
                    personListingDB.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                    personListingDB.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.PersonListing.update(personListingDB);
                    _unitofWork.Save();
                    if (partner != null && personListing.PlaceFor == PersonListing.Purpose.Couple)
                    {
                        if (partner.ID > 0)
                            partner.ID = 0;
                        //add/new partner
                        partner.PersonListingId = personListing.ID;
                        partner.UId = Guid.NewGuid().ToString();
                        _unitofWork.Person.Add(partner);
                        _unitofWork.Save();
                    }
                    if (otherpersons != null)
                    {
                        otherpersons.ForEach(p =>
                        {
                            if (p.ID > 0)//applied check becoz id field was incorrectly taking value
                            {
                                p.ID = 0;
                            }
                            p.UId = Guid.NewGuid().ToString();
                        });
                        if (otherpersons.Any())
                        {
                            _unitofWork.Person.AddRange(otherpersons);
                            _unitofWork.Save();
                        }
                    }
                    if (personListing.User != null)
                    {
                        var userId = HttpContext.Session.GetString("_UserId");
                        var user = _unitofWork.User.Get(u => u.Id == userId);
                        if (personListing.User.Age != null)
                            user.Age = personListing.User.Age;
                        if (personListing.User.Gender != null)
                            user.Gender = personListing.User.Gender;
                        _unitofWork.User.update(user);
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/updatetenant");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(personListingDB));
        }
        [HttpPost]
        public JsonResult updateintroduction(PersonListingVM personListingvm)
        {
            var personListing = personListingvm.PersonListing;
            PersonListing personListingDB = null;
            try
            {
                if (personListing != null && personListing.ID > 0)
                {
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID);
                    personListingDB.Introduction = personListing.Introduction;
                    personListingDB.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                    personListingDB.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.PersonListing.update(personListingDB);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/updateintroduction");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(personListingDB));
        }
        [HttpGet]
        public IActionResult loadlistinginpopup(Int64 listingid, string viewname)
        {
            PersonListingVM personListingVM = null;
            try
            {
                if (listingid > 0)
                {
                    personListingVM = new PersonListingVM
                    {
                        PersonListing = _unitofWork.PersonListing.Get(p => p.ID == listingid
                        || p.Mates.Any(mt => mt.Partner == false),
                        includeProperties: "User,Mates"),
                        //PropertyTypes = _unitofWork.PropertyType.GetAll().ToList(),
                        Mates = _unitofWork.Person.GetAll(p => p.PersonListingId == listingid
                        && p.Partner == false).ToList(),
                        Partner = _unitofWork.Person.Get(p => p.PersonListingId == listingid
                        && p.Partner == true) ?? new Person
                        {
                            Partner = true,
                            PersonListingId = listingid
                        },
                        Employments = _unitofWork.ListingEmployment.GetAll(e => e.PersonListingID == listingid,
                    includeProperties: "Employment").ToList(),
                        //Lifestyles = new List<ListingLifestyle>(),
                        DesiredAccommodations = _unitofWork.ListingPropertyType.GetAll(p => p.personListingid == listingid,
                    includeProperties: "PropertyType").ToList()
                    };
                    personListingVM.PersonListing.Mates = personListingVM.PersonListing.Mates.Where(mt => mt.Partner == false).ToList();
                    return View(viewname, personListingVM);
                }

            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(viewname, personListingVM);
        }
        [HttpGet]
        public IActionResult findplace()
        {
            PersonListingVM personListingVM = null;
            string personListingId = getpersonListingId();
            if (personListingId == string.Empty) //user hasn't created person listing
            {
                personListingVM = new PersonListingVM
                {
                    PersonListing = new PersonListing
                    {
                        User = _unitofWork.User.Get(u => u.Id == HttpContext.Session.GetString("_UserId"))
                    },
                    PropertyTypes = _unitofWork.PropertyType.GetAll().ToList(),
                    Mates = new List<Person>(),
                    Employments = new List<ListingEmployment>(),
                    Lifestyles = new List<ListingLifestyle>()
                };
            }
            else
            {
                return RedirectToAction("findplacepreview", "findplace", new { listingId=personListingId });

            }
            return View(personListingVM);
        }
        private string getpersonListingId()
        {
            string personListingId = string.Empty;
            var userId = HttpContext.Session.GetString("_UserId");
            var personListing = _unitofWork.PersonListing.Get(pl => pl.UserId == userId);
            if(personListing!=null)
            {
                return personListing.UId;
            }
            return personListingId;
        }
        [HttpGet]
        public JsonResult deactivatelisting(Int64 personlistingId)
        {
            try
            {
                var personListing = _unitofWork.PersonListing.Get(p => p.ID == personlistingId);
                personListing.isActive = false;
                personListing.listingstatus = PersonListing.Status.Inactive;
                ApplicationUser.usertype userType;
                Enum.TryParse(HttpContext.Session.GetString("_UserType"),
                    out userType);
                if (userType == ApplicationUser.usertype.admin ||
                    userType == ApplicationUser.usertype.superadmin)
                {
                    personListing.listingstatus = PersonListing.Status.blocked;
                }
                personListing.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                personListing.UpdatedOn = DateTime.UtcNow;
                _unitofWork.PersonListing.update(personListing);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/activatelisting");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpGet]
        public JsonResult activatelisting(Int64 personlistingId)
        {
            try
            {
                var personListing = _unitofWork.PersonListing.Get(p => p.ID == personlistingId);
                personListing.isActive = true;
                personListing.listingstatus = PersonListing.Status.Active;
                personListing.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                personListing.UpdatedOn = DateTime.UtcNow;
                //ApplicationUser.usertype userType;
                //Enum.TryParse(HttpContext.Session.GetString("_UserType"),
                //    out userType);
                //if (userType == ApplicationUser.usertype.admin ||
                //    userType == ApplicationUser.usertype.superadmin)
                //{
                //    personListing.listingstatus = PersonListing.Status.Active;
                //}
                _unitofWork.PersonListing.update(personListing);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/activatelisting");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpGet]
        public IActionResult findplacepreview(string listingId)
        {
            PersonListingVM personListingVm = null;
            try
            {
                var personListing = _unitofWork.PersonListing.Get(p => p.UId == listingId,
                    includeProperties: "User,Mates,Employments,Lifestyles,DesiredAccommodations");
                var id = personListing.ID;

                personListingVm = new PersonListingVM
                {
                    PersonListing = personListing,
                    Employments = _unitofWork.ListingEmployment.GetAll(e => e.PersonListingID == id
                    && e.isSelected == true, includeProperties: "Employment").ToList(),
                    Lifestyles = _unitofWork.ListingLifestyle.GetAll(l => l.PersonListingID == id,
                    includeProperties: "Lifestyle").ToList(),
                    Mates = _unitofWork.Person.GetAll(p => p.PersonListingId == id)
                    .ToList(),
                    DesiredAccommodations = _unitofWork.ListingPropertyType.GetAll(p => p.personListingid == id
                    ,includeProperties: "PropertyType").ToList()
                };
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(personListingVm);
        }

        [HttpPost]
        public JsonResult saveLifestylestep(PersonListing personListing)
        {
            PersonListing personListingDB = null;
            try
            {
                if (personListing != null && personListing.ID > 0 && personListing.Lifestyles.Any())
                {
                    _unitofWork.ListingLifestyle.UpdateRange(personListing.Lifestyles);
                    _unitofWork.Save();
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID,
                        includeProperties: "Employments,Lifestyles");
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/saveLifestylestep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(personListingDB));
        }
        [HttpPost]
        public JsonResult saveemploymenttatusstep(PersonListing personListing)
        {
            PersonListing personListingDB = null;
            try
            {
                if (personListing != null && personListing.ID > 0 && personListing.Employments.Any())
                {
                    _unitofWork.ListingEmployment.UpdateRange(personListing.Employments);
                    _unitofWork.Save();
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID,
                        includeProperties: "Employments,Lifestyles");
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/saveLifestylestep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(personListingDB));
        }
        [HttpPost]
        public JsonResult saveintrostep(PersonListing personListing)
        {
            PersonListing personListingDB = null;
            try
            {
                if (personListing != null && personListing.ID > 0)
                {
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID);
                    personListingDB.FirstName = personListing.FirstName;
                    personListingDB.Age = personListing.Age;
                    personListingDB.gender = personListing.gender;
                    personListingDB.Introduction = personListing.Introduction;
                    personListingDB.PlaceFor = personListing.PlaceFor;
                    personListingDB.listingstatus = PersonListing.Status.Pending;
                    personListingDB.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                    personListingDB.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.PersonListing.update(personListingDB);
                    var otherPesonsDb = _unitofWork.Person.GetAll(p => p.PersonListingId == personListing.ID)
                        .ToList();
                    if (otherPesonsDb.Any())
                    {
                        _unitofWork.Person.DeleteRange(otherPesonsDb); //delete existing records
                    }
                    if (personListing.Mates != null)
                    {
                        personListing.Mates.ForEach(m =>
                        {
                            m.UId = Guid.NewGuid().ToString();
                        });
                        if (personListing.Mates.Any())
                            _unitofWork.Person.AddRange(personListing.Mates);
                    }
                    _unitofWork.Save();
                    if (personListing.User != null && personListing.User.CityID != null)
                    {
                        var userId = HttpContext.Session.GetString("_UserId");
                        var user = _unitofWork.User.Get(u => u.Id == userId);
                        if (user != null)
                        {
                            user.CityID = personListing.User.CityID;
                            user.Age = personListing.User.Age;
                            user.Gender = personListing.User.Gender;
                            _unitofWork.User.update(user);
                            _unitofWork.Save();
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/saveintrostep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(personListingDB));
        }
        [HttpPost]
        [Produces("application/json")]
        public JsonResult uploadPicture4Listing(string listingId, IFormFile image)
        {
            PersonListing personListing = null;
            Int64 listingID = Convert.ToInt64(listingId);
            try
            {
                if (listingID > 0 && image != null)
                {
                    var personListingDB = _unitofWork.PersonListing.Get(p => p.ID == listingID);
                    if (personListingDB.Profilepic != null)
                        _utility.DeletFile(personListingDB.Profilepic);
                    if (image.Length <= 100000)
                        personListingDB.Profilepic = _utility.SaveFile(image, @"assets/img/listingpersons");
                   else
                        personListingDB.Profilepic = _utility.ResizeAndSaveImage(image, "assets/img/profile", 100000, 600, 800);
                    _unitofWork.PersonListing.update(personListingDB);
                    _unitofWork.Save();
                    personListing = personListingDB;
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/uploadPicture4Listing");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult(personListing);
        }
        [HttpPost]
        public JsonResult savepreferencestep(PersonListing personListing)
        {
            PersonListing personListingDB = null;
            PersonListingVM personListingVm = null;
            try
            {
                if (personListing != null && personListing.ID > 0)
                {
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID,
                        includeProperties: "User");
                    personListingDB.FurnishingType = personListing.FurnishingType;
                    personListingDB.InternetStatus = personListing.InternetStatus;
                    personListingDB.Type = personListing.Type;
                    personListingDB.ParkingType = personListing.ParkingType;
                    personListingDB.MaxNo = personListing.MaxNo;
                    personListingDB.Totalpersons = personListing.Totalpersons;
                    _unitofWork.PersonListing.update(personListingDB);
                    _unitofWork.Save();
                }
                personListingVm = new PersonListingVM
                {
                    PersonListing = personListingDB,
                    CityList = getCities()
                };
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/savepreferencestep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            // return (new JsonResult(personListingDB));
            return (new JsonResult(personListingVm));
        }
        private List<SelectListItem> getCities()
        {
            IEnumerable<SelectListItem> Cities = _unitofWork.City.GetAll()
                .OrderBy(c => c.Name).ToList().Select(
                    x => new SelectListItem()
                    {
                        Text = x.Name,
                        Value = Convert.ToString(x.ID)
                    }
                    );
            return Cities.ToList();
        }
        [HttpPost]
        public JsonResult saverentstep(PersonListing personListing)
        {
            PersonListing personListingDB = null;
            List<ListingLifestyle> listingLifestyles = null;
            try
            {
                if (personListing != null && personListing.ID > 0)
                {
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID);
                    personListingDB.MonthlyBudget = personListing.MonthlyBudget;
                    personListingDB.MoveDate = personListing.MoveDate;
                    personListingDB.Length = personListing.Length;
                    _unitofWork.PersonListing.update(personListingDB);
                    _unitofWork.Save();
                    listingLifestyles = _unitofWork.ListingLifestyle.GetAll(
                                    le => le.PersonListingID == personListing.ID, includeProperties: "Lifestyle")
                        .ToList();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/saverentstep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(listingLifestyles));
        }
        [HttpPost]
        public JsonResult saveareastep(PersonListingVM personListingVm)
        {
            var personListing = personListingVm.PersonListing;
            PersonListing personListingDB = null;

            List<ListingEmployment> listingEmployments = null;
            try
            {
                if (personListing != null && personListing.ID > 0)
                {
                    personListingDB = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID,
                    includeProperties: "Employments,Lifestyles");
                    personListingDB.Suburb = personListing.Suburb;
                    _unitofWork.PersonListing.update(personListingDB);
                    _unitofWork.Save();
                    listingEmployments = _unitofWork.ListingEmployment.GetAll(
                        le => le.PersonListingID == personListing.ID, includeProperties: "Employment").ToList();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/saveareastep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(listingEmployments));
        }
        [HttpPost]
        public JsonResult saveaccommodationtypestep(PersonListingVM personListingVm)
        {
            var personListing = personListingVm.PersonListing;
            try
            {
                if (personListing != null && personListing.ID <= 0)
                {
                    personListing.UserId = HttpContext.Session.GetString("_UserId");
                    personListing.UId = Guid.NewGuid().ToString();
                    _unitofWork.PersonListing.Add(personListing);
                    _unitofWork.Save();

                    List<ListingPropertyType> desiredAccommodations = new List<ListingPropertyType>();
                    if (personListingVm.PropertyTypes.Any())
                    {
                        personListingVm.PropertyTypes.ForEach(p =>
                        {
                            desiredAccommodations.Add(new ListingPropertyType
                            {
                                personListingid = personListing.ID,
                                propertytypeid = p.ID,
                                isSelected = p.isSelected ? true : false,
                                CreatedOn = DateTime.UtcNow,
                                CreatedbyUserID = HttpContext.Session.GetString("_UserId"),
                                UId = Guid.NewGuid().ToString()
                            });

                        });
                        if (desiredAccommodations.Any())
                        {
                            _unitofWork.ListingPropertyType.AddRange(desiredAccommodations);
                            _unitofWork.Save();
                        }

                    }

                    //Get employment statuses from lookup table & create employment options for
                    //user creating listing to select later from the list.
                    var employments = _unitofWork.Employment.GetAll().ToList();
                    List<ListingEmployment> listingEmployments = new List<ListingEmployment>();
                    employments.ForEach(a =>
                    {
                        listingEmployments.Add(new ListingEmployment
                        {
                            PersonListingID = personListing.ID,
                            EmploymentID = a.ID,
                            isSelected = false,
                            CreatedOn = DateTime.UtcNow,
                            CreatedbyUserID = personListing.UserId,
                            UId = Guid.NewGuid().ToString()
                        });
                    });
                    _unitofWork.ListingEmployment.AddRange(listingEmployments);

                    //Get lifestyles from lookup table & create lifestyles for user creating listing
                    var lifestyles = _unitofWork.Lifestyle.GetAll().ToList();
                    List<ListingLifestyle> listingLifestyles = new List<ListingLifestyle>();
                    lifestyles.ForEach(ls =>
                    {
                        listingLifestyles.Add(new ListingLifestyle
                        {
                            PersonListingID = personListing.ID,
                            LifestyleID = ls.ID,
                            isSelected = false,
                            CreatedOn = DateTime.UtcNow,
                            CreatedbyUserID = personListing.UserId,
                            UId = Guid.NewGuid().ToString()
                        });
                    });
                    _unitofWork.ListingLifestyle.AddRange(listingLifestyles);
                    _unitofWork.Save();
                }
                else
                {
                    var personListinginDb = _unitofWork.PersonListing.Get(p => p.ID == personListing.ID);

                    /// delete existing desired accommodations for user
                    var desiredAccommodationsDB = _unitofWork.ListingPropertyType.GetAll(
                        p => p.personListingid == personListing.ID).ToList();
                    if (desiredAccommodationsDB.Any())
                        _unitofWork.ListingPropertyType.DeleteRange(desiredAccommodationsDB);
                    _unitofWork.Save();
                    /// End delete existing desired accommodations for user  

                    //Re-add updated desiredAccommodations list for user
                    List<ListingPropertyType> desiredAccommodations = new List<ListingPropertyType>();
                    if (personListingVm.PropertyTypes.Any())
                    {
                        personListingVm.PropertyTypes.ForEach(p =>
                        {
                            if (p.isSelected)
                            {
                                desiredAccommodations.Add(new ListingPropertyType
                                {
                                    personListingid = personListing.ID,
                                    propertytypeid = p.ID,
                                    CreatedOn = DateTime.UtcNow,
                                    CreatedbyUserID = HttpContext.Session.GetString("_UserId"),
                                    UId = Guid.NewGuid().ToString()
                                });
                            }
                        });
                        _unitofWork.ListingPropertyType.AddRange(desiredAccommodations);
                        _unitofWork.Save();
                    }
                    personListing = personListinginDb;
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "findplace/saveaccommodationtypestep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(personListing));
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
                    UId = Guid.NewGuid().ToString()
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
