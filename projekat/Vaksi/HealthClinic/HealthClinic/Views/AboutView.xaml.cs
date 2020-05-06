using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;                 // za pomeranje slajdera vremena videa
namespace HealthClinic.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        DispatcherTimer timer;

        public AboutView()
        {
            InitializeComponent();

            // Omogucenje promene value slajdera vremena videa nakon nekoliko milisekundi klipa.
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;

            KontrolaKlipa.Volume =(double)SlajderZvuka.Value;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            SlajderVremenaVidea.Value = KontrolaKlipa.Position.TotalSeconds;
        }

        private void SlajderVremenaVidea_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            KontrolaKlipa.Position = TimeSpan.FromSeconds(SlajderVremenaVidea.Value);
        }

        private void SlajderZvuka_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            KontrolaKlipa.Volume = (double)SlajderZvuka.Value;
        }

        // Otvaranjem klipa pocinje tiker koji pomera slajder vremena u videu.
        private void KontrolaKlipa_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = KontrolaKlipa.NaturalDuration.TimeSpan;
            SlajderVremenaVidea.Maximum = ts.TotalSeconds;
            timer.Start();
        }


    }
}
