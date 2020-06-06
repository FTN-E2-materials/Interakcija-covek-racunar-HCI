using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HelathClinicPatienteRole
{
    public class MinimumCharacterRule : ValidationRule
    {
        public int MinimumCharacters { get; set; }
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex samo za brojeve

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (IsTextNumber(charString))
            {
                return new ValidationResult(false, $"Samo cifre!");
            }
            if (!(charString.Length == MinimumCharacters))
                return new ValidationResult(false, $"JMBG sadrzi tacno {MinimumCharacters} cifara!.");

            return new ValidationResult(true, null);
        }

        private static bool IsTextNumber(string text)
        {
            return _regex.IsMatch(text);
        }
    }
}