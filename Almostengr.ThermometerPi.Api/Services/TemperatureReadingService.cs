using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Common;
using Almostengr.ThermometerPi.Api.Database;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Almostengr.ThermometerPi.Api.Models;

namespace Almostengr.ThermometerPi.Api.Services
{
    public class TemperatureReadingService : ITemperatureReadingService
    {
        private readonly ITemperatureRepository _repository;

        public TemperatureReadingService(ITemperatureRepository repository)
        {
            _repository = repository;
        }

        public async Task AddReadingAsync(NwsLatestObservationDto observationDto)
        {
            TemperatureReading reading = 
                new TemperatureReading((int) observationDto.Properties.Temperature.Value, TemperatureSource.Exterior);

            await _repository.AddReadingAsync(reading);
            await _repository.SaveChangesAsync();
        }

        public async Task AddReadingAsync(Ds18b20Dto observationDto)
        {
            TemperatureReading reading = 
                new TemperatureReading((int) observationDto.Celsius, TemperatureSource.Interior);

            await _repository.AddReadingAsync(reading);
            await _repository.SaveChangesAsync();
        }

        public async Task<TemperatureDto> GetLatestExteriorReadingAsync()
        {
            return await _repository.GetLatestExteriorReadingAsync();
        }

        public async Task<TemperatureDto> GetLatestInteriorReadingAsync()
        {
            return await _repository.GetLatestInteriorReadingAsync();
        }
    }
}