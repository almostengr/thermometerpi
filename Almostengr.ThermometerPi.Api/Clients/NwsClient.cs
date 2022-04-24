using System.Net.Http;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Api.Clients
{
    public class NwsClient : BaseClient, INwsClient
    {
        private readonly HttpClient _httpClient;

        public NwsClient(ILogger<BaseClient> logger) : base(logger)
        {
            _httpClient = new HttpClient();
        }

        public async Task<NwsLatestObservationDto> GetLatestWeatherObservationAsync()
        {
            const string route = "https://api.weather.gov/stations/KMGM/observations/latest";
            return await HttpGetAsync<NwsLatestObservationDto>(_httpClient, route);
        }
    }
}