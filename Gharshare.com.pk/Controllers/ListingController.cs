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
using System.Globalization;

namespace ShareHouse.pk.Controllers
{
    [Authorize(Roles = WebSiteRoles.Role_FreeUser)]
    public class ListingController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        public ListingController(IUnitofWork unitofWork, Utility utility)
        {
            _unitofWork = unitofWork;
            _utility = utility;
        }
        [HttpPost]
        public JsonResult savelistingimagesneworder(PropertyVM propertyVM)
        {
            Property property = _unitofWork.Property.Get(p => p.ID == propertyVM.Property.ID);
            try
            {
                if (property.ID != null)
                {
                    if (propertyVM.PropertyImages.Any())
                    {
                        propertyVM.PropertyImages.ForEach(img =>
                        {
                            img.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                            img.UpdatedOn = DateTime.UtcNow;
                            if (img.order == 1)
                            {
                                img.isCover = true;
                                property.CoverImage = img.ImagePath;
                                property.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                                property.UpdatedOn = DateTime.UtcNow;
                            }
                            else
                            {
                                img.isCover = false;
                            }
                        });
                        _unitofWork.PropertyImages.UpdateRange(propertyVM.PropertyImages);
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/savelistingimagesneworder");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }

            return (new JsonResult("success"));
        }
        [HttpGet]
        public IActionResult loadlistingPhotos(Int64 propertyid)
        {
            PropertyVM propertyVm = null;
            try
            {
                if (propertyid > 0)
                {
                    propertyVm = new PropertyVM
                    {
                        Property = _unitofWork.Property.Get(p => p.ID == propertyid,
                        includeProperties: "Images"),
                        PropertyImages = _unitofWork.PropertyImages.GetAll(i => i.PropertyID ==
                        propertyid).OrderBy(p => p.order).ToList()
                    };
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View("reorderlistingimages", propertyVm);
        }
        public JsonResult deletelistingimage(Int64 listingId, Int64 imageid)
        {
            List<PropertyImage> images = null;
            try
            {
                PropertyImage propertyImage = _unitofWork.PropertyImages.Get(x => x.ID == imageid
                && x.PropertyID == listingId);

                Int64 propertyImageID = imageid;
                Int64 propertyID = propertyImage.PropertyID;

                _utility.DeletFile(propertyImage.ImagePath);
                propertyImage.ImagePath = String.Empty;
                _unitofWork.PropertyImages.Delete(propertyImage);
                _unitofWork.Save();
                images = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == propertyID).ToList();
            }
            catch (Exception ex)
            {
                LogErrorDetail(ex, "listing/deletelistingimage");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpPost]
        public JsonResult deleteinspectionentry(Int64 inspectionId)
        {
            try
            {
                if (inspectionId != null && inspectionId > 0)
                {
                    var inspectionEntryDb = _unitofWork.PropertyInspection.Get(pi => pi.ID == inspectionId);
                    if (inspectionEntryDb != null)
                    {
                        _unitofWork.PropertyInspection.Delete(inspectionEntryDb);
                        _unitofWork.Save();
                    }
                    else
                    {
                        Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        return Json(new { error = "failed" });
                    }
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { error = "failed" });
                }

            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/deleteinspectionentry");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpPost]
        public JsonResult deleteInspectionSchedule(Int64 propertyId)
        {
            Property property = null;
            string propertyUid = null;
            try
            {
                if (propertyId != null && propertyId > 0)
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyId);
                    property.InspectionLength = null;
                    property.InspectionBreak = null;
                    property.InspectionTime = null;
                    property.NavigationInstructions = null;
                    _unitofWork.Property.update(property);

                    var inspectionInDb = _unitofWork.PropertyInspection.GetAll(i => i.PropertyID == propertyId).ToList();

                    if (inspectionInDb.Any())
                        _unitofWork.PropertyInspection.DeleteRange(inspectionInDb);

                    _unitofWork.Save();
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { error = "failed" });
                }


            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveinspections");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(propertyUid));
        }
        [HttpGet]
        public IActionResult editinspectionschedule(Int64 inspectionId)
        {
            PropertyInspection propertyInspection = null;
            try
            {
                propertyInspection = _unitofWork.PropertyInspection.Get(p => p.ID == inspectionId);
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/loadpropertyInspectioninpopup");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return View(propertyInspection);
        }
        [HttpGet]
        public IActionResult addinspection(Int64 propertyId)
        {

            PropertyInspection propertyInspection = new PropertyInspection
            {
                InspectionDate = DateTime.Now,
                PropertyID=propertyId
            };
            return View(propertyInspection);
        }
        [HttpPost]
        public JsonResult addinspection(PropertyInspection propertyInspection)
        {
            try
            {
                if (propertyInspection != null)
                {
                    propertyInspection.CreatedOn = DateTime.UtcNow;
                    propertyInspection.isActive = true;
                    propertyInspection.UId=Guid.NewGuid().ToString();
                    propertyInspection.CreatedbyUserId = HttpContext.Session.GetString("_UserId");
                    _unitofWork.PropertyInspection.Add(propertyInspection);
                    _unitofWork.Save();
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return Json(new { error = "failed" });
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/addinspection");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpGet]
        public IActionResult deleteinspection(Int64 inspectionId)
        {
            PropertyInspection propertyInspection = null;
            try
            {
                propertyInspection = _unitofWork.PropertyInspection.Get(p => p.ID == inspectionId);
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/deleteinspection");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return View(propertyInspection);
        }
        [HttpPost]
        public JsonResult updatepropertyinspection(PropertyInspection propertyInspection)
        {
            PropertyInspection propertyInspectioninDb = null;
            try
            {
                if (propertyInspection != null && propertyInspection.ID > 0)
                {
                    propertyInspectioninDb = _unitofWork.PropertyInspection.Get(p => p.ID == propertyInspection.ID);
                    propertyInspectioninDb.InspectionDate = propertyInspection.InspectionDate;
                    propertyInspectioninDb.From = propertyInspection.From;
                    propertyInspectioninDb.To = propertyInspection.To;
                    propertyInspectioninDb.UpdatedOn = DateTime.UtcNow;
                    propertyInspectioninDb.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                    _unitofWork.PropertyInspection.update(propertyInspectioninDb);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updatepropertyinspection");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpPost]
        public JsonResult saveinspections(PropertyListingVM propertyListingVM)
        {
            Property property = null;
            string propertyUid = null;
            try
            {
                if (propertyListingVM.Property != null && propertyListingVM.Property.ID > 0)
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);
                    property.InspectionLength = propertyListingVM.Property.InspectionLength;
                    property.InspectionBreak = propertyListingVM.Property.InspectionBreak;
                    _unitofWork.Property.update(property);
                    _unitofWork.Save();
                    propertyUid = property.UId;
                }
                var inspectionInDb = _unitofWork.PropertyInspection.GetAll(i => i.PropertyID ==
                propertyListingVM.Property.ID).ToList();
                if (inspectionInDb.Any())
                {
                    _unitofWork.PropertyInspection.DeleteRange(inspectionInDb);
                    _unitofWork.Save();
                }

                if (propertyListingVM.Inspections != null)
                {
                    if (propertyListingVM.Inspections.Any())
                    {
                        var inspections = propertyListingVM.Inspections;
                        inspections.ForEach(inspection =>
                        {
                            inspection.PropertyID = property.ID;
                            inspection.isActive = true;
                            inspection.CreatedOn = DateTime.UtcNow;
                            inspection.UpdatedOn = DateTime.UtcNow;
                            inspection.UId = Guid.NewGuid().ToString();
                            inspection.CreatedbyUserId = HttpContext.Session.GetString("_UserId");
                        });
                        _unitofWork.PropertyInspection.AddRange(inspections);
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveinspections");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(propertyUid));
        }
        [HttpPost]
        public JsonResult saveinspectionsettings(PropertyListingVM propertyListingVM)
        {
            Property property = null;
            try
            {
                if (propertyListingVM.Property != null && propertyListingVM.Property.ID > 0)
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);
                    property.InspectionTime = propertyListingVM.Property.InspectionTime;
                    property.NavigationInstructions = propertyListingVM.Property.NavigationInstructions;
                    _unitofWork.Property.update(property);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveinspectionsettings");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(property));
        }
        [HttpGet]
        public JsonResult deletevideotour(Int64 listingId)
        {
            Property property = null;
            try
            {
                property = _unitofWork.Property.Get(p => p.ID == listingId);
                property.VideoURL = null;
                _unitofWork.Property.update(property);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/deletevideotour");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(property));
        }
        private string GetYouTubeVideoId(string youtubeUrl)
        {
            try
            {
                // Parse the YouTube URL to extract the video ID
                var uri = new Uri(youtubeUrl);
                var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
                string videoId = queryParams["v"];
                return videoId;
            }
            catch (Exception ex)
            {
                // Handle any exceptions here (e.g., invalid URL)
                return null;
            }
        }
        [HttpPost]
        public JsonResult savepropertyvideotour(PropertyListingVM propertyListingVM)
        {
            Property property = null;
            try
            {
                if (propertyListingVM.Property != null && propertyListingVM.Property.ID > 0)
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);
                    property.VideoURL = GetYouTubeVideoId(propertyListingVM.Property.VideoURL);
                    _unitofWork.Property.update(property);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/savevideotourstep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(property));
        }
        [HttpGet]
        public IActionResult loadvideotour(Int64 listingId)
        {
            PropertyListingVM propertyListingVM = null;
            try
            {
                propertyListingVM = new PropertyListingVM
                {
                    Property = _unitofWork.Property.Get(p => p.ID == listingId,
                    includeProperties: "Images,Rooms,Owner,PropertyInspections"),
                };
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View("editpropertyvideotour", propertyListingVM);
        }
        [HttpGet]
        public IActionResult propertyinspection(Int64 listingId)
        {
            PropertyListingVM propertyListingVM = null;
            try
            {
                propertyListingVM = new PropertyListingVM
                {
                    Property = _unitofWork.Property.Get(p => p.ID == listingId,
                    includeProperties: "Images,Rooms,Owner,PropertyInspections"),
                    Inspections = new List<PropertyInspection>
                    {
                        new PropertyInspection
                        {
                            InspectionDate=DateTime.UtcNow
                        }

                    }
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
        public JsonResult removePersonfromshortlist(Int64 id)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            try
            {
                var shortListRecord = _unitofWork.Shortlist.Get(x => x.PersonlistingId == id &&
                x.UserId == userId);
                if (shortListRecord != null)
                {
                    _unitofWork.Shortlist.Delete(shortListRecord);
                    _unitofWork.Save();
                }
                else
                {
                    return (new JsonResult("alreadyremoved"));
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/removePersonfromshortlist");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        public JsonResult removePlacefromshortlist(Int64 id)
        {
            var userId = HttpContext.Session.GetString("_UserId");
            try
            {
                var shortListRecord = _unitofWork.Shortlist.Get(x => x.PropertyId == id &&
                x.UserId == userId);
                if (shortListRecord != null)
                {
                    _unitofWork.Shortlist.Delete(shortListRecord);
                    _unitofWork.Save();
                }
                else
                {
                    return (new JsonResult("alreadyremoved"));
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/removefromshortlist");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }

        [HttpGet]
        public IActionResult shortlist()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            ShortlistVM shortlistVM = null;
            try
            {
                var placeshortlist = _unitofWork.Shortlist.GetAll(x => x.ShortlistType == Shortlist.type.Propertylisting
                    && x.UserId == userId).ToList();
                var personshortlist = _unitofWork.Shortlist.GetAll(x => x.ShortlistType == Shortlist.type.Personlisting
                    && x.UserId == userId).ToList();

                shortlistVM = new ShortlistVM
                {
                    Placeshortlist = placeshortlist,
                    Personshortlist = personshortlist,
                    Placelistings = _unitofWork.Property.GetAll(p => p.IsActive == true &&
                    p.listingstatus == Property.Status.Active, includeProperties: "Owner,Rooms,Images")
                    .Where(property => placeshortlist.Any(shortlist => shortlist.PropertyId == property.ID))
                    .ToList(),
                    PersonListings = _unitofWork.PersonListing.GetAll(p => p.isActive == true &&
                    p.listingstatus == PersonListing.Status.Active,
                    includeProperties: "User").ToList().Where(p => personshortlist.Any(shortlist => shortlist.PersonlistingId == p.ID))
                    .ToList()
                };
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(shortlistVM);
        }
        [HttpGet]
        public JsonResult addPersontoshortlist(Int64 Id, string userId)
        {
            try
            {
                var shortListedItemDb = _unitofWork.Shortlist.Get(i => i.PersonlistingId == Id &&
                i.UserId == userId);
                if (Id != null && userId != null && shortListedItemDb == null)
                {
                    var shortListitem = new Shortlist
                    {
                        PersonlistingId = Id,
                        ShortlistType = Shortlist.type.Personlisting,
                        UserId = userId,
                        DateCreated = DateTime.UtcNow,
                        UId = Guid.NewGuid().ToString()
                    };
                    _unitofWork.Shortlist.Add(shortListitem);
                    _unitofWork.Save();
                }
                else
                {
                    return (new JsonResult("shortlisted"));
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/addPersontoshortlist");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpGet]
        public JsonResult addtoshortlist(Int64 Id, string userId)
        {
            try
            {
                var shortListedItemDb = _unitofWork.Shortlist.Get(i => i.PropertyId == Id &&
                i.UserId == userId);
                if (Id != null && userId != null && shortListedItemDb == null)
                {
                    var shortListitem = new Shortlist
                    {
                        PropertyId = Id,
                        ShortlistType = Shortlist.type.Propertylisting,
                        UserId = userId,
                        DateCreated = DateTime.UtcNow,
                        UId = Guid.NewGuid().ToString()
                    };
                    _unitofWork.Shortlist.Add(shortListitem);
                    _unitofWork.Save();
                }
                else
                {
                    return (new JsonResult("shortlisted"));
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/shortlist");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpPost]
        public JsonResult updateroomfeatures(RoomVM roomvm)
        {
            Room roomDb = null;
            RoomVM roomvmDb;
            try
            {
                if (roomvm.Room.ID > 0)
                {
                    roomDb = _unitofWork.Room.Get(p => p.ID == roomvm.Room.ID);
                    roomDb.Furnishing = roomvm.Room.Furnishing;
                    _unitofWork.Room.update(roomDb);
                    _unitofWork.Save();
                }
                if (roomvm.RoomFurnishings.Any())
                {
                    roomvm.RoomFurnishings.ForEach(r =>
                    {
                        r.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                        r.UpdatedOn = DateTime.UtcNow;
                    });
                    _unitofWork.RoomFurnishing.UpdateRange(roomvm.RoomFurnishings);
                    _unitofWork.Save();
                }
                roomvmDb = new RoomVM
                {
                    Room = _unitofWork.Room.Get(r => r.ID == roomvm.Room.ID,
                        includeProperties: "RoomFurnishings"),
                    RoomFurnishings = _unitofWork.RoomFurnishing.GetAll(rf => rf.RoomID == roomvm.Room.ID,
                        includeProperties: "Furnishing").ToList(),
                };
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updateroomfeatures");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(roomvmDb));
        }
        [HttpPost]
        public JsonResult updateroomavailability(RoomVM roomvm)
        {
            Room roomDb = null;
            try
            {
                if (roomvm.Room.ID > 0)
                {
                    roomDb = _unitofWork.Room.Get(p => p.ID == roomvm.Room.ID);
                    roomDb.AvailableFrom = roomvm.Room.AvailableFrom;
                    roomDb.MinStay = roomvm.Room.MinStay;
                    roomDb.MaxStay = roomvm.Room.MaxStay;
                    _unitofWork.Room.update(roomDb);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updateroomrent");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(roomDb));
        }
        [HttpPost]
        public JsonResult updateroomrent(RoomVM roomvm)
        {
            Room roomDb = null;
            try
            {
                if (roomvm.Room.ID > 0)
                {
                    roomDb = _unitofWork.Room.Get(p => p.ID == roomvm.Room.ID);
                    roomDb.Rent = roomvm.Room.Rent;
                    roomDb.BondType = roomvm.Room.BondType;
                    roomDb.Billstatus = roomvm.Room.Billstatus;
                    _unitofWork.Room.update(roomDb);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updateroomrent");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(roomDb));
        }
        [HttpGet]
        public IActionResult loadroominpopup(Int64 roomid)
        {
            RoomVM roomVM = null;
            try
            {
                if (roomid > 0)
                {
                    roomVM = new RoomVM
                    {
                        //Property = _unitofWork.Property.Get(p => p.ID == propertyid),
                        Room = _unitofWork.Room.Get(r => r.ID == roomid,
                        includeProperties: "RoomFurnishings"),
                        RoomFurnishings = _unitofWork.RoomFurnishing.GetAll(rf => rf.RoomID == roomid,
                        includeProperties: "Furnishing").ToList(),
                    };
                    return View("editroomdetails", roomVM);
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View("editroomdetails", roomVM);
        }

        [HttpPost]
        public JsonResult updatepropertyfurnishing(Property property)
        {
            Property propertyDb = null;
            try
            {
                if (property.ID > 0)
                {
                    propertyDb = _unitofWork.Property.Get(p => p.ID == property.ID);
                    propertyDb.FurnishingType = property.FurnishingType;
                    propertyDb.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                    propertyDb.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.Property.update(propertyDb);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updatepropertyfurnishing");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(propertyDb));
        }
        [HttpPost]
        public JsonResult updatepropertyavailability(Property property)
        {
            Property propertyDb = null;
            try
            {
                if (property.ID > 0)
                {
                    propertyDb = _unitofWork.Property.Get(p => p.ID == property.ID);
                    propertyDb.AvailableFrom = property.AvailableFrom;
                    propertyDb.MinStay = property.MinStay;
                    propertyDb.MaxStay = property.MaxStay;
                    propertyDb.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                    propertyDb.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.Property.update(propertyDb);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updatepropertyavailability");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(propertyDb));
        }
        [HttpPost]
        public JsonResult updatepropertyrent(Property property)
        {
            Property propertyDb = null;
            try
            {
                if (property.ID > 0)
                {
                    propertyDb = _unitofWork.Property.Get(p => p.ID == property.ID);
                    propertyDb.Rent = property.Rent;
                    propertyDb.InternetStatus = property.InternetStatus;
                    propertyDb.Bond = property.Bond;
                    propertyDb.Bill = property.Bill;
                    propertyDb.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                    propertyDb.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.Property.update(propertyDb);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updatepropertyrent");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(propertyDb));
        }
        [HttpPost]
        public JsonResult updatepropertyacceptings(PropertyListingVM propertyListingVM)
        {
            Property property = propertyListingVM.Property;
            try
            {
                if (property.ID != null)
                {
                    if (propertyListingVM.PropertyAcceptings.Any())
                    {
                        propertyListingVM.PropertyAcceptings.ForEach(x =>
                        {
                            x.UpdatedbyUserID = HttpContext.Session.GetString("_UserId");
                            x.UpdatedOn = DateTime.UtcNow;
                        });
                        _unitofWork.PropertyAccepting.UpdateRange(propertyListingVM.PropertyAcceptings);
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updatepropertyacceptings");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            var propertyAcceptingsDb = _unitofWork.PropertyAccepting.GetAll(pa => pa.isSelected == true
            && pa.PropertyID == property.ID, includeProperties: "Accepting").ToList();
            return (new JsonResult(propertyAcceptingsDb));
        }
        [HttpPost]
        public JsonResult updatepropertydescription(Property property)
        {
            Property propertyDb = null;
            try
            {
                if (property.ID > 0)
                {
                    propertyDb = _unitofWork.Property.Get(p => p.ID == property.ID);
                    propertyDb.Description = property.Description;
                    propertyDb.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                    propertyDb.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.Property.update(propertyDb);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updatepropertydescription");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(propertyDb));
        }
        [HttpPost]
        public JsonResult updateaboutflatmates(Property property)
        {
            Property propertyDb = null;
            try
            {
                if (property.ID > 0)
                {
                    propertyDb = _unitofWork.Property.Get(p => p.ID == property.ID);
                    propertyDb.AboutFlatmates = property.AboutFlatmates;
                    propertyDb.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                    propertyDb.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.Property.update(propertyDb);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updateaboutflatmates");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(propertyDb));
        }
        [HttpPost]
        public JsonResult updatepropertyaddress(Property property)
        {
            Property propertyDb = null;
            try
            {
                if (property.ID > 0)
                {
                    propertyDb = _unitofWork.Property.Get(p => p.ID == property.ID);
                    propertyDb.Address = _utility.RemoveCountryName(property.Address);
                    propertyDb.InternetStatus = property.InternetStatus;
                    propertyDb.Parkingtype = property.Parkingtype;
                    propertyDb.UpdatedbyUserId = HttpContext.Session.GetString("_UserId");
                    propertyDb.UpdatedOn = DateTime.UtcNow;
                    _unitofWork.Property.update(propertyDb);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/updatepropertyaddress");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(propertyDb));
        }
        [HttpGet]
        public IActionResult loadpropertyinpopup(Int64 propertyid, string viewname)
        {
            Property property = null;
            try
            {

                if (propertyid > 0 && viewname == "editpropertyacceptings")
                {
                    PropertyListingVM propertyListingVM = new PropertyListingVM
                    {
                        Property = _unitofWork.Property.Get(p => p.ID == propertyid),
                        PropertyAcceptings = _unitofWork.PropertyAccepting.GetAll(p => p.PropertyID == propertyid,
                        includeProperties: "Accepting").ToList(),
                    };
                    return View(viewname, propertyListingVM);
                }
                else if (propertyid > 0 && viewname == "editpropertydetails")
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyid, includeProperties: "Rooms");

                    return View(viewname, property);
                }                
                else if (propertyid > 0)
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyid);
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(viewname, property);
        }
        [HttpGet]
        public JsonResult activatelisting(Int64 placeListingId)
        {
            try
            {
                var propertyListing = _unitofWork.Property.Get(p => p.ID == placeListingId);
                propertyListing.IsActive = true;
                propertyListing.listingstatus = Property.Status.Active;
                //ApplicationUser.usertype userType;
                //Enum.TryParse(HttpContext.Session.GetString("_UserType"),
                //    out userType);
                //if (userType == ApplicationUser.usertype.admin ||
                //    userType == ApplicationUser.usertype.superadmin)
                //{
                //    propertyListing.listingstatus = Property.Status.Active;
                //}
                _unitofWork.Property.update(propertyListing);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/activatelisting");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpGet]
        public JsonResult deactivatelisting(Int64 listingId)
        {
            try
            {
                var propertyListing = _unitofWork.Property.Get(p => p.ID == listingId);
                propertyListing.IsActive = false;
                propertyListing.listingstatus = Property.Status.Inactive;
                ApplicationUser.usertype userType;
                Enum.TryParse(HttpContext.Session.GetString("_UserType"),
                    out userType);
                if (userType == ApplicationUser.usertype.admin ||
                    userType == ApplicationUser.usertype.superadmin)
                {
                    propertyListing.listingstatus = Property.Status.blocked;
                }

                _unitofWork.Property.update(propertyListing);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/deactivatelisting");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult("success"));
        }
        [HttpGet]
        public IActionResult listplacepreview(string propertyId, bool preview = false)
        {
            PropertyListingVM propertyListingVM = null;
            try
            {
                var property = _unitofWork.Property.Get(p => p.UId == propertyId,
                includeProperties: "Images,Rooms,Owner,PropertyInspections");
                var id = property.ID;

                propertyListingVM = new PropertyListingVM
                {
                    Property = property,
                    PropertyAcceptings = _unitofWork.PropertyAccepting.GetAll(pa => pa.PropertyID == id &&
                    pa.isSelected == true, includeProperties: "Accepting").ToList(),
                    PropertyFeatures = _unitofWork.PropertyFeature.GetAll(pa => pa.PropertyID == id &&
                    pa.isSelected == true, includeProperties: "Feature").ToList(),
                    Room = _unitofWork.Room.GetAll(r => r.PropertyId == id,
                    includeProperties: "RoomFurnishings").ToList(),
                    RoomFurnishingList = _unitofWork.RoomFurnishing.GetAll(rf => rf.PropertyID == id,
                    includeProperties: "Furnishing").OrderBy(rf => rf.RoomID).ToList(),
                    Preview = preview,
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
        [HttpPost]
        public JsonResult saveroomavailabilitystep(PropertyListingVM propertyListingVM)
        {
            var rooms = propertyListingVM.Room;
            List<Room> updatedRooms = new List<Room>();
            try
            {
                if (rooms != null)
                {
                    rooms.ForEach(r =>
                    {
                        var room = _unitofWork.Room.Get(x => x.ID == r.ID);
                        room.AvailableFrom = r.AvailableFrom;
                        room.MinStay = r.MinStay;
                        room.MaxStay = r.MaxStay;
                        updatedRooms.Add(room);
                    });
                    _unitofWork.Room.UpdateRange(updatedRooms);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveroomavailabilitystep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }

            return (new JsonResult("success"));
        }
        [HttpPost]
        public JsonResult saveroomrentstep(PropertyListingVM propertyListingVM)
        {
            var rooms = propertyListingVM.Room;
            Int64 propertyId = 0;
            List<Room> updatedRooms = new List<Room>();
            try
            {
                if (rooms != null)
                {
                    rooms.ForEach(r =>
                    {
                        var room = _unitofWork.Room.Get(x => x.ID == r.ID);
                        room.Rent = r.Rent;
                        room.BondType = r.BondType;
                        room.Billstatus = r.Billstatus;
                        updatedRooms.Add(room);
                    });
                    propertyId = updatedRooms[0].PropertyId;
                    _unitofWork.Room.UpdateRange(updatedRooms);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveroomrentstep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            var propertyAcceptings = _unitofWork.PropertyAccepting.GetAll(pa => pa.PropertyID == propertyId,
                includeProperties: "Accepting")
                .ToList();
            return (new JsonResult(propertyAcceptings));
        }
        [HttpPost]
        public JsonResult saveroomfeaturesstep(PropertyListingVM propertyListingVM)
        {
            var rooms = propertyListingVM.Room;
            List<Room> updatedRooms = new List<Room>();
            try
            {
                if (rooms != null)
                {
                    rooms.ForEach(r =>
                    {
                        var room = _unitofWork.Room.Get(x => x.ID == r.ID);
                        room.Name = r.Name;
                        room.RoomType = r.RoomType;
                        room.Furnishing = r.Furnishing;
                        room.BathroomType = r.BathroomType;
                        room.BedSize = r.BedSize;
                        room.PreferredFlatmate = r.PreferredFlatmate;
                        updatedRooms.Add(room);
                    });
                    _unitofWork.Room.UpdateRange(updatedRooms);
                    _unitofWork.Save();

                    var roomFurnishings = propertyListingVM.RoomFurnishings;
                    if (roomFurnishings != null && roomFurnishings.Any())
                    {
                        for (int i = 0; i < roomFurnishings.Count(); i++)
                        {
                            var currentroomFurnishingList = roomFurnishings[i];
                            _unitofWork.RoomFurnishing.UpdateRange(currentroomFurnishingList);
                        }
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveroomfeaturesstep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }

            return (new JsonResult("success"));
        }
        [HttpPost]
        public JsonResult saveflatmatepreferencestep(PropertyListingVM propertyListingVM)
        {
            var propertyDb = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);

            Property property = null;
            try
            {
                propertyDb.Flatmatepreference = propertyListingVM.Property.Flatmatepreference;
                propertyDb.WomenOnlyProperty = propertyListingVM.Property.WomenOnlyProperty;
                _unitofWork.Property.update(propertyDb);
                _unitofWork.Save();
                if (propertyListingVM.Property != null && propertyListingVM.Property.PropertyAcceptings != null)
                {
                    var propertyAccepting = propertyListingVM.Property.PropertyAcceptings;
                    propertyAccepting.ForEach(pa =>
                    {
                        pa.UpdatedOn = DateTime.UtcNow;
                        pa.UpdatedbyUserID = HttpContext.Session.GetString("_UserId") ?? "";
                    });
                    _unitofWork.PropertyAccepting.UpdateRange(propertyAccepting);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveflatmatepreferencestep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            property = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);
            return (new JsonResult(property));
        }
        [HttpPost]
        public JsonResult saveintrostep(PropertyListingVM propertyListingVM)
        {
            Property property = null;
            try
            {
                if (propertyListingVM.Property != null && propertyListingVM.Property.ID > 0)
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);
                    property.AboutFlatmates = propertyListingVM.Property.AboutFlatmates;
                    property.Description = propertyListingVM.Property.Description;
                    property.listingstatus = Property.Status.Pending;
                    _unitofWork.Property.update(property);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveintrostep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(property));
        }
        [HttpPost]
        public JsonResult savewholepropertyavailabilitystep(PropertyListingVM propertyListingVM)
        {
            Property property = null;
            try
            {
                if (propertyListingVM.Property != null && propertyListingVM.Property.ID > 0)
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);
                    property.AvailableFrom = propertyListingVM.Property.AvailableFrom;
                    property.MinStay = propertyListingVM.Property.MinStay;
                    property.MaxStay = propertyListingVM.Property.MaxStay;
                    _unitofWork.Property.update(property);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/savewholepropertyavailabilitystep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return (new JsonResult(property));
        }
        [HttpPost]
        public JsonResult saveAboutstep(PropertyListingVM propertyListingVM)
        {
            Property property = null;
            List<Room> rooms = new List<Room>();
            try
            {
                if (propertyListingVM.Property != null && propertyListingVM.Property.ID > 0)
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);
                    property.Address = _utility.RemoveCountryName(propertyListingVM.Property.Address);
                    property.CityID = propertyListingVM.Property.CityID;
                    property.Bed = propertyListingVM.Property.Bed;
                    property.Bathroom = propertyListingVM.Property.Bathroom;
                    property.Parkingtype = propertyListingVM.Property.Parkingtype;
                    property.FurnishingType = propertyListingVM.Property.FurnishingType;
                    property.RoomsForRent = propertyListingVM.Property.RoomsForRent;
                    if (property.Accommodationtype != Property.AccType.WholeProperty)
                        property.Totalflatmates = propertyListingVM.Property.Totalflatmates;
                    else
                        property.Totalflatmates = 0;
                    property.InternetStatus = propertyListingVM.Property.InternetStatus;
                    _unitofWork.Property.update(property);
                    _unitofWork.Save();
                }
                var propertyRoominDb = _unitofWork.Room.GetAll(r => r.PropertyId == propertyListingVM.Property.ID)
                    .ToList();
                _unitofWork.Room.DeleteRange(propertyRoominDb);
                _unitofWork.Save();

                if (property.Accommodationtype != Property.AccType.WholeProperty)
                {
                    for (int i = 0; i < property.RoomsForRent; i++)
                    {
                        var room = new Room { PropertyId = property.ID, Name = "Room-" + (i + 1) };
                        room.UId = Guid.NewGuid().ToString();
                        rooms.Add(room);
                    }
                    _unitofWork.Room.AddRange(rooms);
                    _unitofWork.Save();

                    var Furnishings = _unitofWork.Furnishing.GetAll().ToList();
                    rooms.ForEach(r =>
                    {
                        List<RoomFurnishing> roomFurnishings = new List<RoomFurnishing>();
                        Furnishings.ForEach(f =>
                        {
                            roomFurnishings.Add(new RoomFurnishing
                            {
                                RoomID = r.ID,
                                PropertyID = r.PropertyId,
                                isSelected = false,
                                FurnishingID = f.ID,
                                CreatedOn = DateTime.UtcNow,
                                CreatedbyUserId = HttpContext.Session.GetString("_UserId"),
                                Svgd = f.Svgd,
                                UId = Guid.NewGuid().ToString()
                            });
                        });
                        _unitofWork.RoomFurnishing.AddRange(roomFurnishings);

                    });
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveAboutstep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            if (property.Accommodationtype != Property.AccType.WholeProperty)
            {
                rooms = _unitofWork.Room.GetAll(r => r.PropertyId == property.ID).ToList();
                return (new JsonResult(rooms));
            }
            else
                return (new JsonResult(property));
        }
        [HttpPost]
        public JsonResult savewholepropertyrentstep(PropertyListingVM propertyListingVM)
        {
            Property property = null;
            try
            {
                if (propertyListingVM.Property != null && propertyListingVM.Property.ID > 0)
                {
                    property = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);
                    property.Rent = propertyListingVM.Property.Rent;
                    property.Bond = propertyListingVM.Property.Bond;
                    property.Bill = propertyListingVM.Property.Bill;
                    _unitofWork.Property.update(property);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/savewholepropertyrentstep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            var propertyAcceptings = _unitofWork.PropertyAccepting.GetAll(pa => pa.PropertyID == property.ID,
                includeProperties: "Accepting")
                .ToList();
            return (new JsonResult(propertyAcceptings));
        }
        [HttpPost]
        public JsonResult saveaccommodationtypestep(PropertyListingVM propertyListingVM)
        {
            var property = propertyListingVM.Property;
            try
            {
                if (property != null && property.ID <= 0)
                {
                    property.OwnerId = HttpContext.Session.GetString("_UserId");
                    property.UId = Guid.NewGuid().ToString();
                    _unitofWork.Property.Add(property);
                    _unitofWork.Save();
                    var acceptings = _unitofWork.Accepting.GetAll().ToList();
                    List<PropertyAccepting> propertyAccepting = new List<PropertyAccepting>();
                    acceptings.ForEach(a =>
                    {
                        propertyAccepting.Add(new PropertyAccepting
                        {
                            PropertyID = property.ID,
                            AcceptingID = a.ID,
                            isSelected = false,
                            CreatedOn = DateTime.UtcNow,
                            CreatedbyUserID = property.OwnerId,
                            UId = Guid.NewGuid().ToString()
                        });
                    });
                    _unitofWork.PropertyAccepting.AddRange(propertyAccepting);
                    _unitofWork.Save();
                    TempData["successtoaster"] = "Step 1 saved !";
                }
                else
                {
                    var propertyinDb = _unitofWork.Property.Get(p => p.ID == propertyListingVM.Property.ID);
                    propertyinDb.Accommodationtype = propertyListingVM.Property.Accommodationtype;
                    propertyinDb.Propertytype = propertyListingVM.Property.Propertytype;
                    _unitofWork.Property.update(propertyinDb);
                    _unitofWork.Save();
                    property = propertyinDb;
                }
                propertyListingVM.Property = property;
                propertyListingVM.CityList = getCities();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "listing/saveaccommodationtypestep");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            //return (new JsonResult(property));
            return (new JsonResult(propertyListingVM));
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
        [Produces("application/json")]
        public JsonResult addlistingnewphotos(Int64 propertyID, List<IFormFile> files)
        {

            List<PropertyImage> PropertyImages = null;
            try
            {
                Int64 PID = propertyID;
                Property property = _unitofWork.Property.Get(x => x.ID == PID);
                if (PID != 0 && files != null)
                {
                    Int64 _propertyId = property.ID;
                    var ImagesinDb = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == PID)
                        .ToList();
                    var totalImagesinDb = ImagesinDb.Count();
                    var imageOrder = totalImagesinDb + 1;

                    for (int i = 0; i < files.Count(); i++)
                    {

                        IFormFile currentFile = files[i];
                        var _propertyImage = new PropertyImage();
                        string _imagePath = _utility.SaveFile(currentFile, @"assets/PropertyImages");

                        _propertyImage.ImagePath = _imagePath;
                        _propertyImage.PropertyID = _propertyId;
                        _propertyImage.Description = "Listing Image for property with Id:" + Convert.ToString(_propertyId);
                        _propertyImage.CreatedbyUserId = HttpContext.Session.GetString("_UserId");
                        _propertyImage.CreatedOn = DateTime.UtcNow;
                        _propertyImage.order = imageOrder;
                        _propertyImage.UId = Guid.NewGuid().ToString();

                        if (totalImagesinDb == 0 && i == 0)
                        {
                            _propertyImage.isCover = true;
                            property.CoverImage = _imagePath;
                        }
                        _unitofWork.PropertyImages.Add(_propertyImage);
                        imageOrder += 1;
                    }
                    _unitofWork.Save();
                    PropertyImages = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == PID)
                       .OrderByDescending(x => x.isCover == true).ToList();
                }
                else
                {
                    throw new InvalidOperationException("Invalid operation occurred.");

                }
            }
            catch (Exception ex)
            {
                LogErrorDetail(ex, "listing/addlistingnewphotos");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult(PropertyImages);
        }
        [HttpPost]
        [Produces("application/json")]
        public JsonResult uploadPropertyImages(Int64 propertyID, List<IFormFile> files)
        {

            List<PropertyImage> PropertyImages = null;
            try
            {
                Int64 PID = propertyID;
                Property property = _unitofWork.Property.Get(x => x.ID == PID);
                if (PID != null && PID != 0 && files != null)
                {
                    /// Delete previous property images from file system
                    PropertyImage _propertyImage = null;
                    var savedPropertyimages = _unitofWork.PropertyImages.GetAll(p => p.PropertyID == PID)
                        .ToList();
                    savedPropertyimages.ForEach(p =>
                    _utility.DeletFile(p.ImagePath)
                    );
                    _unitofWork.PropertyImages.DeleteRange(savedPropertyimages);
                    ///// End delete previous property

                    Int64 _propertyId = property.ID;
                    for (int i = 0; i < files.Count(); i++)
                    {
                        IFormFile currentFile = files[i];
                        _propertyImage = new PropertyImage();
                        string _imagePath = string.Empty;
                        if (currentFile.Length <= 100000)
                            _imagePath = _utility.SaveFile(currentFile, @"assets/PropertyImages");
                        else
                            _imagePath = _utility.ResizeAndSaveImage(currentFile, "assets/PropertyImages", 100000, 2000, 1000);
                        _propertyImage.ImagePath = _imagePath;
                        _propertyImage.PropertyID = _propertyId;
                        _propertyImage.Description = "Image of property with Id:" + Convert.ToString(_propertyId);
                        if (i == 0)
                        {
                            //_utility.DeletFile(property.CoverImage);
                            property.CoverImage = _imagePath;
                            _unitofWork.Property.update(property);
                            _propertyImage.isCover = true;
                        }
                        _propertyImage.CreatedbyUserId = HttpContext.Session.GetString("_UserId");
                        _propertyImage.CreatedOn = DateTime.UtcNow;
                        _propertyImage.order = i + 1;
                        _propertyImage.UId = Guid.NewGuid().ToString();
                        _unitofWork.PropertyImages.Add(_propertyImage);
                    }
                    _unitofWork.Save();
                    PropertyImages = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == PID)
                       .OrderByDescending(x => x.isCover == true).ToList();
                }
                else
                {
                    throw new InvalidOperationException("Invalid operation occurred.");

                }
            }
            catch (Exception ex)
            {
                LogErrorDetail(ex, "listing/uploadPropertyImages");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult(PropertyImages);
        }
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
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult(images);
        }
        public IActionResult listplace()
        {
            PropertyListingVM propertyListingVM = null;
            try
            {
                //if (canUserCreateListing()) un-comment when subscription is enabled
                //{
                    propertyListingVM = new PropertyListingVM
                    {
                        Property = new Property(),
                        Room = new List<Room> { new Room { Name = "Room 1" } },
                        Furnishings = _unitofWork.Furnishing.GetAll().ToList(),
                        RoomFurnishings = new List<List<RoomFurnishing>>(),
                        Acceptings = _unitofWork.Accepting.GetAll().ToList(),
                        PropertyAcceptings = new List<PropertyAccepting>(),
                    };
                //}
                //else
                //{
                //    return RedirectToAction("plans", "profile");
                //}
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

        private bool canUserCreateListing()
        {
            bool canCreate = false;
            var userId = HttpContext.Session.GetString("_UserId");
            var userPlan = _unitofWork.UserPlan.Get(p => p.UserId == userId
            && p.Status == UserPlan.status.Active);
            var placelistings = _unitofWork.Property.GetAll(p => p.IsActive == true &&
            p.OwnerId == userId).ToList();

            if (userPlan != null)
            {
                var planServices = _unitofWork.PlanService.GetAll(ps => ps.PlanID == userPlan.PlanId).ToList();
                var livePropertylistings = planServices.SingleOrDefault(ps => ps.livepropertylisting != null);
                var allowedPropertylistings = livePropertylistings.livepropertylisting.HasValue
                    ? (Int16)livePropertylistings.livepropertylisting : (Int16)0;
                if (placelistings.Count() < allowedPropertylistings)
                {
                    canCreate = true;
                }
            }
            if (placelistings.Count() == 0)
            {
                canCreate = true;
            }
            return canCreate;
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