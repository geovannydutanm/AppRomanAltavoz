using Newtonsoft.Json.Linq;

namespace REcoSample.weather
{
    class WeatherCoordinates
    {
        private float lon;
        private float lat;

        public WeatherCoordinates() { }

        public WeatherCoordinates(float lon, float lat)
        {
            this.Lon = lon;
            this.Lat = lat;
        }

        public WeatherCoordinates(string json)
        {
            JObject jObject = JObject.Parse(json);
            this.lon = (float)jObject["lon"];
            this.lat = (float)jObject["lat"];
        }

        public float Lon { get => lon; set => lon = value; }
        public float Lat { get => lat; set => lat = value; }
    }
}
