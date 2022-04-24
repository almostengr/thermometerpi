using Almostengr.ThermometerPi.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.ThermometerPi.Api.Database
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TemperatureReading> TemperatureReadings { get; set; }
    }
}