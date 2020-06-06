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
    /// Podesavanje validacije nad username-om applikacije
    /// </summary>
    public class UsernameRule : ValidationRule
    {
        private static readonly Regex _regexForUsername = new Regex("^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (!IsValidUsername(charString))
            {
                return new ValidationResult(false, $"Vase korisnicko ime nije u skladu sa konvencijom");
            }

            return new ValidationResult(true, null);
        }


        private static bool IsValidUsername(string text)
        {
            return _regexForUsername.IsMatch(text);
        }
    }
}
