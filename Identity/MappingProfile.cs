using AutoMapper;
using Identity.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationUserModel, ApplicationUser>();
            CreateMap<ApplicationUser, ReturnUserDTO>();
        }
    }
}
