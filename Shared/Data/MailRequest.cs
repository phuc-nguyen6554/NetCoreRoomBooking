using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Data
{
    public class MailRequest
    {
        public string Email { get; set; }
        public List<string>? Cc { get; set; } = new List<string>();
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
