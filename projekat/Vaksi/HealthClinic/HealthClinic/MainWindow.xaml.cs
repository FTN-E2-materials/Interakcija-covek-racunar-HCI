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
            DataContext = new UcitavanjeViewModel();
        }
        /*
         * <Button Grid.Column="0" Content="Pocetna" Click="PocetnaTab_Click"/>
                <Button Grid.Column="1" Content="Blog" Click="BlogTab_Click" />
                <Button Grid.Column="2" Content="O klinici" Click="AboutTab_Click" />
                <Button Grid.Column="3" Content="Recenzije" Click="RecenzijeTab_Click" />
                <Button Grid.Column="4" Content="Zaposleni" Click="ZaposleniTab_Click" />
                <Button Grid.Column="5" Content="Prostorije" Click="ProstorijeTab_Click" />
                <Button Grid.Column="6" Content="Lekovi" Click="LekoviTab_Click" />
                <Button Grid.Column="7" Content="Profil" Click="ProfilTab_Click" />
         */

        private void PocetnaTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new HomePageVModel();
        }
        private void BlogTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new BlogViewModel();
        }
        private void AboutTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new AboutViewModel();
        }

        private void RecenzijeTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new RecenzijaViewModel();
        }

        private void ZaposleniTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ZaposleniVModel();
        }

        private void ProstorijeTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ZaposleniVModel();
        }

        private void LekoviTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new LekoviViewModel();
        }

        private void ProfilTab_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new ProfilViewModel();
        }

    }
}
