using System.Threading.Tasks;
using Almostengr.ThermometerPi.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThermometerController : ControllerBase
    {
        private readonly ILogger<ThermometerController> _logger;
        private readonly ITemperatureReadingService _temperatureReadingService;

        public ThermometerController(ILogger<ThermometerController> logger,
            ITemperatureReadingService temperatureReadingService)
        {
            _logger = logger;
            _temperatureReadingService = temperatureReadingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetThermometer()
        {
            return await GetLatestInteriorTemperature();
        }

        [HttpGet]
        [Route("exterior")]
        public async Task<IActionResult> GetLatestExteriorTemperature()
        {
            return Ok(await _temperatureReadingService.GetLatestExteriorReadingAsync());
        }

        [HttpGet]
        [Route("interior")]
        public async Task<IActionResult> GetLatestInteriorTemperature()
        {
            return Ok(await _temperatureReadingService.GetLatestInteriorReadingAsync());
        }

    }
}