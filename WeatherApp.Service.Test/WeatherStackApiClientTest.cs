using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Service.Implementation;
using WeatherApp.Service.Model;

namespace WeatherApp.Service.Test
{
    [TestClass()]
    public class WeatherStackApiClientTest
    {
        private readonly IOptions<WeatherAppConfig> _weatherAppConfig;
        public WeatherStackApiClientTest() {
            _weatherAppConfig = Substitute.For<IOptions<WeatherAppConfig>>();
            _weatherAppConfig.Value.Returns(new WeatherAppConfig { Url = "http://api.weatherstack.com/current", Key = "someKey" });
        }

        [TestMethod()]
        public  async Task GetWeatherStackResponse_WhenValidZipCodeIsEntered_ShouldReturnWeatherStackResponse() {
            //Arrange     
            string someValidZipCode = "99501";

            var weatherStackResponse = new WeatherStackResponse
            {
                CurrentWeather = new Current
                {
                    Temperature = 1,
                    UvIndex = 1,
                    WindSpeed = 1,
                }
            };

            WeatherStackResponse expected = new WeatherStackResponse
            {
                CurrentWeather = new Current
                {
                    Temperature = 1,
                    UvIndex = 1,
                    WindSpeed = 1
                }
            };


       
            var messageHandler = new MockHttpMessageHandler(weatherStackResponse, HttpStatusCode.OK);
            var httpClient = new HttpClient(messageHandler);
 
            
            var apiService = new WeatherStackApiClient(httpClient, _weatherAppConfig);

            //Act
            var actual = await apiService.GetWeatherApiResponse(someValidZipCode);


            //Assert
            actual.Should().BeOfType<WeatherStackResponse>().Which.CurrentWeather.Should().BeEquivalentTo(expected.CurrentWeather);
        }


        [TestMethod()]
        public async Task GetWeatherStackResponse_WhenInValidZipCodeIsEntered_ShouldReturnSuccessFalse()
        {
            //Arrange     
            string someInvalidZipCode = "9950";

            var weatherStackResponse = new WeatherStackResponse
            {
                CurrentWeather = null,
                Error = new Error {
                    Code = 615,
                    Type = "Request failed",
                    Info = "Your API request failed. Please try again or contact support."
                }
            };         


            var messageHandler = new MockHttpMessageHandler(weatherStackResponse, HttpStatusCode.OK);
            var httpClient = new HttpClient(messageHandler);


            var apiService = new WeatherStackApiClient(httpClient, _weatherAppConfig);

            //Act
            var actual = await apiService.GetWeatherApiResponse(someInvalidZipCode);


            //Assert
            actual.Success.Should().BeFalse();
        }

    }


}
