using AutoMapper;
using Identity.DTO.Role;
using Identity.DTO.User;
using Identity.Models;
using Identity.Models.Roles;
using Identity.Models.Users;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Services.Implements
{
    public class RoleService : IRoleService
    {
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;

        public RoleService(IdentityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoleCreateResponse> CreateRoleAsync(RoleCreateRequest request)
        {
            var model = _mapper.Map<Role>(request);

            _context.Roles.Add(model);

            await _context.SaveChangesAsync();

            return _mapper.Map<RoleCreateResponse>(model);
        }

        public async Task<RoleDetailResponse> GetRoleDetail(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
                throw new ServiceException(404, "Data Not Found");

            return _mapper.Map<RoleDetailResponse>(role);
        }

        public async Task<List<RoleListResponse>> GetRolesAsync()
        {
            var roles = await _context.Roles.ToPagedList(1, 10);
            // return roles;
            var response =  _mapper.Map<List<RoleListResponse>>(roles);
            return new PagedList<RoleListResponse>(response, roles.TotalCount, roles.CurrentPage, roles.PageSize);
        }

        public async Task<List<UserListResponse>> GetUserByRoleAsync(int roleId)
        {
            var role = await _context.Roles.Where(x => x.Id == roleId).Include(x => x.User).FirstOrDefaultAsync();         
            return _mapper.Map<List<UserListResponse>>(role.User);          
        }
    }
}
