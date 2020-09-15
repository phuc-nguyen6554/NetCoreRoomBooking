using AutoMapper;
using DTO.User;
using Identity.Models;
using Identity.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IdentityContext _context;
        private readonly IConfiguration _configuration;
        public UserService(IdentityContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserDetailResponse> FindOrAddUserAsync(UserCreateRequest request)
        {
            var user = await _context.UserData.Where(x => x.Email == request.Email).FirstOrDefaultAsync();

            if(user == null)
            {
                user = new User
                {
                    Email = request.Email,
                    Name = request.Name,
                    Avatar = request.Avatar
                };

                _context.UserData.Add(user);
                await _context.SaveChangesAsync();
            }

            var response = _mapper.Map<UserDetailResponse>(user);
            response.Token = GenerateJWT(request);

            return response;
        }

        private string GenerateJWT(UserCreateRequest user)
        {
            string JWTKey = _configuration["JWT:secretKey"];
            string issuer = _configuration["JWT:issuer"];
            string audience = _configuration["JWT:audience"];

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
                expires: DateTime.Now.AddDays(3)
            );

            var TokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return TokenString;
        }
    }
}
