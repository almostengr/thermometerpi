namespace Almostengr.ThermometerPi.Worker.DataTransfer
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