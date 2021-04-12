namespace Almostengr.ThermometerPi.Worker.Model
{
    public class AppSettings
    {
        public HomeAssistant HomeAssistant { get; set; }
    }

    public class HomeAssistant
    {
        public string HaUrl { get; set; }
        public string Route { get; set; }
        public string Token { get; set; }
    }
}