using DocumentFormat.OpenXml.Office2010.Word;
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
    class ZakaziPregledPatientViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Lekar> _LekariList;

        public ZakaziPregledPatientViewModel()
        {
            PirkaziPreporukaTerminaDialogCommand = new RelayCommand(PirkaziPreporukaTerminaDialog);
            ZakaziPregledCommand = new RelayCommand(ZakaziPregled);
            PreporukaTerminaCommand = new RelayCommand(PreporukaTermina);

            _LekariList = new ObservableCollection<Lekar>
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

        public RelayCommand PreporukaTerminaCommand { get; private set; }

        public void PreporukaTermina(object obj)
        {
            if (SelektovaniDatumDo.Date == SelektovaniDatumOd.Date)
            {
                MessageBox.Show("Potrebno je izabrati datumski opseg u minimalnom razmaku od jednog dana!");
                return;
            }
            if (SelektovaniLekar is null)
            {
                MessageBox.Show("Potrebno je izabrati lekara!");
                return;
            }
            if(!PreporukaTerminaDialog.IzabranPrioritet)
            {
                MessageBox.Show("Potrebno je izabrati prioritet, ako vam prioritet nije bitan izaberite i Lekara i Datum!");
                return;
            }

            MessageBox.Show("Preporuka termina je ......");
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

        private DateTime vremePrethodnoZakazanogPregleda; 
        public void ZakaziPregled(object obj)
        {
            if(vremePrethodnoZakazanogPregleda.Day == DateTime.Now.Day)
            {
                MessageBox.Show("Nije moguće opet zakazati pregled! Potrebno je da prođe 24h od prethodno zakazanog pregleda!");
                return;
            }

            if (SelektovaniLekar is null)
            {
                MessageBox.Show("Niste selektovali ni jednog lekara!!!");
                return;
            }

            string termin = SelektovaniDatum.Day + "." + SelektovaniDatum.Month+ "." + SelektovaniDatum.Year;
            MessageBox.Show("Usepsno ste zakazali pregled kod " + SelektovaniLekar.FirstAndLastName  + ".");
            Pregled pregled = new Pregled { IdPregleda = 9, NazivPregleda = "Pregled kod lekara opšte prakse", TerminPregleda = termin, StatusPregleda = "Zakazan", Lekar = SelektovaniLekar.FirstAndLastName };
            vremePrethodnoZakazanogPregleda = DateTime.Now;
            PocetnaPatientViewModel.Instance.Pregledi.Add(pregled);

        }

        #endregion


        #region Preporuka termina dialog

        public RelayCommand PirkaziPreporukaTerminaDialogCommand { get; private set; }

        public void PirkaziPreporukaTerminaDialog(object obj)
        {
            var s = new PreporukaTerminaDialog();
            s.ShowDialog();

        }

        #endregion




        #region Selektovani Datum OD za preporuku termina

        private DateTime _selektovaniDatumOd = DateTime.Now;

        public DateTime SelektovaniDatumOd
        {
            get { return _selektovaniDatumOd; }
            set { _selektovaniDatumOd = value; OnPropertyChanged("SelektovaniDatumOd"); }
        }
        #endregion

        #region Selektovani Datum DO za preporuku termina

        private DateTime _selektovaniDatumDo = DateTime.Now;

        public DateTime SelektovaniDatumDo
        {
            get { return _selektovaniDatumDo; }
            set { _selektovaniDatumDo = value; OnPropertyChanged("SelektovaniDatumDo"); }
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

        

        public ObservableCollection<Lekar> Lekari
        {
            get
            {
                return _LekariList;
            }
            set { _LekariList = value; }
        }

        # region properzChange
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        #region Singlton
        private static ZakaziPregledPatientViewModel instance = null;
        private static readonly object padlock = new object();


        public static ZakaziPregledPatientViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ZakaziPregledPatientViewModel();
                    }
                    return instance;
                }
            }
        }
        #endregion
    }
}
