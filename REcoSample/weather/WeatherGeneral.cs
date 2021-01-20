using REcoSample.weather;

namespace REcoSample
{
    class WeatherGeneral
    {
        private WeatherCoordinates coord;
        private Weather[] weather;
        private string baseWeather;
        private WeatherMain main;
        private int visibility;
        private WeatherWind wind;
        private WeatherClouds clouds;
        private int dt;
        private WeatherSys sys;
        private int timezone;
        private int id;
        private string name;
        private string cod;

        public WeatherGeneral()
        {
        }

        public WeatherGeneral(WeatherCoordinates coord, Weather[] weather, string baseWeather, WeatherMain main, int visibility, WeatherWind wind, WeatherClouds clouds, int dt, WeatherSys sys, int timezone, int id, string name, string cod)
        {
            this.coord = coord;
            this.weather = weather;
            this.baseWeather = baseWeather;
            this.main = main;
            this.visibility = visibility;
            this.wind = wind;
            this.clouds = clouds;
            this.dt = dt;
            this.sys = sys;
            this.timezone = timezone;
            this.id = id;
            this.name = name;
            this.cod = cod;
        }

        public string BaseWeather { get => baseWeather; set => baseWeather = value; }
        public int Visibility { get => visibility; set => visibility = value; }
        public int Dt { get => dt; set => dt = value; }
        public int Timezone { get => timezone; set => timezone = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Cod { get => cod; set => cod = value; }
        internal WeatherCoordinates Coord { get => coord; set => coord = value; }
        internal Weather[] Weather { get => weather; set => weather = value; }
        internal WeatherMain Main { get => main; set => main = value; }
        internal WeatherWind Wind { get => wind; set => wind = value; }
        internal WeatherClouds Clouds { get => clouds; set => clouds = value; }
        internal WeatherSys Sys { get => sys; set => sys = value; }
    }
}
