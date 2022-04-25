using System.Net.Http;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Api.Clients
{
    public class NwsClient : BaseClient, INwsClient
    {
        private readonly HttpClient _httpClient;

        public NwsClient(ILogger<NwsClient> logger) : base(logger)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36");
        }

        public async Task<NwsLatestObservationDto> GetLatestWeatherObservationAsync()
        {
            const string route = "https://api.weather.gov/stations/KMGM/observations/latest";
            return await HttpGetAsync<NwsLatestObservationDto>(_httpClient, route);
        }
    }
}