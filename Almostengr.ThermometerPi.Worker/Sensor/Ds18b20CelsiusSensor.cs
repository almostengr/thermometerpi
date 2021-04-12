using System.Diagnostics;

namespace Almostengr.ThermometerPi.Worker.Sensor
{
    public class Ds18b20CelsiusSensor : IThermometerSensor
    {
        public string GetSensorData()
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "/usr/bin/digitemp_DS9097",
                    Arguments = $"-a -q -c /etc/digitemp.conf -o \"%s,%.2C\"",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            process.Start();
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();

            return output;
        }
    }
}