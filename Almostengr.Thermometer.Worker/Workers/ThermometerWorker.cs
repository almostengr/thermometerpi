using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Almostengr.Thermometer.Worker.DataTransfer;
using Almostengr.Thermometer.Worker.Sensor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Almostengr.Thermometer.Worker
{
    public class ThermometerWorker : BackgroundService
    {
        private readonly ILogger<ThermometerWorker> _logger;
        private readonly IConfiguration _configuration;
        private HttpClient _httpClient;
        private StringContent _stringContent;
        private readonly IThermometerSensor _thermometer;
        private string _haUrl;
        public string _haRoute;
        private string _haToken;

        public ThermometerWorker(ILogger<ThermometerWorker> logger, IConfiguration configuration, IThermometerSensor thermometer)
        {
            _logger = logger;
            _configuration = configuration;
            _thermometer = thermometer;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _haUrl = _configuration["HomeAssistant:HaUrl"];
            _haRoute = _configuration["HomeAssistant:Route"];
            _haToken = _configuration["HomeAssistant:Token"];
            _httpClient = new HttpClient() { BaseAddress = new Uri(_haUrl) };

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    string result = _thermometer.GetSensorData();

                    double temperature = ProcessSensorData(result);

                    PostDataToHomeAssistant(temperature);

                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        private async void PostDataToHomeAssistant(double temperature)
        {
            try
            {
                SensorState sensorState = new SensorState(temperature.ToString());
                var jsonState = JsonConvert.SerializeObject(sensorState).ToLower();
                var _stringContent = new StringContent(jsonState, Encoding.ASCII, "application/json");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _haToken);

                HttpResponseMessage response = await _httpClient.PostAsync(_haRoute, _stringContent);

                var logResponse = string.Concat(response.StatusCode, response.ReasonPhrase);
                if (response.IsSuccessStatusCode)
                {
                    HaApiResponse haApiResponse = JsonConvert.DeserializeObject<HaApiResponse>(response.Content.ReadAsStringAsync().Result);
                    _logger.LogInformation(logResponse);
                }
                else
                {
                    _logger.LogError(logResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            if (_stringContent != null)
                _stringContent.Dispose();
        }

        private double ProcessSensorData(string result)
        {
            string[] results = result.Split(",");
            return double.Parse(results[1]);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _httpClient.Dispose();
            _stringContent.Dispose();
            return base.StopAsync(cancellationToken);
        }
    }
}
