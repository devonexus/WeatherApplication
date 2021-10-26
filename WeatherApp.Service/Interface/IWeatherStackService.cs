using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Service.Model;

namespace WeatherApp.Service.Interface
{
    public interface IWeatherStackService
    {
        /// <summary>
        /// Check if Raining and Wind Speed is over 15
        /// </summary>
        /// <param name="weather">Current weather api response</param>
        /// <returns></returns>
        bool IsFlyKiteAllowable(Current weather);
        /// <summary>
        /// Check if UV Index is greater than 3
        /// </summary>
        /// <param name="weather">Current weather api response</param>
        /// <returns></returns>
        bool IsApplySunscreen(Current weather);

        /// <summary>
        /// Check if wind speed is greater than 15s
        /// </summary>
        /// <param name="weather">Current weather api response</param>
        /// <returns></returns>
        bool IsWindyOutside(Current weather);

        /// <summary>
        /// Check for a rainy weather condition using WeatherDescription response
        /// </summary>
        /// <param name="weather">Current weather api response</param>
        /// <returns></returns>
        bool IsRaining(Current weather);
    }
}
