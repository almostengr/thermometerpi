using System;
using Almostengr.ThermometerPi.Api.DataTransferObject;

namespace Almostengr.ThermometerPi.Api.Models
{
    public class TemperatureReading
    {
        public TemperatureReading(int temperatureC, string Source)
        {
            this.TemperatureC = temperatureC;
            this.Source = Source;
            this.Timestamp = DateTime.Now;
            this.TemperatureF = (int) (temperatureC * 1.8 + 32);
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Source { get; set; }

        internal TemperatureDto AsDto()
        {
            return new TemperatureDto(this.TemperatureF, this.TemperatureC);
        }
    }
}