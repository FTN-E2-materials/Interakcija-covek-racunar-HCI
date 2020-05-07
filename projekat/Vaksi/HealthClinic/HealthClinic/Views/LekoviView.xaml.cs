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
using LiveCharts;               // biblioteka za cartove
using LiveCharts.Wpf;           // uz dodatak na WPF
using System.Collections.ObjectModel;       // za kolekciju lekova
using HealthClinic.Models;

namespace HealthClinic.Views
{
    /// <summary>
    /// Interaction logic for LekoviView.xaml
    /// </summary>
    public partial class LekoviView : UserControl
    {

      
        public LekoviView()
        {
            InitializeComponent();
            this.PieChart();


            // Tabela - popunjavanje
            this.DataContext = this;
            Lekovi = new ObservableCollection<Lek>();
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan", SifraLeka = "0x21FDAF", Kolicina = "10" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan2", SifraLeka = "0x22FDAF", Kolicina = "11" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan3", SifraLeka = "0x23FDAF", Kolicina = "12" });
            Lekovi.Add(new Lek() { NazivLeka = "Bromozepan4", SifraLeka = "0x24FDAF", Kolicina = "13" });

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


        #region Tabela
        
        private int brojKolone = 0;

        public ObservableCollection<Lek> Lekovi { get; set; }

        private void generisiKolone(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //brojKolone++;
            //if(brojKolone == 1)
            //{
            //    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            //}
            e.Column.Width = new DataGridLength(++brojKolone, DataGridLengthUnitType.Star);
        }

        #endregion
    }
}
