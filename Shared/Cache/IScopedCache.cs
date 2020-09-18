using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Cache
{
    public interface IScopedCache
    {
        public Guid RCID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
