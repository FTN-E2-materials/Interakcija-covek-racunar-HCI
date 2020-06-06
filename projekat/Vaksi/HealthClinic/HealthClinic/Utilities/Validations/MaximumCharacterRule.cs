using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HealthClinic.Utilities.Validations
{
    public class MaximumCharacterRule : ValidationRule
    {
        public int MaximumCharacters { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (charString.Length > MaximumCharacters)
                return new ValidationResult(false, $"Maksimalan broj karaktera je {MaximumCharacters} ");

            return new ValidationResult(true, null);
        }
    }
}
