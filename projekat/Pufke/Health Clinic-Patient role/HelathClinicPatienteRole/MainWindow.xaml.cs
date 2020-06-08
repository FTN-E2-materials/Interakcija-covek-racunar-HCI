using HelathClinicPatienteRole.ViewModel;
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

namespace HelathClinicPatienteRole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new PocetnaViewModel();
        }


        private void BlogView_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new BlogViewModel();
        }


        private void PocetnaItem_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PocetnaViewModel();
        }

        private void OKliniciItem_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new OKliniciViewModel();
        }

        private void KontaktItem_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new KontaktViewModel();
        }


        private void RecenzijeItem_Click(object sender, RoutedEventArgs e)
        {
            DataContext = RecenzijeViewModel.Instance;
        }

 
    }
}
