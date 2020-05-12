using HelathClinicPatienteRole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HelathClinicPatienteRole.ViewModel
{
    class KartonPatientViewModel
    {
        private MedicinskiKarton _MedicinskiKarton;

        public KartonPatientViewModel()
        {
            _MedicinskiKarton = new MedicinskiKarton { UserId = 1, FirstAndLastName = "Marko Marković", ParentName = "Stefan", DateOfBirth = "01.01.1980", Jmbg = "0208998500079", Address = "Narodnog Fronta, Novi Sad", PhoneNumber = "0622554652", HealthInsuranceNumber = "251365", HealthInsuranceCarrier = "Stefan Marković" };

        }

        public MedicinskiKarton MedicinskiKarton
        {
            get { return _MedicinskiKarton; }
            set { _MedicinskiKarton = value; }
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        private class Updater : ICommand
        {
            #region ICommand Members  

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {

            }

            #endregion
        }
    }
}
