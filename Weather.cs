using System;
namespace weather_parser
{
    public class Weather
    {
        public Place place
        {
            get;
            set;
        }


        public long weatherId
        {
            get;
            set;
        }

        public string description
        {
            get;
            set;
        }


        public string main
        {
            get;
            set;
        }

        public string iconId
        {
            get;
            set;
        }

        public int clouds
        {
            get;
            set;
        }

        public double windSpeed
        {
            get;
            set;
        }

        public double windDirection
        {
            get;
            set;
        }

        public double rainVolume
        {
            get;
            set;
        }

        public double snowVolume
        {
            get;
            set;
        }

        public string calcDateTime
        {
            get;
            set;
        }
 
        public double temp
        {
            get;
            set;
        }

        public double tempMin
        {
            get;
            set;
        }

        public double tempMax
        {
            get;
            set;
        }

        public double pressure
        {
            get;
            set;
        }

        public double seaLevel
        {
            get;
            set;
        }

        public double grndLevel
        {
            get;
            set;
        }

        public int humidity
        {
            get;
            set;
        }

        public double dayTemp
        {
            get;
            set;
        }

        public double nightTemp
        {
            get;
            set;
        }

        public double mornTemp
        {
            get;
            set;
        }

        public double eveTemp
        {
            get;
            set;
        }

    }
}
