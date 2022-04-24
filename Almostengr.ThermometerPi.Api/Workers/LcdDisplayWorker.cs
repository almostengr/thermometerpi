using System;
using System.Threading;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Services;
using Microsoft.Extensions.Hosting;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Microsoft.Extensions.DependencyInjection;

namespace Almostengr.ThermometerPi.Api.Workers
{
    public class LcdDisplayWorker : BackgroundService
    {
        private readonly ITemperatureReadingService _temperatureReadingService;
        private readonly ILcdService _lcdService;

        public LcdDisplayWorker(IServiceScopeFactory factory)
        {
            _temperatureReadingService = factory.CreateScope().ServiceProvider.GetRequiredService<ITemperatureReadingService>();
            _lcdService = factory.CreateScope().ServiceProvider.GetRequiredService<ILcdService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _lcdService.Clear();
            _lcdService.WriteLines("Starting up...");
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                TemperatureDto exteriorTemp = await _temperatureReadingService.GetLatestExteriorReadingAsync();
                TemperatureDto interiorTemp = await _temperatureReadingService.GetLatestInteriorReadingAsync();

                _lcdService.Clear();
                _lcdService.WriteLines($"I: {interiorTemp.Fahrenheit.ToString()}F  E: {exteriorTemp.Fahrenheit.ToString()}F", 
                    $"{DateTime.Now.ToString("ddd MM/dd HH:mm")}");

                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }

    } // end class 
}
