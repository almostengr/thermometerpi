using Almostengr.ThermometerPi.Worker.Console;
using Almostengr.ThermometerPi.Worker.Model;
using Almostengr.ThermometerPi.Worker.Sensor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ILogger logger 

            // switch (args[0])
            // {
            //     case "--installservice":
            //         InstallServiceConsole install = new InstallServiceConsole()
            //         install.RunInstaller();
            //         break;

            //     case "--deleteservice":
            //         UninstallServiceConsole uninstall = new UninstallServiceConsole();
            //         uninstall.RunUninstaller();
            //         break;

            //     default:
            //         break;
            // }
            
            CreateHostBuilder(args).Build().Run();
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
