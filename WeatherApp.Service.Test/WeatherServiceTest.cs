using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Service.Implementation;
using WeatherApp.Service.Interface;
using WeatherApp.Service.Model;

namespace WeatherApp.Service.Test
{
    [TestClass()]
    public class WeatherServiceTest
    {
        private IWeatherStackService _weatherStackService;

        public WeatherServiceTest() {
            _weatherStackService = new WeatherStackService();
        }

        [TestMethod()]
        public void IsApplySunscreen_WhenUvIndexIsAboveThree_ShouldReturnTrue() {

            //Arrange
            Current currentWeather = new Current()
            {
                Temperature = 2,
                UvIndex = 4,
                WeatherDescriptions = new List<string> {
                    "Overcast", "Mist"
                },
                WindSpeed = 15
            };


            //Act       
            var actual = _weatherStackService.IsApplySunscreen(currentWeather);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod()]
        public void IsApplySunscreen_WhenUvIndexIsBelowThree_ShouldReturnFalse() {
            //Arrange
            Current currentWeather = new Current()
            {
                Temperature = 2,
                UvIndex = 2,
                WeatherDescriptions = new List<string> {
                    "Overcast", "Mist"
                },
                WindSpeed = 15
            };

            //Act       
            var actual = _weatherStackService.IsApplySunscreen(currentWeather);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void IsWindyOutSide_WhenWindSpeedOverFifteen_ShouldReturnTrue() {
            //Arrange
            Current currentWeather = new Current()
            {
                Temperature = 2,
                UvIndex = 2,
                WeatherDescriptions = new List<string> {
                    "Windy"
                },
                WindSpeed = 16
            };

            //Act
            var actual = _weatherStackService.IsWindyOutside(currentWeather);

            //Assert
            actual.Should().BeTrue();
        }


        [TestMethod()]
        public void IsWindyOutSide_WhenWindSpeedBelowFifteen_ShouldReturnFalse()
        {
            //Arrange
            Current currentWeather = new Current()
            {
                Temperature = 2,
                UvIndex = 2,
                WeatherDescriptions = new List<string> {
                    "Cloudy"
                },
                WindSpeed = 14
            };

            //Act
            var actual = _weatherStackService.IsWindyOutside(currentWeather);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void IsRaining_WhenWeatherDescriptionsHasRainIndicator_ShouldReturnTrue() {
            //Arrange
            Current currentWeather = new Current()
            {
                WeatherDescriptions = new List<string> {
                    "Light rain", "Patchy light rain"
                }
            };

            //Act
            var actual = _weatherStackService.IsRaining(currentWeather);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod()]
        public void IsRaining_WhenWeatherDescriptionsHasNoRainIndicator_ShouldReturnFalse()
        {
            //Arrange
            Current currentWeather = new Current()
            {
                WeatherDescriptions = new List<string> {
                    "Cloudy", "Clear", "Mist"
                }
            };

            //Act
            var actual = _weatherStackService.IsRaining(currentWeather);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void IsFlyKiteAllowable_WhenWindSpeedOver15_And_IsNotRaining_ShouldReturnTrue() {
            //Arrange
            Current current = new Current
            {
                WeatherDescriptions = new List<string> { "Overcast", "Cloudy", "Clear"},
                WindSpeed = 16
            };

            //Act
            var actual = _weatherStackService.IsFlyKiteAllowable(current);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod()]
        public void IsFlyKiteAllowable_WhenWindSpeedOver15_And_IsRaining_ShouldReturnFalse()
        {
            //Arrange
            Current current = new Current
            {
                WeatherDescriptions = new List<string> { "Patchy light rain", "Light rain"},
                WindSpeed = 16
            };

            //Act
            var actual = _weatherStackService.IsFlyKiteAllowable(current);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void IsFlyKiteAllowable_WhenWindSpeedBelow15_And_IsRaining_ShouldReturnFalse()
        {
            //Arrange
            Current current = new Current
            {
                WeatherDescriptions = new List<string> { "Patchy light rain", "Light rain" },
                WindSpeed = 1
            };

            //Act
            var actual = _weatherStackService.IsFlyKiteAllowable(current);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod()]
        public void IsFlyKiteAllowable_WhenWindSpeedBelow15_And_IsNotRaining_ShouldReturnFalse()
        {
            //Arrange
            Current current = new Current
            {
                WeatherDescriptions = new List<string> { "Overcast", "Cloudy", "Clear" },
                WindSpeed = 1
            };

            //Act
            var actual = _weatherStackService.IsFlyKiteAllowable(current);

            //Assert
            actual.Should().BeFalse();
        }

    }
}
