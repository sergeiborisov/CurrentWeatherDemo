using log4net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace CurrentWeatherDemo.Providers
{
    /// <summary>
    /// Provides weather information from OpenWeatherMap service
    /// </summary>
    public class WeatherProvider: IWeatherProvider
    {
        private static ILog _logger = LogManager.GetLogger(typeof(WeatherProvider));

        private static readonly string WeatherRequestPattern = "http://openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=metric&appid={2}";
        private static readonly string WeatherIconPattern = "http://openweathermap.org/img/w/{0}.png";

        /// <summary>
        /// ApiKey for OpenWeatherMap service
        /// </summary>
        private string ApiKey { get; }

        /// <summary>
        /// Creates new weather provider for OpenWeatherMap service
        /// </summary>
        /// <param name="apiKey">ApiKey for OpenWeatherMap service</param>
        public WeatherProvider(string apiKey)
        {
            ApiKey = apiKey;
        }

        /// <summary>
        /// The method returns weather information for the given coordinates
        /// </summary>
        /// <param name="latitude">Coordinates latitude</param>
        /// <param name="longitude">Coordinates longitude</param>
        /// <returns>Weather information object</returns>
        public WeatherInfo GetCurrentWeather(string latitude, string longitude)
        {
            var weatherInfo = new WeatherInfo();

            _logger.Info($"Get current weather for coords ({latitude}, {longitude})");

            // Get current weather for the given coords
            var request = WebRequest.Create(string.Format(WeatherRequestPattern, latitude, longitude, ApiKey));
            var response = request.GetResponse();

            // Get the reponse body
            string responseText;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                responseText = sr.ReadToEnd();
                _logger.Debug($"The response for the current weather: `{responseText}`");
            }

            // Parse response json and set the model
            if (!string.IsNullOrEmpty(responseText))
            {
                try
                {
                    dynamic json = JsonConvert.DeserializeObject<dynamic>(responseText);

                    var weathers = json.weather as System.Collections.Generic.IEnumerable<dynamic> ?? new dynamic[] { };

                    weatherInfo.Status = string.Join(", ", weathers.Select(w => (string)w.main));
                    weatherInfo.Icons = weathers.Select(w => string.Format(WeatherIconPattern, (string)w.icon));
                    weatherInfo.Temperature = (double)json?.main?.temp;
                }
                catch (Exception ex)
                {
                    _logger.Error("Cannot get weather information from the response", ex);
                    throw new Exception("Cannot get weather information from the response", ex);
                }
            }

            // Set the model status if it is empty
            if (string.IsNullOrEmpty(weatherInfo.Status))
            {
                weatherInfo.Status = "Undefined";
                _logger.Warn($"The weather information was not found");
            }

            return weatherInfo;
        }
    }
}