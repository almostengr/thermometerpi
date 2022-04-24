using System;

namespace Almostengr.ThermometerPi.Api.DataTransferObject
{
    public class TemperatureDto
    {
        public TemperatureDto(int fahrenheit, int celsius)
        {
            this.Fahrenheit = fahrenheit;
            this.Celsius = celsius;
        }

        public int Fahrenheit { get; set; }
        public int Celsius { get; set; }
    }
}