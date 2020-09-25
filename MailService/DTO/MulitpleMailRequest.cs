using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailService.DTO
{
    public class MulitpleMailRequest
    {
        public string [] Email { get; set; }
        public string?[] Cc { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
