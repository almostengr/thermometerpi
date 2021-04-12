using System;

namespace Almostengr.Thermometer.Worker.DataTransfer
{
    public class HaApiResponse
    {
        public string EntityId {get;set;}
        public string State { get; set; }
        public DateTime LastUpdated { get; set; }        
    }
}