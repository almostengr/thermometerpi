using Almostengr.Thermometer.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Thermometer.Api.Data
{
    public class ThermometerContext : DbContext
    {
        public ThermometerContext()
        {
        }

        public ThermometerContext(DbContextOptions<ThermometerContext> options) : base(options) { }

        public DbSet<TemperatureReading> ThermometerReading { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     // code that configures the dbcontext goes here
        //     base.OnConfiguring(optionsBuilder);
        // }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     // code that configures the DbSet entities goes here
        //     base.OnModelCreating(modelBuilder);
        // }
    }
}