using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Service.Model;

namespace WeatherApp.Service.Interface
{
    public interface IWeatherStackApiClient
    {
        /// <summary>
        /// Returns the current weather api response called from an external url
        /// </summary>
        /// <param name="zipCode">Expected query string zipcode value</param>
        /// <returns></returns>
        Task<WeatherStackResponse> GetWeatherApiResponse(string zipCode);
    }
}
