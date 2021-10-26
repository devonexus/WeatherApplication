using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //create service collection
            var services = StartUp.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            Task mainApp = serviceProvider.GetService<AppStart>().Run(args);
            mainApp.Wait();
        }
    }
}
