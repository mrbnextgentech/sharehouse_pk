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

namespace Gharshare.com.pk.Controllers  //ShareHouse.pk.Controllers 
{
    [Authorize(Roles = WebSiteRoles.Role_FreeUser + "," + WebSiteRoles.Role_Admin)]
    public class ProfileController : Controller
    {
        private IUnitofWork _unitofWork;
        private Utility _utility;
        private readonly TwilioService _twilioService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ProfileController(IUnitofWork unitofWork, Utility utility, TwilioService twilioService,
            SignInManager<ApplicationUser> signInManager)
        {
            _unitofWork = unitofWork;
            _utility = utility;
            _twilioService = twilioService;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult userpayments(int pg = 1)
        {
           var userId=HttpContext.Session.GetString("_UserId");
            var payments = _unitofWork.UserPayment.GetAll(u=>u.UserId== userId,
                includeProperties: "User,Plan").ToList();
            #region Pagination
            int pageSize = Pager.GetPageSize();
            if (pg < 1)
            {
                pg = 1;
            }
            int recsCount = payments.Count();           
            int recSkip = (pg - 1) * pageSize;
            var data = payments.Skip(recSkip).Take(pageSize).ToList();
            var pager = new Pager(recsCount, pg, "profile", "userpayments", pageSize,"","",
                data.Count());
            ViewBag.Pager = pager;
            #endregion end pagination
            return View("userpayments", data);
        }
        [HttpPost]
        public JsonResult saveplansubscription(UserPlanVm userPlanVm)
        {
            try
            {
                if (userPlanVm.Plan != null)
                {
                    var plan = userPlanVm.Plan;

                    #region update user type based on selected plan
                    var user = _unitofWork.User.Get(u => u.Id == userPlanVm.User.Id);
                    if (plan.PlanType == Plan.Plantype.forlandlord)
                    {
                        user.UserType = ApplicationUser.usertype.landlord;
                        HttpContext.Session.SetString("_UserType", 
                            ApplicationUser.usertype.landlord.ToString());
                    }
                    else if (plan.PlanType == Plan.Plantype.fortenant)
                    {
                        user.UserType = ApplicationUser.usertype.tenant;
                        HttpContext.Session.SetString("_UserType",
                            ApplicationUser.usertype.tenant.ToString());
                    }
                    _unitofWork.User.update(user);
                    #endregion

                    #region create user payment record
                    UserPayment userPayment = new UserPayment
                    {
                        UserId = user.Id,
                        PlanId = plan.ID,
                        Amount = plan.Price,
                        Paymentmode = "online",
                        Paidon = DateTime.UtcNow,
                        CreatedbyUserID =user.Id,
                        DateCreated=DateTime.UtcNow,
                        UId= Guid.NewGuid().ToString()
                };
                    _unitofWork.UserPayment.Add(userPayment);
                    #endregion

                    #region create user plan
                    UserPlan userPlan = new UserPlan
                    {
                        UserId = user.Id,
                        PlanId = plan.ID,
                        From = DateTime.UtcNow,
                        To = DateTime.UtcNow.AddDays((int)plan.Days),
                        Status = UserPlan.status.Active,
                        DateCreated = DateTime.UtcNow,
                        CreatedbyUserID = user.Id,
                        UId= Guid.NewGuid().ToString()
                };
                    _unitofWork.UserPlan.Add(userPlan);
                    #endregion
                    //save all
                    _unitofWork.Save();
                    return new JsonResult("success");
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "profile/saveplansubscription");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Json(new { error = "failed" });
        }
        [HttpGet]
        public IActionResult subscribeplan(Int64 planId)
        {
            UserPlanVm userPlanvm = null;
            try
            {
                userPlanvm = new UserPlanVm
                {
                    Plan = _unitofWork.Plan.Get(p => p.ID == planId),
                    User = _unitofWork.User.Get(u => u.Id == HttpContext.Session.GetString("_UserId"))
                };
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                return RedirectToAction("Error", "Error");
            }
            return View("subscribeplan", userPlanvm);
        }
        [HttpGet]
        public IActionResult plans()
        {
            PlanserviceVM planService = new PlanserviceVM
            {
                Landlordplans = _unitofWork.Plan.GetAll(p => p.PlanType == Plan.Plantype.forlandlord
                && p.PlanStatus == Plan.Status.active || 
                (p.PlanType == Plan.Plantype.forboth && p.PlanStatus == Plan.Status.active), 
                includeProperties: "Services").OrderBy(p=>p.ID).ToList(),
                
                Landlordplanservices = _unitofWork.PlanService.GetAll(p => p.Plan.PlanStatus == Plan.Status.active
                && p.Plan.PlanType == Plan.Plantype.forlandlord ||
                (p.Plan.PlanType == Plan.Plantype.forboth && p.Plan.PlanStatus == Plan.Status.active)
                , includeProperties: "Plan").OrderBy(p => p.PlanID).ToList(),

                Tenantplans = _unitofWork.Plan.GetAll(p => p.PlanType == Plan.Plantype.fortenant
                && p.PlanStatus == Plan.Status.active || p.PlanType == Plan.Plantype.forboth,
                includeProperties: "Services").OrderBy(p => p.ID).ToList(),

                Tenantplanservices = _unitofWork.PlanService.GetAll(p => p.Plan.PlanStatus == Plan.Status.active
                && p.Plan.PlanType == Plan.Plantype.fortenant ||
                (p.Plan.PlanType == Plan.Plantype.forboth && p.Plan.PlanStatus == Plan.Status.active), 
                includeProperties: "Plan").OrderBy(p => p.PlanID).ToList(),

                allservices = _unitofWork.Service.GetAll(s => s.status == Service.Status.active).ToList(),

                userPlans = _unitofWork.UserPlan.GetAll(p => p.UserId == HttpContext.Session.GetString("_UserId")
                && p.Status == UserPlan.status.Active).ToList(),
                userId = HttpContext.Session.GetString("_UserId")
            };
            return View(planService);
        }

        [HttpGet]
        public bool checkmobilenumberstatus(string userId, string userMobilenum)
        {
            try
            {
                ApplicationUser user;
                if (!string.IsNullOrEmpty(userId))
                {
                    user = _unitofWork.User.Get(u => u.Id == userId.Trim());
                    if (user != null)
                    {
                        if (string.IsNullOrEmpty(user.Mobile))
                        {
                            user.Mobile = userMobilenum;
                            _unitofWork.User.update(user);
                            _unitofWork.Save();
                        }
                        if (user.Mobileverified)
                            return true;
                    }
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "profile/checkmobilenumberstatus");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return true;
            }
            return false;
        }

        [HttpGet]
        public IActionResult index()
        {
            return View("hello world");
        }
        [HttpGet]
        public IActionResult verifymobilenumber(string mobilenumber)
        {
            return View("verifymobileno", mobilenumber);
        }
        [HttpPost]
        public JsonResult verifymobilenumber(string userId, string otp)
        {
            ApplicationUser user = _unitofWork.User.Get(u => u.Id == userId);
            try
            {
                //if (user.OTP == Convert.ToInt32(otp.Trim()))
                //{
                    user.Mobileverified = true;
                    _unitofWork.User.update(user);
                    _unitofWork.Save();
                    return new JsonResult("success");
                //}
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "profile/verifymobilenumber(post)");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            //return new JsonResult("failed");
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return Json(new { error = "failed" });
        }
        [HttpGet]
        public IActionResult sendOTPonMobile()
        {
            return View("sendotponmobile");
        }
        private string GenerateOtp()
        {
            // generate a random OTP
            return new Random().Next(1000, 9999).ToString(); //+15075333943
        }
        [HttpPost]
        public JsonResult sendOTPonMobile(string userId, string mobilenumber)
        {
            ApplicationUser user = _unitofWork.User.Get(u => u.Id == userId);
            try
            {
                if (user.Mobile == "")
                {
                    user.Mobile = mobilenumber;
                }

                string otp = GenerateOtp();
                string message = $"Your OTP for mobile varification at sharehouse.pk is {otp}.";

                //_twilioService.SendSmsAsync("+923435289966", message); //for trial account at Twilio
                //_twilioService.SendSmsAsync(mobilenumber, message);//real scenario using Twilio
                _utility.SendOtpbySendPKGateway("923435289966", message,"sharehouse.pk");// using sendpk API
                user.OTP = Convert.ToInt32(otp);
                _unitofWork.User.update(user);
                _unitofWork.Save();
                return new JsonResult("success");
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "profile/accountsettings");
                return new JsonResult("failed");
            }
            // return new JsonResult("success");

        }
        [HttpGet]
        public IActionResult test()
        {
            return View();
        }
        [HttpGet]
        public IActionResult accountsettings(string userId)
        {
            ApplicationUser user = _unitofWork.User.Get(u => u.Id == userId);
            return View("accountsettings", user);
            //return new JsonResult(user);
        }
        [HttpPost]
        public IActionResult deleteaccount(string userId)
        {
            try
            {
                //Product owner asked for soft delete i.e.only deactivate user

                // First delete errors from error log related to user being deleted
                //var errlog = _unitofWork.ErrorLog.GetAll(e => e.LoggedInUserId == userId).ToList();
                //_unitofWork.ErrorLog.DeleteRange(errlog);

                var user = _unitofWork.User.Get(u => u.Id == userId);
                // _unitofWork.User.Delete(user); PO asked for soft delete i.e. only deactivate user
                user.IsActive = false;
                user.EmailConfirmed = false;
                _unitofWork.Save();

                // deactivate property listings of the user
                var propertyListings = _unitofWork.Property.GetAll(p => p.OwnerId == userId).ToList();
                if (propertyListings.Any())
                {
                    propertyListings.ForEach(p =>
                    {
                        p.IsActive = false;

                    });
                    _unitofWork.Property.UpdateRange(propertyListings);
                }

                // deactivate property listings of the user
                var personListings = _unitofWork.PersonListing.GetAll(p => p.UserId == userId).ToList();
                if (personListings.Any())
                {
                    personListings.ForEach(p =>
                    {
                        p.isActive = false;

                    });
                    _unitofWork.PersonListing.UpdateRange(personListings);
                }
                //code to deactivate alert settings will go here
                var userNotificatons = _unitofWork.UserNotification.GetAll(u => u.UserId == userId).ToList();
                userNotificatons.ForEach(un =>
                {

                    un.Status = UserNotification.NotificationStatus.deactivate;
                });
                // save all changes
                _unitofWork.Save();

                // Sign out the user and destroy the current session
                _signInManager.SignOutAsync();

                return new JsonResult("success");
            }
            catch (Exception exp)
            {
                TempData["ErrorMessage"] = exp.InnerException?.Message;
                TempData["StackTrace"] = exp.StackTrace;
                TempData["Source"] = exp.Source;
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "internal server occur deleting your account. " });
            }
        }
        [HttpPost]
        public IActionResult deActivateaccount(string userId)
        {
            try
            {
                List<Property> propertylistings = _unitofWork.Property.GetAll(p => p.OwnerId == userId)
                    .ToList();
                List<PersonListing> personlistings = _unitofWork.PersonListing.GetAll(p => p.UserId == userId)
                    .ToList();
                propertylistings.ForEach(p =>
                {
                    p.IsActive = false;
                });
                personlistings.ForEach(p =>
                {
                    p.isActive = false;
                });
                //code to deactivate alert settings will go here
                var userNotificatons = _unitofWork.UserNotification.GetAll(u => u.UserId == userId).ToList();
                userNotificatons.ForEach(un =>
                {

                    un.Status = UserNotification.NotificationStatus.deactivate;
                });

                //save all changes
                _unitofWork.Save();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.InnerException?.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
            return View("deactivateaccount");
        }
        [HttpGet]
        public IActionResult deActivateaccount()
        {
            return View("deactivateaccount");
        }
        [HttpPost]
        public JsonResult accountsettings(string userId, string name, string email,
            string mobile, bool showNumber, bool freetoMessage)
        {
            ApplicationUser user = _unitofWork.User.Get(u => u.Id == userId);
            try
            {
                user.Name = name;
                user.Email = email;
                user.Mobile = mobile;
                user.ShowNumber = showNumber;
                user.FreetoMessage = freetoMessage;
                _unitofWork.User.update(user);
                _unitofWork.Save();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "profile/accountsettings");
            }

            return new JsonResult(user);
        }
        [HttpPost]
        [Produces("application/json")]
        public JsonResult uploadProfileImage(string userId, IFormFile image)
        {
            ApplicationUser user = _unitofWork.User.Get(u => u.Id == userId);
            try
            {
                if (userId != null && image != null)
                {
                    _utility.DeletFile(user.ProfilePicture);
                    if(image.Length<= 100000)
                    user.ProfilePicture = _utility.SaveFile(image, @"assets/img/profile");
                    else
                        user.ProfilePicture = _utility.ResizeAndSaveImage(image, "assets/img/profile", 100000, 600,800);
                    _unitofWork.User.update(user);
                    _unitofWork.Save();
                }
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "profile/uploadProfileImage");
            }
            return new JsonResult(user);
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
                    UId= Guid.NewGuid().ToString()
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
