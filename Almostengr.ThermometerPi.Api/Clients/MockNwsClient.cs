using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Almostengr.ThermometerPi.Api.DataTransferObject;

namespace Almostengr.ThermometerPi.Api.Clients
{
    public class MockNwsClient : BaseClient, INwsClient
    {
        public MockNwsClient(ILogger<BaseClient> logger) : base(logger)
        {
        }

        public async Task<NwsLatestObservationDto> GetLatestWeatherObservationAsync()
        {
            Random random = new();
            await Task.Delay(TimeSpan.FromSeconds(random.Next(1, 3)));
            
            return new NwsLatestObservationDto
            {
                Properties = new NwsObservationProperties
                {
                    Temperature = new NwsObservationTemperature
                    {
                        Value = random.Next(0, 35)
                    }
                }
            };
        }

    }
}