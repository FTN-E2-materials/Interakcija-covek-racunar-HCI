using HelathClinicPatienteRole.Dialogs;
using HelathClinicPatienteRole.Model;
using HelathClinicPatienteRole.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HelathClinicPatienteRole.ViewModel
{
    class ProfilPatientViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _UsersList;
      
        public ProfilPatientViewModel()
        {
            ProfilPotvrdiCommand = new RelayCommand(ProfilPotvrdi);

            _UsersList = new ObservableCollection<User>
            {
                new User{UserId = 1,FirstName="Marko",LastName="Markovic", Jmbg="0208998500079", PhoneNumber="0602545687",Email="marko@gmail.com"}
            };
        }

        public ObservableCollection<User> Users
        {
            get { return _UsersList; }
            set { _UsersList = value; }
        }

        # region ICommand
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
        #endregion

        #region Potvrdi profil

        public RelayCommand ProfilPotvrdiCommand { get; private set; }

        public void ProfilPotvrdi(object obj)
        {
            if (Users.ElementAt(0).FirstName is null)
            {
                MessageBox.Show("Niste uneli ispravno ime!");
            }
            MessageBox.Show("Uspešno ste sačuvali promene na profilu!");
        }

        #endregion

        #region Singlton
        private static ProfilPatientViewModel instance = null;
        private static readonly object padlock = new object();


        public static ProfilPatientViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ProfilPatientViewModel();
                    }
                    return instance;
                }
            }
        }
        #endregion






        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
