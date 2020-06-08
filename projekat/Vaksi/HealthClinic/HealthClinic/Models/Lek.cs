using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;        // za podatke/tabelu


namespace HealthClinic.Models
{
    public class Lek : ObservableObject
    {
        private string _nazivLeka;
        private string _sifraLeka;
        private string _kolicina;
        private string _vrstaLeka;
        private string _nuspojave;
        private string _alergije;

        public string Alergije
        {
            get { return _alergije; }
            set
            {
                if (value != _alergije)
                {
                    _alergije = value;
                    OnPropertyChanged("Alergije");
                }
            }
        }

        public string Nuspojave
        {
            get { return _nuspojave; }
            set
            {
                if (value != _nuspojave)
                {
                    _nuspojave = value;
                    OnPropertyChanged("Nuspojave");
                }
            }
        }

        public string NazivLeka
        {
            get
            {
                return _nazivLeka;
            }
            set
            {
                if(value != _nazivLeka)
                {
                    _nazivLeka = value;
                    OnPropertyChanged("NazivLeka");
                }
            }
        }

        public string SifraLeka
        {
            get
            {
                return _sifraLeka;
            }
            set
            {
                if (value != _sifraLeka)
                {
                    _sifraLeka = value;
                    OnPropertyChanged("SifraLeka");
                }
            }
        }

        public string Kolicina
        {
            get
            {
                return _kolicina;
            }
            set
            {
                if(value != _kolicina)
                {
                    _kolicina = value;
                    OnPropertyChanged("Kolicina");
                }
            }
        }

        public string VrstaLeka
        {
            get { return _vrstaLeka; }
            set
            {
                if (value != _vrstaLeka)
                {
                    _vrstaLeka = value;
                    OnPropertyChanged("VrstaLeka");
                }

            }
        }

    }
}
