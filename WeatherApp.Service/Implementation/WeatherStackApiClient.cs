using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Service.Extensions;
using WeatherApp.Service.Interface;
using WeatherApp.Service.Model;

namespace WeatherApp.Service.Implementation
{
    public class WeatherStackApiClient : IWeatherStackApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<WeatherAppConfig> _weatherAppConfig;
        private readonly WeatherAppConfig _apiConfig;
        public WeatherStackApiClient(HttpClient httpClient, IOptions<WeatherAppConfig> weatherAppConfig) {
            _httpClient = httpClient;
            _weatherAppConfig = weatherAppConfig;
            _apiConfig = _weatherAppConfig.Value;
            _httpClient.BaseAddress = new Uri(_apiConfig.Url);
        }

        /// <summary>
        /// Returns the current weather api response called from an external url
        /// </summary>
        /// <param name="zipCode">Expected query string zipcode value</param>
        /// <returns></returns>
        public async Task<WeatherStackResponse> GetWeatherApiResponse(string zipCode)
        {
            WeatherStackResponse weatherStackResponse = null;
            try
            {
                var response = await _httpClient.GetStreamAsync($"current?access_key={_apiConfig.Key}&query={zipCode}");

                weatherStackResponse = await JsonSerializer.DeserializeAsync<WeatherStackResponse>(response);
                return weatherStackResponse.ToWeatherStackResponse();
            }
            catch (Exception e)
            {
                return weatherStackResponse;
            }
        }
    }
}
