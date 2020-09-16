using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Serilog;
using Shared.Cache;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gateway.OcelotAuth
{
    public class OcelotConfiguration
    {
        private readonly IConfiguration _configuration;
        public OcelotConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public OcelotPipelineConfiguration CreateConfig()
        {
            var configuration = new OcelotPipelineConfiguration
            {
                PreAuthenticationMiddleware = async (ctx, next) =>
                {
                    var guid = Guid.NewGuid();
                    ctx.Items.DownstreamRequest().Headers.Add("request-correlation-id", guid.ToString());
                    var scopedCache = ctx.RequestServices.GetRequiredService<IScopedCache>();
                    scopedCache.RCID = guid;
                    await next.Invoke();
                },
                AuthenticationMiddleware = async (ctx, next) =>
                {
                    if(ctx.Request.Path == "/login")
                    {
                        await next.Invoke();
                        return;
                    }

                    string token = ctx.Request.Headers["Authorization"];

                    if(token == null)
                    {
                        ctx.Items.SetError(new UnauthenticatedError("No Token Provided"));
                        return;
                    }

                    var url = _configuration["AuthEndpoint"];
                    HttpClient client = new HttpClient();                   
                    HttpRequestMessage message = new HttpRequestMessage
                    {             
                        RequestUri = new Uri(url),
                        Method = HttpMethod.Get,
                        Headers =
                        {
                            {"Authorization", token}
                        }
                    };

                    try
                    {
                        var response = await client.SendAsync(message);
                        response.EnsureSuccessStatusCode();

                        var body = await response.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<User>(body);

                        ctx.Items.DownstreamRequest().Headers.Add("X-Forwarded-Username", user.Name);
                        ctx.Items.DownstreamRequest().Headers.Add("X-Forwarded-Email", user.Email);
                        ctx.Items.DownstreamRequest().Headers.Add("X-Forwarded-Avatar", user.Avatar);      
                       
                    }
                    catch(Exception ex)
                    {
                        Log.Error(ex.Message);
                        ctx.Items.SetError(new UnauthenticatedError(ex.Message));
                        return;
                    }
                    await next.Invoke();
                },
                
            };

            return configuration;
        }
    }
}
