using MuseumManagementSystem.Application.Contracts;
using System.Security.Claims;

namespace MuseumManagementSystem.Web.Services;



public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
   public string UserId => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
   public string UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name;

}
