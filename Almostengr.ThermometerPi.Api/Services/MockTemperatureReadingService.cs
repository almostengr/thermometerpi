using System;

namespace Almostengr.ThermometerPi.Api.Services
{
    public class MockSensorService : ISensorService
    {
        public string GetSensorData()
        {
            Random random = new Random();
            double randThermometerF = random.Next(79,100);
            double randThermometerC = random.Next(0,20);
            // return $"{randThermometerF},{randThermometerC}";
            return $"{randThermometerC}";
        }
    }
}