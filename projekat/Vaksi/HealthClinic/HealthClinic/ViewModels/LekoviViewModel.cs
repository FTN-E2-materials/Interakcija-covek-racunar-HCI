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

namespace HealthClinic.ViewModels
{
    public class LekoviViewModel : ObservableObject
    {
        
        public LekoviViewModel()
        {
            ucitavanjeLekova();
            PieChart();

            DodajLekCommand = new RelayCommand(PrikaziDijalogDodavanjaLeka);
            IzmeniLekCommand = new RelayCommand(PrikaziDijalogIzmeneLeka);
            GenerisiIzvestajLekaCommand = new RelayCommand(PrikaziDijalogGenerisanjaIzvestaja);
            
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

        #region Komande

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

        public void PrikaziDijalogDodavanjaLeka(object obj)
        {
            var dijalog = new DodajLekDijalog();
            dijalog.ShowDialog();
        }

        public RelayCommand IzmeniLekCommand { get; private set; }

        public void PrikaziDijalogIzmeneLeka(object obj)
        {
            var dijalog = new IzmenaLekaDijalog();
            dijalog.DataContext = this;             // kako bi prebacio podatke iz ovog prozora u dijalog
            dijalog.ShowDialog();                   // podesavam da i dijalog moze upravljati istim podacima
        }

        #endregion

        #region Tabela
        
        private ObservableCollection<Lek> _lek;

        public ObservableCollection<Lek> Lekovi
        {
            get { return _lek; }
            set { _lek = value; }
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
