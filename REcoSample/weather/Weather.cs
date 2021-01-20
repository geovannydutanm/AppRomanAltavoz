using Newtonsoft.Json.Linq;

namespace REcoSample.weather
{
    class Weather
    {
        private int id;
        private string main;
        private string description;
        private string icon;

        public Weather()
        {
        }

        public Weather(string main, string description, string icon)
        {
            this.description = description;
            this.main = main;
            this.icon = icon;
        }

        public Weather(int id, string main, string description, string icon)
        {
            this.main = main;
            this.description = description;
            this.icon = icon;
            this.id = id;
        }

        public Weather(string json)
        {
            JObject jObject = JObject.Parse(json);
            this.main = (string)jObject["main"]; ;
            this.description = (string)jObject["description"]; ;
            this.icon = (string)jObject["icon"]; ;
            this.id = (int)jObject["id"]; ;
        }

        public int Id { get => id; set => id = value; }
        public string Main { get => main; set => main = value; }
        public string Description { get => description; set => description = value; }
        public string Icon { get => icon; set => icon = value; }
    }
}
