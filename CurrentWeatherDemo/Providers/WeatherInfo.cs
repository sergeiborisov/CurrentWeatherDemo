using System.Collections.Generic;

namespace CurrentWeatherDemo.Providers
{
    /// <summary>
    /// Simple information about tthe current weather
    /// </summary>
    public class WeatherInfo
    {
        /// <summary>
        /// Creates new weather information object
        /// </summary>
        public WeatherInfo()
        {
            Icons = new string[] { };
        }

        /// <summary>
        /// Weather status(es)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Temperature Celsius
        /// </summary>
        public double? Temperature { get; set; }

        /// <summary>
        /// Weather icons
        /// </summary>
        public IEnumerable<string> Icons { get; set; }
    }
}