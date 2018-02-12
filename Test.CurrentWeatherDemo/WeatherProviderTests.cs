using CurrentWeatherDemo.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test.CurrentWeatherDemo
{
    [TestClass]
    public class WeatherProviderTests
    {
        [TestMethod]
        public void WeatherProviderCheckStatusTest()
        {
            var weatherProvider = WeatherProviderFactory.GetWeatherProvider();
            // Use coordinates of Saint-Petersburg
            var weatherInfo = weatherProvider.GetCurrentWeather("59.89", "30.26");

            Assert.IsFalse(string.IsNullOrEmpty(weatherInfo.Status), "Weather info contains empty status");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void WeatherProviderWrongCoordinatesTest()
        {
            var weatherProvider = WeatherProviderFactory.GetWeatherProvider();
            // Use wrong coordinates
            weatherProvider.GetCurrentWeather("559.89", "330.26");
        }
    }
}
