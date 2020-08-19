using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth;
using Identity.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private UserManager<ApplicationUser> userManager;

        public AuthController(IConfiguration _configuration, IMapper _mapper, UserManager<ApplicationUser> _userManager)
        {
            configuration = _configuration;
            mapper = _mapper;
            userManager = _userManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ReturnUserDTO>> Login([FromBody] string token_id)
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

        private async Task<ReturnUserDTO> FindOrAddUser(GoogleJsonWebSignature.Payload payload)
        {
            // Find user
            var user = await userManager.FindByEmailAsync(payload.Email);

            // If not exist => add to db
            if(user == null)
            {
                var userData = new RegistrationUserModel
                {
                    Email = payload.Email,
                    UserName = payload.Name
                };

                var registrationUser = mapper.Map<ApplicationUser>(userData);

                var result = await userManager.CreateAsync(registrationUser);

                if(result.Succeeded)
                {
                    var returnUser = mapper.Map<ReturnUserDTO>(registrationUser);
                    returnUser.Token = GenerateJWT(returnUser.UserName, "User", returnUser.Email);

                    return returnUser;
                }
                else
                {
                    return null;
                }
            }
            var return_User = mapper.Map<ReturnUserDTO>(user);
            return_User.Token = GenerateJWT(return_User.UserName, "User", return_User.Email);

            return return_User;
        }

        private string GenerateJWT(string UserName, string Role, string Email)
        {
            string JWTKey = configuration["JWT:secretKey"];
            string issuer = "localhost:5000";
            string audience = "localhost:5000";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserName),
                    new Claim(ClaimTypes.Role, Role),
                    new Claim(ClaimTypes.Email, Email)
                };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: authClaim,
                signingCredentials: credential,
                expires: DateTime.Now.AddHours(1)
            );

            var TokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return TokenString;
        }
    }
}
