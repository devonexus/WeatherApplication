using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherApp.Service.Test
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly object _response;
        private readonly HttpStatusCode _statusCode;

        public string Input { get; private set; }
        public int NumberOfCalls { get; private set; }

        public MockHttpMessageHandler(object response, HttpStatusCode statusCode)
        {
            _response = response;
            _statusCode = statusCode;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            NumberOfCalls++;
            
            if (request.Content != null)
            {
                Input = await request.Content.ReadAsStringAsync();
            }
            
            return new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = new StringContent(JsonSerializer.Serialize(_response))
            };
        }
    }
}
