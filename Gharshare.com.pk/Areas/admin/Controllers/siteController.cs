using Gharshare.CommonHelper;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace Gharshare.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebSiteRoles.Role_Admin)]
    public class siteController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        public siteController(IUnitofWork unitofWork, Utility Utility)
        {
            _unitofWork = unitofWork;
            _utility = Utility;
        }
        [HttpGet]
        public IActionResult propertylist(int pg = 1)
        {
            List<Property> properties = null;
            try
            {
                properties = _unitofWork.Property.GetAll(p => p.ID > 0,
                    includeProperties: "Owner").ToList();
                #region Pagination
                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                int recsCount = properties.Count();
                int recSkip = (pg - 1) * pageSize;
                properties = properties.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "site", "propertylist", pageSize, "", "",
                   properties.Count());
                this.ViewBag.Pager = pager;
                #endregion end pagination   
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(properties);
        }
        [HttpGet]
        public IActionResult personlist(int pg = 1)
        {
            List<PersonListing> personListings = null;
            try
            {
                personListings = _unitofWork.PersonListing.GetAll(p => p.ID > 0,
                    includeProperties: "User").ToList();
                #region Pagination
                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                int recsCount = personListings.Count();
                int recSkip = (pg - 1) * pageSize;
                personListings = personListings.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "site", "personListings", pageSize, "", "",
                   personListings.Count());
                this.ViewBag.Pager = pager;
                #endregion end pagination   
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(personListings);
        }
        public IActionResult Index()
        {
            DashboardVM dashboardvm = null;
            try
            {
                var properties = _unitofWork.Property.GetAll(p=>p.IsActive==true
                && p.listingstatus==Property.Status.Active).ToList().Count();

                var tenantListings = _unitofWork.PersonListing.GetAll(pl => pl.isActive == true &&
                pl.User.UserType == ApplicationUser.usertype.tenant, includeProperties: "User").
                ToList().Count();

                var freeTenantListings = _unitofWork.PersonListing.GetAll(pl => pl.isActive == true &&
                pl.User.UserType == ApplicationUser.usertype.freeuser, includeProperties: "User").
                ToList().Count();

                var landlords = _unitofWork.User.GetAll(u => u.UserType == ApplicationUser.usertype.landlord)
                .ToList().Count();

                var tenants = _unitofWork.User.GetAll(u => u.UserType == ApplicationUser.usertype.tenant)
                .ToList().Count();

                var Customers = _unitofWork.User.GetAll(u => u.UserType == ApplicationUser.usertype.tenant
                   || u.UserType == ApplicationUser.usertype.landlord
                   || u.UserType == ApplicationUser.usertype.freeuser).ToList();
                var totalCustomers = Customers.Count();

                var freeUsers = _unitofWork.User.GetAll(u => u.UserType == ApplicationUser.usertype.freeuser)
                .ToList().Count();

                var totalEarnings = _unitofWork.UserPayment.GetAll().Sum(up => up.Amount);
                var totalListings = properties + tenantListings + freeTenantListings;
                var totalTenantlistings = tenantListings + freeTenantListings;

                var freeUserPercent = 0;
                var tenantPercent = 0;                 
                var landlordPercent = 0;

                if (totalCustomers > 0)
                {
                    freeUserPercent = freeUsers * 100 / totalCustomers;
                    landlordPercent = landlords * 100 / totalCustomers;
                    tenantPercent = tenants * 100 / totalCustomers;
                }
                dashboardvm = new DashboardVM
                {
                    properties = properties, 
                    tenantListings= totalTenantlistings,
                    freeUsers = freeUsers,
                    totalCustomers = totalCustomers,
                    totalListings = totalListings,
                    totalEarnings = totalEarnings,
                    tenantPercent = tenantPercent,
                    landlordPercent = landlordPercent,
                    freeUserPercent = freeUserPercent,
                    recentCustomers = Customers.OrderByDescending(c => c.DateCreated).Take(5).ToList(),
                };
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(dashboardvm);
        }
        public IActionResult lifestylelist()
        {
            List<Lifestyle> lifestyles = null;
            try
            {
                lifestyles = _unitofWork.Lifestyle.GetAll().ToList();
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(lifestyles);
        }
        [HttpGet]
        public IActionResult addlifestyle(Int64? Id)
        {
            Lifestyle lifestyle = null;
            if (Id != null)
            {
                lifestyle = _unitofWork.Lifestyle.Get(e => e.ID == Id);
            }
            else
            {
                lifestyle = new Lifestyle();
            }
            return View(lifestyle);
        }
        [HttpPost]
        public IActionResult addlifestyle(Lifestyle lifestyle, IFormFile? file)
        {
            var scenario = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var lifestyleinDb = _unitofWork.Lifestyle.Get(
                        c => c.ID == lifestyle.ID);
                    if (lifestyleinDb != null)
                        _unitofWork.Lifestyle.Detach(lifestyleinDb);

                    if (lifestyleinDb != null && lifestyle.ID == 0) // add scenario
                    {
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { error = "Lifestyle already exists" });
                    }
                    else
                    {
                        if (lifestyle.ID > 0)
                        {
                            if (file != null)
                            {
                                if (lifestyle.IconPath != null)
                                {
                                    _utility.DeletFile(lifestyle.IconPath);
                                }
                                lifestyle.IconPath = _utility.SaveFile(file, @"img/icons/lifestyle");
                            }
                            _unitofWork.Lifestyle.update(lifestyle);
                            scenario = "edit";
                        }
                        else
                        {
                            if (file != null)
                            {
                                lifestyle.IconPath = _utility.SaveFile(file, @"img/icons/lifestyle");
                            }
                            lifestyle.UId = Guid.NewGuid().ToString();
                            _unitofWork.Lifestyle.Add(lifestyle);
                            scenario = "add";
                        }
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return (new JsonResult(scenario));
        }
        public IActionResult employmentlist()
        {
            List<Employment> employments = null;
            try
            {
                employments = _unitofWork.Employment.GetAll().ToList();
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(employments);
        }
        [HttpGet]
        public IActionResult addemployment(Int64? Id)
        {
            Employment employment = null;
            if (Id != null)
            {
                employment = _unitofWork.Employment.Get(e => e.ID == Id);
            }
            else
            {
                employment = new Employment();
            }
            return View(employment);
        }
        [HttpPost]
        public IActionResult addemployment(Employment employment, IFormFile? file)
        {
            var scenario = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var employmentinDb = _unitofWork.Employment.Get(
                        c => c.ID == employment.ID);
                    if (employmentinDb != null)
                        _unitofWork.Employment.Detach(employmentinDb);

                    if (employmentinDb != null && employment.ID == 0) // add scenario
                    {
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { error = "Profession already exists" });
                    }
                    else
                    {
                        if (employment.ID > 0)
                        {
                            if (file != null)
                            {
                                if (employment.IconPath != null)
                                {
                                    _utility.DeletFile(employment.IconPath);
                                }
                                employment.IconPath = _utility.SaveFile(file, @"img/icons/employment");
                            }
                            _unitofWork.Employment.update(employment);
                            scenario = "edit";
                        }
                        else
                        {
                            if (file != null)
                            {
                                employment.IconPath = _utility.SaveFile(file, @"img/icons/employment");
                            }
                            employment.UId = Guid.NewGuid().ToString();
                            _unitofWork.Employment.Add(employment);
                            scenario = "add";
                        }
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return (new JsonResult(scenario));
        }
        public IActionResult acceptinglist()
        {
            List<Accepting> acceptings = null;
            try
            {
                acceptings = _unitofWork.Accepting.GetAll().ToList();
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(acceptings);
        }
        [HttpGet]
        public IActionResult addaccepting(Int64? Id)
        {
            Accepting accepting = null;
            if (Id != null)
            {
                accepting = _unitofWork.Accepting.Get(a => a.ID == Id);
            }
            else
            {
                accepting = new Accepting();
            }
            return View(accepting);
        }
        [HttpPost]
        public IActionResult addaccepting(Accepting accepting, IFormFile? file)
        {
            var scenario = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var acceptinginDb = _unitofWork.Furnishing.Get(
                        c => c.ID == accepting.ID);
                    if (acceptinginDb != null)
                        _unitofWork.Furnishing.Detach(acceptinginDb);

                    if (acceptinginDb != null && accepting.ID == 0) // add scenario
                    {
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { error = "Accepting items already exists" });
                    }
                    else
                    {
                        if (accepting.ID > 0)
                        {
                            if (file != null)
                            {
                                if (accepting.iconpath != null)
                                {
                                    _utility.DeletFile(accepting.iconpath);
                                }
                                accepting.iconpath = _utility.SaveFile(file, @"img/icons/accepting");
                            }
                            _unitofWork.Accepting.update(accepting);
                            scenario = "edit";
                        }
                        else
                        {
                            accepting.CreatedOn = DateTime.UtcNow;
                            if (file != null)
                            {
                                accepting.iconpath = _utility.SaveFile(file, @"img/icons/accepting");
                            }
                            accepting.UId = Guid.NewGuid().ToString();
                            _unitofWork.Accepting.Add(accepting);
                            scenario = "add";
                        }
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return (new JsonResult(scenario));
        }
        [HttpGet]
        public IActionResult addRoomfeature(Int64? Id)
        {
            Furnishing furnishing = null;
            if (Id != null)
            {
                furnishing = _unitofWork.Furnishing.Get(f => f.ID == Id);
            }
            else
            {
                furnishing = new Furnishing();
            }
            return View(furnishing);
        }
        [HttpPost]
        public IActionResult addRoomfeature(Furnishing furnishing, IFormFile? file)
        {
            var scenario = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var furnishinginDb = _unitofWork.Furnishing.Get(
                        c => c.ID == furnishing.ID);
                    if (furnishinginDb != null)
                        _unitofWork.Furnishing.Detach(furnishinginDb);

                    if (furnishinginDb != null && furnishing.ID == 0) // add scenario
                    {
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { error = "Room feature already exists" });
                    }
                    else
                    {
                        if (furnishing.ID > 0)
                        {
                            furnishing.UpdatedbyUserId = HttpContext.Session.
                                GetString("_UserId");
                            if (file != null)
                            {
                                if (furnishing.Svgd != null)
                                {
                                    _utility.DeletFile(furnishing.Svgd);
                                }
                                furnishing.Svgd = _utility.SaveFile(file, @"img/icons/feature");
                            }
                            furnishing.UpdatedOn = DateTime.UtcNow;
                            _unitofWork.Furnishing.update(furnishing);
                            scenario = "edit";
                        }
                        else
                        {
                            furnishing.CreatedbyUserId = HttpContext.Session.
                                GetString("_UserId");
                            furnishing.UId = Guid.NewGuid().ToString();
                            if (file != null)
                            {
                                furnishing.Svgd = _utility.SaveFile(file, @"img/icons/feature");
                            }
                            _unitofWork.Furnishing.Add(furnishing);
                            scenario = "add";
                        }
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return (new JsonResult(scenario));
        }
        public IActionResult roomfeature()
        {
            List<Furnishing> features = null;
            try
            {
                features = _unitofWork.Furnishing.GetAll().ToList();
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(features);
        }
        [HttpPost]
        public IActionResult addtemplatetype(EmailTemplateType emailTemplateType)
        {
            var scenario = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var emailTemplateTypeinDb = _unitofWork.EmailTemplateType.Get(
                        c => c.Title == emailTemplateType.Title.Trim());
                    if (emailTemplateTypeinDb != null)
                        _unitofWork.EmailTemplateType.Detach(emailTemplateTypeinDb);

                    if (emailTemplateTypeinDb != null && emailTemplateType.ID == 0) // add scenario
                    {
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { error = "Email template type name already exists" });
                    }
                    else
                    {
                        if (emailTemplateType.ID > 0)
                        {
                            emailTemplateType.UpdatedbyUserId = HttpContext.Session.
                                GetString("_UserId");
                            emailTemplateType.UpdatedOn = DateTime.UtcNow;
                            _unitofWork.EmailTemplateType.update(emailTemplateType);
                            scenario = "edit";
                        }
                        else
                        {
                            emailTemplateType.CreatedbyUserId = HttpContext.Session.
                                GetString("_UserId");
                            emailTemplateType.UId = Guid.NewGuid().ToString();
                            _unitofWork.EmailTemplateType.Add(emailTemplateType);
                            scenario = "add";
                        }
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return (new JsonResult(scenario));
        }
        [HttpGet]
        public IActionResult addtemplatetype(Int64? typeId)
        {
            EmailTemplateType emailTemplateType = null;
            if (typeId != null)
            {
                emailTemplateType = _unitofWork.EmailTemplateType.Get(t => t.ID == typeId);
            }
            else
            {
                emailTemplateType = new EmailTemplateType();
            }
            return View(emailTemplateType);
        }
        [HttpGet]
        public IActionResult templatetypes(int pg = 1)
        {
            List<EmailTemplateType> emailTemplates = null;
            try
            {
                var emailTemplateTypes = _unitofWork.EmailTemplateType.GetAll(includeProperties:
                    "CreatedbyUser").OrderBy(c => c.Title).ToList();
                #region Pagination
                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                int recsCount = emailTemplateTypes.Count();
                int recSkip = (pg - 1) * pageSize;
                emailTemplates = emailTemplateTypes.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "site", "templatetypes", pageSize, "", "",
                   emailTemplates.Count());
                this.ViewBag.Pager = pager;
                #endregion end pagination   
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(emailTemplates);
        }
        [HttpPost]
        public IActionResult deleteemailtemplate(EmailTemplate emailTemplate)
        {
            if(emailTemplate!=null)
            {
                _unitofWork.EmailTemplate.Delete(emailTemplate);
                _unitofWork.Save();
                TempData["success"] = "Email template successfully deleted!";
            }
            return RedirectToAction("emailtemplates", "site", new {area="admin"});
        }
        [HttpGet]
        public IActionResult deleteemailtemplate(Int64? templateId)
        {             
            EmailTemplate emailTemplate = null;
            try
            { 
                if (templateId != null)
                {
                    emailTemplate = _unitofWork.EmailTemplate.Get(et => et.ID == templateId);
                } 
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(emailTemplate);
        }
        [HttpPost]
        public IActionResult createemailtemplate(EmailTemplate emailTemplate)
        {
            try
            {                 
                if (emailTemplate != null)
                {
                    if (emailTemplate.ID > 0)
                    {
                        emailTemplate.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                        emailTemplate.UpdatedOn = DateTime.UtcNow;
                        emailTemplate.UId = Guid.NewGuid().ToString();
                        _unitofWork.EmailTemplate.update(emailTemplate);
                        TempData["success"] = "Email template successfully updated!";
                    }
                    else
                    {
                        emailTemplate.CreatedbyUserId = HttpContext.Session.GetString("_UserId");
                        _unitofWork.EmailTemplate.Add(emailTemplate);
                        TempData["success"] = "Email template successfully saved!";
                    }
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return Redirect("emailtemplates");
        }
        [HttpGet]
        public IActionResult createemailtemplate(Int64? templateId)
        {
            EmailTemplateVM emailTemplateVM = null;
            EmailTemplate emailTemplate = null;
            try
            {
                //var emailTemplateTypes = _unitofWork.EmailTemplateType.GetAll(t => t.IsActive == true).
                //    OrderBy(t => t.Title).ToList().Select(x => new SelectListItem()
                //    {
                //        Text = x.Title,
                //        Value = x.ID.ToString()
                //    });


                if (templateId != null)
                {
                    emailTemplate = _unitofWork.EmailTemplate.Get(et => et.ID == templateId);
                    ViewBag.Case = "edit";
                }
                else
                {
                    emailTemplate = new EmailTemplate();
                    ViewBag.Case = "add";
                }
                //emailTemplateVM = new EmailTemplateVM
                //{
                //    emailTemplate = emailTemplate,
                //    templateTypes = emailTemplateTypes
                //};
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(emailTemplate);
        }
        [HttpGet]
        public IActionResult emailtemplates(int pg = 1)
        {
            List<EmailTemplate> data = null;
            try
            {
                var emailTemplates = _unitofWork.EmailTemplate.GetAll(includeProperties:
                    "CreatedbyUser").OrderBy(c => c.Title).ToList();
                #region Pagination
                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                int recsCount = emailTemplates.Count();
                int recSkip = (pg - 1) * pageSize;
                data = emailTemplates.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "site", "emailtemplates", pageSize, "", "",
                    data.Count());
                this.ViewBag.Pager = pager;
                #endregion end pagination   
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult deletecity(City city)
        {
            try
            {
                _unitofWork.City.Delete(city);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "error deleting city" });
            }
            return Redirect("index");
        }
        [HttpGet]
        public IActionResult deletecity(Int64 cityId)
        {
            var city = _unitofWork.City.Get(c => c.ID == cityId);
            return View(city);
        }
        [HttpPost]
        [Produces("application/json")]
        public JsonResult uploadCityImage(Int64 cityId, IFormFile image)
        {
            var city = _unitofWork.City.Get(u => u.ID == cityId);
            try
            {
                if (cityId != null && image != null)
                {
                    _utility.DeletFile(city.Image);
                    if (image.Length <= 100000)
                        city.Image = _utility.SaveFile(image, @"assets/city");
                    else
                        city.Image = _utility.ResizeAndSaveImage(image, "assets/city", 100000, 600, 800);
                    _unitofWork.City.update(city);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "site/uploadCityImage");
            }
            return new JsonResult(city);
        }
        [HttpPost]
        public IActionResult addcity(City city, IFormFile? cityImage)
        {
            var scenario = "";
            try
            {
                if (ModelState.IsValid)
                {
                    var cityinDb = _unitofWork.City.Get(c => c.Name == city.Name.Trim());
                    if (cityinDb != null)
                        _unitofWork.City.Detach(cityinDb);

                    if (cityinDb != null && city.ID == 0) // add city scenario
                    {
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { error = "City name already exists" });
                    }
                    else
                    {
                        if (city.ID > 0)
                        {                             
                            _unitofWork.City.update(city);
                            scenario = "edit";
                        }
                        else
                        {
                            if (cityImage != null)
                                city.Image = _utility.SaveFile(cityImage, @"assets/cities");
                            city.CreatedOn = DateTime.UtcNow;
                            city.UId = Guid.NewGuid().ToString();
                            _unitofWork.City.Add(city);
                            scenario = "add";
                        }
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return (new JsonResult(scenario));
        }
        [HttpGet]
        public IActionResult addcity(Int64? cityId)
        {
            City city = null;
            if (cityId != null)
            {
                city = _unitofWork.City.Get(c => c.ID == cityId);
            }
            else
            {
                city = new City();
            }
            return View(city);
        }
        [HttpGet]
        public IActionResult cities(int pg = 1)
        {
            List<City> data = null;
            try
            {
                var cities = _unitofWork.City.GetAll().OrderBy(c => c.Name).ToList();
                #region Pagination
                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                int recsCount = cities.Count();
                int recSkip = (pg - 1) * pageSize;
                data = cities.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "site", "cities", pageSize, "", "",
                    data.Count());
                this.ViewBag.Pager = pager;
                #endregion end pagination   
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult settings(Sitesetting sitesetting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (sitesetting.ID > 0)
                    {
                        sitesetting.UpdatedOn = DateTime.UtcNow;
                        sitesetting.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                        _unitofWork.Sitesetting.update(sitesetting);
                    }
                    else
                    {
                        sitesetting.CreatedOn = DateTime.UtcNow;
                        sitesetting.CreatedbyUserId = HttpContext.Session.GetString("_UserId");
                        sitesetting.UId = Guid.NewGuid().ToString();
                        _unitofWork.Sitesetting.Add(sitesetting);
                    }
                    _unitofWork.Save();
                    TempData["success"] = "Site settings successfully saved!";
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return RedirectToAction("index", "site");
        }
        [HttpGet]
        public IActionResult settings()
        {
            Sitesetting sitesetting = new Sitesetting();
            Sitesetting sitesettingdb = null;
            try
            {
                sitesettingdb = _unitofWork.Sitesetting.Get(s => s.ID > 0);
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            if (sitesettingdb != null)
                return View(sitesettingdb);
            else
                return View(sitesetting);
        }
        [HttpGet]
        public IActionResult transactions(int pg = 1)
        {
            //var userId = HttpContext.Session.GetString("_UserId");
            var payments = _unitofWork.UserPayment.GetAll(includeProperties: "User,Plan").ToList();
            #region Pagination
            int pageSize = Pager.GetPageSize();
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = payments.Count();
            int recSkip = (pg - 1) * pageSize;
            var data = payments.Skip(recSkip).Take(pageSize).ToList();
            var pager = new Pager(recsCount, pg, "site", "transactions", pageSize, "", "",
                data.Count());
            ViewBag.Pager = pager;
            #endregion end pagination
            return View(data);
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
