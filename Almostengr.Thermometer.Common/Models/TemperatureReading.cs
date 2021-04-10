using System;

namespace Almostengr.Thermometer.Common.Models
{
    public class TemperatureReading
    {
        public int Id { get; set; }
        public string SensorId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Fahrenheit { get; set; }

        public void FromCommand(string[] reading)
        {
            SensorId = reading[0];
            Fahrenheit = double.Parse(reading[1]);
        }
    }
}