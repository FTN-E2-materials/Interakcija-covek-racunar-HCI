using HealthClinic.ViewModels;
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

namespace HealthClinic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new UcitavanjeViewModel();                    // na pocetku dajem jedan view koji ce samo da bude pri ucitavanju
        }                                                               // TODO: Kasnije poraditi na tome da nestane nakon par sekundi

        private void PocetnaTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new HomeViewModel();
        }
        private void BlogTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AboutViewModel();
            //TODO: Otkomentarisati po zavrsetku blog view dela. A gornji deo zakomentarisati.
            //DataContext = new BlogViewModel();
        }
        private void AboutTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AboutViewModel();
        }

        private void RecenzijeTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AboutViewModel();
            // TODO: Otkomentarisati po zavrsetku recenzije view dela. A gornji deo zakomentarisati
            //DataContext = new RecenzijaViewModel();
        }

        private void ZaposleniTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ZaposleniViewModel();
        }

        private void ProstorijeTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ProstorijeViewModel();
        }

        private void LekoviTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new LekoviViewModel();
        }

        private void ProfilTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ProfilViewModel();
        }
        

        // Gasenje aplikacije na klik exit u glavnoj app.
        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            // moze i Application.Current.Shutdown();
        }

        // Pomeranje citave aplikacije na klik od nekih komponeneti koje nemaju event lisener.
        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
