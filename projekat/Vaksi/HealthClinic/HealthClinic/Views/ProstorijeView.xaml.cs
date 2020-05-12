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
using System.Collections.ObjectModel;           // za kolekciju prostorija
using HealthClinic.Models;                      // ubacivanje paketa modela

namespace HealthClinic.Views
{
    /// <summary>
    /// Interaction logic for ProstorijeView.xaml
    /// </summary>
    public partial class ProstorijeView : UserControl
    {
        public ProstorijeView()
        {
            InitializeComponent();
            this.PieChart();

            //Tabela - popunjavanje
            Prostorije = new ObservableCollection<Prostorija>();
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "12", Namena = "operaciona sala", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "9", Namena = "soba", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "13", Namena = "soba", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "decije", BrojSobe = "8", Namena = "soba", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "decije", BrojSobe = "10", Namena = "operaciona sala", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "otorinolaringologija", BrojSobe = "2", Namena = "soba", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "7", Namena = "operaciona sala", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });



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


        #region Tabela prostorija

        private int brojKolone = 0;

        public ObservableCollection<Prostorija> Prostorije { get; set; }

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
