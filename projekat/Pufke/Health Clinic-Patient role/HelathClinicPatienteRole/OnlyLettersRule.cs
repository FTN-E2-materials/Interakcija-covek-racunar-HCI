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
    class OnlyLettersRule : ValidationRule
    {
        public int MinimumCharacters { get; set; }
       

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string charString = value as string;

            if (!Regex.IsMatch(charString, @"^[a-zA-Z]+$"))
            {
                return new ValidationResult(false, $"Dozvoljena su samo slova!");
            }
           

            return new ValidationResult(true, null);
        }

 
    }
}

