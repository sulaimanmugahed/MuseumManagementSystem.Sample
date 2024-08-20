using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "stringLengthValidayion", MinimumLength = 2)]
        [Display(Name = "first-name")]

        public string? FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "stringLengthValidayion", MinimumLength = 2)]
        [Display(Name = "last-name")]

        public string? LastName { get; set; }

        [Remote("IsUserUserNameAvilabel", "RemoteValidations", HttpMethod = "POST",
           AdditionalFields = "__RequestVerificationToken," + nameof(Id),
           ErrorMessage = "userNameAssignedValidation")]
        [Display(Name = "username")]

        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "invalid_email")]
        [Remote("IsUserEmailAvilabel", "RemoteValidations", HttpMethod = "POST",
            AdditionalFields = "__RequestVerificationToken," + nameof(Id),
            ErrorMessage = "emailAssignedValidation")]
        [Display(Name = "email")]

        public string? Email { get; set; }

        [MinLength(6,ErrorMessage ="at least 6 char")]
        [Display(Name = "new-password")]
        public string? NewPassword { get; set; }

        [Display(Name = "confirm-new-password")]

        [Compare("NewPassword", ErrorMessage = "password_not_match")]
        public string? ConfirmNewPassword { get; set; }

        [Display(Name = "role")]

        public string SelectedRole { get; set; }
        public List<SelectListItem> Roles { get; set; } = [];


        [Display(Name = "phone-number")]

        public string? PhoneNumber { get; set; }



    }
}
