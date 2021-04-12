# Thermometer Pi

Thermometer that reads the current temperature for a DS18S20 thermostat on Linux

## Getting Temperature

Once initialised, temperatures can be queried in the same way:

  digitemp_DS9097 -a -q -c /etc/digitemp.conf

which will return a reading from each temperature sensor that is connected:

  Jun 06 12:10:55 Sensor 0 C: 22.61 F: 72.70
  Jun 06 12:10:56 Sensor 1 C: 22.62 F: 72.72
  

## External Resources

https://martybugs.net/electronics/tempsensor/usb.cgi


## First Run

This is the full output when running the commands that are listed on the external resource website.  
Note that I did have to use sudo as it state that my user ID did not have permission to 
access /dev/ttyUSB0

```sh
almostengineer@aeoffice:/tmp$ sudo   digitemp_DS9097 -i -s /dev/ttyUSB0 -c /etc/digitemp.conf
DigiTemp v3.7.2 Copyright 1996-2018 by Brian C. Lane
GNU General Public License v2.0 - http://www.digitemp.com
Turning off all DS2409 Couplers

Searching the 1-Wire LAN
28711E92070000F3 : DS18B20 Temperature Sensor
ROM #0 : 28711E92070000F3
Wrote /etc/digitemp.conf
almostengineer@aeoffice:/tmp$ sudo   digitemp_DS9097 -a -q -c /etc/digitemp.conf
Apr 10 13:05:42 Sensor 0 C: 29.81 F: 85.66
almostengineer@aeoffice:/tmp$ 
```

To be able to read the sensor data without using sudo, then you will need to add the user(s) 
to the dialout group. 

```sh
sudo usermod -a -G dialout $USER
```

Replace "$USER" with the username(s) that need to be added to the group. May need to logout or 
restart the system for the changes to take effect.

To check to see what groups that the user has been added to, you can run the command

```sh
groups $USER
```

replacing $USER with the username that you want to see.
