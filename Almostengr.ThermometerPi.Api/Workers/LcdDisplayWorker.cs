using System;
using System.Device.Gpio;
using System.Device.I2c;
using System.Threading;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Services;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;
using Microsoft.Extensions.Hosting;

namespace Almostengr.ThermometerPi.Api.Workers
{
    public class LcdDisplayWorker : BackgroundService
    {
        private const int TOP_LINE = 0;
        private const int BOTTOM_LINE = 1;
        private readonly ITemperatureService _temperatureSensor;

        public LcdDisplayWorker(ITemperatureService temperatureSensor)
        {
            _temperatureSensor = temperatureSensor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
            using var driver = new Pcf8574(i2c);
            using var lcd = new Lcd2004(registerSelectPin: 0,
                                    enablePin: 2,
                                    dataPins: new int[] { 4, 5, 6, 7 },
                                    backlightPin: 3,
                                    backlightBrightness: 0.1f,
                                    readWritePin: 1,
                                    controller: new GpioController(PinNumberingScheme.Logical, driver));
            // int currentLine = 0;

            while (!stoppingToken.IsCancellationRequested)
            {
                lcd.Clear();
                lcd.SetCursorPosition(0, TOP_LINE);
                lcd.Write($"${_temperatureSensor.GetSensorData()}");

                lcd.SetCursorPosition(0, BOTTOM_LINE);
                lcd.Write($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");

                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }
    } // end class 
}