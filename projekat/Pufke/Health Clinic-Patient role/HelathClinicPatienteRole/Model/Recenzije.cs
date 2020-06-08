using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelathClinicPatienteRole.Model
{
    class Recenzije : INotifyPropertyChanged
    {
        private string korisnik;
        private string recenzija;

        public string Korisnik
        {
            get { return korisnik; }
            set
            {
                korisnik = value;
                OnPropertyChanged("Korisnik");
            }
        }

        public string Recenzija
        {
            get { return recenzija; }
            set
            {
                recenzija = value;
                OnPropertyChanged("Recenzija");
            }
        }

        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

       
        #endregion
    }
}