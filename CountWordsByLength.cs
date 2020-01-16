using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab1
{
    public class CountWordsByLength
    {
        public static int GetCountWordsByLength(string text, int length)
        {
            char[] delimiters = new char[] { ' ', '\r', '\n', ',', '?', '-', '.', ':' }; // разделители
            var words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries); // все слова
            return words.Where(x => x.Length == length).ToList().Count; // количество слов по условию 
        }
    }
}
