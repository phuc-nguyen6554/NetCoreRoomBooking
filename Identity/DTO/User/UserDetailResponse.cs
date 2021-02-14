using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DTO.User
{
    public class UserDetailResponse
    {
        int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string RoleName { get; set; }

        public string Token { get; set; }
    }
}
