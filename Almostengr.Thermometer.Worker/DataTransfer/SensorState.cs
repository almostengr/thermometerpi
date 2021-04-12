namespace Almostengr.Thermometer.Worker.DataTransfer
{
    public class SensorState
    {
        public SensorState(string state)
        {
            State = state;
        }
        
        public string State { get; set; }
    }
}