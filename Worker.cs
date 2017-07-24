using System;
using System.Net;
using System.IO;
using System.Json;
using System.Collections.Generic;

namespace weather_parser
{
    public class Worker
    {

        // **********************************
        // Enter your APPID below:
        private const string APPID = "";
        // *********************************

        // Current weather
        private const string CURRENT_BASE_URL = "http://api.openweathermap.org/data/2.5/weather?";
        // 5 days forecast
        private const string FORECAST_BASE_URL = "http://api.openweathermap.org/data/2.5/forecast?";
       


        private string Download(string type, Place place)
        {

            string url = "";
            string CITY_COMPONENT = "";


            if (place.cityId != 0)
            {
                CITY_COMPONENT = "id=" + place.cityId;
            }
            else if (place.country != null && place.country != "" &&
                    place.zipCode != null && place.zipCode != "")
            {
                CITY_COMPONENT = "zip=" + place.zipCode + "," + place.country;
            }
            else if (place.cityLat != 0 && place.cityLon != 0)
            {

                CITY_COMPONENT = "lat=" + place.cityLat + "&lon=" + place.cityLon;
            }
            else
            {
                CITY_COMPONENT = "q=" + place.cityName;
            }

            switch (type)
            {
                case "weather":
                    {
                        url = CURRENT_BASE_URL + CITY_COMPONENT + "&appid=" + APPID;
                        break;
                    }
                case "forecast":
                    {
                        url = FORECAST_BASE_URL + CITY_COMPONENT + "&appid=" + APPID;
                        break;
                    }
                default:
                    {
                        throw
                        new ArgumentException("Wrong argument. Please use 'weather', 'forecast' or 'daily'");
                    }
            }

            WebClient client = new WebClient();
            return new StreamReader(client.OpenRead(url)).ReadToEnd();
        }


        private Weather Parse(string type, Place userPlace, JsonValue textToParse)
        {
            Weather weather = new Weather();

            JsonValue raw = null;
            if (type.Equals("weather"))
            {
                raw = JsonValue.Parse(Download(type, userPlace));
            }
            else
            {
                raw = textToParse;
            }



            var weatherArray = raw["weather"];

            weather.weatherId = weatherArray[0]["id"];
            weather.main = weatherArray[0]["main"];
            weather.description = weatherArray[0]["description"];
            weather.iconId = weatherArray[0]["icon"];



            if (raw.ContainsKey("main"))
            {
                var mainArray = raw["main"];
                weather.temp = mainArray["temp"];
                weather.pressure = mainArray["pressure"];
                weather.humidity = mainArray["humidity"];
                weather.tempMin = mainArray["temp_min"];
                weather.tempMax = mainArray["temp_max"];
            }




            if (raw.ContainsKey("wind"))
            {
                var windArray = raw["wind"];
                weather.windSpeed = windArray["speed"];
                weather.windDirection = windArray["deg"];
            }

            if (raw.ContainsKey("dt_txt"))
            {
                weather.calcDateTime = raw["dt_txt"];
            }

            if (raw.ContainsKey("clouds") && raw["clouds"] is JsonObject)
            {
                var cloudsArray = raw["clouds"];
                weather.clouds = cloudsArray["all"];
            }


            if (raw.ContainsKey("rain"))
            {
                if (raw["rain"].ContainsKey("3h"))
                {
                    weather.rainVolume = raw["rain"]["3h"];
                }
            }
            if (raw.ContainsKey("snow"))
            {
                if (raw["snow"].ContainsKey("3h"))
                {
                    weather.snowVolume = raw["snow"]["3h"];
                }
            }

            if (raw.ContainsKey("temp"))
            {
                weather.tempMin = raw["temp"]["min"];
                weather.tempMax = raw["temp"]["max"];
                weather.mornTemp = raw["temp"]["morn"];
                weather.dayTemp = raw["temp"]["day"];
                weather.nightTemp = raw["temp"]["night"];
                weather.eveTemp = raw["temp"]["eve"];
            }

            if (raw.ContainsKey("pressure"))
            {
                weather.pressure = raw["pressure"];
            }

            if (raw.ContainsKey("humidity"))
            {
                weather.humidity = raw["humidity"];
            }
            if (raw.ContainsKey("speed"))
            {
                weather.windSpeed = raw["speed"];
            }

            if (raw.ContainsKey("deg"))
            {
                weather.windDirection = raw["deg"];
            }

            Place place = new Place();

            if (raw.ContainsKey("sys") && raw["sys"].ContainsKey("country"))
            {
                place.country = raw["sys"]["country"];
            }

            if (raw.ContainsKey("name"))
            {
                place.cityName = raw["name"];
            }


            weather.place = place;

            return weather;
        }


        public Weather getCurrentWeather(Place place)
        {
            return Parse("weather", place, null);
        }

        public List<Weather> getForFiveDays(Place place)
        {
            JsonValue rawJson = JsonValue.Parse(Download("forecast", place));

            JsonArray listArray = (System.Json.JsonArray)rawJson["list"];

            List<Weather> listOfWeather = new List<Weather>();
            Weather weather = null;
            foreach (JsonObject weatherObject in listArray)
            {
                weather = Parse("forecast", null, weatherObject);
                listOfWeather.Add(weather);

            }

            return listOfWeather;
        }

       
    }
}
