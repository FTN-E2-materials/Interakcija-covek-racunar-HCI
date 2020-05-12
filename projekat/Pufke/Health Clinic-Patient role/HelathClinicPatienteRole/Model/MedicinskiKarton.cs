using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelathClinicPatienteRole.Model
{
    class MedicinskiKarton : INotifyPropertyChanged
    {
        private int userId;
        private string firstAndLastName;
        private string parentName;
        private string dateOfBirth;
        private string jmbg;
        private string address;
        private string phoneNumber;
        private string healthInsuranceNumber;
        private string healthInsuranceCarrier; //Nosilac zdravstvenog osiguranja

        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public string FirstAndLastName
        {
            get
            {
                return firstAndLastName;
            }
            set
            {
                firstAndLastName = value;
                OnPropertyChanged("FirstAndLastName");
            }
        }

        public string ParentName
        {
            get
            {
                return parentName;
            }
            set
            {
                parentName = value;
                OnPropertyChanged("ParentName");
            }
        }

        public string DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public string Jmbg
        {
            get
            {
                return jmbg;
            }
            set
            {
                jmbg = value;
                OnPropertyChanged("Jmbg");
            }
        }

        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public string HealthInsuranceNumber
        {
            get
            {
                return healthInsuranceNumber;
            }
            set
            {
                healthInsuranceNumber = value;
                OnPropertyChanged("HealthInsuranceNumber");
            }
        }

        public string HealthInsuranceCarrier
        {
            get
            {
                return healthInsuranceCarrier;
            }
            set
            {
                healthInsuranceCarrier = value;
                OnPropertyChanged("HealthInsuranceCarrier");
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
