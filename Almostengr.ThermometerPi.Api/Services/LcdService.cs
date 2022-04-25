using System.Device.Gpio;
using System.Device.I2c;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;
using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Api.Services
{
    public class LcdService : ILcdService
    {
        // private Lcd1602 _lcd;
        private const int TOP_LINE = 0;
        private const int BOTTOM_LINE = 1;
        private readonly ILogger<LcdService> _logger;

        public LcdService(ILogger<LcdService> logger)
        {
            _logger = logger;

            I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
            Pcf8574 driver = new Pcf8574(i2c);
        }

        public void WriteLines(string line1, string line2)
        {
            using I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
            using Pcf8574 driver = new Pcf8574(i2c);
            // using Lcd1602 _lcd = new Lcd1602(i2c, true);
            using Lcd1602 _lcd = new Lcd1602(registerSelectPin: 5,
                                            enablePin: 4,
                                            dataPins: new int[] { 0, 1, 2, 3 },
                                            readWritePin: 6,
                                            controller: new GpioController(PinNumberingScheme.Logical, driver),
                                            shouldDispose: false);

            _lcd.DisplayOn = true;
            _lcd.Clear();

            _lcd.BacklightOn = true;
            _lcd.Home();
            _lcd.Write(line1);
            _logger.LogInformation($"LCD: {line1}");

            _lcd.BlinkingCursorVisible = false;
            _lcd.BacklightOn = true;
        }

    }
}