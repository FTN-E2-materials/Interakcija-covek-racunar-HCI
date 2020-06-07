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
            GenerisiIzvestajLekaCommand = new RelayCommand(PrikaziDijalogGenerisanjaIzvestaja);
            IzbrisiLekCommand = new RelayCommand(IzbrisiLek);

            // potvrdjujem izmenjene podatke
            PotvrdaIzmenePodatakaCommand = new RelayCommand(PotvrdaIzmenePodataka);
            // potvrdjujem dodavanje podataka
            PotvrdaDodavanjaPodatakaCommand = new RelayCommand(PotvrdaDodavanjaPodataka);
            // potvrdjujem brisanje podataka
            PotvrdaBrisanjaPodatakaCommand = new RelayCommand(PotvrdaBrisanjaPodataka);
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
                    Lekovi.Remove(trenutniLek);
                    
                    break;
                }
            }
            this.TrenutniProzor.Close();
        }

        public RelayCommand PotvrdaDodavanjaPodatakaCommand { get; private set; }

        public void PotvrdaDodavanjaPodataka(object ojb)
        {
            // dodajem lek u listu lekova
            Lekovi.Add(LekZaDodavanje);
            this.TrenutniProzor.Close();            // gasenje trenutnog prozora
        }

        public RelayCommand PotvrdaIzmenePodatakaCommand { get; private set; }

        public void PotvrdaIzmenePodataka(object obj)
        {
            // selektovani objekat prima vrednosti od menjanog objekta
            SelektovaniLek.Kolicina = LekZaIzmenu.Kolicina;
            SelektovaniLek.NazivLeka = LekZaIzmenu.NazivLeka;
            this.TrenutniProzor.Close();            // gasenje trenutnog prozora
        }

        public RelayCommand GenerisiIzvestajLekaCommand { get; private set; }

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
                document.Save("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\IzvestajLekova.pdf");
            }
            var dijalog = new GenerisiIzvestajLekovaDijalog();
            dijalog.ShowDialog();
        }

        public RelayCommand DodajLekCommand { get; private set; }

        public void DodajLek(object obj)
        {
            // ako bude potvrda za dodavanje ovaj lek cu dodati
            LekZaDodavanje = new Lek();

            // prikaz dijaloga dodavanja leka
            TrenutniProzor = new DodajLekDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        public RelayCommand IzmeniLekCommand { get; private set; }

        public void IzmeniLek(object obj)
        {
            // Lek za izmenu/stimanje preuzima podatke od selektovanog leka
            if (!(SelektovaniLek is null))
                LekZaIzmenu = new Lek() { NazivLeka = SelektovaniLek.NazivLeka, Kolicina = SelektovaniLek.Kolicina, SifraLeka = SelektovaniLek.SifraLeka };



            // podesavanje prikaza dijaloga izmene
            TrenutniProzor = new IzmenaLekaDijalog();
            TrenutniProzor.DataContext = this;             // kako bi prebacio podatke iz ovog prozora u dijalog
            TrenutniProzor.ShowDialog();                   // podesavam da i dijalog moze upravljati istim podacima
        }

        public RelayCommand IzbrisiLekCommand { get; private set; }

        public void IzbrisiLek(object obj)
        {
            // TODO: Mozda dodati nekad da pise tacno koji lek se brise ali u nekim buducim verzijama
            TrenutniProzor = new ObrisiLekDijalog();
            TrenutniProzor.DataContext = this;
            TrenutniProzor.ShowDialog();
        }

        #endregion

        #region Tabela
        
        private ObservableCollection<Lek> _lek;

        public ObservableCollection<Lek> Lekovi
        {
            get { return _lek; }
            set { _lek = value; OnPropertyChanged("Lekovi"); }
        }


        private void ucitavanjeLekova()
        {
            Lekovi = new ObservableCollection<Lek>();
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan", SifraLeka = "0x21FDAF", Kolicina = "10" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan2", SifraLeka = "0x22FDAF", Kolicina = "11" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan3", SifraLeka = "0x23FDAF", Kolicina = "12" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan4", SifraLeka = "0x24FDAF", Kolicina = "13" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan", SifraLeka = "0x21FDAF", Kolicina = "10" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan2", SifraLeka = "0x22FDAF", Kolicina = "11" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan3", SifraLeka = "0x23FDAF", Kolicina = "12" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan4", SifraLeka = "0x24FDAF", Kolicina = "13" });
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
