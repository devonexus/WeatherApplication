using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherApp.Service.Interface;
using WeatherApp.Service.Model;

namespace WeatherApp.Service.Implementation
{
    public class WeatherStackService : IWeatherStackService
    {
        public bool IsApplySunscreen(Current weather)
        {
            if (weather.UvIndex > 3)
                return true;
            return false;
        }

        public bool IsFlyKiteAllowable(Current weather)
        {
            return !IsRaining(weather) && IsWindyOutside(weather);
        }

        public bool IsRaining(Current weather)
        {
            if (weather.WeatherDescriptions.Any(m => m.Contains("rain")))
                return true;
            return false;
        }

        public bool IsWindyOutside(Current weather)
        {
            if (weather.WindSpeed > 15)
                return true;
            return false;
        }
    }
}
