namespace Feeder.Web.API
{
    using Autofac.Extensions.DependencyInjection;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Serilog;
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .WriteTo.File("./Logs/log.log",
                                            outputTemplate: "{ Timestamp: yyyy - MM - dd HH: mm: ss.fff zzz} [{Level:u3}] {Message:lj}" +
                                            "{NewLine}{Exception}")
                            .CreateLogger();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .ConfigureServices(services => services.AddAutofac())
            .Build();
    }
}
