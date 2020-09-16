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
        //public static void CreateLogger()
        //{
        //    Log.Logger = new LoggerConfiguration()
        //        .WriteTo.Console()
        //        .WriteTo.File(@"D:\NetCore\Gateway.API\testlog.txt")
        //        .WriteTo.Seq("http://localhost:5341")
        //        .CreateLogger();
        //}

        public static void ConfigureLogging(HostBuilderContext hostBuilderContext, ILoggingBuilder logging)
        {
            var configuration = hostBuilderContext.Configuration;

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(@"D:\NetCore\Gateway.API\log.txt")
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            logging.ClearProviders();
            logging.AddSerilog();
        }
    }
}
