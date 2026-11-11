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
    [Authorize(Roles = WebSiteRoles.Role_FreeUser)]
    public class MessageController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        private EmailSender _emailSender;

        public MessageController(IUnitofWork unitofWork, Utility utility, EmailSender emailSender)
        {
            _unitofWork = unitofWork;
            _utility = utility;
            _emailSender = emailSender;
        }
        [HttpPost]
        public JsonResult sendinspectionrequest(EnquiryVM enquiryVM)
        {
            try
            {
                if (enquiryVM.Enquiry != null)
                {
                    var enquiry = enquiryVM.Enquiry;
                    enquiry.Enquirytype = Enquiry.EnquriyType.aboutinspection;
                    enquiry.UId = Guid.NewGuid().ToString();
                    _unitofWork.Enquiry.Add(enquiry);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "message/sendinspectionrequest");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult("success");
        }
        [HttpPost]
        public JsonResult sendReply(Int32 enqType, Int64 propertyId, string recepient,
            string sender, Int64? roomId, string reply, Int64 parentEnquiryId)
        {
            try
            {
                if (enqType != null & recepient != null & sender != null)
                {
                    Enquiry.EnquriyType enquiryType;
                    //In reply to an inspection enquiry,set enqyType
                    //to 'aboutproperty', so that in case of further messages,inspection schedule 
                    //is not created and shared on tenant log in. 
                    if (enqType == 3)
                    {
                        enquiryType = Enquiry.EnquriyType.aboutproperty;
                    }
                    else
                    {
                        enquiryType = (Enquiry.EnquriyType)enqType;
                    }
                    Enquiry enquiry = new Enquiry
                    {
                        Enquirytype = enquiryType,
                        ParentEnquiryId = parentEnquiryId,
                        PropertyID = propertyId,
                        RoomId = roomId,
                        FromUserID = sender,
                        ToUserID = recepient,
                        Message = reply,
                        Enquirystatus = Enquiry.EnquiryStatus.unread,
                        CreatedOn = DateTime.UtcNow,
                        UId = Guid.NewGuid().ToString()
                    };
                    if (propertyId == 0)
                        enquiry.PropertyID = null;
                    if (roomId == 0)
                        enquiry.RoomId = null;

                    _unitofWork.Enquiry.Add(enquiry);
                    var parentEnquiry = _unitofWork.Enquiry.Get(e => e.ID == parentEnquiryId);
                    parentEnquiry.isReplied = true;
                    _unitofWork.Save();
                    sendReceiverEmailNotification(recepient, reply);
                    updateSenderResponserate(sender);
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "message/sendReply");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult("success");
        }

        private void updateSenderResponserate(string senderId)
        {
            try
            {
                var sender = _unitofWork.User.Get(u => u.Id == senderId);
                var enquiries = _unitofWork.Enquiry.GetAll(e => e.ToUserID == sender.Id
                && e.ParentEnquiryId == null).ToList();
                var respondedEnq = enquiries.Where(e => e.isReplied == true).ToList();
                if (enquiries != null && respondedEnq != null)
                {
                    var responseRate = (respondedEnq.Count() * 100) / enquiries.Count();
                    sender.Responserate = responseRate;
                    _unitofWork.User.update(sender);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                //return RedirectToAction("Error", "Error");
            }
        }

        private void sendReceiverEmailNotification(string recepientId, string message)
        {
            try
            {
                var recepient = _unitofWork.User.Get(u => u.Id == recepientId,
                    includeProperties: "notificationSettings");

                if (recepient.notificationSettings.Any())
                {
                    var messageSetting = recepient.notificationSettings.Single(x => x.NotificationTypeId == 1);
                    if (messageSetting.Status == UserNotification.NotificationStatus.Email)
                    {
                        _emailSender.SendEmailAsync(recepient.Email, "You received a message in sharehouse.pk", message);
                    }
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                //return RedirectToAction("Error", "Error");
            }
        }

        [HttpGet]
        public IActionResult getPropertyInspections(Int64 propertyId, string recepient,
            string sender, string propAddress)
        {
            List<PropertyInspection> propertyInspections = null;
            string tableInspections = "";
            if (propAddress != "")
                propAddress = "for property at " + propAddress;
            try
            {
                propertyInspections = _unitofWork.PropertyInspection.
                    GetAll(pi => pi.PropertyID == propertyId && pi.isActive == true).ToList();
                if (propertyInspections.Any())
                {
                    tableInspections = $@"Dear {recepient},<br>Thank you for your interest. 
Below is the inspection schedule {propAddress}.<br>
<table class=""table table-responsive-md"">
    <thead>
        <tr>
            <th style=""width: 50px;""><strong>No</strong></th>
            <th style=""width: 100px;""><strong>             
                Inspection Date
            </strong></th>
            <th style=""width: 50px;""><strong>             
                From
            </strong></th>
            <th style=""width: 50px;""><strong>To</strong></th>
        </tr>
    </thead>
    <tbody>";
                    int count = 1;
                    foreach (var inspection in propertyInspections)
                    {
                        tableInspections += "<tr><td>" + count + "</td>";
                        tableInspections += "<td>" + Utility.ConvertToShortDateFormat(inspection.InspectionDate) + "</td>";
                        tableInspections += "<td>" + inspection.From + "</td>";
                        tableInspections += "<td>" + inspection.To + "</td></tr>";
                        count += 1;
                    }
                    tableInspections += "</tbody></table><br>";
                    tableInspections += "Please feel free to visit.<br>";
                    tableInspections += "Thank You,<br>";
                    tableInspections += sender;
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "message /getPropertyInspections");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult(tableInspections);
        }
        [HttpGet]
        public IActionResult openRequestInspectionPopup(Int64 propertyid)
        {
            EnquiryVM enquiryVM = null;
            try
            {
                enquiryVM = new EnquiryVM
                {
                    Property = _unitofWork.Property.Get(p => p.ID == propertyid,
                    includeProperties: "Owner"),
                    Enquiry = new Enquiry()
                };
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View("requestpropertyinspection", enquiryVM);
        }
        [HttpGet]
        public IActionResult openRoomEnquiryPopup(Int64 roomid, Int64 propertyid)
        {
            EnquiryVM enquiryVM = null;
            try
            {
                enquiryVM = new EnquiryVM
                {
                    Room = _unitofWork.Room.Get(r => r.ID == roomid),
                    Property = _unitofWork.Property.Get(p => p.ID == propertyid, includeProperties: "Owner"),
                    Enquiry = new Enquiry()
                };
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View("newenquiry", enquiryVM);
        }
        [HttpPost]
        public JsonResult sendenquiryaboutroom(EnquiryVM enquiryVM)
        {
            try
            {
                if (enquiryVM.Enquiry != null)
                {
                    var enquiry = enquiryVM.Enquiry;
                    enquiry.UId = Guid.NewGuid().ToString();
                    _unitofWork.Enquiry.Add(enquiry);
                    _unitofWork.Save();
                    var recepientUser = _unitofWork.User.Get(u => u.Id == enquiry.ToUserID,
                    includeProperties: "notificationSettings");
                    sendReceiverEmailNotification(enquiry.ToUserID, enquiry.Message);
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "message/sendenquiryaboutroom");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult("success");
        }
        [HttpGet]
        public IActionResult loadenquiry(Int64 enquiryid)
        {
            var enquiry = _unitofWork.Enquiry.Get(e => e.ID == enquiryid,
                includeProperties: "FromUser,ToUser,Property");
            var loggedinUserId = HttpContext.Session.GetString("_UserId");
            try
            {
                if (enquiry != null)
                {
                    if (enquiry.Enquirystatus != Enquiry.EnquiryStatus.read &&
                        enquiry.FromUserID != loggedinUserId)
                    {
                        enquiry.Enquirystatus = Enquiry.EnquiryStatus.read;
                        _unitofWork.Enquiry.update(enquiry);
                        _unitofWork.Save();
                    }
                }
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View("enquiry", enquiry);
        }
        [HttpGet]
        public IActionResult inbox(string type, int pg = 1)
        {
            List<Enquiry> enquiries;
            var loggedinUser = HttpContext.Session.GetString("_UserId");
            try
            {
                if (type == "to")
                    enquiries = _unitofWork.Enquiry.GetAll(e => e.ToUserID == loggedinUser,
                        includeProperties: "FromUser")
                        .OrderByDescending(e => e.DateReceived).ToList();
                else
                    enquiries = _unitofWork.Enquiry.GetAll(e => e.FromUserID == loggedinUser,
                        includeProperties: "FromUser")
                        .OrderByDescending(e => e.DateReceived).ToList();

                #region pagination
                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                this.ViewBag.pg = pg;
                int recsCount = enquiries.Count();
                int recSkip = (pg - 1) * pageSize;
                enquiries = enquiries.Skip(recSkip).Take(pageSize).ToList();
                var pager = new Pager(recsCount, pg, "message", "inbox", pageSize, "", type,
                   enquiries.Count());
                this.ViewBag.Pager = pager;
                #endregion

            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(enquiries);
        }
        [HttpPost]
        public JsonResult sendmessagetoperson(PersonListingVM personListingVM)
        {
            try
            {
                if (personListingVM.Enquiry != null)
                {
                    var enquiry = personListingVM.Enquiry;
                    enquiry.UId = Guid.NewGuid().ToString();
                    _unitofWork.Enquiry.Add(enquiry);
                    _unitofWork.Save();
                    var recepientUser = _unitofWork.User.Get(u => u.Id == enquiry.ToUserID,
                    includeProperties: "notificationSettings");
                    sendReceiverEmailNotification(enquiry.ToUserID, enquiry.Message);
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "message/sendmessagetoperson");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult("success");
        }
        [HttpPost]
        public JsonResult sendmessage(PropertyListingVM propertyListingVM)
        {
            try
            {
                if (propertyListingVM.Enquiry != null)
                {
                    var enquiry = propertyListingVM.Enquiry;
                    enquiry.UId = Guid.NewGuid().ToString();
                    _unitofWork.Enquiry.Add(enquiry);
                    _unitofWork.Save();
                    var recepientUser = _unitofWork.User.Get(u => u.Id == enquiry.ToUserID,
                    includeProperties: "notificationSettings");
                    sendReceiverEmailNotification(enquiry.ToUserID, enquiry.Message);
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "message/sendmessage");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult("success");
        }
        [HttpGet]
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
                
            }
        }
    }
}
