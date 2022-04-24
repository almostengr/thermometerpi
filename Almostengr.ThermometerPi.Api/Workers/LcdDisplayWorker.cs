using System;
using System.Threading;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Services;
using Microsoft.Extensions.Hosting;

namespace Almostengr.ThermometerPi.Api.Workers
{
    public class LcdDisplayWorker : BackgroundService
    {
        private readonly ITemperatureService _temperatureService;
        private readonly ILcdService _lcdService;

        public LcdDisplayWorker(ITemperatureService temperatureService, ILcdService lcdService)
        {
            _temperatureService = temperatureService;
            _lcdService = lcdService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int fahrenheit = Int32.Parse(_temperatureService.GetSensorData().Split(',')[0]);
                _lcdService.Clear();
                _lcdService.WriteLines($"{fahrenheit} F", $"{DateTime.Now.ToString("MM/dd/yy HH:mm")}");

                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    } // end class 
}