using System;
using System.Collections.Generic;
using Almostengr.Thermometer.Common.Models;

namespace Almostengr.Thermometer.Api.Data
{
    public class MockThermometerRepository : IThermometerRepository
    {
        public TemperatureReading GetTemperatureReadingById(int id)
        {
            return new TemperatureReading{
                Id = 0,
                SensorId = "0",
                Fahrenheit = 45.9,
                Timestamp = DateTime.Now
            };
        }

        public IEnumerable<TemperatureReading> GetTemperatureReadings()
        {
            var temperatureReadings = new List<TemperatureReading>
            {
                new TemperatureReading{Id = 0, SensorId = "0",Fahrenheit = 45.9,Timestamp = DateTime.Now},
                new TemperatureReading{Id = 1, SensorId = "0",Fahrenheit = 35.9,Timestamp = DateTime.Now.AddHours(-7)},
                new TemperatureReading{Id = 2, SensorId = "0",Fahrenheit = 25.9,Timestamp = DateTime.Now.AddDays(-1)},
                new TemperatureReading{Id = 3, SensorId = "0",Fahrenheit = 55.9,Timestamp = DateTime.Now.AddHours(-14)},
            };

            return temperatureReadings;
        }
    }
}