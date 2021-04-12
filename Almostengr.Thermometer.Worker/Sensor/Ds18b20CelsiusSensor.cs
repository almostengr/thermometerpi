using System.Diagnostics;

namespace Almostengr.Thermometer.Worker.Sensor
{
    public class Ds18b20CelsiusSensor : IThermometerSensor
    {
        public string GetSensorData()
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "/bin/bash",
                    Arguments = string.Concat("-c \"", "digitemp_DS9097", "-a", "-q", ",-c", "/etc/digitemp.conf", "-o", "\"%s,%.2C\""),
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            return output;
        }
    }
}