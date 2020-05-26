using HealthClinic.Dialogs;
using HealthClinic.Models;
using HealthClinic.Utilities;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HealthClinic.ViewModels
{
    public class ProstorijeViewModel:ObservableObject
    {

        public ProstorijeViewModel()
        {
            PieChart();
            ucitavanjeProstorija();

            
            DodajProstorijuCommand = new RelayCommand(PrikaziDijalogDodavanjaProstorije);
            IzmeniProstorijuCommand = new RelayCommand(PrikaziDijalogIzmeneProstorije);
            GenerisiIzvestajProstorijaCommand = new RelayCommand(PrikaziDijalogGenerisanjaIzvestaja);
            SpisakOpremeCommand = new RelayCommand(PrikaziSpisakOpreme);
            ZauzetostAktivnostCommand = new RelayCommand(PrikaziZauzetostAktivnost);

            KalendarObject = new Calendar();
            PodesavanjeZauzetihDatumaIzabraneProstorije();

            // Zelim da renoviranje bude prvo selektovano u radio button grupi
            Renoviranje = true;                            
            
        }


        #region Radio buttoni u dijalogu dugmeta tabele

        private bool _renoviranje;

        public bool Renoviranje
        {
            get { return _renoviranje; }
            set 
            { 
                _renoviranje = value;
                if (_renoviranje == true)
                {
                    PrikazTipaRenoviranja = Visibility.Visible;
                    PrikazNoveProstorije = Visibility.Collapsed;
                    PrikazDrugeNoveProstorije = Visibility.Collapsed;
                    PrikazSobeSaKojomSpajamo = Visibility.Collapsed;
                }
                OnPropertyChanged("Renoviranje");
            }
        }

        private bool _spajanje;

        public bool Spajanje
        {
            get { return _spajanje; }
            set 
            { 
                _spajanje = value;
                if(_spajanje == true)
                {
                    PrikazNoveProstorije = Visibility.Visible;
                    PrikazSobeSaKojomSpajamo = Visibility.Visible;
                    PrikazDrugeNoveProstorije = Visibility.Collapsed;
                    PrikazTipaRenoviranja = Visibility.Collapsed;
                }
                OnPropertyChanged("Spajanje");
            }
        }

        private bool _deljenje;

        public bool Deljenje
        {
            get { return _deljenje; }
            set 
            { 
                _deljenje = value;
                if(_deljenje == true)
                {
                    PrikazNoveProstorije = Visibility.Visible;
                    PrikazDrugeNoveProstorije = Visibility.Visible;
                    PrikazTipaRenoviranja = Visibility.Collapsed;
                    PrikazSobeSaKojomSpajamo = Visibility.Collapsed;
                }
                OnPropertyChanged("Deljenje");
            }
        }
        #endregion

        #region Prikazi odredjenih fildova

        private Visibility _prikazProstorijeSaKojomSpajamo;

        public Visibility PrikazSobeSaKojomSpajamo
        {
            get { return _prikazProstorijeSaKojomSpajamo; }
            set { _prikazProstorijeSaKojomSpajamo = value; OnPropertyChanged("PrikazSobeSaKojomSpajamo"); }
        }


        private Visibility _prikazTipaRenoviranja;

        public Visibility PrikazTipaRenoviranja
        {
            get { return _prikazTipaRenoviranja; }
            set { _prikazTipaRenoviranja = value; OnPropertyChanged("PrikazTipaRenoviranja"); }
        }

        private Visibility _prikazNoveProstorije;

        public Visibility PrikazNoveProstorije
        {
            get { return _prikazNoveProstorije; }
            set { _prikazNoveProstorije = value; OnPropertyChanged("PrikazNoveProstorije"); }
        }


        private Visibility _prikazDrugeNoveProstorije;

        public Visibility PrikazDrugeNoveProstorije
        {
            get { return _prikazDrugeNoveProstorije; }
            set { _prikazDrugeNoveProstorije = value; OnPropertyChanged("PrikazDrugeNoveProstorije"); }
        }





        #endregion

        #region Kalendar u dijalogu dugmeta tabele

        private Calendar _kalendarObject;

        public Calendar KalendarObject
        {
            get { return _kalendarObject; }
            set 
            { 
                _kalendarObject = value;
                OnPropertyChanged("KalendarObject");
            }
        }

        private void PodesavanjeZauzetihDatumaIzabraneProstorije()
        {
            KalendarObject.BlackoutDates.Add(new CalendarDateRange(new DateTime(1990,1,1),DateTime.Now.Date));

            // TODO: Napraviti da se stave pravi datumi kada je prostorija zauzeta
            KalendarObject.BlackoutDates.Add(new CalendarDateRange(new DateTime(2020, 6, 2), new DateTime(2020, 6, 15)));
        }


        #endregion

        #region Indeks tipa prostorije

        private int _indeksTipaProstorije;

        public int IndeksTipaProstorije
        {
            get { return _indeksTipaProstorije; }
            set { _indeksTipaProstorije = value; }
        }

        #endregion

        #region Selektovana prostorija

        private Prostorija _selektovanaProstorija;


        // Bajndujem na SelectedItem u tabeli i dalje radim sa njim sta hocu
        // mogu ga dalje prikazivati
        // a moze se i proslediti u dijalog
        // tako sto se .DatContext tog dijalog postavi na this
        public Prostorija SelektovanaProstorija
        {
            get { return _selektovanaProstorija; }
            set 
            { 
                _selektovanaProstorija = value;
                if(_selektovanaProstorija.Namena.Equals("soba za preglede"))
                {
                    IndeksTipaProstorije = 0;
                }else if (_selektovanaProstorija.Namena.Equals("soba za pacijente"))
                {
                    IndeksTipaProstorije = 1;
                }
                else if (_selektovanaProstorija.Namena == "operaciona sala")
                {
                    IndeksTipaProstorije = 2;
                }

                OnPropertyChanged("SelektovanaProstorija");
            }
        }


        #endregion

        #region Komande

        public RelayCommand ZauzetostAktivnostCommand { get; private set; }

        public void PrikaziZauzetostAktivnost(object obj)
        {
            var dijalog = new ZauzetostAktivnostDijalog();
            dijalog.DataContext = this;             // kako bi povezao i ViewModel Zaposlenih za ovaj dijalog
            dijalog.ShowDialog();
        }

        public RelayCommand SpisakOpremeCommand { get; private set; }

        public void PrikaziSpisakOpreme(object obj)
        {
            var dijalog = new SpisakOpremeDijalog();
            dijalog.DataContext = this;             // kako bi povezao i ViewModel Zaposlenih za ovaj dijalog
            dijalog.ShowDialog();
        }

        public RelayCommand GenerisiIzvestajProstorijaCommand { get; private set; }
        public void PrikaziDijalogGenerisanjaIzvestaja(object obj)
        {
            var dijalog = new GenerisiIzvestajProstorijaDijalog();
            dijalog.ShowDialog();
        }
        public RelayCommand DodajProstorijuCommand { get; private set; }

        public void PrikaziDijalogDodavanjaProstorije(object obj)
        {
            var dijalog = new DodajProstorijuDijalog();
            dijalog.ShowDialog();
        }

        public RelayCommand IzmeniProstorijuCommand { get; private set; }

        public void PrikaziDijalogIzmeneProstorije(object obj)
        {
            var dijalog = new IzmeniProstorijuDijalog();
            dijalog.DataContext = this;
            dijalog.ShowDialog();
        }


        #endregion 

        #region Tabela
        private void ucitavanjeProstorija()
        {
            Prostorije = new ObservableCollection<Prostorija>();
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "12", Namena = "operaciona sala", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "9", Namena = "soba za preglede", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "13", Namena = "soba za pacijente", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "decije", BrojSobe = "8", Namena = "soba za pacijente", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "decije", BrojSobe = "10", Namena = "operaciona sala", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "otorinolaringologija", BrojSobe = "2", Namena = "soba za preglede", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "7", Namena = "operaciona sala", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
        }

        public ObservableCollection<Prostorija> Prostorije { get; set; }

        #endregion

        #region Grafikon
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        #endregion
    }
}
