
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace LibraryRomanApp
{
    public class UtilitiesRomanApp
    {
        //public string buscarWikipedia(string articulo)
        //{
        //    WebClient wc = new WebClient();
        //    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    string archivo = wc.DownloadString("https://es.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=" + articulo + "&redirects=true");

        //    XmlDocument xml = new XmlDocument();
        //    xml.LoadXml(archivo);

        //    XmlNode nodo = xml.GetElementsByTagName("extract")[0];
        //    try
        //    {
        //        string texto = nodo.InnerText;
        //        Regex rx = new Regex("\\<[^\\>]*\\>");
        //        texto = rx.Replace(texto, "");

        //        texto = texto.Replace("Ã", "í");
        //        texto = texto.Replace("í³", "ó");
        //        texto = texto.Replace("í©", "é");
        //        texto = texto.Replace("í±", "ñ");
        //        texto = texto.Replace("í¡", "á");
        //        texto = texto.Replace("íº", "ú");

        //        return texto;
        //    }
        //    catch (Exception ex) { return "El artículo no existe"; }
        //}
    }
}
