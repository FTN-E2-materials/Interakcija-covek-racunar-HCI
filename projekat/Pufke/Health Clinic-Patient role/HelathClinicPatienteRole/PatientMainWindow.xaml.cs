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
using System.Windows.Shapes;

namespace HelathClinicPatienteRole
{
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {
        private static PocetnaPatientViewModel objectpocetnaPatientViewModel;

        internal static PocetnaPatientViewModel ObjectpocetnaPatientViewModel { get => objectpocetnaPatientViewModel; set => objectpocetnaPatientViewModel = value; }

        public PatientMainWindow()
        {
            InitializeComponent();

            ObjectpocetnaPatientViewModel = new PocetnaPatientViewModel();
            DataContext = ObjectpocetnaPatientViewModel;
           
        }

        private void PocetnaButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = PocetnaPatientViewModel.Instance;
        }
        private void BlogButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new BlogPatientViewModel();
        }
        private void ProfilButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = ProfilPatientViewModel.Instance;
        }
        private void PropisanaTerapijaButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PropisanaTerapijaPatientViewModel();
        }
        private void ZakaziPregledButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = ZakaziPregledPatientViewModel.Instance;
        }
        private void KartonButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new KartonPatientViewModel();
        }
        private void OkliniciButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new OkliniciPatientViewModel();
        }
        private void KontaktButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new KontaktPatientViewModel();
        }
        private void RecenzijaButton_Click(object sender, RoutedEventArgs e)
        {
      
            DataContext = RecenzijaAppPatientViewModel.Instance;
        }
        private void PomocButton_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PomocPatientViewModel();
        }

    }
}

