
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
using HealthClinic.Views.Dialogs.Brisanje;
using Syncfusion.Pdf.Tables;
using System.Data;
using HealthClinic.Views.Dialogs.ProduzeneInformacije;

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
            IzbrisiProstorijuCommand = new RelayCommand(IzbrisiProstoriju);

            GenerisiIzvestajProstorijaCommand = new RelayCommand(GenerisiIzvestaj);
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

            // potvrdjujem brisanje podataka
            PotvrdaBrisanjaPodatakaCommand = new RelayCommand(PotvrdaBrisanjaPodataka);

            odredjivanjeMogucihTipovaProstorije();

            // Potvrdjujem uneto podesavanje za odredjivanje aktivnosti i radova u narednom periodu izabrane prostorije
            PotvrdiZauzetostAktivnostCommand = new RelayCommand(PotvrdiZauzetostAktivnost);

            // prikaz fizickih radova nad prostorijom u narednom periodu
            PrikaziFizickeRadoveCommand = new RelayCommand(PrikaziFizickeRadove);

            // dodavanje opreme
            DodajOpremuCommand = new RelayCommand(DodajOpremu);

            // uklanjanje opreme
            UkloniOpremuCommand = new RelayCommand(UkloniOpremu);
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
        private bool _spajanje;
        private bool _deljenje;

        public bool Renoviranje
        {
            get { return _renoviranje; }
            set 
            { 
                _renoviranje = value;
                if (_renoviranje == true)
                {
                    podesiPropertije();
                }
                OnPropertyChanged("Renoviranje");
            }
        }

        private void podesiPropertije()
        {
            PrikazTipaRenoviranja = Visibility.Visible;
            PrikazNoveProstorije = Visibility.Collapsed;
            PrikazDrugeNoveProstorije = Visibility.Collapsed;
            PrikazSobeSaKojomSpajamo = Visibility.Collapsed;
        }

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

        #region Trenutno aktivni prozor

        /// <summary>
        /// Promenljiva u kojoj cuvam trenutno otvoreni prozor
        /// kako bih kasnije u komandama u zavisnosti od komande
        /// recimo zatvorio trenutni prozor
        /// </summary>
        private Window _trenutniProzor;

        public Window TrenutniProzor
        {
            get { return _trenutniProzor; }
            set { _trenutniProzor = value; }
        }

        #endregion

        #region Komande

        public RelayCommand PotvrdaBrisanjaPodatakaCommand { get; private set; }
        public RelayCommand PotvrdaDodavanjaPodatakaCommand { get; private set; }
        public RelayCommand PotvrdaIzmenePodatakaCommand { get; private set; }
        public RelayCommand ZauzetostAktivnostCommand { get; private set; }
        public RelayCommand SpisakOpremeCommand { get; private set; }
        public RelayCommand GenerisiIzvestajProstorijaCommand { get; private set; }
        public RelayCommand DodajProstorijuCommand { get; private set; }
        public RelayCommand IzmeniProstorijuCommand { get; private set; }
        public RelayCommand IzbrisiProstorijuCommand { get; private set; }
        public RelayCommand PotvrdiZauzetostAktivnostCommand { get; private set; }
        public RelayCommand PrikaziFizickeRadoveCommand { get; private set; }
        public RelayCommand DodajOpremuCommand { get; private set; }
        public RelayCommand UkloniOpremuCommand { get; private set; }

        #endregion

        #region Funkcije koje komande koriste

        public void PotvrdaBrisanjaPodataka(object obj)
        {
            if (SelektovanaProstorija is null)
                return;

            foreach (Prostorija trenutnaProstorija in Prostorije)
            {
                if(trenutnaProstorija.BrojSobe == SelektovanaProstorija.BrojSobe)
                {
                    MessageBox.Show("Uspesno ste izbrisali sobu broj " + trenutnaProstorija.BrojSobe);
                    podesiBrojOdredjenihProstorija(trenutnaProstorija, -1);
                    Prostorije.Remove(trenutnaProstorija);

                    break;
                }
            }

            this.TrenutniProzor.Close();
        }

        public void PotvrdaDodavanjaPodataka(object ojb)
        {
            if (!validnaProstorija(ProstorijaZaDodavanje))
                return;

            // dodajem prostoriju za dodavanje ukoliko je odgovor bio potvrdan
            Prostorije.Add(ProstorijaZaDodavanje);
            podesiBrojOdredjenihProstorija(ProstorijaZaDodavanje, 1);
            this.TrenutniProzor.Close();
        }

        public void PotvrdaIzmenePodataka(object obj)
        {
            // regulisem da prvo povecam kolicinu novo izmenjenog tipa prostorije
            podesiBrojOdredjenihProstorija(ProstorijaZaIzmenu, 1);

            //podesiBrojOdredjenihLekova(SelektovaniLek, -1);
            podesiBrojOdredjenihProstorija(SelektovanaProstorija, -1);

            if (!validnaProstorija(ProstorijaZaIzmenu))
                return;

            // selektovani objekat prima vrednosti od menjanog objekta
            SelektovanaProstorija.BrojSobe = ProstorijaZaIzmenu.BrojSobe;
            SelektovanaProstorija.Namena = ProstorijaZaIzmenu.Namena;
            SelektovanaProstorija.Odeljenje = ProstorijaZaIzmenu.Odeljenje;
            this.TrenutniProzor.Close();
        }
        
        public void PotvrdiZauzetostAktivnost(object obj)
        {
            
            if (Renoviranje)
            {   // preuzimam od pomocne promenljive podatke iz forme
                SelektovanaProstorija.FizickiRadovi.NazivRada = TrenutniFizickiRad.NazivRada;
                SelektovanaProstorija.FizickiRadovi.DoDatuma = TrenutniFizickiRad.DoDatuma;
                SelektovanaProstorija.FizickiRadovi.OdDatuma = TrenutniFizickiRad.OdDatuma;
            }
            else if (Deljenje)
            {
                MessageBox.Show("deljenje" + BrojNoveSobe +" " + BrojDrugeNoveSobe);
                // obrisem trenutno selektovanu prostoriju
                foreach (Prostorija trenutnaProstorija in Prostorije)
                {
                    if (trenutnaProstorija.BrojSobe == SelektovanaProstorija.BrojSobe)
                    {
                        podesiBrojOdredjenihProstorija(trenutnaProstorija, -1);
                        Prostorije.Remove(trenutnaProstorija);

                        break;
                    }
                }

                // dodam ove dve nove
                Prostorija p1 = new Prostorija()
                {
                    BrojSobe = BrojNoveSobe,
                    Namena ="soba za pacijente"
                    
                };

                Prostorija p2 = new Prostorija()
                {
                    BrojSobe = BrojDrugeNoveSobe,
                    Namena = "soba za pacijente"

                };
                Prostorije.Add(p1);
                Prostorije.Add(p2);
                podesiBrojOdredjenihProstorija(p1, 1);
                podesiBrojOdredjenihProstorija(p2, 1);

            }
            else if (Spajanje)
            {
                // prvo moram uklonite ove 2 prostorije
                foreach (Prostorija trenutnaProstorija in Prostorije)
                {
                    if (trenutnaProstorija.BrojSobe == SelektovanaProstorija.BrojSobe)
                    {
                        podesiBrojOdredjenihProstorija(trenutnaProstorija, -1);
                        Prostorije.Remove(trenutnaProstorija);

                        break;
                    }
                }
                // namerno 2 fora kako ne bih doslo do errora zbog brisanja
                foreach (Prostorija trenutnaProstorija in Prostorije)
                {
                    if (trenutnaProstorija.BrojSobe == BrojSobeSaKojomVrsimoSpajanje)
                    {
                        podesiBrojOdredjenihProstorija(trenutnaProstorija, -1);
                        Prostorije.Remove(trenutnaProstorija);

                        break;
                    }
                }

                // dodajem prostoriju
                Prostorija tempProstorija = new Prostorija()
                {
                    BrojSobe = BrojNoveSobe,
                    Namena = NamenaProstorije
                };

                Prostorije.Add(tempProstorija);
                podesiBrojOdredjenihProstorija(tempProstorija, 1);

            }


            this.TrenutniProzor.Close();
        }

        public void PrikaziZauzetostAktivnost(object obj)
        {

            instanciranjeFizickihRadova();
      
            TrenutniProzor = new ZauzetostAktivnostDijalog();
            TrenutniProzor.DataContext = this;             // kako bi povezao i ViewModel Zaposlenih za ovaj dijalog
            TrenutniProzor.ShowDialog();
        }

        public void PrikaziSpisakOpreme(object obj)
        {
            SpisakOpreme = new ObservableCollection<Oprema>();

            TrenutnaOprema = new Oprema();
            if(SelektovanaProstorija.OpremaProstorije is null)
            {
                SelektovanaProstorija.OpremaProstorije = new List<Oprema>();

            }

            // preuzimanje od prave opreme prostorije
            foreach (Oprema oprema in SelektovanaProstorija.OpremaProstorije)
            {
                SpisakOpreme.Add(oprema);
            }
            

            TrenutniProzor = new SpisakOpremeDijalog();
            TrenutniProzor.DataContext = this;             // kako bi povezao i ViewModel Zaposlenih za ovaj dijalog
            TrenutniProzor.ShowDialog();
        }

        public void GenerisiIzvestaj(object obj)
        {
            using (PdfDocument doc = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = doc.Pages.Add();

                // Create a PdfLightTable.
                PdfLightTable pdfLightTable = new PdfLightTable();

                // Initialize DataTable to assign as DateSource to the light table.
                DataTable table = new DataTable();

                //Include columns to the DataTable.
                table.Columns.Add("Odeljenje");

                table.Columns.Add("BrojSobe");

                table.Columns.Add("Namena");

                //Include rows to the DataTable.
                foreach (Prostorija prostorija in Prostorije)
                {
                    table.Rows.Add(new string[] { prostorija.Odeljenje, prostorija.BrojSobe, prostorija.Namena  });
                }


                //Assign data source.
                pdfLightTable.DataSource = table;

                //Draw PdfLightTable.
                pdfLightTable.Draw(page, new PointF(0, 0));

                //Save the document
                doc.Save("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\IzvestajProstorija.pdf");

                doc.Close();
            }
            MessageBox.Show("Uspesno kreiran izvestaj prostorija, mozete ga pogledati u tekucem direktorijumu");
        }

        public void DodajProstoriju(object obj)
        {
            // kreiramo novi objekat koji cemo kasnije u slucaju potvrde dodavanja dodati u listu prostorija
            ProstorijaZaDodavanje = new Prostorija();

            // prikaz dijaloga za dodavanje
            TrenutniProzor = new DodajProstorijuDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();

        }

        public void IzmeniProstoriju(object obj)
        {
            // Prostorija za izmenu/stimanje preuzima podatke od selektovane prostorije
            if (!(SelektovanaProstorija is null))
            {
                ProstorijaZaIzmenu = new Prostorija() { Odeljenje = SelektovanaProstorija.Odeljenje, BrojSobe = SelektovanaProstorija.BrojSobe, Namena = SelektovanaProstorija.Namena };
            }
            else
            {
                return;
            }
            // podesavanje prikaza dijaloga izmene
            TrenutniProzor = new IzmeniProstorijuDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        public void IzbrisiProstoriju(object obj)
        {
            if (SelektovanaProstorija is null)
                return;

            // TODO: Mozda dodati nekad da pise tacno koju prostoriju brisemo ali u nekim buducim verzijama
            TrenutniProzor = new ObrisiProstorijuDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        public void PrikaziFizickeRadove(object obj)
        {
            if (SelektovanaProstorija.FizickiRadovi is null)
            {
                MessageBox.Show("Izabrana prostorija u narednom periodu nema zakazanih fizickih radova");
                return;
            }else if(SelektovanaProstorija.FizickiRadovi.NazivRada is null)
            {
                MessageBox.Show("Izabrana prostorija u narednom periodu nema zakazanih fizickih radova");
                return;
            }
                

            TrenutniProzor = new FizickiRadoviDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        public void DodajOpremu(object obj)
        {
            foreach (Oprema oprema in SpisakOpreme)
            {
                if (oprema.NazivOpreme == TrenutnaOprema.NazivOpreme)
                {
                    oprema.KolicinaOpreme += TrenutnaOprema.KolicinaOpreme;
                    return;
                }
            }

            // ako dodjem dovde znaci da moram celu tu trenutnu opremu dodati jer je nema u spisku opreme
            Oprema opremaZaDodavanje = new Oprema()
            {
                NazivOpreme = TrenutnaOprema.NazivOpreme,
                KolicinaOpreme = TrenutnaOprema.KolicinaOpreme
            };
            // i kolekciji i zapravo selektovanom dodam
            SpisakOpreme.Add(opremaZaDodavanje);
            SelektovanaProstorija.OpremaProstorije.Add(opremaZaDodavanje);
            
        }

        public void UkloniOpremu(object obj)
        {
            foreach (Oprema oprema in SpisakOpreme)
            {
                if (oprema.NazivOpreme == TrenutnaOprema.NazivOpreme)
                {
                    oprema.KolicinaOpreme -= TrenutnaOprema.KolicinaOpreme;
                    if(oprema.KolicinaOpreme <= 0)
                    {
                        // uklonim iz prave opreme
                        foreach (Oprema opremaPrava in SelektovanaProstorija.OpremaProstorije)
                        {
                            if(opremaPrava.NazivOpreme == oprema.NazivOpreme)
                            {
                                SelektovanaProstorija.OpremaProstorije.Remove(opremaPrava);
                                break;
                            }
                        }
                        // ali i iz observable liste
                        SpisakOpreme.Remove(oprema);
                    }
                    return;
                }
            }
            // ako sam dosao do ovoga onda znaci da te opreme zapravo i nema u listi
            MessageBox.Show("Pokusao si ukloniti opremu koju klinika trenutno nema u posedovanju");
        }


        #endregion

        #region Tabela

        private ObservableCollection<Prostorija> _prostorije;

        public ObservableCollection<Prostorija> Prostorije
        {
            get { return _prostorije; }
            set { _prostorije = value; OnPropertyChanged("Prostorije"); }
        }

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

            foreach (Prostorija prostorija in Prostorije)
            {
                podesiBrojOdredjenihProstorija(prostorija, 1);
            }
        }


        


        #endregion

        #region Podesavanje broja odredjenog tipa prostorije

        /// <summary>
        /// Podesavam trenutni broj odredjenog tipa prostorije.
        /// </summary>
        /// <param name="prostorija"></param>
        /// <param name="koeficijentPravca"> Prosledjuje se broj koji govori koliko povecavam/smanjujem broj odredjenih prostorija</param>
        private void podesiBrojOdredjenihProstorija(Prostorija prostorija, int koeficijentPravca)
        {
            if (prostorija.Namena == "soba za preglede")
            {
                if (this.UkupnoSobaZaPreglede is null)
                    BrojacSobaZaPreglede = 1;
                else
                    BrojacSobaZaPreglede += koeficijentPravca;
                this.UkupnoSobaZaPreglede = new ChartValues<int>() { BrojacSobaZaPreglede };

            }
            else if (prostorija.Namena == "soba za pacijente")
            {
                if (this.UkupnoSobaZaPacijente is null)
                    BrojacSobaZaPacijente = 1;
                else
                    BrojacSobaZaPacijente += koeficijentPravca;
                this.UkupnoSobaZaPacijente = new ChartValues<int>() { BrojacSobaZaPacijente };
            }
            else if (prostorija.Namena == "operaciona sala")
            {
                if (this.UkupnoOperacionihSala is null)
                    BrojacOperacionihSala = 1;
                else
                    BrojacOperacionihSala += koeficijentPravca;
                this.UkupnoOperacionihSala = new ChartValues<int>() { BrojacOperacionihSala };
            }
        }

        #endregion

        #region Deo vezan za grafikon

        private ChartValues<int> _ukupnoSobaZaPacijente;
        private ChartValues<int> _ukupnoSobaZaPreglede;
        private ChartValues<int> _ukupnoOperacionihSala;


        public ChartValues<int> UkupnoSobaZaPacijente
        {
            get { return _ukupnoSobaZaPacijente; }
            set { _ukupnoSobaZaPacijente = value; OnPropertyChanged("UkupnoSobaZaPacijente"); }
        }

        public ChartValues<int> UkupnoSobaZaPreglede
        {
            get { return _ukupnoSobaZaPreglede; }
            set { _ukupnoSobaZaPreglede = value; OnPropertyChanged("UkupnoSobaZaPreglede"); }
        }

        public ChartValues<int> UkupnoOperacionihSala
        {
            get { return _ukupnoOperacionihSala; }
            set { _ukupnoOperacionihSala = value; OnPropertyChanged("UkupnoOperacionihSala"); }
        }


        public Func<ChartPoint, string> PointLabel { get; set; }

        private void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        #endregion

        #region Moguci tipovi prostorija

        private ObservableCollection<string> _moguciTipoviProstorije;

        public ObservableCollection<string> MoguciTipoviProstorije
        {
            get { return _moguciTipoviProstorije; }
            set { _moguciTipoviProstorije = value; OnPropertyChanged("MoguciTipoviProstorije"); }
        }

        private void odredjivanjeMogucihTipovaProstorije()
        {
            MoguciTipoviProstorije = new ObservableCollection<string>();
            MoguciTipoviProstorije.Add("soba za pacijente");
            MoguciTipoviProstorije.Add("soba za preglede");
            MoguciTipoviProstorije.Add("operaciona sala");
            
        }

        #endregion

        #region Brojaci odredjenog tipa prostorije

        /// <summary>
        /// Potreban mi je i brojac koji ce se upisivati u cart,
        /// ne moze direktno i samo brojac da bude ali ni cart.
        /// </summary>
        private int _brojacSobaZaPreglede;
        private int _brojacSobaZaPacijente;
        private int _brojacOperacionihSala;


        public int BrojacSobaZaPreglede
        {
            get { return _brojacSobaZaPreglede; }
            set { _brojacSobaZaPreglede = value; OnPropertyChanged("BrojacSobaZaPreglede"); }
        }

        public int BrojacSobaZaPacijente
        {
            get { return _brojacSobaZaPacijente; }
            set { _brojacSobaZaPacijente = value; OnPropertyChanged("BrojacSobaZaPacijente"); }
        }

        public int BrojacOperacionihSala
        {
            get { return _brojacOperacionihSala; }
            set { _brojacOperacionihSala = value; OnPropertyChanged("BrojacOperacionihSala"); }
        }


        #endregion

        #region Validiranje prostorije

        private bool validnaProstorija(Prostorija prostorija)
        {
            if(prostorija.BrojSobe is null)
            {
                MessageBox.Show("Niste popunili polje za broj sobe");
                return false;
            }

            if (prostorija.Namena is null)
            {
                MessageBox.Show("Niste popunili namenu sobe(tip prostorije)");
                return false;
            }

            if (prostorija.Odeljenje is null)
            {
                MessageBox.Show("Niste popunili polje za odeljenje na kojem se nalazi soba");
                return false;
            }

            // TODO: Kada saznas kako dobiti koje trenutno validacione probleme ima app, ovde to mogu hendlovati

            return true;
        }

        #endregion

        #region Singlton pattern
        private static ProstorijeViewModel instance = null;
        private static readonly object padlock = new object();


        public static ProstorijeViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ProstorijeViewModel();
                    }
                    return instance;
                }
            }
        }
        #endregion

        #region Spisak opreme odredjene prostorije
        private ObservableCollection<Oprema> _spisakOpreme;

        public ObservableCollection<Oprema> SpisakOpreme
        {
            get { return _spisakOpreme; }
            set { _spisakOpreme = value; OnPropertyChanged("SpisakOpreme"); }
        }


        #endregion

        #region Trenutna oprema

        private Oprema _trenutnaOprema;

        public Oprema TrenutnaOprema
        {
            get { return _trenutnaOprema; }
            set { _trenutnaOprema = value; OnPropertyChanged("TrenutnaOprema"); }
        }
        #endregion

        #region Fizicki radovi za dodavanje/prikaz
        private FizickiRad _trenutniFizickiRad;

        public FizickiRad TrenutniFizickiRad
        {
            get { return _trenutniFizickiRad; }
            set { _trenutniFizickiRad = value; OnPropertyChanged("TrenutniFizickiRad"); }
        }

        private void instanciranjeFizickihRadova()
        {
            if (TrenutniFizickiRad is null)
            {
                TrenutniFizickiRad = new FizickiRad()
                {
                    OdDatuma = new DateTime(2020, 1, 1),
                    DoDatuma = new DateTime(2020, 12, 12)
                };
            }

            // instanciram fizicke radove selektovane prostorije
            if (SelektovanaProstorija.FizickiRadovi is null)
            {
                SelektovanaProstorija.FizickiRadovi = new FizickiRad();
            }
        }

        #endregion

        #region Soba(samo broj trenutno) sa kojom vrsimo spajanje
        private string _brojSobeSaKojomVrsimoSpajanje;

        public string BrojSobeSaKojomVrsimoSpajanje
        {
            get { return _brojSobeSaKojomVrsimoSpajanje; }
            set { _brojSobeSaKojomVrsimoSpajanje = value; OnPropertyChanged("BrojSobeSaKojomVrsimoSpajanje"); }
        }

        #endregion

        #region Broj nove sobe u spajanju + druge nove sobe za deljenje

        private string _brojNoveSobe;
        private string _brojDrugeNoveSobe;


        public string BrojNoveSobe
        {
            get { return _brojNoveSobe; }
            set { _brojNoveSobe = value; OnPropertyChanged("BrojNoveSobe"); }
        }

        public string BrojDrugeNoveSobe
        {
            get { return _brojDrugeNoveSobe; }
            set { _brojDrugeNoveSobe = value; OnPropertyChanged("BrojDrugeNoveSobe"); }
        }

        #endregion

        #region Namena prostorije prilikom spajanja prostorija
        private string _namenaProstorije;

        public string NamenaProstorije
        {
            get { return _namenaProstorije; }
            set { _namenaProstorije = value; OnPropertyChanged("NamenaProstorije"); }
        }

        #endregion
    }

}

