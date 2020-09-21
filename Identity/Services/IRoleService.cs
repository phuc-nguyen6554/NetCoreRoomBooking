using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.DTO.Role;
using Identity.DTO.User;
using Identity.Models.Users;

namespace Identity.Services
{
    public interface IRoleService
    {      

        public Task<RoleCreateResponse> CreateRoleAsync(RoleCreateRequest request);
        public Task<List<RoleListResponse>> GetRolesAsync();
        public Task<RoleDetailResponse> GetRoleDetail(int id);
        public Task<List<UserListResponse>> GetUserByRoleAsync(int roleId);
        public Task<PagedListResponse<RoleListResponse>> GetRolesPagedAsync();
    }
}
