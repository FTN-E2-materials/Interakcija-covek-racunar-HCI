using HelathClinicPatienteRole.Dialogs;
using HelathClinicPatienteRole.Model;
using HelathClinicPatienteRole.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HelathClinicPatienteRole.ViewModel
{
    class ZakaziPregledPatientViewModel : INotifyPropertyChanged
    {
        private IList<Lekar> _LekariList;

        public ZakaziPregledPatientViewModel()
        {
            PirkaziPreporukaTerminaDialogCommand = new RelayCommand(PirkaziPreporukaTerminaDialog);
            ZakaziPregledCommand = new RelayCommand(ZakaziPregled);

            _LekariList = new List<Lekar>
            {
                new Lekar{DoctorId=1, FirstAndLastName = "Pera Perić" },
                new Lekar{DoctorId=1, FirstAndLastName = "Mika Mikić" },
                new Lekar{DoctorId=1, FirstAndLastName = "Miodrag Milić" },
                new Lekar{DoctorId=1, FirstAndLastName = "Miodrag Mitrović" },
                new Lekar{DoctorId=1, FirstAndLastName = "Jovan Jovanović" },
                new Lekar{DoctorId=1, FirstAndLastName = "Milomir Mirković" },
                new Lekar{DoctorId=1, FirstAndLastName = "Stevan Jovanić" },
                new Lekar{DoctorId=1, FirstAndLastName = "Mirko Mikić" }
            };
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

        #region Preporuka termina 

        public RelayCommand PirkaziPreporukaTerminaDialogCommand { get; private set; }

        public void PirkaziPreporukaTerminaDialog(object obj)
        {
            var s = new PreporukaTerminaDialog();
            s.ShowDialog();

        }

        #endregion

        #region Selektovani Lekar

        private Lekar _selektovaniLekar;

        public Lekar SelektovaniLekar
        {
            get { return _selektovaniLekar; }
            set { _selektovaniLekar = value; OnPropertyChanged("SelektovaniLekar"); }
        }
        #endregion

        #region Zakazi pregled command

        public RelayCommand ZakaziPregledCommand { get; private set; }

        public void ZakaziPregled(object obj)
        {

            if (SelektovaniLekar is null)
            {
                MessageBox.Show("Niste selektovali ni jednog lekara!!!");
                return;
            }
            

            MessageBox.Show("Usepsno ste zakazali pregled kod " + SelektovaniLekar.FirstAndLastName  + "!!!" + SelektovaniDatum);
           // Pregled pregled = new Pregled { IdPregleda = 9, NazivPregleda = "Novo zakazan pregled", TerminPregleda = "22.06.2020  19:00h", StatusPregleda = "Zakazan", Lekar = SelektovaniLekar };
           // Pregledi.Add(pregled);



        }

        #endregion

        #region Selektovani Datum

        private DateTime _selektovaniDatum = DateTime.Now;

        public DateTime SelektovaniDatum
        {
            get { return _selektovaniDatum; }
            set { _selektovaniDatum = value; OnPropertyChanged("SelektovaniDatum"); }
        }
        #endregion

        public IList<Lekar> Lekari
        {
            get
            {
                return _LekariList;
            }
            set { _LekariList = value; }
        }

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
