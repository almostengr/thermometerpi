using System;
using Almostengr.ThermometerPi.Api.Enums;

namespace Almostengr.ThermometerPi.Api.DataTransferObject
{
    public class TemperatureDto
    {
        public TemperatureDto(int fahrenheit, int celsius, TemperatureSource source, DateTime timestamp)
        {
            this.Fahrenheit = fahrenheit;
            this.Celsius = celsius;
            this.Source = source;
            this.Timestamp = timestamp;
        }

        public int Fahrenheit { get; set; }
        public int Celsius { get; set; }
        public TemperatureSource Source { get; set; }
        public DateTime Timestamp { get; set; }
    }
}