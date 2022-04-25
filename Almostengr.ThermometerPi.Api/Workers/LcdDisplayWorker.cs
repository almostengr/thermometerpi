using System;
using System.Threading;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Services;
using Microsoft.Extensions.Hosting;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Device.Gpio;
using System.Device.I2c;
using Iot.Device.Pcx857x;
using Iot.Device.CharacterLcd;

namespace Almostengr.ThermometerPi.Api.Workers
{
    public class LcdDisplayWorker : BackgroundService
    {
        private readonly ILogger<LcdDisplayWorker> _logger;
        private readonly ITemperatureReadingService _temperatureReadingService;
        // private readonly ILcdService _lcdService;

        public LcdDisplayWorker(IServiceScopeFactory factory, ILogger<LcdDisplayWorker> logger)
        {
            _logger = logger;
            _temperatureReadingService = factory.CreateScope().ServiceProvider.GetRequiredService<ITemperatureReadingService>();
            // _lcdService = factory.CreateScope().ServiceProvider.GetRequiredService<ILcdService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
            using Pcf8574 driver = new Pcf8574(i2c);

            var lcd = new Lcd1602(registerSelectPin: 0,
                    enablePin: 2,
                    dataPins: new int[] { 4, 5, 6, 7 },
                    backlightPin: 3,
                    backlightBrightness: 1f,
                    readWritePin: 1,
                    controller: new GpioController(PinNumberingScheme.Logical, driver));

            while (!stoppingToken.IsCancellationRequested)
            {
                TemperatureDto exteriorTemp = await _temperatureReadingService.GetLatestExteriorReadingAsync();
                TemperatureDto interiorTemp = await _temperatureReadingService.GetLatestInteriorReadingAsync();

                lcd.Clear();
                lcd.SetCursorPosition(0, 0);
                string output = string.Empty;

                if (interiorTemp != null)
                {
                    output += $"I: {interiorTemp.Fahrenheit.ToString()}F  ";
                }

                if (exteriorTemp != null)
                {
                    output += $"E: {exteriorTemp.Fahrenheit.ToString()}F";
                }

                lcd.Write(output);

                lcd.SetCursorPosition(0, 1);
                lcd.Write(DateTime.Now.ToString("ddd MM/dd") + " " + DateTime.Now.ToShortTimeString());

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

    } // end class 
}
