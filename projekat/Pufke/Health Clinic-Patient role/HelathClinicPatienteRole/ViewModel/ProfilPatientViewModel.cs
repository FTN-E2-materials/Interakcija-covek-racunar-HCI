using HelathClinicPatienteRole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HelathClinicPatienteRole.ViewModel
{
    class ProfilPatientViewModel
    {
        private IList<User> _UsersList;
      
        public ProfilPatientViewModel()
        {
            _UsersList = new List<User>
            {
                new User{UserId = 1,FirstName="Marko",LastName="Markovic", Jmbg="0208998500079", PhoneNumber="0602545687",Email="marko@gmail.com"}
            };
        }

        public IList<User> Users
        {
            get { return _UsersList; }
            set { _UsersList = value; }
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
