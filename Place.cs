using System;
namespace weather_parser
{
    public class Place
    {
        public long cityId { get; set; }
        public string cityName { get; set; }
        public double cityLon { get; set; }
        public double cityLat { get; set; }
        public string country { get; set; }
        public string zipCode { get; set; }
    }
}