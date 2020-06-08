using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelathClinicPatienteRole.Model
{
    class Lekar : INotifyPropertyChanged
    {
        private int doctorId;
        private string firstAndLastName;

        public string FirstAndLastName
        {
            get { return firstAndLastName; }
            set { firstAndLastName = value;
                OnPropertyChanged("FirstAndLastName");
            }
        }

        public int DoctorId
        {
            get { return doctorId; }
            set { doctorId = value;
                OnPropertyChanged("DoctorId");
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
