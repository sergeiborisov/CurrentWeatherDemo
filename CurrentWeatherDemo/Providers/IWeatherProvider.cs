namespace CurrentWeatherDemo.Providers
{
    public interface IWeatherProvider
    {
        /// <summary>
        /// The method returns weather information for the given coordinates
        /// </summary>
        /// <param name="latitude">Coordinates latitude</param>
        /// <param name="longitude">Coordinates longitude</param>
        /// <returns>Weather information object</returns>
        WeatherInfo GetCurrentWeather(string latitude, string longitude);
    }
}
