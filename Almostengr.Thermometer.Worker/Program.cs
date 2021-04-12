using Almostengr.Thermometer.Worker.Console;
using Almostengr.Thermometer.Worker.Model;
using Almostengr.Thermometer.Worker.Sensor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Almostengr.Thermometer.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ILogger logger 

            switch (args[0])
            {
            //     case "--installservice":
            //         InstallServiceConsole install = new InstallServiceConsole()
            //         install.RunInstaller();
            //         break;

            //     case "--deleteservice":
            //         UninstallServiceConsole uninstall = new UninstallServiceConsole();
            //         uninstall.RunUninstaller();
            //         break;

                default:
                    CreateHostBuilder(args).Build().Run();
                    break;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    HomeAssistant haOptions = configuration.GetSection("HomeAssistant").Get<HomeAssistant>();
                    services.AddSingleton(haOptions);

                    services.AddHostedService<ThermometerWorker>();
                    services.AddSingleton<IThermometerSensor, MockThermostatSensor>();
                });
    }
}
