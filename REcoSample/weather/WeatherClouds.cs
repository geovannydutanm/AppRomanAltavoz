using Newtonsoft.Json.Linq;

namespace REcoSample.weather
{
    class WeatherClouds
    {
        private int all;

        public WeatherClouds() { }
        public WeatherClouds(int all)
        {
            this.All = all;
        }

        public WeatherClouds(string json)
        {
            JObject jObject = JObject.Parse(json);
            this.all = (int)jObject["all"];
        }

        public int All { get => all; set => all = value; }
    }
}
