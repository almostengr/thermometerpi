using Microsoft.Extensions.Logging;

namespace Almostengr.ThermometerPi.Api.Services
{
    public class MockLcdService : ILcdService
    {
        private readonly ILogger<MockLcdService> _logger;

        public MockLcdService(ILogger<MockLcdService> logger)
        {
            _logger = logger;
        }
        
        public void Clear()
        {
            _logger.LogInformation("Clearing LCD");
        }

        public void WriteLines(string line1, string line2)
        {
            _logger.LogInformation($"Writing to LCD: {line1}, {line2}");
        }
    }
}