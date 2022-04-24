using System.Device.Gpio;
using System.Device.I2c;
using Iot.Device.CharacterLcd;
using Iot.Device.Pcx857x;

namespace Almostengr.ThermometerPi.Api.Services
{
    public class LcdService : ILcdService
    {
        private readonly Lcd1602 _lcd;
        private const int TOP_LINE = 0;
        private const int BOTTOM_LINE = 1;

        public LcdService()
        {
            using I2cDevice i2c = I2cDevice.Create(new I2cConnectionSettings(1, 0x27));
            // using Pcf8574 driver = new Pcf8574(i2c);
            // using Lcd2004 _lcd = new Lcd2004(registerSelectPin: 0,
            //                     enablePin: 2,
            //                     // dataPins: new int[] { 4, 5, 6, 7 },
            //                     dataPins: new int[] { 14, 15 },
            //                     // backlightPin: 3,
            //                     // backlightBrightness: 0.1f,
            //                     readWritePin: 1,
            //                     controller: new GpioController(PinNumberingScheme.Logical, driver));
            Lcd1602 _lcd = new Lcd1602(i2c, true);
        }

        public void Clear()
        {
            _lcd.Clear();
        }

        public void WriteLines(string line1, string line2)
        {
            _lcd.SetCursorPosition(0, TOP_LINE);
            _lcd.Write(line1);
            _lcd.SetCursorPosition(0, BOTTOM_LINE);
            _lcd.Write(line2);
        }

    }
}