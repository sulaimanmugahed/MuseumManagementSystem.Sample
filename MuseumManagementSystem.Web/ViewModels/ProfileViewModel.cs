using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        [Display(Name = "first-name")]

        public string? FirstName { get; set; }
        [Display(Name = "last-name")]

        public string? LastName { get; set; }
        [Display(Name = "username")]

        public string? UserName { get; set; }
        [Display(Name = "phone-number")]

        public string? PhoneNumber { get; set; }
        public IFormFile? ProfilePicture { get; set; } 
        public string? ProfilePictureUrl { get; set; } 
        
    }
}
