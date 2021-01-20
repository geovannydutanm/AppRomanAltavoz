using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Globalization;
using System.Diagnostics;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using REcoSample.weather;


namespace REcoSample
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        static bool completed;
        Boolean RomanStart = false;
        private SpeechSynthesizer synth = new SpeechSynthesizer();
        int contadorStartRoman = 0;
        static CultureInfo ci = new CultureInfo("en-GB");// linguagem utilizada
        private SpeechRecognitionEngine _reconocedor = new SpeechRecognitionEngine();
        SpeechSynthesizer _responder = new SpeechSynthesizer();// sintetizador de voz

        //GD
        string _habla;

        //string RomanOperadoUI = "Roman";
        string[] _ayuda = { "que puedes hacer", "ayuda" };
        //string[] _comandos = { "buscar", "saludo", "inicia"};
        string[] _ciudades = { "Madrid", "Valencia", "Barcelona", "London", "Bucharest", "Paris" };
        //string[] pregunta = { "que es" };
        //string[] _animal = { "gato", "perro", "leon"}; //"gallina","pollo","vaca","lobo", "cabra","chivo","leon","pavo","conejo","animales","tortuga"};
        //string[] _fruta = { "manzana","pera", "tomate","zanahoria"};
        string[] _tiempo = { "que hora es", "la hora"};
        string[] _clima = {"Temperatura" }; 
        //string[] ordenTarea = { "temporizador", "alarma" };
        string[] buscadores = { "google", "wikipedia" };
        //string[] buscadores = { "cuando", "que" };
        string[] terminos = { "significa", "definicion" };


        string[] saludo = { "buenos dias", "buenas tardes", "como estas" };
        string[] _iniciarPrueba = { "inicia prueba" };
        string[] _generoMusical = { "buscar musica rock", "buscar musica electronica", "buscar musica vallenato", "buscar musica bachata", "buscar musica romantica", "buscar musica salsa", "buscar musica cumbia", "buscar musica flamenco" };

        string[] _animal = { "wikipedia gato", "wikipedia perro", "wikipedia gallo", "wikipedia gallina",
            "wikipedia pollo", "wikipedia vaca", "wikipedia lobo", "wikipedia cabra",
            "wikipedia leon", "wikipedia pavo", "wikipedia conejo", "wikipedia tortuga" };
        string[] _fruta = { "wikipedia manzana", "wikipedia pera", "wikipedia tomate", "wikipedia zanahoria", "wikipedia manzana" };

        //Comandos del sistema O

        public const int KEYEVENTF_EXTENTEDKEY = 1;
        public const int KEYEVENTF_KEYUP = 0;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;// Saltar a siguiente canción
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;// Reproducir o pausar canción
        public const int VK_MEDIA_PREV_TRACK = 0xB1;

        //GD
        SpeechRecognitionEngine sre;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);



        private void _mainUtils()
        {
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                // show installed voices
                foreach (var v in synthesizer.GetInstalledVoices())
                {
                    if (v.VoiceInfo.Description.ToLower().Contains("spain"))
                    {
                        synth.SelectVoiceByHints(v.VoiceInfo.Gender, v.VoiceInfo.Age, 2, v.VoiceInfo.Culture);
                        _responder.SelectVoiceByHints(v.VoiceInfo.Gender, v.VoiceInfo.Age, 2, v.VoiceInfo.Culture);
                    }
                }
                synth.SetOutputToDefaultAudioDevice();
                _responder.SetOutputToDefaultAudioDevice();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _mainUtils();
                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
                {
                    this.SpeakText("Buenas días!!.");
                }
                if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 19)
                {
                    this.SpeakText("Buenas tardes!!.");
                }
                if (DateTime.Now.Hour >= 19 && DateTime.Now.Hour < 23)
                {
                    this.SpeakText("Buenas noches!!.");
                }
                
                Choices lista = new Choices();
                //_ayuda,_comandos,_ciudades,pregunta,_animal,_tiempo,_clima
                lista.Add(_ayuda);
                //lista.Add(_comandos);
                lista.Add(_ciudades);
                //lista.Add(pregunta);
                lista.Add(_animal);
                lista.Add(_fruta);
                lista.Add(_tiempo);
                lista.Add(_clima);
                
                //Define que hace la altavoz
                GrammarBuilder fraseAyuda = new GrammarBuilder();
                fraseAyuda.Append(new Choices(_ayuda));
                _reconocedor.LoadGrammar(new Grammar(fraseAyuda));
                lista.Add(fraseAyuda);


                //Realiza busquedas animales
                GrammarBuilder frase2 = new GrammarBuilder();
                //frase2.Append(new Choices(pregunta));
                //frase2.Append(new Choices("un"), 0, 1);
                //frase2.Append(new Choices("una"), 0, 1);
                frase2.Append(new Choices(_animal));
                _reconocedor.LoadGrammar(new Grammar(frase2));
                lista.Add(frase2);

                //Realiza busquedas fruta
                //GrammarBuilder fraseFruta = new GrammarBuilder();
                //fraseFruta.Append(new Choices(pregunta));
                //fraseFruta.Append(new Choices("un"), 0, 1);
                ////fraseFruta.Append(new Choices("una"), 0, 1);
                //fraseFruta.Append(new Choices(_fruta));
                //_reconocedor.LoadGrammar(new Grammar(fraseFruta));
                //lista.Add(fraseFruta);

                ////hacer preguntas All
                //GrammarBuilder frasePreguntasAll = new GrammarBuilder();
                //frasePreguntasAll.Append(new Choices(pregunta));
                //frasePreguntasAll.Append(new Choices("un"), 0, 1);
                //frasePreguntasAll.Append(new Choices("una"), 0, 1);
                //_reconocedor.LoadGrammar(new Grammar(frasePreguntasAll));
                //lista.Add(frasePreguntasAll);

                //Preguntar Tiempo
                GrammarBuilder fraseTiempo = new GrammarBuilder();
                fraseTiempo.Append(new Choices(_tiempo));
                _reconocedor.LoadGrammar(new Grammar(fraseTiempo));
                lista.Add(fraseTiempo);

                //Preguntar Clima
                GrammarBuilder fraseClima = new GrammarBuilder();
                fraseClima.Append(new Choices(_clima));
                _reconocedor.LoadGrammar(new Grammar(fraseClima));
                lista.Add(fraseClima);

                //DETENER REPRODUCCION
                GrammarBuilder frasePara = new GrammarBuilder();
                frasePara.Append(new Choices("detener"));
                _reconocedor.LoadGrammar(new Grammar(frasePara));
                lista.Add(frasePara);
                
                //NAVEGA A LA SIGUIENTE
                GrammarBuilder frasSiguiente = new GrammarBuilder();
                frasSiguiente.Append(new Choices("siguiente"));
                _reconocedor.LoadGrammar(new Grammar(frasSiguiente));
                lista.Add(frasSiguiente);

                //navega hhaci atras
                GrammarBuilder fraseAntior= new GrammarBuilder();
                fraseAntior.Append(new Choices("anterior"));
                _reconocedor.LoadGrammar(new Grammar(fraseAntior));
                lista.Add(fraseAntior);

                //hacer preguntas All
                //GrammarBuilder frasePonMusica = new GrammarBuilder();
                //frasePonMusica.Append(new Choices("pon"));
                //frasePonMusica.Append(new Choices("musica"));
                ////frasePonMusica.Append(new Choices(_generoMusical));
                //_reconocedor.LoadGrammar(new Grammar(frasePonMusica));
                //lista.Add(frasePonMusica);

                GrammarBuilder fraseBuscarMusica = new GrammarBuilder();
                //fraseBuscarMusica.Append(new Choices("buscar"));
                //fraseBuscarMusica.Append(new Choices("musica"));
                fraseBuscarMusica.Append(new Choices(_generoMusical));
                //fraseBuscarMusica.Append(new Choices("en"), 0, 1);
                //fraseBuscarMusica.Append(new Choices("youtube"), 0, 1);
                _reconocedor.LoadGrammar(new Grammar(fraseBuscarMusica));
                lista.Add(fraseBuscarMusica);
                


                //GrammarBuilder fraseHora1 = new GrammarBuilder();
                //fraseHora1.Append(new Choices("que hora son"));
                //_reconocedor.LoadGrammar(new Grammar(fraseHora1));
                //lista.Add(fraseHora1);

                //GrammarBuilder fraseHora2 = new GrammarBuilder(RomanOperadoUI);
                //fraseHora2.Append(new Choices("la hora"));
                //_reconocedor.LoadGrammar(new Grammar(fraseHora2));
                //lista.Add(fraseHora2);

                //GrammarBuilder fraseHora3 = new GrammarBuilder(RomanOperadoUI);
                //fraseHora3.Append(new Choices("a que horas son"));
                //_reconocedor.LoadGrammar(new Grammar(fraseHora3));
                //lista.Add(fraseHora3);

                //                if (comandoOrden.Contains("que hora son") || comandoOrden.Contains("la hora") || comandoOrden.Contains("a que horas son"))

                Grammar gramatica = new Grammar(lista);
                try
                {
                    _reconocedor.SetInputToDefaultAudioDevice();
                    _reconocedor.LoadGrammar(gramatica);
                    _reconocedor.UpdateRecognizerSetting("CFGConfidenceRejectionThreshold", 50);
                    _reconocedor.SpeechRecognized += reconocimientoVozRoman;
                    _reconocedor.RecognizeAsync(RecognizeMode.Multiple);
                }
                catch (Exception)
                {

                }
                this.SpeakText("Estoy lista para ayudarte.");

            }
            catch (Exception ex)
            {

            }
        }

        private bool VerificaPalabra(string[] vectorBusqueda, string palabraBusqueda)
        {
            bool result = false;
            try
            {
                bool existeEnArray = false;
                for (int i = 0; i < vectorBusqueda.Length; i++)
                {
                    if (vectorBusqueda[i] == palabraBusqueda)
                    {
                        result = true;
                    }
                }

            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        int ContadorAyuda = 0;
        private void reconocimientoVozRoman(object sender, SpeechRecognizedEventArgs e)
        {
            lblHoraSystemaBig.Visible = false;
            VisualizaImagen.Image = Properties.Resources.RomanUI;
            VisualizaImagen.Visible = true;
            synth.Resume();
            String comandoOrden = e.Result.Text;
            //if ( comandoOrden.Contains("tiempo") || comandoOrden.Contains("tiempo") ||
            //    comandoOrden.Contains("Clima")  || comandoOrden.Contains("clima") ||
            //    comandoOrden.Contains("Temperatura") || comandoOrden.Contains("clima"))
            //{
            //    Random rnd = new Random();
            //    string city = _ciudades[(rnd.Next(0, _ciudades.Length))];
            //    string CityDefault = "Valencia";
            //    if (true)
            //    {
            //        getWeater(CityDefault);
            //    }
            //    else
            //    {
            //        getWeater(city);
            //    }
            //}
            if (comandoOrden.Contains("temperatura"))
            {
                Random rnd = new Random();
                string city = _ciudades[(rnd.Next(0, _ciudades.Length))];
                city = city == null ? "Valencia" : city;
                getWeater(city);
            }
            else if (comandoOrden.Contains("que puedes hacer"))/// || comandoOrden.Contains(" ayuda"))
            {
                SpeakText("Me siento muy emocionada por empezar a ayudarte");
                SpeakText("Por el momento me puedes preguntar: la hora, el tiempo, puedo ayudarte con definiciones de palabras, usando del buscador de wikipedia, visualizar imagenes si los encuentro en mi base de conocimiento y  ponerte musica en youtube");
            }
            else if (comandoOrden.Contains("que hora es"))//|| comandoOrden.Contains("que horas son"))
            {
                this.SpeakText("Son a las " + DateTime.Now.Hour.ToString() + " y " + DateTime.Now.Minute.ToString());
                lblHoraSystemaBig.Text = DateTime.Now.ToString("HH:mm:ss");
                lblHoraSystemaBig.Visible=true;
            }
            if (comandoOrden.Contains("buscar musica rock"))
            {
                Process.Start("https://www.youtube.com/watch?v=pAgnJDJN4VA&list=PLNxOe-buLm6cz8UQ-hyG1nm3RTNBUBv3K&index=20");
                System.Threading.Thread.Sleep(2000);

                Process p = Process.GetProcessesByName("chrome")[0];
                IntPtr pointer = p.Handle;
            }
            else if (comandoOrden.Contains("buscar musica electronica"))
            {
                Process.Start("https://www.youtube.com/watch?v=q6h0_9Zv7aM&list=PLK6EdlauAnBpJmGIUOWWemUMqmM1rIo8N&index=16");
                System.Threading.Thread.Sleep(2000);

                Process p = Process.GetProcessesByName("chrome")[0];
                IntPtr pointer = p.Handle;
            }
            else if (comandoOrden.Contains("buscar musica vallenato"))
            {
                Process.Start("https://www.youtube.com/watch?v=CKBLy6aG-ZI&list=PLri7antxiAGZmPgqJoV88HQHq8_VGqDPr&index=2");
                System.Threading.Thread.Sleep(2000);
                Process p = Process.GetProcessesByName("chrome")[0];
                IntPtr pointer = p.Handle;
            }
            else if (comandoOrden.Contains("buscar musica bachata"))
            {
                Process.Start("https://www.youtube.com/watch?v=cizMmhgzfOc");
                System.Threading.Thread.Sleep(2000);

                Process p = Process.GetProcessesByName("chrome")[0];
                IntPtr pointer = p.Handle;
            }
            else if (comandoOrden.Contains("buscar musica romantica"))
            {
                Process.Start("https://www.youtube.com/watch?v=uL4PAajvnrY&list=PLhEeP9J9zuhQxeDJhV-5wWEkqzWQpLZK8&index=10");
                //BALADA https://www.youtube.com/watch?v=YkVbgpXXR0M&list=PLuHuA1WMbCs0Rt-8zfTAHL5liJDwgNHgy&index=40
                System.Threading.Thread.Sleep(2000);

                Process p = Process.GetProcessesByName("chrome")[0];
                IntPtr pointer = p.Handle;
            }
            else if (comandoOrden.Contains("buscar musica salsa"))
            {
                Process.Start("https://www.youtube.com/watch?v=7kbjKCj-rMQ&list=PLhxNyMmTLzoxuruCiW1gcYfLKVpY8OkQs&index=2");
                System.Threading.Thread.Sleep(2000);

                Process p = Process.GetProcessesByName("chrome")[0];
                IntPtr pointer = p.Handle;
            }
            else if (comandoOrden.Contains("buscar musica cumbia"))
            {
                Process.Start("https://www.youtube.com/watch?v=BokdSWC2R68&list=PLI_7Mg2Z_-4IzxbySOWX5xNT7vDV2HCgG");
                System.Threading.Thread.Sleep(2000);

                Process p = Process.GetProcessesByName("chrome")[0];
                IntPtr pointer = p.Handle;
            }
            else if (comandoOrden.Contains("buscar musica flamenco"))
            {
                Process.Start("https://www.youtube.com/watch?v=XLT57AoOrwM&list=PLz1LeT5FVsBsZYX2J5yhW8tHuTsRdlAKb");
                System.Threading.Thread.Sleep(2000);

                Process p = Process.GetProcessesByName("chrome")[0];
                IntPtr pointer = p.Handle;
            }
            else if (comandoOrden.Contains("detener"))
            {
                keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
                synth.SpeakAsyncCancelAll();
            }
            else if (comandoOrden.Contains("siguiente"))
            {
                keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            }
            else if (comandoOrden.Contains("anterior"))
            {
                keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
            }
            else if (comandoOrden.Contains("wikipedia"))
            {
                string pBusqueda = comandoOrden.Split(' ')[1];
                if (pBusqueda != "")
                {
                    CargarImagen(pBusqueda);
                    string urlBUsqueda = "";// https://es.wikipedia.org/w/index.php?search="+ pBusqueda + "&title=Especial%3ABuscar&wprov=acrw1_0";
                    urlBUsqueda = "Segun Wikipedia: " + buscarWikipedia(pBusqueda);
                    synth.Resume();
                    SpeakText(urlBUsqueda);
                    if (ContadorAyuda == 0 || ContadorAyuda == 3 || ContadorAyuda == 7)
                    {
                        SpeakText("Espero haberte ayudado");
                    }
                }
            }
            //if (e.Result.Words.Count == 1)
            //{
            //    if (comandoOrden.Contains("Roman para"))
            //    {
            //        keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);

            //    }
            //}

        }

        private void CargarImagen(string imagen)
        {
            try
            {
                VisualizaImagen.Visible = false;
                if (imagen=="")
                {
                    VisualizaImagen.Image = Properties.Resources.RomanUI;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "RomanUI")
                {
                    VisualizaImagen.Image = Properties.Resources.RomanUI;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "animales")
                {
                    VisualizaImagen.Image = Properties.Resources.animalesS;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "cabra" || imagen == "chivo")
                {
                    VisualizaImagen.Image = Properties.Resources.cabra;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "conejo")
                {
                    VisualizaImagen.Image = Properties.Resources.conejo;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "gallina")
                {
                    VisualizaImagen.Image = Properties.Resources.gallina;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "gallo")
                {
                    VisualizaImagen.Image = Properties.Resources.gallo;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "gato")
                {
                    VisualizaImagen.Image = Properties.Resources.gato;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "leon")
                {
                    VisualizaImagen.Image = Properties.Resources.leon;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "lobo")
                {
                    VisualizaImagen.Image = Properties.Resources.lobo;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "pavo")
                {
                    VisualizaImagen.Image = Properties.Resources.pavo;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "perro")
                {
                    VisualizaImagen.Image = Properties.Resources.perro;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "pollo")
                {
                    VisualizaImagen.Image = Properties.Resources.pollo;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "vaca")
                {
                    VisualizaImagen.Image = Properties.Resources.vaca;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "tortuga")
                {
                    VisualizaImagen.Image = Properties.Resources.tortuga;
                    VisualizaImagen.Visible = true;
                }

                //
                if (imagen == "manzana")
                {
                    VisualizaImagen.Image = Properties.Resources.manzana;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "pera")
                {
                    VisualizaImagen.Image = Properties.Resources.pera;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "tomate")
                {
                    VisualizaImagen.Image = Properties.Resources.tomate;
                    VisualizaImagen.Visible = true;
                }
                if (imagen == "zanahoria")
                {
                    VisualizaImagen.Image = Properties.Resources.zanahoria;
                    VisualizaImagen.Visible = true;
                }

            }
            catch (Exception)
            {

            }
        }


        private string buscarWikipedia(string articulo)
        {
            WebClient wc = new WebClient();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string archivo = wc.DownloadString("https://es.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=" + articulo + "&redirects=true");

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(archivo);

            XmlNode nodo = xml.GetElementsByTagName("extract")[0];
            try
            {
                string texto = nodo.InnerText;
                Regex rx = new Regex("\\<[^\\>]*\\>");
                texto = rx.Replace(texto, "");

                texto = texto.Replace("Ã", "í");
                texto = texto.Replace("í³", "ó");
                texto = texto.Replace("í©", "é");
                texto = texto.Replace("í±", "ñ");
                texto = texto.Replace("í¡", "á");
                texto = texto.Replace("íº", "ú");
                texto = texto.Replace("[1]", "");
                texto = texto.Replace("[2]", "");
                texto = texto.Replace("[3]", "");
                texto = texto.Replace("[4]", "");
                texto = texto.Replace("[5]", "");
                texto = texto.Replace("[6]", "");
                texto = texto.Replace("[7]", "");
                texto = texto.Replace("[8]", "");
                texto = texto.Replace("[9]", "");
                texto = texto.Replace("[10]", "");
                texto = texto.Replace("â", "");
                texto = texto.Replace("€", "");
                texto = texto.Replace("‹", "");

                String resultBusqueda = texto.Substring(0, 150);

                return resultBusqueda;
            }
            catch (Exception ex) { return "El artículo no existe"; }
        }

        private void SpeakText(string Text)
        {
            try
            {
                if (Text != null)
                {
                    if (Text == "Segun Wikipedia: El artículo no existe")
                    {
                        synth.Speak("No se ha encontrado una respuesta, intenta probar de otra manera");
                    }
                    else
                    {
                        synth.Speak(Text);
                    }
                }

            }
            catch (Exception)
            {

            }
        }

        async private void getWeater(string place)
        {
            try
            {
                string apiKey = "66481bb3b15801875457a089cecd30da";
                string apiBase = "https://api.openweathermap.org/data/2.5/weather?q=";
                string unit = "metric";

                if (string.IsNullOrEmpty(place))
                {
                    SpeakText("Give name city");
                }
                string url = apiBase + place + "&appid=" + apiKey + "&units=" + unit;

                var handler = new HttpClientHandler();
                HttpClient client = new HttpClient(handler);
                string result = await client.GetStringAsync(url);
                Console.WriteLine(result);
                JObject json = JObject.Parse(result);
                WeatherGeneral weatherGeneral = new WeatherGeneral();
                foreach (KeyValuePair<String, JToken> app in json)
                {
                    var appName = app.Key;
                    switch (app.Key)
                    {
                        case "coord":
                            weatherGeneral.Coord = new WeatherCoordinates(app.Value.ToString());
                            break;
                        case "weather":
                            var objects = JArray.Parse(app.Value.ToString());
                            Weather[] weathers = new Weather[objects.Count];
                            int i = 0;
                            foreach (JObject jObj in objects)
                            {
                                Weather weather = new Weather(jObj.ToString());
                                if (weather != null)
                                {
                                    weathers[i] = weather;
                                }
                                i++;
                            }
                            weatherGeneral.Weather = weathers;
                            break;
                        case "base":
                            weatherGeneral.BaseWeather = app.Value.ToString();
                            break;
                        case "main":
                            weatherGeneral.Main = new WeatherMain(app.Value.ToString());
                            break;
                        case "visibility":
                            weatherGeneral.Visibility = (int)app.Value;
                            break;
                        case "wind":
                            weatherGeneral.Wind = new WeatherWind(app.Value.ToString());
                            break;
                        case "clouds":
                            weatherGeneral.Clouds = new WeatherClouds(app.Value.ToString());
                            break;
                        case "dt":
                            weatherGeneral.Dt = (int)app.Value;
                            break;
                        case "sys":
                            weatherGeneral.Sys = new WeatherSys(app.Value.ToString());
                            break;
                        case "timezone":
                            weatherGeneral.Timezone = (int)app.Value;
                            break;
                        case "id":
                            weatherGeneral.Id = (int)app.Value;
                            break;
                        case "name":
                            weatherGeneral.Name = app.Value.ToString();
                            break;
                        case "cod":
                            weatherGeneral.Cod = app.Value.ToString();
                            break;
                    }
                }
                //float valorT = weatherGeneral.Main.Temp;
                //string temperatura = valorT.ToString("N2");
                //if (valorT >= 0)
                //{
                //    SpeakText("Ahora en " + place.ToString() + " hay una temperatura " + temperatura + "grados");
                //}
                //{
                //    SpeakText("Ahora en " + place.ToString() + " hay una temperatura de menos" + temperatura + "grados");
                //}
                SpeakText("La temperatura actual en " + place.ToString() + " es de " + weatherGeneral.Main.Temp.ToString("N2")
                        + " grados. La temperatura maxima fue de " + weatherGeneral.Main.Temp_max.ToString("N2")
                        + " grados y la minima de " + weatherGeneral.Main.Temp_min.ToString("N2")
                        + " grados");
            }
            catch (Exception)
            {
                SpeakText("He tenido problemas, intente nuevamente");
            }
            
        }

    }
}