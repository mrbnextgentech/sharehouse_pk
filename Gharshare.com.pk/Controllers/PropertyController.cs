using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using System.Security.Claims;
//using Microsoft.AspNetCore.Hosting.IWebHosEnvironment;
using Gharshare.Views.Shared.Components.SearchBar;
using Gharshare.CommonHelper;
using System.Net.Mail;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using IronPdf;
using Microsoft.AspNetCore.SignalR;
using Gharshare.com.pk.Hubs;
using System.Text.Json;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ShareHouseManagement.Areas.Admin.Controllers
{
    //[Area("Admin")]
    // [Authorize(Roles=WebSiteRoles.Role_Admin+","+WebSiteRoles.Role_Manager + "," +WebSiteRoles.Role_Landlord)]
    public class PropertyController : Controller
    {
        private IUnitofWork _unitofWork;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IUserStore<ApplicationUser> _userStore;
        private Utility _utility;
        private readonly IEmailSender _emailSender;
        private readonly EmailSender _emailSender1;
        private readonly IHubContext<ChatHub> _chat;
        public PropertyController(
            IUnitofWork iunitofWork,
            Utility utility,
            IWebHostEnvironment hostingEnvironment,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            IEmailSender emailSender,
            EmailSender emailSender1, IHubContext<ChatHub> chat
            )
        {
            _unitofWork = iunitofWork;
            _utility = utility;
            _hostingEnvironment = hostingEnvironment;
            _signInManager = signInManager;
            _userManager = userManager;
            //_emailStore = GetEmailStore();
            _emailSender = emailSender;
            _emailSender1 = emailSender1;
            _chat = chat;
        }
        private List<SelectListItem> LoadCities()
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
        public IActionResult Filter([FromBody] PropertyFilter selection)
        {
            List<Property> filteredPropertyList = new List<Property>();
            try
            {
                if (selection.selection != null)
                {
                    List<Int64> feature = selection.selection.Feature.Select(s => Int64.Parse(s)).ToList();
                    List<int> type = selection.selection.Type.Select(s => int.Parse(s)).ToList();
                    List<int> bed = selection.selection.Bed.Select(s => int.Parse(s)).ToList();
                    int? maxBed = bed.FirstOrDefault(b => b >= 5);

                    List<string> rent = selection.selection.Rent;

                    List<RentRange> rentRanges = new List<RentRange>();
                    foreach (string r in rent)
                    {
                        string[] rentValues = r.Split('-');
                        int minValue = int.Parse(rentValues[0]);
                        int maxValue = rentValues.Length > 1 ? int.Parse(rentValues[1]) : int.MaxValue;
                        rentRanges.Add(new RentRange(minValue, maxValue));
                    }
                    var query = _unitofWork.Property.GetAll(P => P.IsActive == true
                        && P.IsAdlive == true
                        && (type.Count == 0)
                        && (maxBed == null || P.Bed >= maxBed)
                        && (!bed.Any() || bed.Contains((int)P.Bed))
                        && (!feature.Any() || P.Features.Any(pf =>
                        feature.Contains((long)pf.FeatureID)
                        && pf.isSelected == true)), includeProperties: "Features").ToList();

                    if (rentRanges.Count > 0)
                    {
                        query = query.Where(p => rentRanges.Any(rr => p.Rent >= rr.MinValue
                        && p.Rent <= rr.MaxValue)).ToList();
                    }
                    filteredPropertyList = query;
                }
            }
            catch (Exception ex)
            {
                //LogErrorDetail(ex, "/Property/Filter");
                return new JsonResult(new { error = true, message = ex.Message });
            }
            return new JsonResult(filteredPropertyList);
        }
        public JsonResult sort(int sortid, string searchPhrase)
        {
            List<Property> properties = null;
            try
            {
                if (searchPhrase != null)
                {
                    if (sortid == 1)//default sort
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true &&
                              p.Address.Contains(searchPhrase)).OrderBy(p => p.Address)
                              .ToList();
                    }
                    else if (sortid == 2)//sort by popularity
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true &&
                              p.Address.Contains(searchPhrase)).
                              OrderByDescending(p => p.Rating)
                              .ThenByDescending(p => p.UserClicks)
                              .ToList();
                    }
                    else if (sortid == 3)//Sort by new arrivals
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true &&
                              p.Address.Contains(searchPhrase)).
                              OrderByDescending(p => p.CreatedOn)
                              .ToList();
                    }
                    else if (sortid == 4)//sort by rent: low to high
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true &&
                              p.Address.Contains(searchPhrase)).
                              OrderBy(p => p.Rent)
                              .ToList();
                    }
                    else if (sortid == 5)//sort by rent: high to low
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true &&
                              p.Address.Contains(searchPhrase)).
                              OrderByDescending(p => p.Rent)
                              .ToList();
                    }
                }
                else
                {
                    if (sortid == 1)//default sort
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true).OrderBy(p => p.Address)
                              .ToList();
                    }
                    else if (sortid == 2)//sort by popularity
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true).
                              OrderByDescending(p => p.Rating)
                              .ThenByDescending(p => p.UserClicks)
                              .ToList();
                    }
                    else if (sortid == 3)//Sort by new arrivals
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true).
                              OrderByDescending(p => p.CreatedOn)
                              .ToList();
                    }
                    else if (sortid == 4)//sort by rent: low to high
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true).
                              OrderBy(p => p.Rent)
                              .ToList();
                    }
                    else if (sortid == 5)//sort by rent: high to low
                    {
                        properties = _unitofWork.Property.GetAll
                              (p => p.IsActive == true && p.IsAdlive == true).
                              OrderByDescending(p => p.Rent)
                              .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.InnerException?.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                // return RedirectToAction("Error", "Error");
            }
            return new JsonResult(properties);
            //return Json(new { message = "Success" });
        }
        public JsonResult searchonInput(string Searchphrase)
        {
            List<Property> properties = null;
            try
            {
                if (Searchphrase != null)
                {
                    properties = _unitofWork.Property.GetAll
                          (p => p.IsActive == true && p.IsAdlive == true &&
                          p.Address.Contains(Searchphrase)).ToList();
                }
                else
                {
                    properties = _unitofWork.Property.GetAll
                          (p => p.IsActive == true && p.IsAdlive == true).
                          ToList();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                // return RedirectToAction("Error", "Error");
            }
            return new JsonResult(properties);
        }
        public async Task<IActionResult> PropertyList(string? Searchphrase = null, int? pg = 1)
        {
            SearchVM searchvm = null;
            //try
            //{
            List<Property> properties = null;

            if (Searchphrase != null)
            {
                properties = _unitofWork.Property.GetAll
                      (p => p.IsActive == true && p.IsAdlive == true &&
                      p.Address.Contains(Searchphrase),
                      includeProperties: "State,City").ToList();
            }
            searchvm = new SearchVM
            {
                Properties = properties,
                Searchphrase = string.Empty,
                Features = _unitofWork.Feature.GetAll(f => f.IsActive == true).
                OrderBy(f => f.Title).ToList()
            };
            #region Pagination
            int pageSize = Pager.GetPageSize();
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = searchvm.Properties.Count();
            var pager = new Pager(recsCount, (int)pg, "property", "PropertyList",
                pageSize);//, Searchphrase);
                          //pager.HasSearchPhrase = true;
            int recSkip = (int)((pg - 1) * pageSize);
            var data = searchvm.Properties.Skip(recSkip).Take(pager.PageSize).ToList();
            searchvm.Properties = data;
            this.ViewBag.Pager = pager;
            #endregion end pagination
            return View(searchvm);
            //}
            //catch (Exception ex)
            //{
            //    TempData["ErrorMessage"] = ex.Message;
            //    TempData["StackTrace"] = ex.StackTrace;
            //    TempData["Source"] = ex.Source;
            //    return RedirectToAction("Error", "Error");
            //}
        }

        [HttpPost]
        public async Task<IActionResult> search(string? Searchphrase = null, int? pg = 1)
        {
            SearchVM searchvm = null;
            try
            {
                List<Property> properties = null;

                if (Searchphrase != null)
                {
                    properties = _unitofWork.Property.GetAll
                          (p => p.IsActive == true && p.IsAdlive == true &&
                          p.Address.Contains(Searchphrase),
                          includeProperties: "State,City").ToList();
                }
                searchvm = new SearchVM
                {
                    Properties = properties,
                    Searchphrase = string.Empty,
                    Features = _unitofWork.Feature.GetAll(f => f.IsActive == true).
                    OrderBy(f => f.Title).ToList()
                };
                #region Pagination
                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                int recsCount = searchvm.Properties.Count();
                var pager = new Pager(recsCount, (int)pg, "property", "PropertyList",
                    pageSize);//, Searchphrase);
                              // pager.HasSearchPhrase = true;
                int recSkip = (int)((pg - 1) * pageSize);
                var data = searchvm.Properties.Skip(recSkip).Take(pager.PageSize).ToList();
                searchvm.Properties = data;
                this.ViewBag.Pager = pager;
                #endregion end pagination
                return View(searchvm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> search(string cid, string type)
        {
            SearchVM searchvm = null;
            try
            {
                List<Property> properties = null;

                if (cid != null && type != null)
                {
                    properties = _unitofWork.Property.GetAll
                          (p => p.IsActive == true && p.IsAdlive == true &&
                          p.CityID == Convert.ToInt64(cid),
                          includeProperties: "State,City").ToList();
                }
                else if (cid != null && type == null)
                {
                    properties = _unitofWork.Property.GetAll
                         (p => p.IsActive == true && p.IsAdlive == true &&
                         p.CityID == Convert.ToInt64(cid),
                         includeProperties: "State,City").ToList();
                }
                else if (cid == null && type != null)
                {
                    properties = _unitofWork.Property.GetAll
                         (p => p.IsActive == true && p.IsAdlive == true,
                         includeProperties: "State,City").ToList();
                }
                else
                {
                    properties = _unitofWork.Property.GetAll
                         (p => p.IsActive == true && p.IsAdlive == true,
                         includeProperties: "State,City").ToList();
                }

                searchvm = new SearchVM
                {
                    Properties = properties,
                    Searchphrase = null,
                    Features = _unitofWork.Feature.GetAll(f => f.IsActive == true).
                    OrderBy(f => f.Title).ToList()
                };
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");

            }
            return View(searchvm);
        }
         
        private List<SelectListItem> LoadPropertyTypes()
        {
            IEnumerable<SelectListItem> PropertyTypes = _unitofWork.PropertyType.GetAll().ToList().Select(
                    x => new SelectListItem()
                    {
                        Text = x.Title,
                        Value = Convert.ToString(x.ID)
                    }
                    );
            return PropertyTypes.ToList();
        }        

        public JsonResult updateEnquiryStatus(Enquiry.EnquiryStatus statusId, Int64 enquiryId)
        {
            try
            {
                Enquiry propertyEnquiry = _unitofWork.Enquiry.Get(x => x.ID == enquiryId);
                propertyEnquiry.Enquirystatus = statusId;
                propertyEnquiry.UpdatedOn = DateTime.UtcNow;
                _unitofWork.Enquiry.update(propertyEnquiry);
                _unitofWork.Save();
            }
            catch (Exception ex)
            {
                LogErrorDetail(ex, "Property/updateEnquiryStatus");
            }
            return new JsonResult("success");
        }
        [HttpGet]
        public IActionResult Enquiries(Int64 ID)
        {
            try
            {
                List<Enquiry> propertyEnquiries = _unitofWork.Enquiry.GetAll(x => x.PropertyID == ID).ToList();
                return View(propertyEnquiries);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var UserId = HttpContext.Session.GetString("_UserId");
                ViewData["userID"] = UserId;
                ViewData["userType"] = HttpContext.Session.GetString("_UserType");

                List<Property> Properties = null;
                if (UserId != null)
                {
                    Properties = _unitofWork.Property.GetAll(x => x.OwnerId == UserId, includeProperties: "Enquiries,Lease").ToList();
                    return View(Properties);
                }
                else
                    return Redirect("/Identity/Account/login");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        public IActionResult Index(int pg = 1, string SearchText = "", string sortOrder = "")
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ApplicationUser user = _userManager.Users.Where(_u => _u.Id == userId).FirstOrDefault();
                List<Property> Properties;

                #region Get Data on Search criterion
                Search search = new Search() { Action = "index", Controller = "Property", SearchText = SearchText };
                ViewBag.Search = search;


                if (user.UserType == ApplicationUser.usertype.admin)
                {
                    if (SearchText == string.Empty || SearchText == null)
                        Properties = _unitofWork.Property.GetAll(includeProperties: "Images,Owner,State,City").ToList();

                    else
                        Properties = _unitofWork.Property.GetAll(p => p.Address.Contains(SearchText.Trim()) || p.City.Name.Contains(SearchText.Trim()), includeProperties: "Images,Owner,State,City").ToList();
                }
                else
                {
                    if (SearchText == string.Empty || SearchText == null)
                        Properties = _unitofWork.Property.GetAll(x => x.OwnerId == userId, includeProperties: "Images,Owner,State,City").ToList();
                    //Properties = _unitofWork.Property.GetAll(x => x.OwnerId == userId).ToList();
                    else
                        Properties = _unitofWork.Property.GetAll(x => x.OwnerId == userId && x.Address.Contains(SearchText.Trim()) || x.City.Name.Contains(SearchText.Trim()), includeProperties: "Images,Owner,State,City").ToList();

                }

                #endregion

                #region Apply pagination & sorting
                ViewBag.LocationSortParam = string.IsNullOrEmpty(sortOrder) ? "location_desc" : "";
                ViewBag.CitySortParam = sortOrder == "City" ? "city_desc" : "City";

                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Json", optional: false);
                IConfiguration configuration = builder.Build();
                int pageSize = Convert.ToInt32(configuration.GetValue<string>("PageSize:Size"));

                if (pg < 1)
                {
                    pg = 1;
                }
                ViewBag.pg = pg;
                int recsCount = Properties.Count();
                var pager = new Pager(recsCount, pg, "Property", "index", pageSize);
                pager.SortOrder = sortOrder;
                int recSkip = (pg - 1) * pageSize;

                List<Property> data;
                switch (sortOrder)
                {
                    case "location_desc":
                        data = Properties.Skip(recSkip).Take(pager.PageSize).OrderByDescending(p => p.Address).ToList();
                        ViewData["SortIcon"] = "fa fa-arrow-up";
                        break;
                    case "City":
                        data = Properties.Skip(recSkip).Take(pager.PageSize).OrderBy(p => p.City.Name).ToList();
                        ViewData["SortIconCity"] = "fa fa-arrow-down";
                        break;
                    case "city_desc":
                        data = Properties.Skip(recSkip).Take(pager.PageSize).OrderByDescending(p => p.City.Name).ToList();
                        ViewData["SortIconCity"] = "fa fa-arrow-up";
                        break;

                    default:
                        data = Properties.Skip(recSkip).Take(pager.PageSize).OrderBy(p => p.Address).ToList();
                        ViewData["SortIcon"] = "fa fa-arrow-down";
                        break;
                }

                this.ViewBag.Pager = pager;
                #endregion

                return View(data);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpGet]
        public IActionResult CreateAd(Int64 ID)
        {
            try
            {
                Property Property = _unitofWork.Property.Get(x => x.ID == ID, includeProperties: "Images,Owner");
                string _userId = Property.OwnerId;

                PropertyVM propertyVM = new PropertyVM()
                {
                    Property = _unitofWork.Property.Get(x => x.ID == ID, includeProperties: "Images,Owner,State,City"),
                    Properties = _unitofWork.Property.GetAll().ToList(),
                    Landlords = _userManager.Users.Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id
                        }
                        ),
                    Landlord = _unitofWork.User.Get(x => x.Id == _userId, includeProperties: "City,State"),
                    PropertyImages = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == ID).ToList(),
                    States = _unitofWork.State.GetAll().OrderBy(s => s.Name).ToList().Select(
                            s => new SelectListItem()
                            {
                                Text = s.Name,
                                Value = s.ID.ToString()
                            }),
                    State = _unitofWork.State.Get(s => s.ID == Property.StateID),
                    //Cities = _unitofWork.City.GetAll(x => x.StateID == Property.StateID).OrderBy(c => c.Name).ToList().Select(
                    Cities = _unitofWork.City.GetAll().OrderBy(c => c.Name).ToList().Select(
                    c => new SelectListItem()
                    {
                        Text = c.Name,
                        Value = c.ID.ToString()
                    }),
                    City = _unitofWork.City.Get(c => c.ID == Property.CityID),
                    Suburbs = _unitofWork.Suburb.GetAll(s => s.CityID == Property.CityID).
                    OrderBy(s => s.Name).ToList().Select(
                            s => new SelectListItem()
                            {
                                Text = s.Name,
                                Value = s.ID.ToString()

                            }),
                    PropertyFeatures = _unitofWork.PropertyFeature.GetAll(x => x.PropertyID == ID, includeProperties: "Feature").ToList(),
                    PropertyAcceptings = _unitofWork.PropertyAccepting.GetAll(x => x.PropertyID == ID, includeProperties: "Accepting").ToList(),
                    Inspections = _unitofWork.PropertyInspection.GetAll(x => x.PropertyID == ID).ToList(),

                };
                return View(propertyVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpPost]
        [Produces("application/json")]
        public JsonResult savePropertyImages(Int64 propertyID, List<IFormFile> files)
        {
            List<PropertyImage> PropertyImages = null;
            try
            {
                Int64 PID = propertyID;
                Property property = _unitofWork.Property.Get(x => x.ID == PID);
                if (PID != null && PID != 0 && files != null)
                {
                    PropertyImage _propertyImage = null;
                    Int64 _propertyId = property.ID;
                    for (int i = 0; i < files.Count(); i++)
                    {
                        if (i == 0)
                        {
                            _utility.DeletFile(property.CoverImage);
                            property.CoverImage = _utility.SaveFile(files[i],
                                @"assets/PropertyImages");
                            _unitofWork.Property.update(property);
                        }

                        IFormFile currentFile = files[i];
                        _propertyImage = new PropertyImage();
                        string _imagePath = _utility.SaveFile(currentFile, @"assets/PropertyImages");
                        if (i == 0)
                            _propertyImage.isCover = true;
                        _propertyImage.ImagePath = _imagePath;
                        _propertyImage.PropertyID = _propertyId;
                        _propertyImage.Description = "Image of property with Id:" + Convert.ToString(_propertyId);
                        if (i == 0)
                        {
                            _propertyImage.isCover = true;
                        }
                        _propertyImage.CreatedbyUserId= HttpContext.Session.GetString("_UserId");
                        _propertyImage.CreatedOn = DateTime.UtcNow;
                        _propertyImage.order = i + 1;
                        _unitofWork.PropertyImages.Add(_propertyImage);
                    }
                    _unitofWork.Save();
                    PropertyImages = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == PID)
                       .OrderByDescending(x=>x.isCover==true).ToList();
                }
            }
            catch (Exception ex)
            {
                LogErrorDetail(ex, "Property/savePropertyImages");
            }
            //return new JsonResult(JsonConvert.SerializeObject(PropertyImages));
            //string json = JsonConvert.SerializeObject(PropertyImages);
            return new JsonResult(PropertyImages);
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
        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult CreateAd(PropertyVM propertyVM, IFormFile? filecoverImage, List<IFormFile> files)
        public IActionResult CreateAd(PropertyVM propertyVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Property property = propertyVM.Property;
                    property.OwnerId = propertyVM.Landlord.Id;
                    property.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.Property.update(property);
                    _unitofWork.PropertyFeature.UpdateRange(propertyVM.PropertyFeatures);
                    _unitofWork.PropertyAccepting.UpdateRange(propertyVM.PropertyAcceptings);
                    _unitofWork.PropertyInspection.UpdateRange(propertyVM.Inspections);
                    //UpdatePropertyImages(propertyVM.Property, filecoverImage, files);
                    _unitofWork.Save();
                    TempData["success"] = "Ad successfully created!";
                }
                else
                {
                    TempData["error"] = "Error creating2 property ad!";
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                Property property = new Property
                {
                    Owneremail = HttpContext.Session.GetString("ownerEmail"),
                    Ownermobile = HttpContext.Session.GetString("ownerPhone"),
                    OwnerId = HttpContext.Session.GetString("_UserId")

                };
                var States = _unitofWork.State.GetAll().OrderBy(s => s.Name).ToList().Select(
                            s => new SelectListItem()
                            {
                                Text = s.Name,
                                Value = s.ID.ToString()
                            });
                ViewBag.States = States;
                var cities = LoadCities();
                ViewBag.Cities = cities;
                return View(property);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Property property)
        {
            try
            {
                ApplicationUser Owner = _userManager.Users.Where(_u =>
                _u.Id == HttpContext.Session.GetString("_UserId")).FirstOrDefault();
                property.OwnerId = Owner.Id;
                property.OwnerName = Owner.Name;
                _unitofWork.Property.Add(property);
                _unitofWork.Save();
                List<PropertyFeature> propertyFeatureList = new List<PropertyFeature>();
                var features = _unitofWork.Feature.GetAll(x => x.IsActive == true).ToList();

                for (int i = 0; i < features.Count(); i++)
                {
                    PropertyFeature propertyFeature = new PropertyFeature
                    {
                        PropertyID = property.ID,
                        CreatedOn = DateTime.UtcNow,
                        FeatureID = features[i].ID,
                        isSelected = false
                    };
                    propertyFeatureList.Add(propertyFeature);
                }
                _unitofWork.PropertyFeature.AddRange(propertyFeatureList);

                List<PropertyAccepting> propertyAcceptingList = new List<PropertyAccepting>();
                var acceptings = _unitofWork.Accepting.GetAll(x => x.IsActive == true).ToList();

                for (int i = 0; i < acceptings.Count(); i++)
                {
                    PropertyAccepting propertyAccepting = new PropertyAccepting
                    {
                        PropertyID = property.ID,
                        AcceptingID = acceptings[i].ID,
                        isSelected = false,
                        CreatedOn = DateTime.UtcNow,
                    };
                    propertyAcceptingList.Add(propertyAccepting);
                }
                _unitofWork.PropertyAccepting.AddRange(propertyAcceptingList);

                List<PropertyInspection> Inspections = new List<PropertyInspection>();
                for (int i = 0; i < 3; i++)
                {
                    PropertyInspection inspection = new PropertyInspection
                    {
                        PropertyID = property.ID,
                        InspectionDate = DateTime.UtcNow,
                        From = null,
                        To = null,
                        isActive = true,
                        CreatedOn = DateTime.UtcNow,
                        CreatedbyUser = null,
                        UpdatedOn = DateTime.UtcNow,
                        UpdatedbyUser = null
                    };
                    Inspections.Add(inspection);
                }
                _unitofWork.PropertyInspection.AddRange(Inspections);
                _unitofWork.Save();
                TempData["success"] = "Property successfully added";
                return RedirectToAction("list");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                var UserId = HttpContext.Session.GetString("_UserId");
                var userType = HttpContext.Session.GetString("_UserType");

                PropertyVM propertyVM = null;

                if (userType == "Admin" || userType == "Manager")
                {
                    propertyVM = new PropertyVM()
                    {
                        Property = new Property(),
                        Properties = _unitofWork.Property.GetAll().ToList(),
                        Landlords = _userManager.Users.Select(
                            x => new SelectListItem()
                            {
                                Text = x.Name,
                                Value = x.Id
                            }
                            ),
                        Landlord = new ApplicationUser(),
                        PropertyImages = null,
                        States = _unitofWork.State.GetAll().OrderBy(s => s.Name).ToList().Select(
                            s => new SelectListItem()
                            {
                                Text = s.Name,
                                Value = s.ID.ToString()

                            }),
                        State = new State()
                    };
                }
                else if (userType == "Landlord")
                {
                    propertyVM = new PropertyVM()
                    {
                        Property = new Property(),
                        Properties = _unitofWork.Property.GetAll().ToList(),
                        Landlords = _userManager.Users.Where(x =>x.Id == UserId).Select(
                             x => new SelectListItem()
                             {
                                 Text = x.Name,
                                 Value = x.Id
                             }
                             ),
                        Landlord = new ApplicationUser(),
                        PropertyImages = null,
                        States = _unitofWork.State.GetAll().OrderBy(s => s.Name).ToList().Select(
                            s => new SelectListItem()
                            {
                                Text = s.Name,
                                Value = s.ID.ToString()

                            }),
                        State = new State()
                    };
                }
                return View(propertyVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }

        public JsonResult GetSuburbs(int cityid)
        {
            List<Suburb> suburbs = null;
            try
            {
                suburbs = _unitofWork.Suburb.GetAll(s => s.CityID == cityid).OrderBy(s => s.Name).ToList();

            }
            catch (Exception ex)
            {
                LogErrorDetail(ex, "Property/GetSuburbs");
            }
            return new JsonResult(suburbs);
        }
        public JsonResult GetCities(int stateid)
        {
            List<City> cities = null;
            try
            {
                cities = _unitofWork.City.GetAll().OrderBy(c => c.Name).ToList();
            }
            catch (Exception ex)
            {
                LogErrorDetail(ex, "Property/GetCities");
            }
            return new JsonResult(cities);
        }

        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAd(PropertyVM propertyVM, IFormFile? filecoverImage, List<IFormFile> files)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Property property = propertyVM.Property;
                    property.OwnerId = propertyVM.Landlord.Id;
                    property.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.Property.update(property);
                    _unitofWork.PropertyFeature.UpdateRange(propertyVM.PropertyFeatures);
                    _unitofWork.PropertyAccepting.UpdateRange(propertyVM.PropertyAcceptings);
                    UpdatePropertyImages(propertyVM.Property, filecoverImage, files);
                    _unitofWork.PropertyInspection.UpdateRange(propertyVM.Inspections);
                    _unitofWork.Save();
                    TempData["success"] = "Ad successfully updated!";
                }
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }

        #region Commented code for reference only
        //propertyVM.Inspections.ForEach(x => { x.PropertyID = property.ID; });
        //foreach (var inspection  in propertyVM.Inspections.ToList())
        //{
        //    if(inspection.InspectionDate==null || inspection.From == null && inspection.To == null)
        //    {
        //        propertyVM.Inspections.Remove(inspection);
        //    }
        //}
        #endregion

        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpGet]
        public IActionResult EditAd(Int64 ID)
        {
            try
            {
                Property Property = _unitofWork.Property.Get(x => x.ID == ID, includeProperties: "Images,Owner");
                string _userId = Property.OwnerId;

                PropertyVM propertyVM = new PropertyVM()
                {
                    Property = _unitofWork.Property.Get(x => x.ID == ID, includeProperties: "Images,Owner,State,City"),
                    Properties = _unitofWork.Property.GetAll().ToList(),
                    Landlords = _userManager.Users.Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id
                        }
                        ),
                    Landlord = _unitofWork.User.Get(x => x.Id == _userId, includeProperties: "City,State"),
                    PropertyImages = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == ID).ToList(),
                    States = _unitofWork.State.GetAll().OrderBy(s => s.Name).ToList().Select(
                            s => new SelectListItem()
                            {
                                Text = s.Name,
                                Value = s.ID.ToString()
                            }),
                    State = _unitofWork.State.Get(s => s.ID == Property.StateID),
                    Cities = _unitofWork.City.GetAll().OrderBy(c => c.Name).ToList().Select(
                            c => new SelectListItem()
                            {
                                Text = c.Name,
                                Value = c.ID.ToString()
                            }),
                    City = _unitofWork.City.Get(c => c.ID == Property.CityID),
                    Suburbs = _unitofWork.Suburb.GetAll(s => s.CityID == Property.CityID).OrderBy(s => s.Name).ToList().Select(
                            s => new SelectListItem()
                            {
                                Text = s.Name,
                                Value = s.ID.ToString()
                            }),
                    PropertyFeatures = _unitofWork.PropertyFeature.GetAll(x => x.PropertyID == ID, includeProperties: "Feature").ToList(),
                    PropertyAcceptings = _unitofWork.PropertyAccepting.GetAll(x => x.PropertyID == ID, includeProperties: "Accepting").ToList(),
                    Inspections = _unitofWork.PropertyInspection.GetAll(x => x.PropertyID == ID).ToList()
                };
                return View(propertyVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PropertyVM propertyVm, List<IFormFile> files, IFormFile filecoverImage)
        {
            try
            {
                string _userId = propertyVm.Landlord.Id;
                string _coverImagePath = string.Empty;
                if (ModelState.IsValid)
                {
                    if (filecoverImage != null)
                    {
                        _coverImagePath = _utility.SaveFile(filecoverImage, @"assets/PropertyImages");
                    }
                    propertyVm.Property.CoverImage = _coverImagePath;
                    propertyVm.Property.OwnerId = _userId;
                    _unitofWork.Property.Add(propertyVm.Property);
                    _unitofWork.Save();
                    Int64 _propertyId = propertyVm.Property.ID;
                    foreach (var file in files)
                    {
                        IFormFile currentFile = file;
                        PropertyImage _propertyImage = new PropertyImage();
                        string _imagePath = _utility.SaveFile(currentFile, @"assets/PropertyImages");
                        _propertyImage.ImagePath = _imagePath;
                        _propertyImage.PropertyID = _propertyId;
                        _propertyImage.Description = "Image of property with Id:" + Convert.ToString(_propertyId);
                        _unitofWork.PropertyImages.Add(_propertyImage);
                        _unitofWork.Save();
                    }
                    TempData["success"] = "Property created successfully!";
                }
                else
                    TempData["error"] = "Error creating property. Please contact administrator.";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        public IActionResult Detail(Int64 ID)
        {
            try
            {
                Property Property = _unitofWork.Property.Get(x => x.ID == ID);
                string _userId = Property.OwnerId;
                var userClicks = Property.UserClicks;
                if (userClicks == null)
                    userClicks = 0;
                userClicks += 1;
                Property.UserClicks = userClicks;
                _unitofWork.Property.update(Property);
                _unitofWork.Save();
                HttpContext.Session.SetString("PropertyID", ID.ToString());

                List<PropertyImage> floorImages = _unitofWork.PropertyImages.
                    GetAll(x => x.PropertyID == ID &&
                    x.ImageType == PropertyImage.type.floor).ToList();
                // Top 5 Property Categories
                var topCategories = _unitofWork.Property.GetAll(p => p.IsActive == true && p.IsAdlive == true)
                .GroupBy(p => (Property.PropertyType)p.Propertytype)
                .OrderByDescending(g => g.Count())
                .Take(5).Select(g => new TopCategories
                {
                    CategoryName = g.Key.ToString(),
                    Total = g.Count()
                }).ToList();
                //----
                // Top Five best rating properties
                var topProperties = _unitofWork.Property
                .GetAll(p => p.IsActive == true && p.IsAdlive == true,
                includeProperties: "City")
                .OrderByDescending(p => p.Rating)
                .ThenByDescending(p => p.UserClicks)
                .Take(5).ToList();
                //--

                // Popular properties
                var popularProperties = _unitofWork.Property
                .GetAll(p => p.IsActive == true && p.IsAdlive == true &&
                p.Rating >= 4, includeProperties: "City")
                .OrderByDescending(p => p.Rating)
                .ThenByDescending(p => p.UserClicks).ToList();
                //--
                // Related properties
                var relatedProperties = _unitofWork.Property
                .GetAll(p => p.IsActive == true && p.IsAdlive == true &&
                p.CityID == Property.CityID, includeProperties: "City")
                .OrderByDescending(p => p.Rating)
                .ThenByDescending(p => p.UserClicks).ToList();
                //--

                PropertyVM propertyVM = new PropertyVM()
                {
                    Enquiry = new Enquiry
                    {
                        Enquirystatus = Enquiry.EnquiryStatus.unread,
                        PropertyID = ID
                    },
                    Property = _unitofWork.Property.Get(x => x.ID == ID, includeProperties: "State,City"),
                    Properties = _unitofWork.Property.GetAll().ToList(),
                    Landlords = _userManager.Users.Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id
                        }
                        ),
                    Landlord = _userManager.Users.Single(x => x.Id == _userId),
                    PropertyImages = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == ID).ToList(),
                    PropertyFeatures = _unitofWork.PropertyFeature.GetAll(x => x.PropertyID == ID && x.isSelected == true, includeProperties: "Feature").ToList(),
                    PropertyAcceptings = _unitofWork.PropertyAccepting.GetAll(x => x.PropertyID == ID && x.isSelected == true, includeProperties: "Accepting").ToList(),
                    Inspections = _unitofWork.PropertyInspection.
                    GetAll(x => x.PropertyID == ID && x.From != null).ToList(),
                    FloorImages = floorImages,
                    TopCategories = topCategories,
                    TopProperties = topProperties,
                    PopularProperties = popularProperties,
                    RelatedProperties = relatedProperties,
                    Searchphrase = string.Empty
                };
                var usertype = HttpContext.Session.GetString("_UserType");
                var userid = HttpContext.Session.GetString("_UserId");
                if (usertype == WebSiteRoles.Role_FreeUser && userid != null)
                    RecordUserPropertyView(ID, usertype, userid);
                return View(propertyVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        private void RecordUserPropertyView(long iD, string usertype, string userid)
        {
            try
            {
                PropertiesViewed propertiesViewed = _unitofWork.PropertiesViewed.Get(x => x.PropertyID == iD && x.UserID == userid);
                if (propertiesViewed != null)
                {
                    propertiesViewed.ViewDate = DateTime.UtcNow;
                    _unitofWork.PropertiesViewed.update(propertiesViewed);
                    _unitofWork.Save();
                }
                else
                {
                    propertiesViewed = new PropertiesViewed();
                    propertiesViewed.PropertyID = iD;
                    propertiesViewed.UserID = userid;
                    propertiesViewed.ViewDate = DateTime.UtcNow;
                    _unitofWork.PropertiesViewed.Add(propertiesViewed);
                    _unitofWork.Save();
                }
            }
            catch (Exception ex)
            {
                // LogErrorDetail(ex, "Property/RecordUserPropertyView");
                throw;
            }
        }

        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpGet]
        public IActionResult Delete(Int64 ID)
        {
            try
            {
                Property Property = _unitofWork.Property.Get(x => x.ID == ID, includeProperties: "Images,Owner,State,City");
                return View(Property);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }

        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpPost]
        public IActionResult Delete(Property property)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<PropertyImage> propertyImage = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == property.ID);
                    _unitofWork.PropertyImages.DeleteRange(propertyImage);
                    _unitofWork.Property.Delete(property);
                    _unitofWork.Save();
                    _utility.DeletFile(property.CoverImage);
                    foreach (var image in propertyImage)
                    {
                        _utility.DeletFile(image.ImagePath);
                    }
                }
                TempData["success"] = "Property deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpGet]
        public IActionResult PropertyPics(Int64 ID)
        {
            try
            {
                Property Property = _unitofWork.Property.Get(x => x.ID == ID);
                string _userId = Property.OwnerId;
                PropertyVM propertyVM = new PropertyVM()
                {
                    Property = _unitofWork.Property.Get(x => x.ID == ID),
                    Properties = _unitofWork.Property.GetAll().ToList(),
                    Landlords = _userManager.Users.Select(
                        x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.Id
                        }
                        ),
                    Landlord = _userManager.Users.Single(x => x.Id == _userId),
                    PropertyImages = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == ID).ToList()
                };
                return View(propertyVM);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }

        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        public JsonResult DeleteCover(Int64 ID)
        {
            try
            {
                Property property = _unitofWork.Property.Get(x => x.ID == ID);
                Int64 propertyID = ID;
                ViewBag.PropertyID = propertyID;
                _utility.DeletFile(property.CoverImage);
                property.CoverImage = String.Empty;
                _unitofWork.Property.update(property);
                _unitofWork.Save();
                TempData["success"] = "Property cover image deleted successfully!";
            }
            catch (Exception ex)
            {
                LogErrorDetail(ex, "Property/DeleteCover");
            }
            return Json(true);
        }
        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        public JsonResult DeletePropertyImage(Int64 ID)
        {
            List<PropertyImage> images = null;
            try
            {
                PropertyImage propertyImage = _unitofWork.PropertyImages.Get(x => x.ID == ID);
                Int64 propertyImageID = ID;
                Int64 propertyID = propertyImage.PropertyID;

                _utility.DeletFile(propertyImage.ImagePath);
                propertyImage.ImagePath = String.Empty;
                _unitofWork.PropertyImages.Delete(propertyImage);
                _unitofWork.Save();
                images = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == propertyID).ToList();
                //TempData["success"] = "Property image deleted successfully!";
            }
            catch (Exception ex)
            {
                LogErrorDetail(ex, "Property/DeletePropertyImage");
            }
            return new JsonResult(images);
        }
        public void UpdatePropertyImages(Property property, IFormFile filecoverImage, List<IFormFile> imageFiles)
        {
            try
            {
                Int64 PID = property.ID;
                if (PID != null && PID != 0)
                {
                    if (filecoverImage != null || imageFiles != null)
                    {
                        //Property property = _unitofWork.Property.Get(x => x.ID == PID);
                        if (filecoverImage != null)
                        {
                            if (property.CoverImage != null)
                                _utility.DeletFile(property.CoverImage);
                            string _coverImagePath = _utility.SaveFile(filecoverImage, @"assets/PropertyImages");
                            property.CoverImage = _coverImagePath;
                            _unitofWork.Property.update(property);
                            _unitofWork.Save();
                        }
                        if (imageFiles != null)
                        {
                            Int64 _propertyId = property.ID;
                            foreach (var file in imageFiles)
                            {
                                IFormFile currentFile = file;
                                PropertyImage _propertyImage = new PropertyImage();
                                string _imagePath = _utility.SaveFile(currentFile, @"assets/PropertyImages");
                                _propertyImage.ImagePath = _imagePath;
                                _propertyImage.PropertyID = _propertyId;
                                _propertyImage.Description = "Image of property with Id:" + Convert.ToString(_propertyId);
                                _unitofWork.PropertyImages.Add(_propertyImage);
                                // _unitofWork.Save();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LogErrorDetail(ex, "Property/UpdatePropertyImages");
                throw;
            }
        }
        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PropertyPics(PropertyVM propertyVM, IFormFile filecoverImage, List<IFormFile> imageFiles)
        {
            try
            {
                Int64 PID = propertyVM.Property.ID;
                ModelState.Clear();
                if (PID != null && PID != 0)
                {
                    if (filecoverImage != null || imageFiles != null)
                    {
                        Property property = _unitofWork.Property.Get(x => x.ID == PID);
                        if (filecoverImage != null)
                        {
                            _utility.DeletFile(property.CoverImage);
                            string _coverImagePath = _utility.SaveFile(filecoverImage, @"assets/PropertyImages");
                            property.CoverImage = _coverImagePath;
                            _unitofWork.Property.update(property);
                            _unitofWork.Save();
                            TempData["success"] = "New cover image for property added successfully!";
                        }
                        if (imageFiles != null)
                        {
                            Int64 _propertyId = property.ID;
                            foreach (var file in imageFiles)
                            {
                                IFormFile currentFile = file;
                                PropertyImage _propertyImage = new PropertyImage();
                                string _imagePath = _utility.SaveFile(currentFile, @"assets/PropertyImages");
                                _propertyImage.ImagePath = _imagePath;
                                _propertyImage.PropertyID = _propertyId;
                                _propertyImage.Description = "Image of property with Id:" + Convert.ToString(_propertyId);
                                _unitofWork.PropertyImages.Add(_propertyImage);
                                _unitofWork.Save();
                            }
                            if (TempData["success"] != null)
                            {
                                TempData["success"] = "New cover and images for property has been added successfully !";
                            }
                            else
                            {
                                TempData["success"] = "New images for property has been added successfully !";
                            }
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
        }
    }
}
