using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HealthClinic.Utilities.Validations
{
    /// <summary>
    /// Pravilo ogranicenja na samo reci u okviru polja.
    /// </summary>
    public class WordRule : ValidationRule
    {
        /// <summary>
        /// Regex za reci po stekovom predlogu.
        /// link: https://stackoverflow.com/questions/2385701/regular-expression-for-first-and-last-name
        /// </summary>
        private static readonly Regex _regex = new Regex("^[\\w'\\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\\]]{2,}$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (!IsWord(charString))
            {
                return new ValidationResult(false, $"Unelite ste nedozvoljene karaktere");
            }

            return new ValidationResult(true, null);
        }

        private static bool IsWord(string text)
        {
            return _regex.IsMatch(text);
        }
    }
}
