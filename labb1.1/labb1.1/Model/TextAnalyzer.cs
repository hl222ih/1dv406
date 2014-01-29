using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labb1._1.Model
{
    public static class TextAnalyzer
    {
        /// <summary>
        /// Beräknar antalet versaler i inskickad sträng.
        /// </summary>
        /// <param name="text">Sträng som ska testas.</param>
        /// <returns>Antalet versaler i strängen.</returns>
        public static int GetNumberOfCapitals(string text)
        {
            return text.Count(c => char.IsLetter(c) && c.Equals(char.ToUpper(c)));
        }
    }
}