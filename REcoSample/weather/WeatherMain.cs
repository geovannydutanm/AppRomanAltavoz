using Newtonsoft.Json.Linq;

namespace REcoSample.weather
{
    class WeatherMain
    {
        private float temp;
        private float feels_like;
        private float temp_min;
        private float temp_max;
        private float pressure;
        private float humidity;

        public WeatherMain()
        {
        }

        public WeatherMain(float temp, float feels_like, float temp_min, float temp_max, float pressure, float humidity)
        {
            this.temp = temp;
            this.feels_like = feels_like;
            this.temp_min = temp_min;
            this.temp_max = temp_max;
            this.pressure = pressure;
            this.humidity = humidity;
        }

        public WeatherMain(string json)
        {
            JObject jObject = JObject.Parse(json);
            this.temp = (float)jObject["temp"];
            this.feels_like = (float)jObject["feels_like"];
            this.temp_min = (float)jObject["temp_min"];
            this.temp_max = (float)jObject["temp_max"];
            this.pressure = (float)jObject["pressure"];
            this.humidity = (float)jObject["humidity"];
        }

        public float Temp { get => temp; set => temp = value; }
        public float Feels_like { get => feels_like; set => feels_like = value; }
        public float Temp_min { get => temp_min; set => temp_min = value; }
        public float Temp_max { get => temp_max; set => temp_max = value; }
        public float Pressure { get => pressure; set => pressure = value; }
        public float Humidity { get => humidity; set => humidity = value; }
    }
}
