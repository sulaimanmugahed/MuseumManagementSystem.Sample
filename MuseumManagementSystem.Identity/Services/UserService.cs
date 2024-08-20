using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MuseumManagementSystem.Application.Constants;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Exceptions;
using MuseumManagementSystem.Application.Models;
using MuseumManagementSystem.Application.Models.Identity;



using MuseumManagementSystem.Identity.Models;

using System.Net.Mail;
using System.Security.Claims;


namespace MuseumManagementSystem.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;
            _signInManager = signInManager;
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return null;

            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName!,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,
            };
        }

        public List<User> GetAll()
        {
            return _userManager.Users.Where(x => x.Id != SuperAdminDefaultData.Id)
               .Select(user => new User
               {
                   Id = user.Id,
                   FirstName = user.FirstName,
                   LastName = user.LastName,
                   UserName = user.UserName!,
                   Email = user.Email!,
                   PhoneNumber = user.PhoneNumber,
                   ProfilePicture = user.ProfilePicture,

               }).ToList();

        }

        public string GetRoleName(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            return _userManager.GetRolesAsync(user).Result.FirstOrDefault();
        }

        public async Task<string> GetRoleId(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return string.Empty;

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count == 0)
                return string.Empty;

            var roleId = await _roleManager.GetRoleIdAsync(_roleManager.Roles.Single(r => r.Name == roles[0]));
            return roleId;
        }

        public async Task<Result> UpdateAsync(User user, string roleId, string? newPassword = null)
        {
            var userToUpdate = await _userManager.FindByIdAsync(user.Id)
            ?? throw new NullValueException();

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.UserName = user.UserName;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            var result = await _userManager.UpdateAsync(userToUpdate);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result.Failed(errors);
            }

            var oldRole = _userManager.GetRolesAsync(userToUpdate).Result.FirstOrDefault();
            var newRole = _roleManager.FindByIdAsync(roleId).Result.Name;

            if (!string.Equals(newRole, oldRole))
            {
                var deleteFromRoleResult = await _userManager.RemoveFromRoleAsync(userToUpdate, oldRole);
                if (!deleteFromRoleResult.Succeeded)
                {
                    var errors = deleteFromRoleResult.Errors.Select(e => e.Description).ToList();
                    return Result.Failed(errors);
                }


                var addToRoleResult = await _userManager.AddToRoleAsync(userToUpdate, newRole);
                if (!addToRoleResult.Succeeded)
                {
                    var errors = addToRoleResult.Errors.Select(e => e.Description).ToList();
                    return Result.Failed(errors);
                }

                await _userManager.UpdateSecurityStampAsync(userToUpdate);
            }
          


            if (newPassword != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(userToUpdate);
                var ResetPasswordRoleResult = await _userManager.ResetPasswordAsync(userToUpdate, token, newPassword);
                if (!ResetPasswordRoleResult.Succeeded)
                {
                    var errors = ResetPasswordRoleResult.Errors.Select(e => e.Description).ToList();
                    return Result.Failed(errors);
                }

            }

            return Result.Success();
        }

        public async Task<Result> CreateAsync(User user, string password, string? roleId = null)
        {
            var userToCreate = new ApplicationUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = new MailAddress(user.Email).User,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(userToCreate, password);


            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result.Failed(errors);

            }

            var role = _roleManager.FindByIdAsync(roleId ?? Roles.BaseId).Result;
            var addToRoleResult = await _userManager.AddToRoleAsync(userToCreate, role.Name);
            if (!addToRoleResult.Succeeded)
            {
                var addToRoleError = addToRoleResult.Errors.Select(e => e.Description).ToList();
                return Result.Failed(addToRoleError);
            }

            return Result.Success();

        }


        public async Task<Result> UpdateProfile(User user)
        {
            var userToUpdate = await _userManager.FindByIdAsync(user.Id)
            ?? throw new NullValueException();

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.PhoneNumber = user.PhoneNumber;
            userToUpdate.ProfilePicture = user.ProfilePicture;

            var result = await _userManager.UpdateAsync(userToUpdate);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result.Failed(errors);
            }

            return Result.Success();

        }

        public async Task<bool> IsEmailAssigned(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
                return true;

            return false;
        }

        public async Task<bool> IsEmailAssigned(string email, string id)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && user.Id != id)
                return true;

            return false;
        }


        public async Task<bool> IsUserNameAssigned(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
                return true;

            return false;
        }

        public async Task<bool> IsUserNameAssigned(string email, string id)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null && user.Id != id)
                return true;

            return false;
        }



        public async Task<Result> DeleteAsync(User user)
        {
            var userToDelete = await _userManager.FindByIdAsync(user.Id);

            var result = await _userManager.DeleteAsync(userToDelete!);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Result.Failed(errors);
            }

            return Result.Success();
        }

        //public IQueryable<User> GetUsersToDataTable(string searchValue)
        //{
        //    var users = _userManager.Users.Where(u => string.IsNullOrEmpty(searchValue)
        // ? true
        // : (
        // u.FirstName!.Contains(searchValue)
        // || u.LastName!.Contains(searchValue)
        // || u.UserName!.Contains(searchValue)
        // || u.Email!.Contains(searchValue)
        // ));


        //    return users.Where(x => x.Id != SuperAdminDefaultData.Id).Select(user => new User
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        UserName = user.UserName!,
        //        Email = user.Email!,
        //        ProfilePicture = user.ProfilePicture,
        //    });
        //}

        //public async Task<User> GetCurrentUserAsync()
        //{
        //    var userId = _contextAccessor.HttpContext.User
        //        .FindFirst(ClaimTypes.NameIdentifier)!.Value
        //        ?? throw new NullValueException();

        //    var user = await _userManager.FindByIdAsync(userId);
        //    return new User
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        UserName = user.UserName!,
        //        Email = user.Email!,
        //        PhoneNumber = user.PhoneNumber,
        //        ProfilePicture = user.ProfilePicture,
        //    };
        //}





        public async Task<PagedResult<User>> GetAllUsersPagedAsync(string? searchValue, int? start, int? length, string? sortColumn = null, string? sortDirection = "ASC")
        {

            var query = _userManager.Users.Where(u =>
            u.Id != SuperAdminDefaultData.Id).AsQueryable();

            // Apply search filter if a search value is provided
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(u =>
                u.FirstName.Contains(searchValue)
                || u.LastName.Contains(searchValue)
                || u.Email.Contains(searchValue)
                || u.UserName.Contains(searchValue)
                || u.PhoneNumber.Contains(searchValue)
                );
            }

            // Get the total count of records for pagination
            int totalCount = await query.CountAsync();

            // Apply sorting
            if (!string.IsNullOrEmpty(sortColumn))
                if (sortDirection == "desc")
                    query = query.OrderByDescending(a => EF.Property<object>(a, sortColumn));
                else
                    query = query.OrderBy(a => EF.Property<object>(a, sortColumn));


            // Apply pagination
            if (start.HasValue)
                query = query.Skip(start.Value);

            if (length.HasValue)
                query = query.Take(length.Value);


            // Execute the query and retrieve the data
            var users = await query.Select(user => new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture,

            }).ToListAsync();

            // Return the paginated result
            return new PagedResult<User>
            {
                Data = users,
                Draw = 1, // You can pass the DataTables "draw" parameter here
                RecordsFiltered = totalCount,
                RecordsTotal = totalCount,
  
            };

        }

        public async Task<string?> GetUserName(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.UserName;
        }
    }
}
