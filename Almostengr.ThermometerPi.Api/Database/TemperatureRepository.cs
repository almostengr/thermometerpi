using System.Linq;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Constants;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Almostengr.ThermometerPi.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.ThermometerPi.Api.Database
{
    public class TemperatureRepository : ITemperatureRepository
    {
        private readonly ApiDbContext _dbContext;

        public TemperatureRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddReadingAsync(TemperatureReading reading)
        {
            await _dbContext.TemperatureReadings.AddAsync(reading);
        }
        
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public Task<TemperatureReading> DeleteReadingAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TemperatureDto> GetLatestExteriorReadingAsync()
        {
            return await _dbContext.TemperatureReadings
                .Where(r => r.Source == TemperatureSource.Exterior)
                .OrderByDescending(r => r.Timestamp)
                .Select(t => t.AsDto())
                .FirstOrDefaultAsync();
        }

        public async Task<TemperatureDto> GetLatestInteriorReadingAsync()
        {
            return await _dbContext.TemperatureReadings
                .Where(r => r.Source == TemperatureSource.Interior)
                .OrderByDescending(r => r.Timestamp)
                .Select(t => t.AsDto())
                .FirstOrDefaultAsync();
        }

    }
}