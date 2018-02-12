using CurrentWeatherDemo.Models;
using CurrentWeatherDemo.Providers;
using log4net;
using System;
using System.Net;
using System.Web.Mvc;

namespace CurrentWeatherDemo.Controllers
{
    public class HomeController : Controller
    {
        private static ILog _logger = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The method to get current weather by the given coordinates
        /// </summary>
        /// <param name="latitude">Coordinates latitude</param>
        /// <param name="longitude">Coordinates longitude</param>/param>
        /// <returns>Partial view to show weather information</returns>
        [HttpGet]
        public ActionResult GetCurrentWeather(string latitude, string longitude)
        {
            try
            {
                var weatherProvider = WeatherProviderFactory.GetWeatherProvider();
                var weatherInfo = weatherProvider.GetCurrentWeather(latitude, longitude);

                return PartialView("_WeatherView", new WeatherModel(weatherInfo));
            }
            catch (Exception ex)
            {
                _logger.Error("Error to get current weather action", ex);
                throw new WebException("Failed to get current weather", ex);
            }
        }
    }
}