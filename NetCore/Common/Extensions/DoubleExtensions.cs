using System;
using System.Threading;

namespace Common.Extensions
{
    public static class DoubleExtensions
    {
        public static double Round(this double number, int decimalPLaces)
        {
            number = number * Math.Pow(10, decimalPLaces);
            number = Math.Round(number);
            number = number / Math.Pow(10, decimalPLaces);

            return number;
        }

        public static double Truncate(this double number, int decimalPLaces)
        {
            number = number * Math.Pow(10, decimalPLaces);
            number = Math.Truncate(number);
            number = number / Math.Pow(10, decimalPLaces);

            return number;
        }

        public static string ToCurrency(this double number)
        {
            return number >= 0 ? number.ToString("N", Thread.CurrentThread.CurrentCulture) : "- " + Math.Abs(number).ToString("N", Thread.CurrentThread.CurrentCulture);
        }

        public static string ToCurrencyNoRound(this double number, int decimalPLaces)
        {
            number = Math.Truncate(number * Math.Pow(10, decimalPLaces)) / Math.Pow(10, decimalPLaces);
            return string.Format("{0:N2}", number);
        }
    }
}

