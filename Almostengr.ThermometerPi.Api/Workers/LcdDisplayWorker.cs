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
using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Api.Workers
{
    public class LcdDisplayWorker : BackgroundService
    {
        private readonly ITemperatureReadingService _temperatureReadingService;
        private readonly ILogger<LcdDisplayWorker> _logger;
        private const int DelaySeconds = 5;
        private Lcd1602 lcd;

        public LcdDisplayWorker(IServiceScopeFactory factory,
            ILogger<LcdDisplayWorker> logger)
        {
            _temperatureReadingService = factory.CreateScope().ServiceProvider.GetRequiredService<ITemperatureReadingService>();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
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
                    TemperatureDto interiorTemp = await _temperatureReadingService.GetLatestInteriorReadingAsync();

                    lcd.Clear();
                    string output = string.Empty;

                    DisplayLcdText(
                        interiorTemp != null ?
                            $"In: {interiorTemp.Fahrenheit.ToString()}F {interiorTemp.Celsius.ToString()}C" :
                            "No Data",
                        DateTime.Now.ToString("ddd MM/dd HH:mm")
                    );

                    await Task.Delay(TimeSpan.FromSeconds(DelaySeconds), stoppingToken);

                    TemperatureDto minInteriorTemp = await _temperatureReadingService.GetMinInteriorReadingAsync();
                    TemperatureDto maxInteriorTemp = await _temperatureReadingService.GetMaxInteriorReadingAsync();

                    output = string.Empty;

                    DisplayLcdText(
                        minInteriorTemp != null ?
                            $"Min: {minInteriorTemp.Fahrenheit.ToString()}F {minInteriorTemp.Celsius.ToString()}C" :
                            string.Empty,
                        maxInteriorTemp != null ?
                            $"Max: {maxInteriorTemp.Fahrenheit.ToString()}F {maxInteriorTemp.Celsius.ToString()}C" :
                            string.Empty
                    );

                    await Task.Delay(TimeSpan.FromSeconds(DelaySeconds), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private void DisplayLcdText(string line1 = "No data", string line2 = "")
        {
            lcd.Clear();
            lcd.SetCursorPosition(0, 0);
            lcd.Write(line1);
            lcd.SetCursorPosition(0, 1);
            lcd.Write(line2);
        }

    } // end class
}
