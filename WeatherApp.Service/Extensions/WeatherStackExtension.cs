using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Service.Constant;
using WeatherApp.Service.Model;

namespace WeatherApp.Service.Extensions
{
    public static class WeatherStackExtension
    {
        public static WeatherStackResponse ToWeatherStackResponse(this WeatherStackResponse response) {
            response.Success = response.CurrentWeather != null;
            return response;
        }

        public static string ToWeatherQuestionResponse(this bool flag)
        {
            return flag ? Constants.YES : Constants.NO;
        }
    }
}
