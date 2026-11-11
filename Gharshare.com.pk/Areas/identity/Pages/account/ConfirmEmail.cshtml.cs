// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Gharshare.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Microsoft.AspNetCore.Html;

namespace Gharshare.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IUnitofWork _unitofWork;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender,
            IUnitofWork unitofWork, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _unitofWork = unitofWork;
           _emailSender = emailSender;
            _signInManager = signInManager;

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public String StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            try { 
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
                await _signInManager.SignInAsync(user, isPersistent: false);
                if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
                var welcomeUser = $"Thanks for confirming your email. " +
                    $"Your account has successfully been varified" ;           
                StatusMessage = result.Succeeded ? welcomeUser :"Error confirming your email.";
                //set session variable
                HttpContext.Session.SetString("_UserId", user.Id);
                HttpContext.Session.SetString("_UserType", user.UserType.ToString());
                HttpContext.Session.SetString("ownerEmail", user.Email);

                ///send welcome & security instructions emails
                var welcomeTemplate = _unitofWork.EmailTemplate
                    .Get(t => t.EmailTemplateType == EmailTemplate.TemplateType.welcome);
            if (welcomeTemplate != null)
            {
                string welcomeMessage = welcomeTemplate.Template;
                string subject = welcomeTemplate.Title;
                await _emailSender.SendEmailAsync(user.Email, subject, welcomeMessage);
            }

            var securityInstructionTemplate = _unitofWork.EmailTemplate
                    .Get(t => t.EmailTemplateType == EmailTemplate.TemplateType.security);
                if (securityInstructionTemplate != null)
            {
                string securityInstructions = securityInstructionTemplate.Template;
                string subject = securityInstructionTemplate.Title;
                await _emailSender.SendEmailAsync(user.Email, subject, securityInstructions);
            }
                ///End send welcome & security instructions emails                
            }
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.InnerException?.Message;
                TempData["StackTrace"] = ex.StackTrace;
                TempData["Source"] = ex.Source;
                return RedirectToAction("Error", "Error");
            }
            return Page();
        }
    }
}
