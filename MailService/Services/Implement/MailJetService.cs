using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;
using Shared.Exceptions;
using Shared.Data;
using MailService.DTO;

namespace MailService.Services.Implement
{
    public class MailJetService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly MailjetClient _client;

        public MailJetService(IConfiguration configuration)
        {
            _configuration = configuration;

            _client = new MailjetClient(_configuration["MailJet:ApiKey"], _configuration["MailJet:SecretKey"])
            {
                Version = ApiVersion.V3_1,
            };
        }
        public async Task SendMailAsync(SingleMailRequest mailRequest)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            }.Property(Send.Messages, new JArray {
                new JObject{
                    {
                        "From", new JObject {
                            {"Email", "phuc.nguyen@siliconstack.com.au" }
                        }
                    },
                    {
                        "To",
                        new JArray
                        {
                            new JObject{
                                {"Email", mailRequest.Email }
                            }
                        }
                    },
                    {
                        "Cc",
                        CreateArray("Email", mailRequest.Cc)
                    },
                    {
                        "Subject", mailRequest.Subject
                    },
                    {
                        "TextPart", mailRequest.Content
                    }
                },

            });
            await SendProccess(request);
        }

        public async Task SendMailAsync(MailRequest mailRequest)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            }.Property(Send.Messages, new JArray {
                new JObject{
                    {
                        "From", new JObject {
                            {"Email", "phuc.nguyen@siliconstack.com.au" }
                        }
                    },
                    {
                        "To",
                        new JArray
                        {
                            new JObject{
                                {"Email", mailRequest.Email }
                            }
                        }
                    },
                    {
                        "Cc",
                        CreateArray("Email", mailRequest.Cc.ToArray())
                    },
                    {
                        "Subject", mailRequest.Subject
                    },
                    {
                        "TextPart", mailRequest.Content
                    }
                },

            });
            await SendProccess(request);
        }



        public async Task SendMailAsync(MulitpleMailRequest mailRequest)
        {
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            }.Property(Send.Messages, new JArray {
                new JObject{
                    {
                        "From", new JObject {"Email", "phuc.nguyen@siliconstack.com.au"}
                    },
                    {
                        "To",
                        CreateArray("Email", mailRequest.Email)
                    },
                    {
                        "Cc",
                        CreateArray("Email", mailRequest.Cc)
                    },
                    {
                        "Subject", mailRequest.Subject
                    },
                    {
                        "TextPart", mailRequest.Content
                    }
                },

            });

            await SendProccess(request);
        }

        private JArray CreateArray(string key, string[] items)
        {
            var jArray = new JArray();
            if(items == null || items.Length == 0)
            {
                return jArray;
            }

            foreach (var item in items)
            {
                jArray.Add(new JObject { { key, item } });
            }

            return jArray;
        }

        private async Task SendProccess(MailjetRequest request)
        {
            var response = await _client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new ServiceException(response.StatusCode, response.GetErrorMessage());
            }               
        }
    }
}
