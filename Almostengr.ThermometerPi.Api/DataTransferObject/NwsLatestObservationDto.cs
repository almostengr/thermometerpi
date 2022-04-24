namespace Almostengr.ThermometerPi.Api.DataTransferObject
{
    public class NwsLatestObservationDto
    {
        public NwsObservationProperties Properties { get; set; }
    }

    public class NwsObservationProperties
    {
        public NwsObservationTemperature Temperature { get; set; }
    }

    public class NwsObservationTemperature
    {
        public double Value { get; set; }
        public string UnitCode { get; set; }
    }
}