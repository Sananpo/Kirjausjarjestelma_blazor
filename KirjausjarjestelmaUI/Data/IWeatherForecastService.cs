using KirjausjarjestelmaDB.Data.BlazorApp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KirjausjarjestelmaUI.Data
{
    public interface IWeatherForecastService
    {
        Task<List<WeatherForecast>> GetForecastAsync(string strCurrentUser);
        Task<WeatherForecast> CreateForecastAsync(WeatherForecast objWeatherForecast);
        Task<bool> UpdateForecastAsync(WeatherForecast objWeatherForecast);
        Task<bool> DeleteForecastAsync(WeatherForecast objWeatherForecast);
    }
}