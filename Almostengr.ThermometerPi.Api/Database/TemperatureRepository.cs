using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Almostengr.ThermometerPi.Api.Enums;
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

        public async Task<TemperatureDto> GetLatestExteriorReadingAsync()
        {
            return await _dbContext.TemperatureReadings
                .Where(r => r.Source == (int) TemperatureSource.Exterior && r.Timestamp >= DateTime.Now.AddHours(-2))
                .OrderByDescending(r => r.Timestamp)
                .Select(t => t.AsDto())
                .FirstOrDefaultAsync();
        }

        public async Task<TemperatureDto> GetLatestInteriorReadingAsync()
        {
            return await _dbContext.TemperatureReadings
                .Where(r => r.Source == (int) TemperatureSource.Interior && r.Timestamp >= DateTime.Now.AddHours(-2))
                .OrderByDescending(r => r.Timestamp)
                .Select(t => t.AsDto())
                .FirstOrDefaultAsync();
        }

        public async Task DeleteOldReadingsAsync()
        {
            var readings = await _dbContext.TemperatureReadings
                .Where(r => r.Timestamp < DateTime.Now.AddDays(-1))
                .ToListAsync();

            _dbContext.TemperatureReadings.RemoveRange(readings);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<TemperatureDto>> GetAllReadingsAsync()
        {
            return await _dbContext.TemperatureReadings
                .OrderByDescending(r => r.Timestamp)
                .Select(t => t.AsDto())
                .ToListAsync();
        }

        public async Task<TemperatureDto> GetMinInteriorReadingAsync()
        {
            return await _dbContext.TemperatureReadings
                .Where(r => r.Source == (int) TemperatureSource.Interior)
                .OrderBy(r => r.TemperatureF)
                .Select(t => t.AsDto())
                .FirstOrDefaultAsync();
        }

        public async Task<TemperatureDto> GetMaxInteriorReadingAsync()
        {
            return await _dbContext.TemperatureReadings
                .Where(r => r.Source == (int) TemperatureSource.Interior)
                .OrderByDescending(r => r.TemperatureF)
                .Select(t => t.AsDto())
                .FirstOrDefaultAsync();
        }
    }
}