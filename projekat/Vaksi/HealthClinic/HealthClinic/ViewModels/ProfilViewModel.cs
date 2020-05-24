using HealthClinic.Dialogs;
using HealthClinic.Models;
using HealthClinic.ViewModels.Commands;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthClinic.ViewModels
{
    public class ProfilViewModel
    {

        public ProfilViewModel()
        {
            this.PieChart();
            this.Cartesian();

            PrikaziPorukuCommand = new PorukaCommand(PrikaziPoruku);
        }

        #region Poruka 

        
        public PorukaCommand PrikaziPorukuCommand { get; private set; }

        public void PrikaziPoruku(string poruka)
        {
            var s = new IzmenaProfilaDijalog();
            s.ShowDialog();

        }

        #endregion

        #region Grafikon PieChart
        public Func<ChartPoint, string> PointLabel { get; set; }

        public void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
            

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
        }

        #endregion

    }
}
