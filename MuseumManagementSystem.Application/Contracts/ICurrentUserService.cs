namespace MuseumManagementSystem.Application.Contracts;
public interface ICurrentUserService
{
   string UserId { get; }
   string UserName { get; }

}
