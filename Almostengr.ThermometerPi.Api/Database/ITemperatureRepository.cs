using System.Collections.Generic;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Almostengr.ThermometerPi.Api.Models;

namespace Almostengr.ThermometerPi.Api.Database
{
    public interface ITemperatureRepository
    {
        Task<TemperatureDto> GetLatestInteriorReadingAsync();
        Task<TemperatureDto> GetLatestExteriorReadingAsync();
        Task AddReadingAsync(TemperatureReading reading);
        Task DeleteOldReadingsAsync();
        Task SaveChangesAsync();
        Task<List<TemperatureDto>> GetAllReadingsAsync();
        Task<TemperatureDto> GetMinInteriorReadingAsync();
        Task<TemperatureDto> GetMaxInteriorReadingAsync();
    }
}