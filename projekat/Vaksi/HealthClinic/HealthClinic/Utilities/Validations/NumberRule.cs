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
    /// Restrikcija koriscena samo brojeva
    /// </summary>
    public class NumberRule : ValidationRule
    {
        /// <summary>
        /// Jedan ili vise brojeva samo
        /// </summary>
        private static readonly Regex _regexForNumber = new Regex("^[0-9]+$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (!IsOnlyNumber(charString))
            {
                return new ValidationResult(false, $"U ovo polje je dozvoljeno samo unositi brojeve");
            }

            return new ValidationResult(true, null);
        }

        private static bool IsOnlyNumber(string text)
        {
            return _regexForNumber.IsMatch(text);
        }
    }
}
