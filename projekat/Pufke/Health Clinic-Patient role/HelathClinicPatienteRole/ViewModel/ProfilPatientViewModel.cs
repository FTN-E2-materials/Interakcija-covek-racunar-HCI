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
using System.Windows.Controls;
using System.Windows.Input;

namespace HelathClinicPatienteRole.ViewModel
{
    class ProfilPatientViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> _UsersList;
      
        public ProfilPatientViewModel()
        {
          
            IskljuciToolTipsCommand = new RelayCommand(IskljuciToolTips);
            UkljuciToolTipsCommand = new RelayCommand(UkljuciToolTips);
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

        #region  Iskljuci ToolTipove

        public RelayCommand IskljuciToolTipsCommand { get; private set; }

        private bool _isToolTipVisible = true;
        public void IskljuciToolTips(object obj)
        {
            Style style = new Style(typeof(ToolTip));
            style.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            style.Seal();

            if (_isToolTipVisible)
            {
                _isToolTipVisible = false;
                foreach (Window window in Application.Current.Windows)
                {
                    window.Resources.Add(typeof(ToolTip), style); //Show
                   
                    /* _isToolTipVisible = true;
                     window.Resources.Remove(typeof(ToolTip));  //hide*/

                }
                MessageBox.Show("Uspešno isključeni tooltipovi");
            }
            else
            {
                MessageBox.Show("Već ste isključili tooltipove!");
            }
        }

        #endregion

        #region  Ukljkuci ToolTipove

        public RelayCommand UkljuciToolTipsCommand { get; private set; }

        public void UkljuciToolTips(object obj)
        {
            Style style = new Style(typeof(ToolTip));
            style.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            style.Seal();

            if (!_isToolTipVisible)
            {
                _isToolTipVisible = true;
                foreach (Window window in Application.Current.Windows)
                {
                   

                     _isToolTipVisible = true;
                     window.Resources.Remove(typeof(ToolTip));  //hide
                 

                }
                MessageBox.Show("Uspešno uključeni tooltipovi");
            }
            else
            {
                MessageBox.Show("Već su uključeni tooltipovi");
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
