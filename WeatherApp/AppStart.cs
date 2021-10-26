using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Service.Extensions;
using WeatherApp.Service.Implementation;
using WeatherApp.Service.Interface;
using WeatherApp.Service.Model;

namespace WeatherApp
{
    public class AppStart
    {
        private readonly IWeatherStackApiClient _client;
        public AppStart(IWeatherStackApiClient client)
        {
            _client = client;
        }

        public async Task Run(String[] args)
        {
            //start getting for inputs
            Console.WriteLine("Enter a valid zipcode:");

            string zipCode = Console.ReadLine();

            var weatherResult = await _client.GetWeatherApiResponse(zipCode);
            if (!weatherResult.Success)
            {
                Console.WriteLine("Error Info: " + weatherResult.Error.Info);
            }
            else {
                IWeatherStackService weatherStackService = new WeatherStackService();
                Console.WriteLine("\n=== Weather Summary ===");            
                Console.WriteLine("Weather Conditions: \t" + string.Join(",", weatherResult.CurrentWeather.WeatherDescriptions.ToArray()));
                Console.WriteLine("Wind Speed: \t" + weatherResult.CurrentWeather.WindSpeed);
                Console.WriteLine("UV Index: \t" + weatherResult.CurrentWeather.UvIndex);
                Console.WriteLine("=== End Weather Summary ===\n");
                Console.WriteLine("Should I go outside? " + (!weatherStackService.IsRaining(weatherResult.CurrentWeather)).ToWeatherQuestionResponse());
                Console.WriteLine("Should I wear sunscreen? " + weatherStackService.IsApplySunscreen(weatherResult.CurrentWeather).ToWeatherQuestionResponse());
                Console.WriteLine("Can I fly my kite? " + weatherStackService.IsFlyKiteAllowable(weatherResult.CurrentWeather).ToWeatherQuestionResponse());
                Console.ReadLine();
            }         
        }
    }
}
