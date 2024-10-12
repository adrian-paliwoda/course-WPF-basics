using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherAppModel;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public static async Task<List<City>> GetCities(string query)
        {
            List<City>? cities;

            string url = AccuWeatherConstants.GetAccuWeatherAutoCompleteEndpoint(query);

            using (var client = new HttpClient())
            {
                var respond = await client.GetAsync(url);
                respond.EnsureSuccessStatusCode();

                string json = await respond.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities ?? new List<City>();
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions? conditions;

            string url = AccuWeatherConstants.GetAccuWeatherCurrentConditionsEndpoint(cityKey);

            using (var client = new HttpClient())
            {
                var respond = await client.GetAsync(url);
                respond.EnsureSuccessStatusCode();

                string json = await respond.Content.ReadAsStringAsync();
                conditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json)?.FirstOrDefault();
            }

            return conditions ?? new CurrentConditions();
        }
    }
}