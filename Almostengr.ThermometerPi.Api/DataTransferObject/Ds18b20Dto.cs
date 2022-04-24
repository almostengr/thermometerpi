using System;

namespace Almostengr.ThermometerPi.Api.DataTransferObject
{
    public class Ds18b20Dto
    {
        public Ds18b20Dto(string output)
        {
            this.Celsius = Double.Parse(output);
        }

        public double Celsius { get; set; }
    }
}