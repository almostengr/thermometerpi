using System;
using System.Threading;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Clients;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Almostengr.ThermometerPi.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Api.Workers
{
    public class NwsLatestWorker : BackgroundService
    {
        private readonly ILogger<NwsLatestWorker> _logger;
        private readonly INwsClient _nwsClient;
        private readonly ITemperatureReadingService _temperatureReadingService;

        public NwsLatestWorker(IServiceScopeFactory factory, ILogger<NwsLatestWorker> logger)
        {
            _logger = logger;
            _nwsClient = factory.CreateScope().ServiceProvider.GetRequiredService<INwsClient>();
            _temperatureReadingService = factory.CreateScope().ServiceProvider.GetRequiredService<ITemperatureReadingService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                NwsLatestObservationDto observationDto = await _nwsClient.GetLatestWeatherObservationAsync();
                _logger.LogInformation($"Exterior Temperature: {observationDto.Properties.Temperature.Value}C");
                await _temperatureReadingService.AddReadingAsync(observationDto);

                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }
}