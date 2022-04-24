using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.DataTransferObject;

namespace Almostengr.ThermometerPi.Api.Services
{
    public interface ITemperatureReadingService
    {
        Task AddReadingAsync(NwsLatestObservationDto observationDto);
        Task AddReadingAsync(Ds18b20Dto observationDto);
        Task<TemperatureDto> GetLatestExteriorReadingAsync();
        Task<TemperatureDto> GetLatestInteriorReadingAsync();
    }
}