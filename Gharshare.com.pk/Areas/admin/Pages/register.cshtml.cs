// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Gharshare.CommonHelper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Net;

namespace Gharshare.com.pk.Areas.Admin.Pages
{
    public class RegisterModelAdmin : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModelAdmin> _logger;
        private readonly IEmailSender _emailSender;
        private IUnitofWork _unitofWork;
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        public RegisterModelAdmin(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModelAdmin> logger,
            IEmailSender emailSender,
            IUnitofWork iunitofWork
            )

        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _unitofWork = iunitofWork;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "* The {0} field is required.")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "* The {0} field is required.")]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "* The {0} field is required.")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            
            [Required(ErrorMessage = "The {0} field is required.")]
            public string Name { get; set; }
            public string? SurName { get; set; }

            public string? Address { get; set; }

            public int CityID { get; set; }
            public int StateID { get; set; }

            [Display(Name = "Postalcode")]
            public string? Postalcode { get; set; }
            [Required(ErrorMessage = "* The {0} field is required.")]
            public ApplicationUser.usertype UserType { get; set; }
            [ValidateNever]
            [Required(ErrorMessage = "* The {0} field is required.")]
            public string? Mobile { get; set; }
            [ValidateNever]
            [Required(ErrorMessage = "* The {0} field is required.")]
            public string? CNIC { get; set; }
            [Required(ErrorMessage = "* The {0} field is required.")]
            //public string? Gender { get; set; }
            public gender? Gender { get; set; }
            public enum gender
            {
                [Display(Name = "Male")]
                Man = 1,
                [Display(Name = "Female")]
                Woman = 2,
                [Display(Name = "Transgender")]
                Transgender = 3,
                [Display(Name = "Undisclosed")]
                Undisclosed = 4
            }
            public bool AgreedtoTerms { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            States = _unitofWork.State.GetAll().OrderBy(s => s.Name).ToList().Select(
                            s => new SelectListItem()
                            {
                                Text = s.Name,
                                Value = s.ID.ToString()

                            });
            //Input = new InputModel();

            Cities = null;
            ReturnUrl = returnUrl;
            //Input = new InputModel();
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            #region commented code 
            //if (!_roleManager.RoleExistsAsync(WebSiteRoles.Role_Admin).GetAwaiter().GetResult())
            //{
            //    _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Role_Admin)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Role_Manager)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Role_Agent)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Role_Landlord)).GetAwaiter().GetResult();
            //    _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.Role_Tenant)).GetAwaiter().GetResult();            //}
            #endregion

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        public async Task<JsonResult> OnPostAsync(string returnUrl = null)
        {
            List<ApplicationUser> users = null;
            try
            {
                returnUrl ??= Url.Content("~/");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                if (ModelState.IsValid)
                {
                    var user = CreateUser();
                    //await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                    MailAddress address = new MailAddress(Input.Email);
                    string userName = address.User;
                    await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                    user.UserName = userName;
                    user.Name = Input.Name;
                    user.UserType = Input.UserType;
                    user.EmailConfirmed = true;
                    //user.Address = Input.Address;
                    //user.CityID = Input.CityID;
                    //user.StateID = Input.StateID;
                    //user.PostalCode = Input.Postalcode;                
                    //user.Mobile = Input.Mobile;
                    //user.CNIC = Input.CNIC;
                    user.Gender =(ApplicationUser.gender)Input.Gender;
                    user.AgreedtoTerms = true;

                    var result = await _userManager.CreateAsync(user, Input.Password);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        switch (user.UserType)
                        {
                            case ApplicationUser.usertype.admin:
                                _userManager.AddToRoleAsync(user, WebSiteRoles.Role_Admin).GetAwaiter().GetResult();
                                break;
                            case ApplicationUser.usertype.freeuser:
                                _userManager.AddToRoleAsync(user, WebSiteRoles.Role_FreeUser).GetAwaiter().GetResult();
                                break;
                            case ApplicationUser.usertype.landlord:
                                _userManager.AddToRoleAsync(user, WebSiteRoles.Role_Landlord).GetAwaiter().GetResult();
                                break;
                            case ApplicationUser.usertype.tenant:
                                _userManager.AddToRoleAsync(user, WebSiteRoles.Role_Tenant).GetAwaiter().GetResult();
                                break;
                        }

                        //return RedirectToAction("index", "user");
                        users = _unitofWork.User.GetAll().OrderBy(u => u.UserType).ToList();
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return new JsonResult(new { error = "failed" });
                }
            }
            catch (Exception exp)
            {
                //TempData["ErrorMessage"] = ex.InnerException?.Message;
                //TempData["StackTrace"] = ex.StackTrace;
                //TempData["Source"] = ex.Source;
                //return RedirectToAction("Error", "Error");
                LogErrorDetail(exp, "admin/register");
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new JsonResult(new { error = "failed" });
            }
            // If we got this far, something failed, redisplay form
            //return Page();
            return new JsonResult(users);
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
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
