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


namespace HealthClinic.Views
{
    /// <summary>
    /// Interaction logic for ProfilView.xaml
    /// </summary>
    public partial class ProfilView : UserControl
    {
        public ProfilView()
        {
            InitializeComponent();
            this.PieChart();
            this.Cartesian();
        }

        #region Grafikon PieChart
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

        #region Grafikon Cartesian

        public Func<double, string> yFormatter { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        public void Cartesian()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title="Decije", Values = new  ChartValues<double>{100,200,100,200 }
                },
                new LineSeries
                {
                    Title="Hirurgija", Values = new  ChartValues<double>{1000,1200,1300,1900 }
                },
                new LineSeries
                {
                    Title="Oporavak", Values = new  ChartValues<double>{200,600,100,200 }
                },
                new LineSeries
                {
                    Title="Psihijatrija", Values = new  ChartValues<double>{10,3000,10,20 }
                }

            };

            //Labels = new[] { "q1", "Q2", "Q3", "Q4" };
            //yFormatter = value => value.ToString("C");      //TODO: Proveriti sta je ovo "C" u toString

            //SeriesCollection.Add(new LineSeries
            //{
            //    Title = "Nesto bzv",Values = new ChartValues<double> { 10,11,12}
            //});

            //SeriesCollection[3].Values.Add(1000d);
        }

        #endregion
    }
}
