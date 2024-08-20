using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Application.Models.Identity;
using MuseumManagementSystem.Domain.Models;
using System;

namespace MuseumManagementSystem.Application.Contracts.Identity
{
    public interface IUserService
    {
        List<User> GetAll();
        //IQueryable<User> GetUsersToDataTable(string searchValue);
        Task<User?> GetByIdAsync(string userId);
        string GetRoleName(string id);

        Task<Result> CreateAsync(User user, string password, string? roleId = null);
        Task<Result> UpdateAsync(User user, string roleId, string? newPassword = null);
        Task<Result> DeleteAsync(User user);

        Task<string?> GetUserName(string userId);


        Task<bool> IsEmailAssigned(string email);
        Task<bool> IsUserNameAssigned(string email);
        Task<bool> IsEmailAssigned(string email, string id);
        Task<bool> IsUserNameAssigned(string email, string id);
        Task<string> GetRoleId(string id);

        //Task<User> GetCurrentUserAsync();
        Task<Result> UpdateProfile(User user);
        Task<PagedResult<User>> GetAllUsersPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC");
    }
}
