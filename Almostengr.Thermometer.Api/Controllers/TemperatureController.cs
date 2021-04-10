using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Almostengr.Thermometer.Api.Data;
using Almostengr.Thermometer.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Almostengr.Thermometer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        private readonly IThermometerRepository _repository;

        public TemperatureController(IThermometerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TemperatureReading>> GetTemperatures()
        {
            var readings = _repository.GetTemperatureReadings();
            return Ok(readings);
        }

        [HttpGet("{id}")]
        public ActionResult<TemperatureReading> GetTemperatureById(int id)
        {
            var reading = _repository.GetTemperatureReadingById(0);
            return Ok(reading);
        }

        // [HttpPost]
        // public async Task<IActionResult<TemperatureReading>> Post(TemperatureReading temperatureReading)
        // {

        // }
    }
}