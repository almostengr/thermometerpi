namespace Almostengr.ThermometerPi.Worker.Console
{
    public abstract class BaseConsole
    {
        public string SystemdDirectory = "/lib/systemd/system";
        public string ServiceFilename = "falconpimonitor.service";
        public string AppDirectory =
            System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
    }
}