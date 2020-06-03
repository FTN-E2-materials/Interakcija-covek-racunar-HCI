using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelathClinicPatienteRole.Model
{
    class Pregled : INotifyPropertyChanged
    {
        private string nazivPregleda;
        private string terminPregleda;
        private string statusPregleda;

        public string StatusPregleda
        {
            get
            {
                return statusPregleda;
            }
            set
            {
                statusPregleda = value;
                OnPropertyChanged("StatusPregleda");
            }
        }

        public string TerminPregleda
        {
            get
            {
                return terminPregleda;
            }
            set
            {
                terminPregleda = value;
                OnPropertyChanged("TerminPregleda");
            }
        }
        public string NazivPregleda
        {
            get
            {
                return nazivPregleda;
            }
            set
            {
                nazivPregleda = value;
                OnPropertyChanged("NazivPregleda");
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
