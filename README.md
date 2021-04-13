# Thermometer Pi

Thermometer that reads the current temperature for a DS18S20. This is an application created on .NET Core 3.1.

## Getting Temperature

Once initialized, temperatures can be queried in the same way:

  digitemp_DS9097 -a -q -c /etc/digitemp.conf

which will return a reading from each temperature sensor that is connected:

  Jun 06 12:10:55 Sensor 0 C: 22.61 F: 72.70
  Jun 06 12:10:56 Sensor 1 C: 22.62 F: 72.72

## Resources

Visit the project page at https://thealmostengineer.com/thermometerpi

## Additional Resources

https://martybugs.net/electronics/tempsensor/usb.cgi
