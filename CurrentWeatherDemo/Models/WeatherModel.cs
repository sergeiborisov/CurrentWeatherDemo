using CurrentWeatherDemo.Providers;
using System.Collections.Generic;

namespace CurrentWeatherDemo.Models
{
    /// <summary>
    /// View model containing the weather information
    /// </summary>
    public class WeatherModel
    {
        private double? _temperatureCelsius;

        /// <summary>
        /// Creates new weather view model
        /// </summary>
        /// <param name="weatherInfo">Weather information for the model</param>
        public WeatherModel(WeatherInfo weatherInfo)
        {
            Status = weatherInfo.Status;
            Icons = weatherInfo.Icons;

            _temperatureCelsius = weatherInfo.Temperature;
        }

        /// <summary>
        /// Temperature Celsius
        /// </summary>
        public double? TemperatureCelsius => _temperatureCelsius;

        /// <summary>
        /// Temperature Fahrenheit
        /// </summary>
        public double? TemperatureFahrenheit => _temperatureCelsius.HasValue ? _temperatureCelsius * 1.8 + 32 : null;

        /// <summary>
        /// Weather status(es)
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Weather icons
        /// </summary>
        public IEnumerable<string> Icons { get; }
    }
}