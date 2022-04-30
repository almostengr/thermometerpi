using System;

namespace Almostengr.ThermometerPi.Api.Services
{
    public class MockSensorService : ISensorService
    {
        public string GetSensorData()
        {
            Random random = new Random();
            double randThermometerC = random.Next(0,20);
            return $"{randThermometerC}";
        }
    }
}