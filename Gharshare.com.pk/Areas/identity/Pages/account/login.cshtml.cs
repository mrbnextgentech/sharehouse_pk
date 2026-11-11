// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Gharshare.Models;
using Gharshare.CommonHelper;
using System.Security.Claims;
using System.Security.Principal;
using System.Net.Mail;

namespace Gharshare.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
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
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

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
            [Required]
            //[EmailAddress]
            [Display(Name = "Email / Username")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }

        }
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                var userName = Input.Email;
                if (IsValidEmail(Input.Email))
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        userName = user.UserName;
                    }
                }
                //default implementation
                //var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                //Modified below to allow log in with both user name and email
                var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    ApplicationUser _user = _signInManager.UserManager.Users.
                    Where(x => x.Email == Input.Email || x.UserName == Input.Email).FirstOrDefault();
                    if (_user != null)
                    {

                        if (_user.IsActive==true /*&& _user.EmailConfirmed== true*/) //un-comment when email confirmation is set to true during sign up
                        {
                            _logger.LogInformation("User logged in.");
                            _user.Lastloggedin = DateTime.UtcNow;
                          await _signInManager.UserManager.UpdateAsync(_user);
                            HttpContext.Session.SetString("_UserId", _user.Id);
                            HttpContext.Session.SetString("_UserType", _user.UserType.ToString());
                            if (_user.Email != null)
                                HttpContext.Session.SetString("ownerEmail", _user.Email);
                            else
                                HttpContext.Session.SetString("ownerEmail","");

                            if (_user.PhoneNumber != null)
                                HttpContext.Session.SetString("ownerPhone", _user.PhoneNumber);
                            else
                                HttpContext.Session.SetString("ownerPhone", "");

                            ViewData["userID"] = _user.Id;
                            ViewData["userType"] = _user.UserType;

                            if (_user.UserType == ApplicationUser.usertype.admin ||
                                _user.UserType == ApplicationUser.usertype.superadmin)
                            {
                                if (returnUrl == null)
                                    returnUrl += "~/admin/site/";
                            }
                            else
                            {
                                if (returnUrl != null)
                                    return LocalRedirect(returnUrl);
                                else
                                    // return Redirect("/Identity/Account/Manage/Index");
                                    return Redirect("~/");
                            }
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Lockedout", "Error", new { area = "" });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid user name or password.");
                        ViewData["FailedLogin"] = true;
                        return Page();
                    }
                }
                if (result.IsNotAllowed)
                {
                    return RedirectToAction("Lockedout", "Error", new { area = "" });
                }
                    if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid user name or password.");
                    ViewData["FailedLogin"] = true;
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
