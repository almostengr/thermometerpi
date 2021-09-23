using Almostengr.ThermometerPi.Worker.Model;
using Almostengr.ThermometerPi.Worker.Sensor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Almostengr.ThermometerPi.Worker
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
                .UseContentRoot(
                    System.IO.Path.GetDirectoryName(
                        System.Reflection.Assembly.GetExecutingAssembly().Location))
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    HomeAssistant haOptions = configuration.GetSection("HomeAssistant").Get<HomeAssistant>();
                    services.AddSingleton(haOptions);

                    services.AddHostedService<ThermometerWorker>();
                    services.AddSingleton<IThermometerSensor, Ds18b20FahrenheitSensor>();
                    // services.AddSingleton<IThermometerSensor, MockThermostatSensor>();
                });
    }
}
