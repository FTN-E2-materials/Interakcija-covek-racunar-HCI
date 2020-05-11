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

        public int UserId { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
