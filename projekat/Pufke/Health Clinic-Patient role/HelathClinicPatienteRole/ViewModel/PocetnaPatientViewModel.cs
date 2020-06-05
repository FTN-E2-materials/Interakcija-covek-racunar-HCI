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
    class PocetnaPatientViewModel : INotifyPropertyChanged
    {
        private IList<Pregled> _PregledList;
      

        public PocetnaPatientViewModel()
        {
            PirkaziIzmeniPregledDialogCommand = new RelayCommand(PirkaziIzmeniPregledDialog);
            PirkaziOtkaziPregledDialogCommand = new RelayCommand(PirkaziOtkaziPregledDialog);
            PirkaziAnketaLekaraDialogCommand = new RelayCommand(PirkaziAnketaLekaraDialog);
            ProcitajViseDialogCommand = new RelayCommand(ProcitajViseDialog);
            _PregledList = new List<Pregled>
            {
                new Pregled{NazivPregleda = "Specijalisticki pregled",TerminPregleda = "22.06.2020  19:00h",StatusPregleda="Zakazan",Lekar="Pera Perić"},
                new Pregled{NazivPregleda = "Ortorinolaringoloski pregled",TerminPregleda = "26.06.2020  9:00h",StatusPregleda="Obavljen",Lekar="Mika Mikić"},
                new Pregled{NazivPregleda = "Očni pregled",TerminPregleda = "28.06.2020  8:00h",StatusPregleda="Zakazan",Lekar="Miodrag Milić"},
                new Pregled{NazivPregleda = "Specijalisticki pregled",TerminPregleda ="30.06.2020  11:00h",StatusPregleda="Obavljen",Lekar="Miodrag Mitrović"},
                new Pregled{NazivPregleda = "Specijalisticki pregled",TerminPregleda ="30.06.2020  11:00h",StatusPregleda="Obavljen",Lekar="Jovan Jovanović"},
                new Pregled{NazivPregleda = "Specijalisticki pregled",TerminPregleda = "22.06.2020  19:00h",StatusPregleda="Zakazan",Lekar="Milomir Mirković"},
                new Pregled{NazivPregleda = "Ortorinolaringoloski pregled",TerminPregleda = "26.06.2020  9:00h",StatusPregleda="Obavljen",Lekar="Stevan Jovanić"},
                new Pregled{NazivPregleda = "Očni pregled",TerminPregleda = "28.06.2020  8:00h",StatusPregleda="Zakazan",Lekar="Mirko Mikić"}
            };
        }

        public IList<Pregled> Pregledi
        {
            get {  return _PregledList;
            }
            set { _PregledList = value; }
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
        #region Procitaj vise dialog

        public RelayCommand ProcitajViseDialogCommand { get; private set; }

        public void ProcitajViseDialog(object obj)
        {
            var s = new ProcitajVise();
            s.ShowDialog();

        }

        #endregion

        #region Anketa lekara 

        public RelayCommand PirkaziAnketaLekaraDialogCommand { get; private set; }

        public void PirkaziAnketaLekaraDialog(object obj)
        {
            var s = new AnketaPregledaDialog();
            s.DataContext = this;
            s.ShowDialog();

        }

        #endregion

        #region Izmeni pregled Dialog 

        public RelayCommand PirkaziIzmeniPregledDialogCommand { get; private set; }

        public void PirkaziIzmeniPregledDialog(object obj)
        {
            var s = new IzmenaPregledaDialog();
             s.DataContext = this;             // kako bi prebacio podatke iz ovog prozora u dijalog
             s.ShowDialog();

        }

        #endregion


        #region Otkazi pregled Dialog 

        public RelayCommand PirkaziOtkaziPregledDialogCommand { get; private set; }

        public void PirkaziOtkaziPregledDialog(object obj)
        {
            var s = new OtkaziPregled();
            s.ShowDialog();

        }

        #endregion

   

        #region Selektovani pregled

        private Pregled _selektovaniPregled;

        public Pregled SelektovaniPregled
        {
            get { return _selektovaniPregled; }
            set { _selektovaniPregled = value; OnPropertyChanged("SelektovaniPregled"); }
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
