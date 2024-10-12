namespace WeatherApp.ViewModel
{
    public static class AccuWeatherConstants
    {
        private const string ApiKey = "fGGnxlqpmAQeqV54IW1eRFAubqpfx3qd";

        private const string AccuWeatherBaseUrl =
            "dataservice.accuweather.com";

        private const string AccuWeatherAutoCompleteEndpoint = "/locations/v1/cities/autocomplete";

        private const string AccuWeatherCurrentConditionsEndpoint = "/currentconditions/v1/";

        public static string GetAccuWeatherAutoCompleteEndpoint(string query, string protocol = "http")
        {
            if (string.IsNullOrEmpty(query))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(protocol))
            {
                protocol = "http";
            }

            return string.Concat(protocol, "://", AccuWeatherBaseUrl, AccuWeatherAutoCompleteEndpoint, "?apikey=",
                ApiKey, "&q=", query);
        }

        public static string GetAccuWeatherCurrentConditionsEndpoint(string cityKey, string protocol = "http")
        {
            if (string.IsNullOrEmpty(cityKey))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(protocol))
            {
                protocol = "http";
            }

            return string.Concat(protocol, "://", AccuWeatherBaseUrl, AccuWeatherCurrentConditionsEndpoint, cityKey,
                "?apikey=",
                ApiKey);
        }
    }
}