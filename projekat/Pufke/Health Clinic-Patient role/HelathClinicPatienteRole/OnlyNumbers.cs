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
    class OnlyNumbers : ValidationRule
    {
        public int MinimumCharacters { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
           
            string charString = value as string;

            if (!Regex.IsMatch(charString, @"^[0-9]+$"))
            {
                return new ValidationResult(false, $"Dozvoljene su samo cifre!");
            }


            return new ValidationResult(true, null);
        }

       
    }
}