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
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.ComponentModel;
using System.Drawing;
using HealthClinic.Views.Dialogs.Brisanje;
using System.Windows;
using Syncfusion.Pdf.Tables;
using System.Data;
using HealthClinic.Utilities.Validations;

namespace HealthClinic.ViewModels
{
    public class LekoviViewModel : ObservableObject
    {
        
        public LekoviViewModel()
        {
            ucitavanjeLekova();
            PieChart();

            DodajLekCommand = new RelayCommand(DodajLek);
            IzmeniLekCommand = new RelayCommand(IzmeniLek);
            GenerisiIzvestajLekaCommand = new RelayCommand(GenerisiIzvestaj);
            IzbrisiLekCommand = new RelayCommand(IzbrisiLek);

            // potvrdjujem izmenjene podatke
            PotvrdaIzmenePodatakaCommand = new RelayCommand(PotvrdaIzmenePodataka);
            // potvrdjujem dodavanje podataka
            PotvrdaDodavanjaPodatakaCommand = new RelayCommand(PotvrdaDodavanjaPodataka);
            // potvrdjujem brisanje podataka
            PotvrdaBrisanjaPodatakaCommand = new RelayCommand(PotvrdaBrisanjaPodataka);

            odredjivanjeMogucihVrstaLekova();
        }

        



        #region Selektovani lek

        private Lek _selektovaniLek;


        // Bajndujem na SelectedItem u tabeli i dalje radim sa njim sta hocu
        // mogu ga dalje prikazivati
        // a moze se i proslediti u dijalog
        // tako sto se .DatContext tog dijalog postavi na this
        public Lek SelektovaniLek
        {
            get { return _selektovaniLek; }
            set { _selektovaniLek = value; OnPropertyChanged("SelektovaniLek"); }
        }


        #endregion

        #region Lek za izmenu

        private Lek _lekZaIzmenu;

        public Lek LekZaIzmenu
        {
            get { return _lekZaIzmenu; }
            set { _lekZaIzmenu = value; OnPropertyChanged("LekZaIzmenu"); }
        }

        #endregion

        #region Lek za dodavanje

        private Lek _lekZaDodavanje;

        public Lek LekZaDodavanje
        {
            get { return _lekZaDodavanje; }
            set { _lekZaDodavanje = value; OnPropertyChanged("LekZaDodavanje"); }
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
        public RelayCommand GenerisiIzvestajLekaCommand { get; private set; }
        public RelayCommand DodajLekCommand { get; private set; }
        public RelayCommand IzmeniLekCommand { get; private set; }
        public RelayCommand IzbrisiLekCommand { get; private set; }

        #endregion

        #region Funkcije koje komande koriste
        public void PotvrdaBrisanjaPodataka(object obj)
        {
            // sprecavam kada nije selektovan lek da ne pukne program
            if (SelektovaniLek is null)
                return;
            foreach (Lek trenutniLek in Lekovi)
            {
                if (trenutniLek.NazivLeka == SelektovaniLek.NazivLeka)
                {
                    MessageBox.Show("Uspesno ste izbrisali lek " + trenutniLek.NazivLeka);
                    podesiBrojOdredjenihLekova(trenutniLek, -1);        // -1 da bih mu smanjio kolicinu vrste ovog leka
                    Lekovi.Remove(trenutniLek);
                    
                    break;
                }
            }
            this.TrenutniProzor.Close();
        }

        public void PotvrdaDodavanjaPodataka(object ojb)
        {

            if (!validanLek(LekZaDodavanje))
                return;

            // dodajem lek u listu lekova
            Lekovi.Add(LekZaDodavanje);
            podesiBrojOdredjenihLekova(LekZaDodavanje,1);

            this.TrenutniProzor.Close();            // gasenje trenutnog prozora
        }

        public void PotvrdaIzmenePodataka(object obj)
        {
            // regulisem da prvo povecam kolicinu novo izmenjene vrste leka
            podesiBrojOdredjenihLekova(LekZaIzmenu, 1);

            // a onda i smanjim od stare vrste
            podesiBrojOdredjenihLekova(SelektovaniLek, -1);

            // provera leka koji je korisnik menjao
            if (!validanLek(LekZaIzmenu))
                return;

            // selektovani objekat prima vrednosti od menjanog objekta
            SelektovaniLek.Kolicina = LekZaIzmenu.Kolicina;
            SelektovaniLek.NazivLeka = LekZaIzmenu.NazivLeka;
            SelektovaniLek.VrstaLeka = LekZaIzmenu.VrstaLeka;

            this.TrenutniProzor.Close();            // gasenje trenutnog prozora
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
                table.Columns.Add("Naziv");

                table.Columns.Add("Kolicina");

                //Include rows to the DataTable.
                foreach (Lek lek in Lekovi)
                {
                    table.Rows.Add(new string[] { lek.NazivLeka, lek.Kolicina });
                }


                //Assign data source.
                pdfLightTable.DataSource = table;

                //Draw PdfLightTable.
                pdfLightTable.Draw(page, new PointF(0, 0));

                //Save the document
                doc.Save("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\IzvestajLekova.pdf");

                doc.Close();
            }
            var dijalog = new GenerisiIzvestajLekovaDijalog();
            dijalog.ShowDialog();
        }

        public void DodajLek(object obj)
        {
            // ako bude potvrda za dodavanje ovaj lek cu dodati
            LekZaDodavanje = new Lek();

            // prikaz dijaloga dodavanja leka
            TrenutniProzor = new DodajLekDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();

        }

        public void IzmeniLek(object obj)
        {
            // Lek za izmenu/stimanje preuzima podatke od selektovanog leka
            if (!(SelektovaniLek is null))
            {
                LekZaIzmenu = new Lek()
                {
                    NazivLeka = SelektovaniLek.NazivLeka,
                    Kolicina = SelektovaniLek.Kolicina,
                    SifraLeka = SelektovaniLek.SifraLeka,
                    VrstaLeka = SelektovaniLek.VrstaLeka
                };
            }
            else
            {
                return;
            }
                



            // podesavanje prikaza dijaloga izmene
            TrenutniProzor = new IzmenaLekaDijalog();
            TrenutniProzor.DataContext = this;             // kako bi prebacio podatke iz ovog prozora u dijalog
            TrenutniProzor.ShowDialog();                   // podesavam da i dijalog moze upravljati istim podacima
        }

        public void IzbrisiLek(object obj)
        {
            if (SelektovaniLek is null)
                return;

            // TODO: Mozda dodati nekad da pise tacno koji lek se brise ali u nekim buducim verzijama
            TrenutniProzor = new ObrisiLekDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        #endregion

        #region Tabela & vrste lekova
        
        private ObservableCollection<Lek> _lek;

        public ObservableCollection<Lek> Lekovi
        {
            get { return _lek; }
            set { _lek = value; OnPropertyChanged("Lekovi"); }
        }

        private void ucitavanjeLekova()
        {
            Lekovi = new ObservableCollection<Lek>();
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan", SifraLeka = "0x21FDAF", Kolicina = "10" , VrstaLeka="antibiotik"});
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan2", SifraLeka = "0x22FDAF", Kolicina = "11", VrstaLeka = "analgetik" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan3", SifraLeka = "0x23FDAF", Kolicina = "12", VrstaLeka = "kardio vaskularni" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan4", SifraLeka = "0x24FDAF", Kolicina = "13", VrstaLeka = "anestetik" });
            
            // Odredjivanje koliko imamo kog leka
            foreach (Lek lek in Lekovi)
            {
                podesiBrojOdredjenihLekova(lek,1);
            }

        }
        /// <summary>
        /// Podesavam trenutnu kolicinu odredjene GRUPE LEKOVA
        /// </summary>
        /// <param name="lek"></param>
        /// <param name="koeficijentPravca"> Prosledjuje se broj koji govori koliko povecavam/smanjujem odredjenog leka</param>
        private void podesiBrojOdredjenihLekova(Lek lek, int koeficijentPravca)
        {
            if (lek.VrstaLeka == "antibiotik")
            {
                if (this.UkupnoAntibiotika is null)
                    BrojacAntibiotika = 1;
                else
                    BrojacAntibiotika += koeficijentPravca;
                this.UkupnoAntibiotika = new ChartValues<int>() { BrojacAntibiotika };

            }
            else if (lek.VrstaLeka == "analgetik")
            {
                if (this.UkupnoAnalgetika is null)
                    BrojacAnalgetika = 1;
                else
                    BrojacAnalgetika += koeficijentPravca;
                this.UkupnoAnalgetika = new ChartValues<int>() { BrojacAnalgetika };
            }
            else if (lek.VrstaLeka == "kardio vaskularni")
            {
                if (this.UkupnoKardioVaskularnih is null)
                    BrojacKardioVaskularnih = 1;
                else
                    BrojacKardioVaskularnih += koeficijentPravca;
                this.UkupnoKardioVaskularnih = new ChartValues<int>() { BrojacKardioVaskularnih };
            }
            else if (lek.VrstaLeka == "anestetik")
            {
                if (this.UkupnoAnestetika is null)
                    BrojacAnestetika = 1;
                else
                    BrojacAnestetika += koeficijentPravca;
                this.UkupnoAnestetika = new ChartValues<int>() { BrojacAnestetika };
            }
        }

        #endregion

        #region Brojaci odredjene vrste lekova

        /// <summary>
        /// Potreban mi je i brojac koji ce se upisivati u cart,
        /// ne moze direktno i samo brojac da bude ali ni cart.
        /// </summary>
        private int _brojacAnalgetika;
        private int _brojacAnestetika;
        private int _brojacKardioVaskularnih;
        private int _brojacAntibiotika;


        public int BrojacAnalgetika
        {
            get { return _brojacAnalgetika; }
            set { _brojacAnalgetika = value; OnPropertyChanged("BrojacAnalgetika"); }
        }

        public int BrojacAnestetika
        {
            get { return _brojacAnestetika; }
            set { _brojacAnestetika = value; OnPropertyChanged("BrojacAnestetika"); }
        }

        public int BrojacKardioVaskularnih
        {
            get { return _brojacKardioVaskularnih; }
            set { _brojacKardioVaskularnih = value; OnPropertyChanged("BrojacKardioVaskularnih"); }
        }

        public int BrojacAntibiotika
        {
            get { return _brojacAntibiotika; }
            set { _brojacAntibiotika = value; OnPropertyChanged("BrojacAntibiotika"); }
        }

        #endregion

        #region Deo vezan za grafikon

        private ChartValues<int> _ukupnoAntibiotika;
        private ChartValues<int> _ukupnoAnalgetika;
        private ChartValues<int> _ukupnoKardioVaskularnih;
        private ChartValues<int> _ukupnoAnestetika;

       
        public ChartValues<int> UkupnoAntibiotika
        {
            get { return _ukupnoAntibiotika; }
            set { _ukupnoAntibiotika = value; OnPropertyChanged("UkupnoAntibiotika"); }
        }

        public ChartValues<int> UkupnoAnalgetika
        {
            get { return _ukupnoAnalgetika; }
            set { _ukupnoAnalgetika = value; OnPropertyChanged("UkupnoAnalgetika"); }
        }

        public ChartValues<int> UkupnoKardioVaskularnih
        {
            get { return _ukupnoKardioVaskularnih; }
            set { _ukupnoKardioVaskularnih = value; OnPropertyChanged("UkupnoKardioVaskularnih"); }
        }
        
        public ChartValues<int> UkupnoAnestetika
        {
            get { return _ukupnoAnestetika; }
            set { _ukupnoAnestetika = value; OnPropertyChanged("UkupnoAnestetika"); }
        }


        public Func<ChartPoint, string> LabelPoint { get; set; }

        private void PieChart()
        {
            LabelPoint = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);

        }

        #endregion

        #region Moguce vrste lekova 

        private ObservableCollection<string> _moguceVrsteLekova;

        public ObservableCollection<string> MoguceVrsteLekova
        {
            get { return _moguceVrsteLekova; }
            set { _moguceVrsteLekova = value; OnPropertyChanged("MoguceVrsteLekova"); }
        }

        private void odredjivanjeMogucihVrstaLekova()
        {
            MoguceVrsteLekova = new ObservableCollection<string>();
            MoguceVrsteLekova.Add("analgetik");
            MoguceVrsteLekova.Add("antibiotik");
            MoguceVrsteLekova.Add("kardio vaskularni");
            MoguceVrsteLekova.Add("anestetik");
        }

        #endregion


        #region Validiranje lekova
        private bool validanLek(Lek lek)
        {
            if (lek.Kolicina is null)
            {
                MessageBox.Show("Niste uneli kolicinu leka");
                return false;
            }

            if (lek.NazivLeka is null)
            {
                MessageBox.Show("Niste uneli naziv leka");
                return false;
            }

            if (lek.VrstaLeka is null)
            {
                MessageBox.Show("Niste uneli vrstu leka");
                return false;
            }
            
            // TODO: Kako da proverim da li nasa aplikacija ima ErrorContenta da bih bacio poruku greske ovde neku


            return true;
        }

        #endregion
    }
}
