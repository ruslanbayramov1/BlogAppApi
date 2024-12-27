using AutoMapper;
using BlogApp.BL.DTOs.Users;
using BlogApp.Core.Entities;
using BlogApp.Core.Enums;

namespace BlogApp.BL.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserGetDto>()
            .ForMember(d => d.Role, opt => opt.MapFrom(u => Enum.ToObject(typeof(Roles), u.Role).ToString()));

        CreateMap<UserCreateDto, User>()
            .ForMember(u => u.PasswordHash, opt => opt.Ignore());

        CreateMap<UserUpdateDto, User>()
            .ForMember(u => u.PasswordHash, opt => opt.Ignore()); ;
    }
}
