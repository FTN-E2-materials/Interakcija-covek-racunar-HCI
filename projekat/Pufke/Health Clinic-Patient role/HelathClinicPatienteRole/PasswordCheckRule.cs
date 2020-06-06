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
    class PasswordCheckRule : ValidationRule
    {
        public int MinimumCharacters { get; set; }
   

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

        
            if (charString.Length < MinimumCharacters)
                return new ValidationResult(false, $"Šifra mora da sadrži minimun {MinimumCharacters} karaktera!.");

            return new ValidationResult(true, null);
        }


    }
}
