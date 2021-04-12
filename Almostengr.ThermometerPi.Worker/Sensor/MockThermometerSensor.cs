using System;

namespace Almostengr.ThermometerPi.Worker.Sensor
{
    public class MockThermostatSensor : IThermometerSensor
    {
        public string GetSensorData()
        {
            Random random = new Random();
            double randTemperature = random.Next(32,100);
            return $"0,{randTemperature}";
        }
    }
}