using Gharshare.CommonHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Gharshare.Views.Shared.Components.SearchBar;
using System.Collections;
using System.Net;

namespace Gharshare.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebSiteRoles.Role_Admin)]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IUnitofWork _unitofWork;
        private Utility _utility;

        public UserController(UserManager<ApplicationUser> userManager,
            IUnitofWork unitofWork, Utility utility)
        {
            _userManager = userManager;
            _unitofWork = unitofWork;
            _utility = utility;
        }
        public string StatusMessage { get; set; }

        public JsonResult getuserlist(int pg = 1, string SearchText = "",
            string sortOrder = "", Int64 usertype = 0)
        {
            List<ApplicationUser> users = null;
            try
            {                 
                users = _unitofWork.User.GetAll().OrderBy(u => u.UserType).ToList();

                #region Filter Data based on user type & search criterion
                var type = (ApplicationUser.usertype)usertype;
                switch (type)
                {
                    case ApplicationUser.usertype.superadmin:
                        users = users.Where(u => u.UserType == ApplicationUser.usertype.superadmin).ToList();
                        break;
                    case ApplicationUser.usertype.admin:
                        users = users.Where(u => u.UserType == ApplicationUser.usertype.admin).ToList();
                        break;
                    case ApplicationUser.usertype.freeuser:
                        users = users.Where(u => u.UserType == ApplicationUser.usertype.freeuser).ToList();
                        break;
                    case ApplicationUser.usertype.landlord:
                        users = users.Where(u => u.UserType == ApplicationUser.usertype.landlord).ToList();
                        break;
                    case ApplicationUser.usertype.tenant:
                        users = users.Where(u => u.UserType == ApplicationUser.usertype.tenant).ToList();
                        break;
                    default:
                        users = users;
                        break;
                }
                if (SearchText != string.Empty && SearchText != null && SearchText != "")
                    users = users.Where(u => u.Name.IndexOf(SearchText,
                        StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                #endregion

                #region Apply Pagination & Sorting
                ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
                ViewBag.TypeSortParam = string.IsNullOrEmpty(sortOrder) ? "usertype_desc" : "";
                ViewBag.AddressSortParam = sortOrder == "address" ? "address_desc" : "address";


                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                ViewBag.pg = pg;
                int recsCount = users.Count();
                var pager = new Pager(recsCount, pg, "User", "index", pageSize);
                pager.SortOrder = sortOrder;
                int recSkip = (pg - 1) * pageSize;
                switch (sortOrder)
                {
                    case "name":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderBy(u => u.Name).ToList();
                        ViewData["SortIcon"] = "fa fa-arrow-down";
                        break;
                    case "name_desc":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderByDescending(u => u.Name).ToList();
                        ViewData["SortIcon"] = "fa fa-arrow-up";
                        break;
                    case "address":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderBy(u => u.Address).ToList();
                        ViewData["SortIconAddress"] = "fa fa-arrow-down";
                        break;
                    case "address_desc":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderByDescending(u => u.Address).ToList();
                        ViewData["SortIconAddress"] = "fa fa-arrow-up";
                        break;
                    case "usertype_desc":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderByDescending(u => u.UserType).ToList();
                        ViewData["SortIconType"] = "fa fa-arrow-up";
                        break;
                    default:
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderBy(u => u.UserType).ToList();
                        ViewData["SortIconType"] = "fa fa-arrow-down";
                        break;
                }
                this.ViewBag.Pager = pager;
                #endregion
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/user/getuserlist");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return Json(users);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult edituser(ApplicationUser _user)
        {
            List<ApplicationUser> users = null;
            ApplicationUser user = _unitofWork.User.Get(u => u.Id == _user.Id);
            try
            {
                user.Name = _user.Name;
                user.Email = _user.Email;
                user.Mobile = _user.Mobile;
                user.Address = _user.Address;
                user.IsActive = _user.IsActive;
                if (!_user.IsActive)
                    user.EmailConfirmed = false;
                else
                    user.EmailConfirmed = true;
                if (_user.UserType != ApplicationUser.usertype.admin)
                    user.ShowNumber = _user.ShowNumber;
                _unitofWork.User.update(user);
                _unitofWork.Save();
                users = _unitofWork.User.GetAll().OrderBy(u => u.UserType).ToList();
            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/user/edituser");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }

            return new JsonResult(users);
        }
        
        [HttpGet]
        public IActionResult edituser(string userId)
        {
            ApplicationUser user = _unitofWork.User.Get(u => u.Id == userId);
            return View("edituser", user);
        }
        public IActionResult Index(int pg = 1, string SearchText = "",
            string sortOrder = "", ApplicationUser.usertype? usertype = 0)
        {
            List<ApplicationUser> users = null;
            try
            {
                string _userType = String.Empty;
                users = _unitofWork.User.GetAll().OrderBy(u => u.UserType).ToList();

                #region Get Data on Search criterion

                switch (usertype)
                {
                    case ApplicationUser.usertype.admin:
                        users = users.Where(u => u.UserType == ApplicationUser.usertype.admin).ToList();
                        break;
                    case ApplicationUser.usertype.freeuser:
                        users = users.Where(u => u.UserType == ApplicationUser.usertype.freeuser).ToList();
                        break;
                    case ApplicationUser.usertype.landlord:
                        users = users.Where(u => u.UserType == ApplicationUser.usertype.landlord).ToList();
                        break;
                    case ApplicationUser.usertype.tenant:
                        users = users.Where(u => u.UserType == ApplicationUser.usertype.admin).ToList();
                        break;
                    default:
                        users = users;
                        break;
                }
                if (SearchText == string.Empty || SearchText == null)
                    users = users.Where(u => u.Name.Contains(SearchText)).ToList();

                Search search = new Search() { Action = "index", Controller = "User", SearchText = SearchText };
                ViewBag.Search = search;
                #endregion

                #region Apply Pagination & Sorting
                ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
                ViewBag.TypeSortParam = string.IsNullOrEmpty(sortOrder) ? "usertype_desc" : "";
                ViewBag.AddressSortParam = sortOrder == "address" ? "address_desc" : "address";


                int pageSize = Pager.GetPageSize();
                if (pg < 1)
                {
                    pg = 1;
                }
                ViewBag.pg = pg;
                int recsCount = users.Count();
                var pager = new Pager(recsCount, pg, "User", "index", pageSize);
                pager.SortOrder = sortOrder;
                int recSkip = (pg - 1) * pageSize;
                switch (sortOrder)
                {
                    case "name":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderBy(u => u.Name).ToList();
                        ViewData["SortIcon"] = "fa fa-arrow-down";
                        break;
                    case "name_desc":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderByDescending(u => u.Name).ToList();
                        ViewData["SortIcon"] = "fa fa-arrow-up";
                        break;
                    case "address":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderBy(u => u.Address).ToList();
                        ViewData["SortIconAddress"] = "fa fa-arrow-down";
                        break;
                    case "address_desc":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderByDescending(u => u.Address).ToList();
                        ViewData["SortIconAddress"] = "fa fa-arrow-up";
                        break;
                    case "usertype_desc":
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderByDescending(u => u.UserType).ToList();
                        ViewData["SortIconType"] = "fa fa-arrow-up";
                        break;
                    default:
                        users = users.Skip(recSkip).Take(pager.PageSize).OrderBy(u => u.UserType).ToList();
                        ViewData["SortIconType"] = "fa fa-arrow-down";
                        break;
                }
                pager.ItemsinCurrentPage = users.Count();
                pager.FromRecord = ((pager.CurrentPage - 1) * pager.PageSize) + 1;
                pager.ToRecord = ((pager.CurrentPage - 1) * pager.PageSize) + (int)pager.ItemsinCurrentPage;
                this.ViewBag.Pager = pager;
                #endregion
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
            return View(users);

        }
        [HttpPost]
        public JsonResult harddeleteuser(ApplicationUser user)
        {
            List<ApplicationUser> users = null;
            try
            {
                //var userErrorlog = _unitofWork.ErrorLog.GetAll(e => e.LoggedInUserId == user.Id).ToList();
                //var enquries = _unitofWork.Enquiry.GetAll(e => e.ToUserID == user.Id ||
                //e.FromUserID==user.Id).ToList();
                //_unitofWork.ErrorLog.DeleteRange(userErrorlog);
                //_unitofWork.Enquiry.DeleteRange(enquries);
                //_unitofWork.Save();
                var _user = _unitofWork.User.Get(u => u.Id == user.Id);                
                _unitofWork.User.Delete(_user);
                _unitofWork.Save();
                users = _unitofWork.User.GetAll().OrderBy(u => u.UserType).ToList();

            }
            catch (Exception exp)
            {
                LogErrorDetail(exp, "admin/user/deleteadmin");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { error = "failed" });
            }
            return new JsonResult(users);
        }

        [HttpGet]
        public IActionResult harddeleteuser(string userId)
        {
            ApplicationUser user = _userManager.Users.Where(_u => _u.Id == userId).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WebSiteRoles.Role_Admin)]
        public async Task<IActionResult> DeleteUser(string Id, string UserType)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id={Id} cannot found";
                return View("Not Found");
            }
            else
            {
                if (UserType == "Landlord")
                {
                    IEnumerable properties = _unitofWork.Property.GetAll(x => x.OwnerId == Id).ToList();
                    foreach (Property property in properties)
                    {
                        //Delete images of user property
                        IEnumerable<PropertyImage> propertyImages = _unitofWork.PropertyImages.GetAll(x => x.PropertyID == property.ID);
                        foreach (var image in propertyImages)
                        {
                            _utility.DeletFile(image.ImagePath);
                        }
                        _unitofWork.PropertyImages.DeleteRange(propertyImages);
                        _unitofWork.Save();

                        //Delete cover image of the user property from the file system.
                        _utility.DeletFile(property.CoverImage);

                        //Delete user property
                        _unitofWork.Property.Delete(property);
                        _unitofWork.Save();
                    }
                }
                //Delete user profile picture from the file system
                _utility.DeletFile(user.ProfilePicture);
                var result = await _userManager.DeleteAsync(user);
                if (UserType == WebSiteRoles.Role_Admin)
                {
                    _userManager.RemoveFromRoleAsync(user, WebSiteRoles.Role_Admin).GetAwaiter().GetResult();
                    _userManager.RemoveFromRoleAsync(user, WebSiteRoles.Role_FreeUser).GetAwaiter().GetResult();
                }
                else if (UserType == WebSiteRoles.Role_FreeUser)
                {
                    _userManager.RemoveFromRoleAsync(user, WebSiteRoles.Role_FreeUser).GetAwaiter().GetResult();
                }

                if (result.Succeeded)
                {
                    TempData["success"] = "User has been deleted!";
                    return Redirect("/Admin/User/Index/All");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return Redirect("/Admin/User/Index/All");
                }
            }

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
