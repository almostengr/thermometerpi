# Thermometer Pi

This project uses a Raspberry Pi to make data available via an API from a temperature sensor.
Thermometer that reads the current temperature for a DS18S20.
This is an application created on .NET 5.0 with C#.

In addition, the application gets weather information from the National Weather Service via
API. Both internal and external temperature are then displayed on an LCD that is connected to the
Raspberry Pi via I2C.

My Home Assistant instance calls the API every 5 minutes. Based on the temperature, a Wemo Smart Outlet
is either switched on or off by Home Assistant. The window air conditioners (AC) are connected to the Wemo
outlets.

## Resources

Visit the project page at https://thealmostengineer.com/projects/thermometer-pi/

Business inquiries https://rhtservices.net
