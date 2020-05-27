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

namespace HealthClinic.ViewModels
{
    public class ZaposleniViewModel:ObservableObject
    {   
        public ZaposleniViewModel()
        {
            PieChart();                // init piecharta

            ucitavanjePodatakaUTabelu();

            DodajZaposlenogCommand = new RelayCommand(PrikaziDijalogDodavanjaZaposlenog);
            IzmeniZaposlenogCommand = new RelayCommand(PrikaziDijalogIzmeneZaposlenog);
            GenerisiIzvestajZaposlenogCommand = new RelayCommand(PrikaziDijalogGenerisanjaIzvestaja);
            RadniKalendarCommand = new RelayCommand(PrikaziRadniKalendar);

            PocetniDatum = DateTime.Now.Date;
            KrajnjiDatum = DateTime.Now.Date;

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

        #region Komande

        public RelayCommand RadniKalendarCommand { get; private set; }

        public void PrikaziRadniKalendar(object obj)
        {
            var dijalog = new RadniKalendarDijalog();
            dijalog.DataContext = this;             // kako bi povezao i ViewModel Zaposlenih za ovaj dijalog
            dijalog.ShowDialog();
        }

        public RelayCommand GenerisiIzvestajZaposlenogCommand { get; private set; }

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
                document.Save("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\IzvestajZaposlenih.pdf");
            }

            var dijalog = new GenerisiIzvestajZaposlenihDijalog();
            dijalog.ShowDialog();
        }

        public RelayCommand DodajZaposlenogCommand { get; private set; }

        public void PrikaziDijalogDodavanjaZaposlenog(object obj)
        {
            var dijalog = new DodajZaposlenogDijalog();
            dijalog.ShowDialog();
        }

        public RelayCommand IzmeniZaposlenogCommand { get; private set; }

        public void PrikaziDijalogIzmeneZaposlenog(object obj)
        {
            var dijalog = new IzmeniZaposlenogDijalog();
            dijalog.DataContext = this;             // kako bi prebacio podatke iz ovog prozora u dijalog
            dijalog.ShowDialog();                   // podesavam da i dijalog moze upravljati istim podacima
        }

        #endregion

        #region Tabela
        private void ucitavanjePodatakaUTabelu()
        {
            //Tabela - popunjavanje
            Zaposleni = new ObservableCollection<Zaposlen>();
            Zaposleni.Add(new Zaposlen() { Ime = "Zika", Prezime = "Vojvodic", Struka = "Otorinolaringolog", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "10" });
            Zaposleni.Add(new Zaposlen() { Ime = "Nikola", Prezime = "Zigic", Struka = "Oftamolog", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "8" });
            Zaposleni.Add(new Zaposlen() { Ime = "Marko", Prezime = "Bogdanovic", Struka = "Kardio hirurg", Sifra = "*****", RadniKalendar = "slobodan", BrojOperacijaOveNedelje = "18" });
            Zaposleni.Add(new Zaposlen() { Ime = "Boban", Prezime = "Jokic", Struka = "Pedijatar", Sifra = "*****", RadniKalendar = "slobodan", BrojOperacijaOveNedelje = "9" });
            Zaposleni.Add(new Zaposlen() { Ime = "Nikola", Prezime = "Marjanovic", Struka = "Doktor opste prakse", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "12" });
            Zaposleni.Add(new Zaposlen() { Ime = "Zika", Prezime = "Vojvodic", Struka = "Otorinolaringolog", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "10" });
            Zaposleni.Add(new Zaposlen() { Ime = "Nikola", Prezime = "Zigic", Struka = "Oftamolog", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "8" });
            Zaposleni.Add(new Zaposlen() { Ime = "Marko", Prezime = "Bogdanovic", Struka = "Kardio hirurg", Sifra = "*****", RadniKalendar = "slobodan", BrojOperacijaOveNedelje = "18" });
            Zaposleni.Add(new Zaposlen() { Ime = "Boban", Prezime = "Jokic", Struka = "Pedijatar", Sifra = "*****", RadniKalendar = "slobodan", BrojOperacijaOveNedelje = "9" });
            Zaposleni.Add(new Zaposlen() { Ime = "Nikola", Prezime = "Marjanovic", Struka = "Doktor opste prakse", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "12" });
        }
        public ObservableCollection<Zaposlen> Zaposleni { get; set; }

        #endregion

        #region Grafikon
        public Func<ChartPoint, string> PointLabel { get; set; }

        public void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
       

        }
        #endregion

    }
}
