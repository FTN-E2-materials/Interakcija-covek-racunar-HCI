using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;                               // biblioteka za cartove
using LiveCharts.Wpf;                           // uz dodatak na WPF
using System.Collections.ObjectModel;           // za kolekciju lekova
using HealthClinic.Models;                      // ubacivanje paketa modela

namespace HealthClinic.Views
{
    /// <summary>
    /// Interaction logic for ZaposleniView.xaml
    /// </summary>
    public partial class ZaposleniView : UserControl
    {
        public ZaposleniView()
        {
            InitializeComponent();
            this.PieChart();                // init piecharta


            //Tabela - popunjavanje
            this.DataContext = this;
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

        #region Grafikon
        public Func<ChartPoint, string> PointLabel { get; set; }

        public void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
            DataContext = this;

        }

        private void PieChart_DataClick(object sender, ChartPoint chartPoint)
        {

        }

        #endregion


        #region Tabela zaposlenih

        private int brojKolone = 0;
        public ObservableCollection<Zaposlen> Zaposleni { get; set; }

        private void generisiKolone(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //brojKolone++;
            //if(brojKolone == 1)
            //{
            //    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            //}
            //e.Column.Width = new DataGridLength(++brojKolone, DataGridLengthUnitType.Star);
        }

        #endregion
    }
}
