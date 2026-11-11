using Microsoft.AspNetCore.Mvc;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Microsoft.AspNetCore.Authorization;
using Gharshare.CommonHelper;

namespace Gharshare.com.pk.Controllers
{
    public class privacyController : Controller
    {
        private IUnitofWork _unitofWork;
        public privacyController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [HttpPost]
        public JsonResult updaeusernotificationsettings(List<UserNotification> userNotifications)
        {
            string userId = HttpContext.Session.GetString("_UserId");
            try
            {
                userNotifications.ForEach(notification =>
                {
                    notification.UpdatedOn = DateTime.UtcNow;
                    notification.UpdatedbyUserID = userId;
                }
                    );
                _unitofWork.UserNotification.UpdateRange(userNotifications);
                _unitofWork.Save();                 
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "profile/updaeusernotificationsettings");
            }
            var usernotifications = _unitofWork.UserNotification.GetAll(x => x.UserId == userId,
                includeProperties: "NotificationType").ToList();
            return new JsonResult(usernotifications);
        }
        [Authorize(Roles = WebSiteRoles.Role_FreeUser)]
        [HttpPost]
        public IActionResult notificationssettings(List<UserNotification>userNotifications)
        {
            string userId= HttpContext.Session.GetString("_UserId");
            try
            {
                userNotifications.ForEach(notification =>
                    {
                        notification.UpdatedOn = DateTime.UtcNow;
                        notification.UpdatedbyUserID = userId;
                    }
                    );
                _unitofWork.UserNotification.UpdateRange(userNotifications);
                _unitofWork.Save();
                TempData["success"] = "Notification settings updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
            var usernotifications = _unitofWork.UserNotification.GetAll(x => x.UserId == userId,
                includeProperties: "NotificationType").ToList();

            return View(usernotifications);
        }
            [Authorize(Roles = WebSiteRoles.Role_FreeUser)]

        [HttpGet]
        public IActionResult loadnotificationssettingsinpopup()
        {
            var userId = HttpContext.Session.GetString("_UserId");
            List<UserNotificationType> notificationTypes = _unitofWork.UserNotificationType.GetAll().ToList();
            var usernotifications = _unitofWork.UserNotification.GetAll(x => x.UserId == userId,
                includeProperties: "NotificationType").ToList();
            try
            {
                if (usernotifications.Count() == 0)
                {
                    notificationTypes.ForEach(n =>
                    {
                        usernotifications.Add(new UserNotification
                        {
                            UserId = userId,
                            NotificationTypeId = n.ID,
                            Status = n.ID == 2 ? UserNotification.NotificationStatus.Instant :
                            UserNotification.NotificationStatus.Email,
                            CreatedbyUserID = userId,
                            DateCreated = DateTime.UtcNow,
                            UId= Guid.NewGuid().ToString()
                    });
                    }
                    );
                    _unitofWork.UserNotification.AddRange(usernotifications);
                    _unitofWork.Save();
                    usernotifications = _unitofWork.UserNotification.GetAll(x => x.UserId == userId,
                    includeProperties: "NotificationType").ToList();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
               // return RedirectToAction("Error", "Error");
            }
            return View("notificationssettings", usernotifications);
        }
        [HttpGet]
        public IActionResult notificationssettings()
        {                         
            var userId = HttpContext.Session.GetString("_UserId");
            List<UserNotificationType> notificationTypes = _unitofWork.UserNotificationType.GetAll().ToList();
            var usernotifications = _unitofWork.UserNotification.GetAll(x => x.UserId == userId,
                includeProperties: "NotificationType").ToList();
            try
            {
                if (usernotifications.Count() == 0)
            {
                notificationTypes.ForEach(n =>
                    {
                        usernotifications.Add(new UserNotification
                        {
                            UserId = userId,
                            NotificationTypeId = n.ID,                            
                            Status= n.ID == 2? UserNotification.NotificationStatus.Instant:
                            UserNotification.NotificationStatus.Email,
                            CreatedbyUserID = userId,
                            DateCreated = DateTime.UtcNow,
                            UId= Guid.NewGuid().ToString()
                    });
                    }
                );
                _unitofWork.UserNotification.AddRange(usernotifications);
                _unitofWork.Save();
                usernotifications = _unitofWork.UserNotification.GetAll(x => x.UserId == userId,
                includeProperties: "NotificationType").ToList();
            }
            }
            catch(Exception ex)                           
            {                 
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
        }
            return View(usernotifications);
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

                //throw;
            }
        }
    }
}
