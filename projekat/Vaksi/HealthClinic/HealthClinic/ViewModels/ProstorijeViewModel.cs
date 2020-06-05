
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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;

namespace HealthClinic.ViewModels
{
    public class ProstorijeViewModel:ObservableObject
    {

        public ProstorijeViewModel()
        {
            PieChart();
            ucitavanjeProstorija();

            
            DodajProstorijuCommand = new RelayCommand(DodajProstoriju);
            IzmeniProstorijuCommand = new RelayCommand(IzmeniProstoriju);
            GenerisiIzvestajProstorijaCommand = new RelayCommand(PrikaziDijalogGenerisanjaIzvestaja);
            SpisakOpremeCommand = new RelayCommand(PrikaziSpisakOpreme);
            ZauzetostAktivnostCommand = new RelayCommand(PrikaziZauzetostAktivnost);

            KalendarObject = new Calendar();
            PodesavanjeZauzetihDatumaIzabraneProstorije();

            // Zelim da renoviranje bude prvo selektovano u radio button grupi
            Renoviranje = true;

            // potvrdjujem izmenjene podatke
            PotvrdaIzmenePodatakaCommand = new RelayCommand(PotvrdaIzmenePodataka);

            // potvrdjujem dodavanje podataka
            PotvrdaDodavanjaPodatakaCommand = new RelayCommand(PotvrdaDodavanjaPodataka);
            
        }
        #region Prostorija koja sluzi za dodavanje u listu prostorija

        private Prostorija _prostorijaZaDodavanje;

        public Prostorija ProstorijaZaDodavanje
        {
            get { return _prostorijaZaDodavanje; }
            set { _prostorijaZaDodavanje = value; OnPropertyChanged("ProstorijaZaDodavanje"); }
        }

        #endregion


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
                OnPropertyChanged("SelektovanaProstorija");
            }
        }


        #endregion

        #region Prostorija za izmene



        private Prostorija _prostorijaZaIzmenu;
        /// <summary>
        /// Algoritam izmene prostorije: http://prntscr.com/sul6bj
        /// </summary>
        public Prostorija ProstorijaZaIzmenu
        {
            get { return _prostorijaZaIzmenu; }
            set { _prostorijaZaIzmenu = value; OnPropertyChanged("ProstorijaZaIzmenu"); }
        }



        #endregion

        #region Komande

        public RelayCommand PotvrdaDodavanjaPodatakaCommand { get; private set; }

        public void PotvrdaDodavanjaPodataka(object ojb)
        {
            // dodajem prostoriju za dodavanje ukoliko je odgovor bio potvrdan
            Prostorije.Add(ProstorijaZaDodavanje);
        }

        public RelayCommand PotvrdaIzmenePodatakaCommand { get; private set; }

        public void PotvrdaIzmenePodataka(object obj)
        {
            // selektovani objekat prima vrednosti od menjanog objekta
            SelektovanaProstorija.BrojSobe = ProstorijaZaIzmenu.BrojSobe;
            SelektovanaProstorija.Namena = ProstorijaZaIzmenu.Namena;
            SelektovanaProstorija.Odeljenje = ProstorijaZaIzmenu.Odeljenje;

        }

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
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for a page
                PdfGraphics graphics = page.Graphics;

                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                //Draw the text
                graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));

                //Save the document
                document.Save("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\IzvestajProstorija.pdf");
            }
            var dijalog = new GenerisiIzvestajProstorijaDijalog();
            dijalog.ShowDialog();
        }
        public RelayCommand DodajProstorijuCommand { get; private set; }

        public void DodajProstoriju(object obj)
        {
            // kreiramo novi objekat koji cemo kasnije u slucaju potvrde dodavanja dodati u listu prostorija
            ProstorijaZaDodavanje = new Prostorija();

            // prikaz dijaloga za dodavanje
            var dijalog = new DodajProstorijuDijalog();
            dijalog.DataContext = this;
            dijalog.ShowDialog();
        }

        public RelayCommand IzmeniProstorijuCommand { get; private set; }

        public void IzmeniProstoriju(object obj)
        {
            // Prostorija za izmenu/stimanje preuzima podatke od selektovane prostorije
            if(!(SelektovanaProstorija is null))
                ProstorijaZaIzmenu = new Prostorija() { Odeljenje = SelektovanaProstorija.Odeljenje, BrojSobe = SelektovanaProstorija.BrojSobe, Namena = SelektovanaProstorija.Namena };

            // podesavanje prikaza dijaloga izmene
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


        private ObservableCollection<Prostorija> _prostorije;

        public ObservableCollection<Prostorija> Prostorije
        {
            get { return _prostorije; }
            set { _prostorije = value; OnPropertyChanged("Prostorije"); }
        }


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
