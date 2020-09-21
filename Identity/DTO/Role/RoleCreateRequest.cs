using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.DTO.Role
{
    public class RoleCreateRequest
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}
