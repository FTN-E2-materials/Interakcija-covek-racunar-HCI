using C_Validation_ByCustom;
using HelathClinicPatienteRole.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HelathClinicPatienteRole.ViewModel
{
    class LoginViewModel : ObservableObject
    {
        private string _username;
        private string _password;


        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }




        #region Login Command

        public RelayCommand LoginCommand { get; private set; }

        public void Login(object obj)
        {

            if (Username is null)
            {
                MessageBox.Show("Niste uneli JMBG!");
                return;
            }
            if (Password is null)
            {
                MessageBox.Show("Niste uneli šifru!");
                return;
            }
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            this.Visibility = Visibility.Hidden;
            patientMainWindow.Show();

            var win = new WizardWindow();
            win.ShowDialog();


        }

        #endregion

        public string Username
        {
            get { return _username; }
            set
            {
                OnPropertyChanged(ref _username, value);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                OnPropertyChanged(ref _password, value);
            }
        }

        #region ICommand
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

        public Visibility Visibility { get; private set; }

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
    }
}
