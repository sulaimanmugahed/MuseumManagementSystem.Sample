using AutoMapper;
using Microsoft.Extensions.Localization;
using MuseumManagementSystem.Application.Contracts.Identity;
using MuseumManagementSystem.Application.Models.Identity;
using MuseumManagementSystem.Web.ViewModels;

namespace MuseumManagementSystem.Web.Mapping
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Role,opt => opt.MapFrom<UserRoleNameResolver>());

            CreateMap<AddUserViewModel, User>();
            CreateMap<User, EditUserViewModel>().ReverseMap();

            CreateMap<ProfileViewModel, User>()
                .ForMember(dest=> dest.ProfilePicture,opt=> opt.Ignore());

            CreateMap<User, ProfileViewModel > ()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePicture));
        }
    }

    
    public class UserRoleNameResolver(IUserService userService, IStringLocalizerFactory stringLocalizerFactory)
        : IValueResolver<User, UserViewModel, string>
    {
        private readonly IStringLocalizer _stringLocalizer = stringLocalizerFactory.Create(typeof(UserRoleNameResolver));
        public string Resolve(User source, UserViewModel destination, string destMember, ResolutionContext context)
        {
            return _stringLocalizer[$"nameOf{userService.GetRoleName(source.Id)}"].Value;
        }
    }
}
