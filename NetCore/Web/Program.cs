using Common.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace NetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Information)
                .WriteTo.File(AppSettings.LoggerSettings.InfoPath, rollingInterval: RollingInterval.Day))
                .WriteTo.File(AppSettings.LoggerSettings.ErrorPath, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error)
                .CreateLogger();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
