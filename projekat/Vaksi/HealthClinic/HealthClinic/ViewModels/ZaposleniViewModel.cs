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
using System.Windows.Controls;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using HealthClinic.Views.Dialogs.Brisanje;
using System.Windows.Input;
using Syncfusion.Pdf.Tables;
using System.Data;

namespace HealthClinic.ViewModels
{
    public class ZaposleniViewModel : ObservableObject
    {   
        public ZaposleniViewModel()
        {
            PieChart();                // init piecharta

            ucitavanjePodatakaUTabelu();
            DodajZaposlenogCommand = new RelayCommand(DodajZaposlenog);
            
            IzmeniZaposlenogCommand = new RelayCommand(IzmeniZaposlenog);
            GenerisiIzvestajZaposlenogCommand = new RelayCommand(GenerisiIzvestaj);
            RadniKalendarCommand = new RelayCommand(PrikaziRadniKalendar);
            IzbrisiZaposlenogCommand = new RelayCommand(IzbrisiZaposlenog);

            PocetniDatum = DateTime.Now.Date;
            KrajnjiDatum = DateTime.Now.Date;

            // potvrdjujem izmenjene podatke
            PotvrdaIzmenePodatakaCommand = new RelayCommand(PotvrdaIzmenePodataka);

            // potvrdjujem dodavanje podataka
            PotvrdaDodavanjaPodatakaCommand = new RelayCommand(PotvrdaDodavanjaPodataka);

            // potvrdjujem brisanje podataka
            PotvrdaBrisanjaPodatakaCommand = new RelayCommand(PotvrdaBrisanjaPodataka);

            odredjivanjeMogucihTipovaZaposlenih();

            

        }

        #region Radno vreme zaposlenih

        private DateTime _pocetniDatum;

        public DateTime PocetniDatum
        {
            get { return _pocetniDatum; }
            set { _pocetniDatum = value; OnPropertyChanged("PocetniDatum"); }
        }

        private DateTime _krajnjiDatum;

        public DateTime KrajnjiDatum
        {
            get { return _krajnjiDatum; }
            set { _krajnjiDatum = value; OnPropertyChanged("KrajnjiDatum"); }
        }


        private DateTime _pocetniSat;

        public DateTime PocetniSat
        {
            get { return _pocetniSat; }
            set { _pocetniSat = value; OnPropertyChanged("PocetniSat"); }
        }

        private DateTime _krajnjiSat;

        public DateTime KrajnjiSat
        {
            get { return _krajnjiSat; }
            set { _krajnjiSat = value; }
        }


        #endregion

        #region Selektovani zaposlen

        private Zaposlen _selektovaniZaposleni;


        // Bajndujem na SelectedItem u tabeli i dalje radim sa njim sta hocu
        // mogu ga dalje prikazivati
        // a moze se i proslediti u dijalog
        // tako sto se .DatContext tog dijalog postavi na this
        public Zaposlen SelektovaniZaposleni
        {
            get { return _selektovaniZaposleni; }
            set { _selektovaniZaposleni = value; OnPropertyChanged("SelektovaniZaposleni"); }
        }

        #endregion

        #region Zaposleni za izmenu

        private Zaposlen _zaposleniZaIzmenu;

        public Zaposlen ZaposleniZaIzmenu
        {
            get { return _zaposleniZaIzmenu; }
            set { _zaposleniZaIzmenu = value; OnPropertyChanged("ZaposleniZaIzmenu"); }
        }


        #endregion

        #region Zaposleni za dodavanje

        private Zaposlen _zaposleniZaDodavanje;

        public Zaposlen ZaposleniZaDodavanje
        {
            get { return _zaposleniZaDodavanje; }
            set { _zaposleniZaDodavanje = value; OnPropertyChanged("ZaposleniZaDodavanje"); }
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
        public RelayCommand RadniKalendarCommand { get; private set; }
        public RelayCommand GenerisiIzvestajZaposlenogCommand { get; private set; }
        public RelayCommand DodajZaposlenogCommand { get; private set; }
        public RelayCommand IzmeniZaposlenogCommand { get; private set; }
        public RelayCommand IzbrisiZaposlenogCommand { get; private set; }

        #endregion

        #region Funkcije koje komande koriste

        public void PotvrdaBrisanjaPodataka(object obj)
        {
            // sprecavam kada niko nije selektovan da ne dodje do erroa
            if (SelektovaniZaposleni is null)
                return;


            foreach (Zaposlen trenutniZaposleni in Zaposleni)
            {
                if(trenutniZaposleni.KorisnickoIme == SelektovaniZaposleni.KorisnickoIme)
                {
                    MessageBox.Show("Uspesno ste izbrisali radnika sa korisnickim imenom " + trenutniZaposleni.KorisnickoIme);
                    podesiBrojOdredjenihZaposlenih(trenutniZaposleni, -1);
                    Zaposleni.Remove(trenutniZaposleni);

                    break;
                }
            }

            this.TrenutniProzor.Close();
        }

        public void PotvrdaDodavanjaPodataka(object ojb)
        {
            if (!validanZaposleni(ZaposleniZaDodavanje))
                return;

            // dodajem zaposlenog ukoliko je odgovor bio potvrdan
            Zaposleni.Add(ZaposleniZaDodavanje);
            podesiBrojOdredjenihZaposlenih(ZaposleniZaDodavanje, 1);
            this.TrenutniProzor.Close();
        }

        public void PotvrdaIzmenePodataka(object obj)
        {
            // regulisem da prvo povecam kolicinu novo izmenjenog broja odredjenog tipa zaposlenih
            podesiBrojOdredjenihZaposlenih(ZaposleniZaIzmenu, 1);

            //podesiBrojOdredjenihLekova(SelektovaniLek, -1);
            podesiBrojOdredjenihZaposlenih(SelektovaniZaposleni, -1);

            if (!validanZaposleni(ZaposleniZaIzmenu))
                return;

            // selektovani objekat prima vrednosti od menjanog objekta
            SelektovaniZaposleni.Ime = ZaposleniZaIzmenu.Ime;
            SelektovaniZaposleni.Prezime = ZaposleniZaIzmenu.Prezime;
            SelektovaniZaposleni.Struka = ZaposleniZaIzmenu.Struka;
            SelektovaniZaposleni.Sifra = ZaposleniZaIzmenu.Sifra;
            SelektovaniZaposleni.KorisnickoIme = ZaposleniZaIzmenu.KorisnickoIme;

            this.TrenutniProzor.Close();
        }

        public void PrikaziRadniKalendar(object obj)
        {
            var dijalog = new RadniKalendarDijalog();
            dijalog.DataContext = this;             // kako bi povezao i ViewModel Zaposlenih za ovaj dijalog
            dijalog.ShowDialog();
        }

        public void GenerisiIzvestaj(object obj)
        {
            using (PdfDocument doc = new PdfDocument())
            {
                //Create a new PDF document.
                //PdfDocument doc = new PdfDocument();

                //Add a page.
                PdfPage page = doc.Pages.Add();

                // Create a PdfLightTable.
                PdfLightTable pdfLightTable = new PdfLightTable();

                // Initialize DataTable to assign as DateSource to the light table.
                DataTable table = new DataTable();

                //Include columns to the DataTable.
                table.Columns.Add("Ime");

                table.Columns.Add("Prezime");

                table.Columns.Add("Struka");

                //Include rows to the DataTable.
                foreach (Zaposlen zaposlen in Zaposleni)
                {
                    table.Rows.Add(new string[] { zaposlen.Ime, zaposlen.Prezime, zaposlen.Struka });
                }


                //Assign data source.
                pdfLightTable.DataSource = table;

                //Draw PdfLightTable.
                pdfLightTable.Draw(page, new PointF(0, 0));

                //Save the document
                doc.Save("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\IzvestajZaposlenih.pdf");

                //Close the document

                doc.Close(true);
            }

            var dijalog = new GenerisiIzvestajZaposlenihDijalog();
            dijalog.ShowDialog();
        }

        public void DodajZaposlenog(object obj)
        {
            // kreiram novog zaposlenog da u slucaju potvrde mogu da ga dodam u listu zaposlenih
            ZaposleniZaDodavanje = new Zaposlen();

            // prikaz dijaloga za dodavanje
            TrenutniProzor = new DodajZaposlenogDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        public void IzmeniZaposlenog(object obj)
        {
            
            if (SelektovaniZaposleni is null)
                return;

            // Zaposleni za izmenu/stimanje preuzima podatke od selektovanog zaposlenog
            ZaposleniZaIzmenu = new Zaposlen() {
                Ime = SelektovaniZaposleni.Ime,
                Prezime = SelektovaniZaposleni.Prezime,
                Sifra = SelektovaniZaposleni.Sifra,
                Struka = SelektovaniZaposleni.Struka,
                KorisnickoIme = SelektovaniZaposleni.KorisnickoIme
            };


            // podesavanje prikaza dijaloga izmene
            TrenutniProzor = new IzmeniZaposlenogDijalog();
            TrenutniProzor.DataContext = this;             // kako bi prebacio podatke iz ovog prozora u dijalog
            TrenutniProzor.ShowDialog();                   // podesavam da i dijalog moze upravljati istim podacima
        }

        public void IzbrisiZaposlenog(object obj)
        {
            if (SelektovaniZaposleni is null)
                return;

            // TODO: Mozda dodati nekad da pise tacno kog zaposlenog brisemo ali u nekim buducim verzijama
            TrenutniProzor = new ObrisiZaposlenogDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        #endregion

        #region Tabela

        private ObservableCollection<Zaposlen> _zaposleni;

        public ObservableCollection<Zaposlen> Zaposleni
        {
            get { return _zaposleni; }
            set { _zaposleni = value; OnPropertyChanged("Zaposleni"); }
        }

        private void ucitavanjePodatakaUTabelu()
        {
            //Tabela - popunjavanje
            Zaposleni = new ObservableCollection<Zaposlen>();
            Zaposleni.Add(new Zaposlen() { KorisnickoIme="zikaa", Ime = "Zika", Prezime = "Vojvodic", Struka = "Otorinolaringolog", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme="dzoni", Ime = "Nikola", Prezime = "Zigic", Struka = "Oftamolog", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme="markoni", Ime = "Marko", Prezime = "Bogdanovic", Struka = "Kardio hirurg", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme="bobi", Ime = "Boban", Prezime = "Jokic", Struka = "Pedijatar", Sifra = "*****" });
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "niki", Ime = "Nikola", Prezime = "Marjanovic", Struka = "Lekar opste prakse", Sifra = "*****" });
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "zare", Ime = "Zika", Prezime = "Vojvodic", Struka = "Otorinolaringolog", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "nidroni", Ime = "Nikola", Prezime = "Zigic", Struka = "Sekretar", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "maron", Ime = "Marko", Prezime = "Bogdanovic", Struka = "Kardio hirurg", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "bobi2", Ime = "Boban", Prezime = "Jokic", Struka = "Pedijatar", Sifra = "*****"});
            Zaposleni.Add(new Zaposlen() { KorisnickoIme = "dzoni2", Ime = "Nikola", Prezime = "Marjanovic", Struka = "Lekar opste prakse", Sifra = "*****"});

            foreach (Zaposlen zaposlen in Zaposleni)
            {
                podesiBrojOdredjenihZaposlenih(zaposlen, 1);
            }
        }


        #endregion

        #region Podesavanje broja odredjenog tipa zaposlenih

        /// <summary>
        /// Podesavam trenutni broj odredjenog tipa zaposlenih
        /// </summary>
        /// <param name="zaposlen"></param>
        /// <param name="koeficijentPravca"> Prosledjuje se broj koji govori koliko povecavam/smanjujem broj odredjenih zaposlenih </param>
        private void podesiBrojOdredjenihZaposlenih(Zaposlen zaposlen, int koeficijentPravca)
        {
            if (zaposlen.Struka == "Lekar opste prakse")
            {
                if (this.UkupnoLekaraOpstePrakse is null)
                    BrojacLekaraOpstePrakse = 1;
                else
                    BrojacLekaraOpstePrakse += koeficijentPravca;
                this.UkupnoLekaraOpstePrakse = new ChartValues<int>() { BrojacLekaraOpstePrakse };

            }
            else if (zaposlen.Struka == "Stomatolog" || zaposlen.Struka == "Kardio hirurg" || zaposlen.Struka == "Pedijatar" || zaposlen.Struka == "Otorinolaringolog" || zaposlen.Struka == "Oftamolog")
            {
                if (this.UkupnoLekaraSpecijalista is null)
                    BrojacLekaraSpecijalista = 1;
                else
                    BrojacLekaraSpecijalista += koeficijentPravca;
                this.UkupnoLekaraSpecijalista = new ChartValues<int>() { BrojacLekaraSpecijalista };
            }
            else if (zaposlen.Struka == "Sekretar")
            {
                if (this.UkupnoOstalihZaposlenih is null)
                    BrojacOstalihZaposlenih = 1;
                else
                    BrojacOstalihZaposlenih += koeficijentPravca;
                this.UkupnoOstalihZaposlenih = new ChartValues<int>() { BrojacOstalihZaposlenih };
            }
        }

        #endregion

        #region Grafikon


        private ChartValues<int> _ukupnoLekaraOpstePrakse;
        private ChartValues<int> _ukupnoLekaraSpecijalista;
        private ChartValues<int> _ukupnoOstalihZaposlenih;


        public ChartValues<int> UkupnoLekaraOpstePrakse
        {
            get { return _ukupnoLekaraOpstePrakse; }
            set { _ukupnoLekaraOpstePrakse = value; OnPropertyChanged("UkupnoLekaraOpstePrakse"); }
        }

        public ChartValues<int> UkupnoLekaraSpecijalista
        {
            get { return _ukupnoLekaraSpecijalista; }
            set { _ukupnoLekaraSpecijalista = value; OnPropertyChanged("UkupnoLekaraSpecijalista"); }
        }

        public ChartValues<int> UkupnoOstalihZaposlenih
        {
            get { return _ukupnoOstalihZaposlenih; }
            set { _ukupnoOstalihZaposlenih = value; OnPropertyChanged("UkupnoOstalihZaposlenih"); }
        }

        public Func<ChartPoint, string> PointLabel { get; set; }

        public void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
       

        }
        #endregion

        #region Moguci tipovi zaposlenih

        private ObservableCollection<string> _moguciTipoviZaposlenih;

        public ObservableCollection<string> MoguciTipoviZaposlenih
        {
            get { return _moguciTipoviZaposlenih; }
            set { _moguciTipoviZaposlenih = value; OnPropertyChanged("MoguciTipoviZaposlenih"); }
        }

        private void odredjivanjeMogucihTipovaZaposlenih()
        {
            MoguciTipoviZaposlenih = new ObservableCollection<string>();
            MoguciTipoviZaposlenih.Add("Sekretar");
            MoguciTipoviZaposlenih.Add("Lekar opste prakse");
            MoguciTipoviZaposlenih.Add("Stomatolog");
            MoguciTipoviZaposlenih.Add("Kardio hirurg");
            MoguciTipoviZaposlenih.Add("Otorinolaringolog");
            MoguciTipoviZaposlenih.Add("Oftamolog");
            MoguciTipoviZaposlenih.Add("Pedijatar");
        }

        #endregion

        #region Brojaci odredjene kategorije zaposlenog
        // Brojim lekare opste i specijaliste kao i ostale zaposlene

        /// <summary>
        /// Potreban mi je i brojac koji ce se upisivati u cart,
        /// ne moze direktno i samo brojac da bude ali ni cart.
        /// </summary>
        private int _brojacLekaraOpstePrakse;
        private int _brojacLekaraSpecijalista;
        private int _brojacOstalihZaposlenih;


        public int BrojacLekaraOpstePrakse
        {
            get { return _brojacLekaraOpstePrakse; }
            set { _brojacLekaraOpstePrakse = value; OnPropertyChanged("BrojacLekaraOpstePrakse"); }
        }

        public int BrojacLekaraSpecijalista
        {
            get { return _brojacLekaraSpecijalista; }
            set { _brojacLekaraSpecijalista = value; OnPropertyChanged("BrojacLekaraSpecijalista"); }
        }

        public int BrojacOstalihZaposlenih
        {
            get { return _brojacOstalihZaposlenih; }
            set { _brojacOstalihZaposlenih = value; OnPropertyChanged("BrojacOstalihZaposlenih"); }
        }

        #endregion

        #region Validacija zaposlenog

        private bool validanZaposleni(Zaposlen zaposlen)
        {
            if(zaposlen.Ime is null)
            {
                MessageBox.Show("Niste uneli ime zaposlenog");
                return false;
            }

            if (zaposlen.Prezime is null)
            {
                MessageBox.Show("Niste uneli prezime zaposlenog");
                return false;
            }

            if (zaposlen.Struka is null)
            {
                MessageBox.Show("Niste uneli zanimanje/struku zaposlenog");
                return false;
            }

            if (zaposlen.KorisnickoIme is null)
            {
                MessageBox.Show("Niste uneli korisnicko ime zaposlenog");
                return false;
            }

            // TODO: Kada saznas kako dobiti koje trenutno validacione probleme ima app, ovde to mogu hendlovati

            return true;
        }

        #endregion

        #region Singlton pattern
        private static ZaposleniViewModel instance = null;
        private static readonly object padlock = new object();


        public static ZaposleniViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ZaposleniViewModel();
                    }
                    return instance;
                }
            }
        }
        #endregion
    }
}
