using System;
using System.Threading;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Almostengr.ThermometerPi.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Api.Workers
{
    public class InteriorLatestWorker : BackgroundService
    {
        private readonly ILogger<InteriorLatestWorker> _logger;
        private readonly ISensorService _sensorService;
        private readonly ITemperatureReadingService _temperatureReadingService;

        public InteriorLatestWorker(IServiceScopeFactory factory, ILogger<InteriorLatestWorker> logger)
        {
            _logger = logger;
            _sensorService = factory.CreateScope().ServiceProvider.GetRequiredService<ISensorService>();
            _temperatureReadingService = factory.CreateScope().ServiceProvider.GetRequiredService<ITemperatureReadingService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Ds18b20Dto ds18b20Dto = new(_sensorService.GetSensorData());
                _logger.LogInformation($"Interior Temperature: {ds18b20Dto.Celsius}C");
                await _temperatureReadingService.AddReadingAsync(ds18b20Dto);

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}