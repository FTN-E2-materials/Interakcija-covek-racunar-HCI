using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;        // za podatke/tabelu

namespace HealthClinic.Models
{
    public class Prostorija : ObservableObject
    {

        private string _odeljenje;
        private string _brojSobe;
        private string _namena;
        private string _spisakOpreme;     //TODO: Na otvaranje ovoga dobijamo dijalog sa tabelom opreme
        private string _uvidZauzetosti;  //TODO: Na otvaranje ovoga mozemo da vidimo kad je soba zauzeta/slobodna
        private List<Oprema> _opremaProstorije;
        private FizickiRad _fizickiRadovi;


        public string Odeljenje
        {
            get { return _odeljenje; }
            set { _odeljenje = value; OnPropertyChanged("Odeljenje"); }
        }

        public string BrojSobe
        {
            get { return _brojSobe; }
            set { _brojSobe = value; OnPropertyChanged("BrojSobe"); }
        }

        public string Namena
        {
            get { return _namena; }
            set { _namena = value; OnPropertyChanged("Namena"); }
        }

        public string SpisakOpreme
        {
            get { return _spisakOpreme; }
            set { _spisakOpreme = value; OnPropertyChanged("SpisakOpreme"); }
        }

        public string UvidZauzetosti
        {
            get { return _uvidZauzetosti; }
            set { _uvidZauzetosti = value; OnPropertyChanged("UvidZauzetosti"); }
        }

        public List<Oprema> OpremaProstorije
        {
            get { return _opremaProstorije; }
            set { _opremaProstorije = value; OnPropertyChanged("OpremaProstorije"); }
        }

        public FizickiRad FizickiRadovi
        {
            get { return _fizickiRadovi; }
            set { _fizickiRadovi = value; OnPropertyChanged("FizickiRadovi"); }
        }











    }
}
