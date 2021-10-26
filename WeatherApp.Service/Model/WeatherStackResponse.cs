﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WeatherApp.Service.Model
{
    public class WeatherStackResponse
    {
        [JsonPropertyName("current")]
        public Current CurrentWeather { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }
}
