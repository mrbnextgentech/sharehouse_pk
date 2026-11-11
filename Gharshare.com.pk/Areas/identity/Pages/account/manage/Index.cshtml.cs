// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Gharshare.CommonHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gharshare.DataAccessLayer.Data.Infrastructure.IRepository;
using Gharshare.Models;

namespace Gharshare.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private Utility _utility;
        private IUnitofWork _unitofWork;
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, Utility utility, IUnitofWork iunitofWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _utility = utility;
            _unitofWork = iunitofWork;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            public string Id { get; set; }
            [Phone]
            [Display(Name = "Phone number")]
            [Required]
            public string PhoneNumber { get; set; }

            [Required]
            public string Name { get; set; }

            [Display(Name = "Sur Name")]
            public string SurName { get; set; }            
            
            [Display(Name = "Profile Picture")]
            public string ProfilePicture { get; set; }

            [Display(Name = "PostalCode")]
            public string PostalCode { get; set; }            
            public string Address { get; set; }
            public ApplicationUser.usertype UserType { get; set; }
            public Int64? CityID { get; set; }
            public Int64? StateID { get; set; }
            public string? Mobile { get; set; }
            public string? CNIC { get; set; }
        }

        //private async Task LoadAsync(ApplicationUser user)
        private async Task LoadAsync(ApplicationUser user,string Id)
        {
            // var userName = await _userManager.GetUserNameAsync(user);
            ApplicationUser _user;
            if (Id==null || Id==string.Empty)
            {
                _user = await _userManager.FindByNameAsync(user.UserName);
            }
            else
            {
                _user = await _userManager.FindByIdAsync(Id);
            }            
            
            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var phoneNumber = _user.PhoneNumber;

            #region poplulate State and City dropdowns
            States = _unitofWork.State.GetAll().OrderBy(s => s.Name).ToList().Select(
                            s => new SelectListItem()
                            {
                                Text = s.Name,
                                Value = s.ID.ToString()

                            });

            Cities = _unitofWork.City.GetAll().OrderBy(s => s.Name).ToList().Select(
                c => new SelectListItem()
                {
                    Text=c.Name,
                    Value=c.ID.ToString()
                });

            #endregion

            // Username = userName;
            Username = _user.UserName;

            Input = new InputModel
            {
                Id = _user.Id,
                PhoneNumber = phoneNumber,
                Name = _user.Name,
                SurName = _user.SurName,
                PostalCode = _user.PostalCode,                
                Address = _user.Address,
                UserType = _user.UserType,
                ProfilePicture = _user.ProfilePicture,
                StateID = _user.StateID,
                CityID = _user.CityID,
                Mobile=_user.Mobile,
                CNIC=_user.CNIC                
            };
        }

        public async Task<IActionResult> OnGetAsync(string? Id)
        {
            ApplicationUser user;
            if(Id ==null)
            { 
             user = await _userManager.GetUserAsync(User);
            }
            else
            {
                user = await _userManager.FindByIdAsync(Id);
            }
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (Id == null)
            {
                await LoadAsync(user, "");
            }
            else
            {
                await LoadAsync(user, Id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? Id,IFormFile file)
        {
            ApplicationUser user;
            if (Id == null || Id == string.Empty)
            {
                user = await _userManager.GetUserAsync(User);
            }
            else
            {
                user = await _userManager.FindByIdAsync(Id);
            }
            
            

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user,"");
                return Page();
            }
            var profilePicture = user.ProfilePicture;
            string _profilePicturePath = string.Empty;
            if (file != null)
            {
                //Delete previous picture,if any, from the file system.
                if (profilePicture !=null)
                _utility.DeletFile(profilePicture);
                _profilePicturePath = _utility.SaveFile(file, @"assets/img/ProfileImage");           
                            
            if (_profilePicturePath != profilePicture)
            {
                user.ProfilePicture = _profilePicturePath;
                var setProfilePicResult = await _userManager.UpdateAsync(user);
                if (!setProfilePicResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set profile picture.";
                    return RedirectToPage();
                }
            }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                //var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                user.PhoneNumber=Input.PhoneNumber;
                var setPhoneResult= await _userManager.UpdateAsync(user);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            var name = user.Name;
            if(Input.Name != name)
            {
                user.Name = Input.Name;
                var setNameResult = await _userManager.UpdateAsync(user);
                if (!setNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set name.";
                    return RedirectToPage();
                }
            }
            var surName = user.SurName;
            if (Input.SurName != surName)
            {
                user.SurName = Input.SurName;
                var setNameResult = await _userManager.UpdateAsync(user);
                if (!setNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set sur name.";
                    return RedirectToPage();
                }
            }

            var stateID = user.StateID;
            if (Input.StateID != stateID)
            {
                user.StateID = Input.StateID;
                var setStateResult = await _userManager.UpdateAsync(user);
                if (!setStateResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set state name.";
                    return RedirectToPage();
                }
            }

            var postalCode = user.PostalCode;
            if (Input.PostalCode != postalCode)
            {
                user.PostalCode = Input.PostalCode;
                var setPinResult = await _userManager.UpdateAsync(user);
                if (!setPinResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set state pin code.";
                    return RedirectToPage();
                }
            }

            var cityID = user.CityID;
            if (Input.CityID != cityID)
            {
                user.CityID = Input.CityID;
                var setCityResult = await _userManager.UpdateAsync(user);
                if (!setCityResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set city.";
                    return RedirectToPage();
                }
            }

            var address = user.Address;
            if (Input.Address != address)
            {
                user.Address = Input.Address;
                var setAddressResult = await _userManager.UpdateAsync(user);
                if (!setAddressResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set address.";
                    return RedirectToPage();
                }
            }

           // Commented below lines of code so that admin remains logged in and not the user under edit.
           // await _signInManager.RefreshSignInAsync(user);
            //StatusMessage = "Your profile has been updated";            
            // return RedirectToPage();

            TempData["success"] = "Your profile has been updated";
            if(HttpContext.Session.GetString("_UserType")=="Admin")
            {
                return Redirect("/Admin/User/Index/All");
            }
            else
            {
                return RedirectToPage();
            }
            
            
        }
    }
}
