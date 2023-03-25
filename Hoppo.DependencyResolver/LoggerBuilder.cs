using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Hoppo.DependencyResolver
{
    public static class LoggerBuilder
    {
        public static void BuildLogger(this ILoggingBuilder loggingBuilder, IHostBuilder hostBuilder)
        {
            string dateTime = DateTime.Now.ToShortDateString();

            loggingBuilder.ClearProviders();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"logs/log_{dateTime}.txt")
                .WriteTo.Seq("http://localhost:5341/")
                .MinimumLevel.Warning()
                .Enrich.FromLogContext()
                .CreateLogger();

            hostBuilder.UseSerilog();

        }
    }
}
