namespace Almostengr.ThermometerPi.Api.DataTransferObject
{
    public class ThermometerDto
    {
        public ThermometerDto(string output)
        {
            var parts = output.Split(',');
            this.Fahrenheit = parts[0];
            this.Celsius = parts[1];
        }

        public string Fahrenheit { get; set; }
        public string Celsius { get; set; }
    }
}