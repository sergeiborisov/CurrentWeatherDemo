using System.Configuration;

namespace CurrentWeatherDemo.Providers
{
    public class WeatherProviderFactory
    {
        public static IWeatherProvider GetWeatherProvider() => new WeatherProvider(ConfigurationManager.AppSettings["OpenWeatherApiKey"]);
    }
}