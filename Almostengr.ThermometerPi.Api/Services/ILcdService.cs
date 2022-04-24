namespace Almostengr.ThermometerPi.Api.Services
{
    public interface ILcdService
    {
        void WriteLines(string line1, string line2);
        void Clear();
    }
}