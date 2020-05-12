using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;        // za podatke/tabelu

namespace HealthClinic.Models
{
    public class Prostorija : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _odeljenje;
        private string _brojSobe;
        private string _namena;
        private string _oprema;     //TODO: Na otvaranje ovoga dobijamo dijalog sa tabelom opreme
        private string _zauzetost;  //TODO: Na otvaranje ovoga mozemo da vidimo kad je soba zauzeta/slobodna

        public string Zauzetost
        {
            get { return _zauzetost; }
            set { _zauzetost = value; OnPropertyChanged("Zauzetost"); }
        }

        public string Oprema
        {
            get { return _oprema; }
            set { _oprema = value;  OnPropertyChanged("Oprema"); }
        }


        public string Namena
        {
            get { return _namena; }
            set { _namena = value; OnPropertyChanged("Namena"); }
        }



        public string BrojSobe
        {
            get { return _brojSobe; }
            set { _brojSobe = value; OnPropertyChanged("BrojSobe"); }
        }


        public string Odeljenje
		{
			get { return _odeljenje; }
			set { _odeljenje = value; OnPropertyChanged("Odeljenje"); }
		}

	}
}
