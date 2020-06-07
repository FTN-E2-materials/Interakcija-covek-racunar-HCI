using HealthClinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.ViewModels
{
    public class UcitavanjeViewModel
    {
        public Zaposlen Zaposlen { get; set; } = new Zaposlen()
        {
            Ime = "Vlado",
            Prezime = "Maksimovic",
            Struka = "Dr.",
            Sifra = "1234"
        };

    }
}
