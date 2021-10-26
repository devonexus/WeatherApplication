using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net.Http;
using WeatherApp.Service.Implementation;
using WeatherApp.Service.Interface;
using WeatherApp.Service.Model;

namespace WeatherApp
{
    public static class StartUp
    {
        public static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            //build configuration
            var basePath =
                Directory.GetParent(
                Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName;
            var configuration = new ConfigurationBuilder()
                            .SetBasePath(basePath)
                            .AddJsonFile("appsettings.json", optional: false)
                            .Build();

            //reqister the services using default DI
            services.AddScoped<HttpClient>();
            services.AddScoped<IWeatherStackApiClient, WeatherStackApiClient>();
            services.Configure<WeatherAppConfig>(configuration.GetSection("WeatherStackConfig"));
            services.AddTransient<AppStart>();
            return services;

        }
    }
}
