using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;        // za podatke/tabelu

namespace HealthClinic.Models
{
    public class Zaposlen : ObservableObject
    {
        private string _ime;
        private string _prezime;
        private string _struka;
        private string _sifra;  
        private string _korisnickoIme;
        private BusinessHours _businessHours;

        public BusinessHours RadniKalendar
        {
            get { return _businessHours; }
            set
            {
                if (!(value.Equals(_businessHours)))
                {
                    _businessHours = value;
                    OnPropertyChanged("RadniKalendar");
                }

            }
        }



        public string KorisnickoIme
        {
            get { return _korisnickoIme; }
            set 
            { 
                if( value != _korisnickoIme)
                {
                    _korisnickoIme = value;
                    OnPropertyChanged("KorisnickoIme");
                }
                
            }
        }

        public string Ime
        {
            get
            {
                return _ime;
            }
            set
            {
                if(value != _ime)
                {
                    _ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        public string Prezime
        {
            get
            {
                return _prezime;
            }
            set
            {
                if (value != _prezime)
                {
                    _prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }

        public string Struka
        {
            get
            {
                return _struka;
            }
            set
            {
                if (value != _struka)
                {
                    _struka = value;
                    OnPropertyChanged("Struka");
                }
            }
        }

        public string Sifra
        {
            get
            {
                return _sifra;
            }
            set
            {
                if (value != _sifra)
                {
                    _sifra = value;
                    OnPropertyChanged("Sifra");
                }
            }
        }

    }
}
