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
    /// Custom klasa za odredjivanje validne lozinke
    /// </summary>
    public class PasswordRule : ValidationRule
    {
        /// <summary>
        /// Vazenje: Minimum eight characters, at least one uppercase letter, one lowercase letter and one number:
        /// link: https://stackoverflow.com/questions/19605150/regex-for-password-must-contain-at-least-eight-characters-at-least-one-number-a
        /// </summary>
        private static readonly Regex _regexForPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (!IsValidPassword(charString))
            {
                return new ValidationResult(false, $"Bar 8 slova(1 malo, 1 veliko, 1 broj)");
            }

            return new ValidationResult(true, null);
        }


        private static bool IsValidPassword(string text)
        {
            return _regexForPassword.IsMatch(text);
        }
    }
}
