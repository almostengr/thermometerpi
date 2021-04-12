using System.Diagnostics;

namespace Almostengr.ThermometerPi.Worker.Sensor
{
    public class Ds18b20FahrenheitSensor : IThermometerSensor
    {
        public string GetSensorData()
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "/bin/bash",
                    Arguments = string.Concat("-c \"", "digitemp_DS9097", "-a", "-q", ",-c", "/etc/digitemp.conf", "-o", "\"%s,%.2F\""),
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