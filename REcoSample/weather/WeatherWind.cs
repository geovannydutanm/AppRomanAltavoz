using Newtonsoft.Json.Linq;

namespace REcoSample.weather
{
    class WeatherWind
    {
        private float speed;
        private int deg;

        public WeatherWind() { }

        public WeatherWind(float speed, int deg)
        {
            this.speed = speed;
            this.deg = deg;
        }

        public WeatherWind(string json)
        {
            JObject jObject = JObject.Parse(json);
            this.speed = (float)jObject["speed"];
            this.deg = (int)jObject["deg"];
        }

        public float Speed { get => speed; set => speed = value; }
        public int Deg { get => deg; set => deg = value; }
    }
}
