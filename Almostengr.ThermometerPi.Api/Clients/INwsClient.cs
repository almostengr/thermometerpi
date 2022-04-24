using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.DataTransferObject;

namespace Almostengr.ThermometerPi.Api.Clients
{
    public interface INwsClient : IBaseClient
    {
        Task<NwsLatestObservationDto> GetLatestWeatherObservationAsync();
    }
}