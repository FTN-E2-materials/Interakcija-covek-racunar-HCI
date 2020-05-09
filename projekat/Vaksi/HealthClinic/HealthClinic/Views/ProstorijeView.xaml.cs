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
    /// Interaction logic for ProstorijeView.xaml
    /// </summary>
    public partial class ProstorijeView : UserControl
    {
        public ProstorijeView()
        {
            InitializeComponent();
            this.PieChart();

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
    }
}
