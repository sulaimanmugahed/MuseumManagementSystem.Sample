
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using MuseumManagementSystem.Web.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace MuseumManagementSystem.Web.ViewModels
{
    public class ArtifactEditViewModel
    {
        


        public Guid Id { get; set; }

        [Required(ErrorMessage = "requiredFailedValidation")]
        [StringLength(200, ErrorMessage = "stringLengthValidayion", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "name")]
        public string? Name { get; set; }


        [Display(Name = "nameInEnglish")]
        [StringLength(200, ErrorMessage = "stringLengthValidayion", MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string? NameInEnglish { get; set; }


        [Required(ErrorMessage = "requiredFailedValidation")]
        [RegularExpression("([0-9]+)", ErrorMessage = "inValidNumberValidation")]
        [Remote("IsSerialNumberAvilabel", "RemoteValidations", HttpMethod = "POST",
            AdditionalFields = "__RequestVerificationToken," + nameof(Id),
            ErrorMessage = "serialNumberAssignedValidation")]
        [StringLength(10, ErrorMessage = "numberLengthValidayion")]
        [Display(Name = "serialNumber")]
        public string? SerialNumber { get; set; }


        [RegularExpression("([0-9]+)", ErrorMessage = "inValidNumberValidation")]
        [StringLength(20, ErrorMessage = "numberLengthValidayion")]
        [Display(Name = "oldMuseumNumber")]
        public string? OldMuseumNumber { get; set; }


        [RegularExpression("([0-9]+)", ErrorMessage = "inValidNumberValidation")]
        [StringLength(20, ErrorMessage = "numberLengthValidayion")]
        [Display(Name = "newMuseumNumber")]
        public string? NewMuseumNumber { get; set; }


        [RegularExpression("([0-9]+)", ErrorMessage = "inValidNumberValidation")]
        [StringLength(5, ErrorMessage = "numberLengthValidayion")]
        [Display(Name = "count")]
        public string? Count { get; set; }


        [StringLength(4000, ErrorMessage = "stringLengthValidayion", MinimumLength = 2)]
        [Display(Name = "description")]
        public string? Description { get; set; }


        [StringLength(50, ErrorMessage = "stringLengthValidayion", MinimumLength = 2)]
        [Display(Name = "size")]
        public string? Size { get; set; }


        [StringLength(4000, ErrorMessage = "stringLengthValidayion", MinimumLength = 2)]
        [Display(Name = "note")]
        public string? Note { get; set; }


        [StringLength(1000, ErrorMessage = "numberLengthValidayion")]
        [Url(ErrorMessage = "invalidUrlValidation")]
        [Display(Name = "imageLink")]
        public string? ImageLink { get; set; }


        [Display(Name = "importantMaterial")]
        public SelectListViewModel ImportantMaterialsSelectList { get; set; } = new();


        [Display(Name = "materials")]
        public SelectListViewModel MaterialsSelectList { get; set; } = new();


        [Display(Name = "artifactType")]
        public SelectListViewModel ArtifactTypesSelectList { get; set; } = new();


        [Display(Name = "artifactCondition")]
        public SelectListViewModel ArtifactConditionsSelectList { get; set; } = new();


        [Display(Name = "bioDeg")]
        public SelectListViewModel BioDegsSelectList { get; set; } = new();


        [Display(Name = "timePeriod")]
        public SelectListViewModel TimePeriodsSelectList { get; set; } = new();


        [Display(Name = "safe")]
        public SelectListViewModel SafesSelectList { get; set; } = new();


        public List<ArtifactImageViewModel> Images { get; set; } = new();


    }
}
