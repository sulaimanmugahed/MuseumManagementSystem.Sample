using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Contracts.Persistence;

namespace MuseumManagementSystem.Web.Controllers
{
    
    public class RemoteValidationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public RemoteValidationsController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }



        [Authorize]
        //Artifacts remote validations
        [AcceptVerbs("GET", "POST")]
        public JsonResult IsSerialNumberAvilabel(string serialNumber, Guid Id)
        {
            if (Id == Guid.Empty)
                return Json(!_unitOfWork.Artifacts.IsSerialNumberAssigned(serialNumber));

            return Json(!_unitOfWork.Artifacts.IsSerialNumberAssigned(serialNumber, Id));
        }

        //Users remote validations
        [AcceptVerbs("GET", "POST")]
        public async Task<JsonResult> IsUserEmailAvilabel(string email, string Id)
        {
            if (Id == string.Empty)
                return Json(! await _userService.IsEmailAssigned(email));

            return Json(!await _userService.IsEmailAssigned(email,Id));
        } 
        
        public async Task<JsonResult> IsUserUserNameAvilabel(string userName, string Id)
        {
            if (Id == string.Empty)
                return Json(! await _userService.IsUserNameAssigned(userName));

            return Json(!await _userService.IsUserNameAssigned(userName, Id));
        }

        [Authorize]
        //Stowages remote validations
        [AcceptVerbs("GET", "POST")]
        public JsonResult IsStowageNameAvilabel(string name, Guid Id)
        {
            if (Id == Guid.Empty)
                return Json(!_unitOfWork.Stowages.IsNameAssigned(name));

            return Json(!_unitOfWork.Stowages.IsNameAssigned(name, Id));
        }
        [Authorize]
        //Safes remote validations
        [AcceptVerbs("GET", "POST")]
        public JsonResult IsSafeNameAvilabel(string name, Guid Id)
        {
            if (Id == Guid.Empty)
                return Json(!_unitOfWork.Safes.IsNameAssigned(name));

            return Json(!_unitOfWork.Safes.IsNameAssigned(name, Id));
        }
        [Authorize]
        //BioDegs remote validations
        [AcceptVerbs("GET", "POST")]
        public JsonResult IsBioDegNameAvilabel(string name, Guid Id)
        {
            if (Id == Guid.Empty)
                return Json(!_unitOfWork.BioDegs.IsNameAssigned(name));

            return Json(!_unitOfWork.BioDegs.IsNameAssigned(name, Id));
        }
        [Authorize]
        //TimePeriods remote validations
        [AcceptVerbs("GET", "POST")]
        public JsonResult IsTimePeriodNameAvilabel(string name, Guid Id)
        {
            if (Id == Guid.Empty)
                return Json(!_unitOfWork.TimePeriods.IsNameAssigned(name));

            return Json(!_unitOfWork.TimePeriods.IsNameAssigned(name, Id));
        }

        [Authorize]
        //ArtifactTypes remote validations
        [AcceptVerbs("GET", "POST")]
        public JsonResult IsArtifactTypeNameAvilabel(string name, Guid Id)
        {
            if (Id == Guid.Empty)
                return Json(!_unitOfWork.ArtifactTypes.IsNameAssigned(name));

            return Json(!_unitOfWork.ArtifactTypes.IsNameAssigned(name, Id));
        }

        [Authorize]
        //ArtifactConditions remote validations
        [AcceptVerbs("GET", "POST")]
        public JsonResult IsArtifactConditionNameAvilabel(string name, Guid Id)
        {
            if (Id == Guid.Empty)
                return Json(!_unitOfWork.ArtifactConditions.IsNameAssigned(name));

            return Json(!_unitOfWork.ArtifactConditions.IsNameAssigned(name, Id));
        }
        [Authorize]
        //Materials remote validations
        [AcceptVerbs("GET", "POST")]
        public JsonResult IsMaterialNameAvilabel(string name, Guid Id)
        {
            if (Id == Guid.Empty)
                return Json(!_unitOfWork.Materials.IsNameAssigned(name));

            return Json(!_unitOfWork.Materials.IsNameAssigned(name, Id));
        }

      


    }
}
