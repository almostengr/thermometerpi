using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Almostengr.Thermometer.Worker.Console
{
    public class InstallServiceConsole : BaseConsole
    {
        private readonly ILogger<InstallServiceConsole> _logger;

        public InstallServiceConsole()
        {
        }

        public InstallServiceConsole(ILogger<InstallServiceConsole> logger)
        {
            _logger = logger;
        }

        public void RunInstaller()
        {
            try
            {
                _logger.LogInformation("Installing service");

                IList commands = new List<string>();
                commands.Add("sudo /bin/systemctl status falconpimonitor");
                commands.Add("sudo /bin/systemctl daemon-reload");
                commands.Add("sudo /bin/systemctl enable falconpimonitor");
                commands.Add("sudo /bin/systemctl start falconpimonitor");
                commands.Add("sudo /bin/systemctl status falconpimonitor");

                foreach (var command in commands)
                {
                    Process process = new Process()
                    {
                        StartInfo = new ProcessStartInfo()
                        {
                            FileName = "/bin/bash",
                            Arguments = string.Concat("-c \"", command, "\""),
                            RedirectStandardError = true,
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                        }
                    };

                    _logger.LogInformation($"{command}");

                    process.Start();

                    string result = process.StandardOutput.ReadToEnd();

                    process.WaitForExit();

                    _logger.LogInformation(result);

                    _logger.LogInformation("Done installing service");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}