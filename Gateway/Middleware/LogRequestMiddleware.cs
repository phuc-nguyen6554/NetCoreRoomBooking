using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Serilog;

namespace Gateway.Middleware
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public LogRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext ctx)
        {
            Log.Information("{method} {path} {id}", ctx.Request.Method, ctx.Request.Path, ctx.Request.Headers["request-correlation-id"]);
            await _next(ctx);
            Log.Information("Response {path} is {statusCode} {id}", ctx.Request.Path, ctx.Response.StatusCode, ctx.Request.Headers["request-correlation-id"]);
        }
    }
}
