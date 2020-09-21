using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.DTO.Role;
using Identity.DTO.User;
using Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IEnumerable<RoleListResponse>> GetRoles()
        {
            return await _service.GetRolesAsync();
        }

        [HttpPost]
        public async Task<RoleCreateResponse> CreateRoleAsync(RoleCreateRequest request)
        {
            return await _service.CreateRoleAsync(request);
        }

        [HttpGet("{id}")]
        public async Task<RoleDetailResponse> GetDetailAsync(int id)
        {
            return await _service.GetRoleDetail(id);
        }

        [HttpGet("get-users/{id}")]
        public async Task<IEnumerable<UserListResponse>> GetUser(int id)
        {
            var user = await _service.GetUserByRoleAsync(id);
            return user;
        }
    }
}
