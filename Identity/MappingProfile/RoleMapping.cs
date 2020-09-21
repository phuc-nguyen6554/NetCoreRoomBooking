using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.DTO.Role;
using Identity.Models.Roles;

namespace Identity.MappingProfile
{
    public class RoleMapping:Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleCreateResponse>();
            CreateMap<Role, RoleDetailResponse>();
            CreateMap<Role, RoleListResponse>();
            CreateMap<RoleCreateRequest, Role>();
        }
    }
}
