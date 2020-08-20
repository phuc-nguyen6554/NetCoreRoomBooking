using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth;
using Identity.Models;
using Identity.Models.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IdentityContext context;

        public AuthController(IConfiguration _configuration, IMapper _mapper, IdentityContext _context)
        {
            configuration = _configuration;
            mapper = _mapper;
            context = _context;
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
            var user = await context.UserData.Where(user => user.Email == payload.Email).FirstOrDefaultAsync();

            // User is existed
            if(user != null)
            {
                string jwt = this.GenerateJWT(user);

                var userDTO = mapper.Map<ReturnUserDTO>(user);
                userDTO.Token = jwt;

                return userDTO;
            }

            // User is not existed
            user = new User { Name = payload.Name, Email = payload.Email, Avatar = payload.Picture };
            context.UserData.Add(user);
            await context.SaveChangesAsync();

            string token = this.GenerateJWT(user);

            var user_DTO = mapper.Map<ReturnUserDTO>(user);
            user_DTO.Token = token;

            return user_DTO;
        }

        private string GenerateJWT(User user)
        {
            string JWTKey = configuration["JWT:secretKey"];
            string issuer = "localhost:5000";
            string audience = "localhost:5000";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Avatar", user.Avatar)
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
