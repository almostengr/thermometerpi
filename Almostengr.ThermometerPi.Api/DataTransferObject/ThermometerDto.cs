using System;

namespace Almostengr.ThermometerPi.Api.DataTransferObject
{
    public class ThermometerDto
    {
        public ThermometerDto(string output)
        {
            output = output.Replace("\n", string.Empty);
            var parts = output.Split(',');
            this.Fahrenheit = Double.Parse(parts[0]);
            this.Celsius = Double.Parse(parts[1]);
        }

        public double Fahrenheit { get; set; }
        public double Celsius { get; set; }
    }
}