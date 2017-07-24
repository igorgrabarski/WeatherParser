using System;
using System.Collections.Generic;

namespace weather_parser
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Worker worker = new Worker();
            Place place = new Place();
            //place.cityName = "London,ON";
            place.cityId = 1851632;
            //place.cityLat = 35;
            //place.cityLon = 139;

            // Not all cities are located with zip code!!!
            //place.country = "ca";
            //place.zipCode = "n6g5e4";
            //Weather weather = worker.getCurrentWeather(place);

            //Console.WriteLine(weather.description);
            //Console.WriteLine(weather.main);
            //Console.WriteLine(weather.iconId);
            //Console.WriteLine(weather.weatherId);
            //Console.WriteLine(weather.temp);
            //Console.WriteLine(weather.pressure);
            //Console.WriteLine(weather.humidity);
            //Console.WriteLine(weather.tempMax);
            //Console.WriteLine(weather.tempMin);
            //Console.WriteLine(weather.windSpeed);
            //Console.WriteLine(weather.windDirection);
            //Console.WriteLine(weather.rainVolume);
            //Console.WriteLine(weather.snowVolume);
            //Console.WriteLine(weather.place.cityName);
            //Console.WriteLine(weather.place.country);


            List<Weather>  listOfWeather  = worker.getForFiveDays(place);

            foreach (var weather in listOfWeather)
            {
                Console.WriteLine(weather.calcDateTime + " : " +weather.description);
            }

        }
    }
}
