using System;
using System.Threading;
using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Services;
using Microsoft.Extensions.Hosting;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Microsoft.Extensions.DependencyInjection;
using System.Device.Gpio;
using System.Device.I2c;
using Iot.Device.Pcx857x;
using Iot.Device.CharacterLcd;

namespace Almostengr.ThermometerPi.Api.Workers
{
    public class LcdDisplayWorker : BackgroundService
    {
        private readonly ITemperatureReadingService _temperatureReadingService;
        private const int DelaySeconds = 5;
        private Lcd1602 lcd;

        public LcdDisplayWorker(IServiceScopeFactory factory)
        {
            _temperatureReadingService = factory.CreateScope().ServiceProvider.GetRequiredService<ITemperatureReadingService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
            using Pcf8574 driver = new Pcf8574(i2c);

            lcd = new Lcd1602(registerSelectPin: 0,
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
                string output = string.Empty;

                if (interiorTemp != null)
                {
                    output += $"In: {interiorTemp.Fahrenheit.ToString()}F ";
                }

                if (exteriorTemp != null)
                {
                    output += $"Out: {exteriorTemp.Fahrenheit.ToString()}F";
                }

                DisplayLcdText(output);

                await Task.Delay(TimeSpan.FromSeconds(DelaySeconds), stoppingToken);

                TemperatureDto minInteriorTemp = await _temperatureReadingService.GetMinInteriorReadingAsync();
                TemperatureDto maxInteriorTemp = await _temperatureReadingService.GetMaxInteriorReadingAsync();

                output = string.Empty;

                if (minInteriorTemp != null)
                {
                    output = $"Min: {minInteriorTemp.Fahrenheit.ToString()}F ";
                }

                if (maxInteriorTemp != null)
                {
                    output += $"Max: {maxInteriorTemp.Fahrenheit.ToString()}F";
                }

                DisplayLcdText(output);

                await Task.Delay(TimeSpan.FromSeconds(DelaySeconds), stoppingToken);
            }
        }

        private void DisplayLcdText(string line1 = "No data")
        {
            lcd.Clear();
            lcd.SetCursorPosition(0, 0);
            lcd.Write(line1);
            lcd.SetCursorPosition(0, 1);
            lcd.Write(DateTime.Now.ToString("ddd MM/dd HH:mm"));
        }

    } // end class 
}
