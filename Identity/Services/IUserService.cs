using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Services
{
    public interface IUserService
    {
        public Task<UserDetailResponse> FindOrAddUserAsync(UserCreateRequest request);
    }
}
