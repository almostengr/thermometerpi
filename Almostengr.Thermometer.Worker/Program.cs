using Almostengr.Thermometer.Worker.Model;
using Almostengr.Thermometer.Worker.Sensor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Almostengr.Thermometer.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
