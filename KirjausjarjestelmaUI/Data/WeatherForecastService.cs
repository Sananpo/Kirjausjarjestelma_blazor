using KirjausjarjestelmaDB.Data.BlazorApp;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace KirjausjarjestelmaUI.Data
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly BlazorappDevContext context;

        public WeatherForecastService(BlazorappDevContext context)
        {
            this.context = context;
        }
        public async Task<List<WeatherForecast>>
            GetForecastAsync(string strCurrentUser)
        {
            return await context.WeatherForecast
                 .Where(x => x.UserName == strCurrentUser)
                 .AsNoTracking().ToListAsync();
        }

        public Task<WeatherForecast>
            CreateForecastAsync(WeatherForecast objWeatherForecast)
        {
            context.WeatherForecast.Add(objWeatherForecast);
            context.SaveChanges();
            return Task.FromResult(objWeatherForecast);
        }

        public Task<bool>
            UpdateForecastAsync(WeatherForecast objWeatherForecast)
        {
            var ExistingWeatherForecast =
                context.WeatherForecast
                .Where(x => x.Id == objWeatherForecast.Id)
                .FirstOrDefault();
            if (ExistingWeatherForecast != null)
            {
                ExistingWeatherForecast.Date =
                    objWeatherForecast.Date;
                ExistingWeatherForecast.Summary =
                    objWeatherForecast.Summary;
                ExistingWeatherForecast.TemperatureC =
                    objWeatherForecast.TemperatureC;
                ExistingWeatherForecast.TemperatureF =
                    objWeatherForecast.TemperatureF;
                context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool>
            DeleteForecastAsync(WeatherForecast objWeatherForecast)
        {
            var ExistingWeatherForecast =
                context.WeatherForecast
                .Where(x => x.Id == objWeatherForecast.Id)
                .FirstOrDefault();
            if (ExistingWeatherForecast != null)
            {
                context.WeatherForecast.Remove(ExistingWeatherForecast);
                context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}