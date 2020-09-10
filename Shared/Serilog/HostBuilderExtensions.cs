using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Serilog
{
    public static class HostBuilderExtensions
    {
        public static void CreateLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(@"D:\NetCore\Gateway.API\log.txt")
                //.WriteTo.Seq("http://192.168.1.200:5341", apiKey: "cq1yUQGgrvyVKBJfnS9o")
                .CreateLogger();
        }

        public static void ConfigureLogging(HostBuilderContext hostBuilderContext, ILoggingBuilder logging)
        {
            var configuration = hostBuilderContext.Configuration;

            Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File(@"D:\NetCore\Gateway.API\log.txt").CreateLogger();

            logging.ClearProviders();
            logging.AddSerilog();
        }
    }
}
