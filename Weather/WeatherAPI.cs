using HtmlAgilityPack;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;



namespace Weather
{
    class WeatherAPI
    {
        //статический метод для получения информации о погоде
        public static WeatherModel GetForecast(string City)
        {   
            //получение координат по городу
            string[] coords = GetCoordinates(City).Split(", ");

            //строка для хранения url
            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                $"lat={coords[0].Substring(0, coords[0].Length - 1)}" +
                $"&lon={coords[1].Substring(0, coords[1].Length - 1)}" +
                $"&appid=982b29008a5849b15ae6c0876a6f5cc8";
           
            //подключение 
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            //получение ответа
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            string info = "";
            //чтение + сохранение в строку
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                info = sr.ReadToEnd();
                sr.Close();
            }
            //метод возвращает полученную строку с информацией
            WeatherModel wm = JsonSerializer.Deserialize<WeatherModel>(info);
            return wm;
        }

        public static string GetCoordinates(string City) {
            HtmlWeb web = new HtmlWeb();
            //загружается сайт для парсинга широты и долготы
            HtmlDocument doc = web.Load($"https://latitudelongitude.org/ru/{City.ToLower()}/");
            //по узлам находится искомый текст
            var coords = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/section[1]/p/span").First().InnerText;
            //методв возвращает координты в виде строки
            return coords;
        }
    }
}
