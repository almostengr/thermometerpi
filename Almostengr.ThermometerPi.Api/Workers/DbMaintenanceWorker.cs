using System;
using System.Threading;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Almostengr.ThermometerPi.Api.Workers
{
    public class DbMaintenanceWorker : BackgroundService
    {
        private readonly ITemperatureReadingService _temperatureReadingService;

        // public DbMaintenanceWorker(ITemperatureReadingService temperatureReadingService)
        public DbMaintenanceWorker(IServiceScopeFactory factory)
        {
            _temperatureReadingService = factory.CreateScope().ServiceProvider.GetRequiredService<ITemperatureReadingService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _temperatureReadingService.DeleteOldReadingsAsync();
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}