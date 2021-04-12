using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Almostengr.Thermometer.Worker.Console
{
    public class UninstallServiceConsole : BaseConsole
    {
        private readonly ILogger<UninstallServiceConsole> _logger;

        public UninstallServiceConsole(ILogger<UninstallServiceConsole> logger)
        {
            _logger = logger;
        }

        public void RunUninstaller()
        {
            try
            {
                IList commands = new List<string>();
                commands.Add("sudo /bin/systemctl status falconpimonitor");
                commands.Add("sudo /bin/systemctl stop falconpimonitor");
                commands.Add("sudo /bin/systemctl disable falconpimonitor");
                commands.Add("sudo /bin/systemctl status falconpimonitor");
                commands.Add("sudo /bin/systemctl daemon-reload");

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
                }

                _logger.LogInformation("Removing service file");
                File.Delete(string.Concat(SystemdDirectory, "/", ServiceFilename));

                _logger.LogInformation("Done uninstalling service");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}