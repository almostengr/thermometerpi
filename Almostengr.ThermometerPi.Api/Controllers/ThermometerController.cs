using System;
using Almostengr.ThermometerPi.Api.DataTransferObject;
using Almostengr.ThermometerPi.Api.Sensors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThermometerController : ControllerBase
    {
        private readonly ILogger<ThermometerController> _logger;
        private readonly ITemperatureSensor _sensor;

        public ThermometerController(ILogger<ThermometerController> logger, ITemperatureSensor sensor)
        {
            _logger = logger;
            _sensor = sensor;
        }

        [HttpGet]
        public ThermometerDto GetThermometer()
        {
            var dataReading = _sensor.GetSensorData();

            _logger.LogInformation($"At {DateTime.Now} temperature is {dataReading}");

            return new ThermometerDto(dataReading);
        }
    }
}