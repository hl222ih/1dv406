using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Labb1_3.Model
{
    public static class TemperatureConverter
    {

        public static int CelciusToFahrenheit(int degreesC)
        {
            return Convert.ToInt32(Math.Round(degreesC * 1.8 + 32));
        }

        public static int FahrenheitToCelcius(int degreesF)
        {
            return Convert.ToInt32(Math.Round((degreesF - 32) * 5 / 9.0));
        }
    }
}