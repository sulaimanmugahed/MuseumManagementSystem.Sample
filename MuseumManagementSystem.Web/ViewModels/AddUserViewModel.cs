using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "requiredFailedValidation")]
        [StringLength(100, ErrorMessage = "stringLengthValidayion", MinimumLength = 2)]
        [Display(Name = "first-name")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "requiredFailedValidation")]

        [Remote("IsUserUserNameAvilabel", "RemoteValidations", HttpMethod = "POST",

          ErrorMessage = "userNameAssignedValidation")]
        [Display(Name = "username")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "requiredFailedValidation")]

        [StringLength(100, ErrorMessage = "stringLengthValidayion", MinimumLength = 2)]

        [Display(Name = "last-name")]

        public string? LastName { get; set; }

        [Required(ErrorMessage = "requiredFailedValidation")]

        [EmailAddress(ErrorMessage = "invalid_email")]
        [Remote("IsUserEmailAvilabel", "RemoteValidations", HttpMethod = "POST",
            ErrorMessage = "emailAssignedValidation")]
        [Display(Name = "email")]

        public string Email { get; set; }

        [Required(ErrorMessage = "requiredFailedValidation")]
        [StringLength(100, ErrorMessage = "stringLengthValidayion", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "password")]

        public string Password { get; set; }

       
        [DataType(DataType.Password)]
        
        [Compare("Password", ErrorMessage = "password_not_match")]

        [Display(Name = "confirm-password")]

        public string? ConfirmPassword { get; set; }

        [Display(Name = "phone-number")]

        public string? PhoneNumber { get; set; }


        [Display(Name = "role")]

        public string? SelectedRole { get; set; }

        public List<SelectListItem> Roles { get; set; } = [];



       
    }
}
