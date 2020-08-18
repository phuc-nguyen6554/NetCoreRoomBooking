using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

        [HttpPost("Register")]
        public async Task<ActionResult<ReturnUserDTO>> Register(RegistrationUserModel user)
        {
            if (user == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(user);
            }

            var registrationUser = mapper.Map<ApplicationUser>(user);
            var result = await userManager.CreateAsync(registrationUser, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Registration not success");
            }
            else
            {
                await userManager.AddToRoleAsync(registrationUser, "User");
                string JWT_token = GenerateJWT(user.UserName, "User", user.Email);

                ReturnUserDTO returnUser = mapper.Map<ReturnUserDTO>(registrationUser);
                returnUser.Token = JWT_token;
                return returnUser;
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ReturnUserDTO>> Login(LoginUser modelUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("User is required");
            }

            var user = await userManager.FindByNameAsync(modelUser.UserName);

            if (user != null && await userManager.CheckPasswordAsync(user, modelUser.Password))
            {
                var roles = await userManager.GetRolesAsync(user);
                string JWT_Token = GenerateJWT(modelUser.UserName, roles[0], user.Email);

                ReturnUserDTO returnUser = mapper.Map<ReturnUserDTO>(user);
                returnUser.Token = JWT_Token;

                return returnUser;
            }

            return Unauthorized("Username or Password is not correct");
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
