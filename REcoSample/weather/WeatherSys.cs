using Newtonsoft.Json.Linq;

namespace REcoSample.weather
{
    class WeatherSys
    {
        private int type;
        private int id;
        private string country;
        private int sunrise;
        private int sunset;

        public WeatherSys()
        {
        }
        public WeatherSys(int type, int id, string country, int sunrise, int sunset)
        {
            this.type = type;
            this.id = id;
            this.country = country;
            this.sunrise = sunrise;
            this.sunset = sunset;
        }

        public WeatherSys(string json)
        {
            JObject jObject = JObject.Parse(json);
            this.type = (int)jObject["type"];
            this.id = (int)jObject["id"];
            this.country = (string)jObject["country"];
            this.sunrise = (int)jObject["sunrise"];
            this.sunset = (int)jObject["sunset"];
        }

        public int Type { get => type; set => type = value; }
        public int Id { get => id; set => id = value; }
        public string Country { get => country; set => country = value; }
        public int Sunrise { get => sunrise; set => sunrise = value; }
        public int Sunset { get => sunset; set => sunset = value; }
    }
}
