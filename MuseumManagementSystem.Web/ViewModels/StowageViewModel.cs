using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class StowageViewModel
    {
        public Guid Id { get; set; }

        [Remote("IsStowageNameAvilabel", "RemoteValidations", HttpMethod = "POST",
         AdditionalFields = "__RequestVerificationToken," + nameof(Id), ErrorMessage = "stowageNameAssignedValidation")]
        [Display(Name ="name")]
        public string Name { get; set; }

        [Display(Name = "address")]

        public string? Address { get; set; }
        public int? SafesCount { get; set; }
        public int? ArtifactsCount { get; set; }
        
        

    }
}
