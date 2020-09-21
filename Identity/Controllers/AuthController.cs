using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DTO.User;
using Identity.Services;
using Identity.Models.Users;
using Identity.DTO.User;

namespace Identity.Controllers
{
    [Route("identities")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDetailResponse>> Login([FromBody] string token_id)
        {
            try
            {
                // Validate token
                var payload = GoogleJsonWebSignature.ValidateAsync(token_id, new GoogleJsonWebSignature.ValidationSettings()).Result;

                var user = await FindOrAddUser(payload);

                return user;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize]
        [HttpGet("get-user")]
        public ActionResult<UserDetailResponse> GetUserData()
        {
            string username = User.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            string email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            string avatar = User.Claims.Where(x => x.Type == "Avatar").FirstOrDefault()?.Value;

            var user = new UserDetailResponse
            {
                Name = username,
                Email = email,
                Avatar = avatar,
                Token = Request.Headers["Authorization"]
            };

            return user;
        }

        private async Task<UserDetailResponse> FindOrAddUser(GoogleJsonWebSignature.Payload payload)
        {
            var user = new UserCreateRequest
            {
                Name = payload.Name,
                Email = payload.Email,
                Avatar = payload.Picture
            };

            return await _service.FindOrAddUserAsync(user);
        }       
    }
}
