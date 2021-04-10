using System.Collections.Generic;
using Almostengr.Thermometer.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Almostengr.Thermometer.Api.Data
{
    public interface IThermometerRepository
    {
        IEnumerable<TemperatureReading> GetTemperatureReadings();
        TemperatureReading GetTemperatureReadingById(int id);
        // TemperatureReading GetLatestTemperatureReading();
        // TemperatureReading Save();
    }
}