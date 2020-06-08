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
    class RecenzijaAppPatientViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Recenzije> _RecenzijeList;
        private string noviKomentar;

        public RecenzijaAppPatientViewModel()
        {
           OstaviRecenzijuCommand = new RelayCommand(OstaviRecenziju);

            _RecenzijeList = new ObservableCollection<Recenzije>
            {
                new Recenzije{Korisnik = "Pero Mikić",Recenzija="Veoma dobra aplikacija, vrlo mi olakšava zakazivanje pregleda kao i pregled svih ostalih informacija koje me zanimaju. Blog vam je odličan!" },
                new Recenzije{Korisnik = "Pero Mikić",Recenzija="Veoma dobra aplikacija, vrlo mi olakšava zakazivanje pregleda kao i pregled svih ostalih informacija koje me zanimaju. Blog vam je odličan!" },
                new Recenzije{Korisnik = "Pero Mikić",Recenzija="Veoma dobra aplikacija, vrlo mi olakšava zakazivanje pregleda kao i pregled svih ostalih informacija koje me zanimaju. Blog vam je odličan!" },
            };
        }

        #region Ostavi recenziju

        private bool recenzijaOstavljena;
        public RelayCommand OstaviRecenzijuCommand { get; private set; }

        public void OstaviRecenziju(object obj)
        {
            if (recenzijaOstavljena)
            {
                MessageBox.Show("Već ste ostavili vašu recenziju!");
                return;
            }
            if (NoviKomentar == null)
            {
                MessageBox.Show("Morate uneti vaš komentar! ");
                return;
            }

            Recenzije novaRecenzija = new Recenzije();
            novaRecenzija.Korisnik = "Marko Marković";
            novaRecenzija.Recenzija = NoviKomentar;

            
            Recenzije.Add(novaRecenzija);

            recenzijaOstavljena = true;
            MessageBox.Show("Uspesno ste ostavili recenziju ");

        }

        #endregion

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
   

        public string NoviKomentar
        {
            get
            {
                return noviKomentar;
            }
            set { noviKomentar = value; }
        }

        public ObservableCollection<Recenzije> Recenzije
        {
            get
            {
                return _RecenzijeList;
            }
            set { _RecenzijeList = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #region Selektovana recenzija

        private Recenzije _selektovanaRecenzija;

        public Recenzije SelektovanaRecenzija
        {
            get { return _selektovanaRecenzija; }
            set { _selektovanaRecenzija = value; OnPropertyChanged("SelektovanaRecenzija"); }
        }

        #endregion

        #region Singlton
        private static RecenzijaAppPatientViewModel instance = null;
        private static readonly object padlock = new object();


        public static RecenzijaAppPatientViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RecenzijaAppPatientViewModel();
                    }
                    return instance;
                }
            }
        }
        #endregion

    }
}
