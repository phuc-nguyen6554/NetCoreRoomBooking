using AutoMapper;
using DTO.User;
using Identity.DTO.User;
using Identity.Models.Users;

namespace Identity.MappingProfile
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDetailResponse>().ForMember(x => x.RoleName, opt => opt.MapFrom(y => y.Role.RoleName));
            CreateMap<User, UserListResponse>().ForMember(x => x.RoleName, opt => opt.MapFrom(y => y.Role.RoleName));
        }
    }
}
