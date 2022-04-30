using System;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Almostengr.ThermometerPi.Api.Enums;

namespace Almostengr.ThermometerPi.Api.Models
{
    public class TemperatureReading
    {
        public TemperatureReading(int temperatureC, TemperatureSource source)
        {
            this.TemperatureC = temperatureC;
            this.Source = (int)source;
            this.Timestamp = DateTime.Now;
            this.TemperatureF = (int)(temperatureC * 1.8 + 32);
        }

        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public int Source { get; set; }

        internal TemperatureDto AsDto()
        {
            return new TemperatureDto(
                this.TemperatureF,
                this.TemperatureC,
                (TemperatureSource)this.Source,
                this.Timestamp);
        }
    }
}