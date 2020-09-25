using System;
using System.Collections.Generic;
using System.Text;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Shared.Mail
{
    public static class MailService
    {
        public static async Task SendMail(string toEmail, string Subject ,string Content)
        {
            MailjetClient client = new MailjetClient("7e9bf4b62098b1ad35b6d7a083e90bb1", "6bc6095041a4e29292e2b32d5117c637")
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
                 new JObject {
                  {
                   "From",
                   new JObject {
                    {"Email", "phuc.nguyen@siliconstack.com.au"},
                    {"Name", "John"}
                   }
                  }, 
                  {
                   "To",
                   new JArray {
                    new JObject {
                     {
                      "Email",
                      toEmail
                     }, {
                      "Name",
                      "John"
                     }
                    }
                   }
                  }, {
                   "Subject",
                   Subject
                  }, {
                   "TextPart",
                   "My first Mailjet email"
                  }, {
                   "HTMLPart",
                   Content
                  }, {
                   "CustomID",
                   "AppGettingStartedTest"
                  }
                 }
             });
            MailjetResponse response = await client.PostAsync(request);
        }
    }

    
}
