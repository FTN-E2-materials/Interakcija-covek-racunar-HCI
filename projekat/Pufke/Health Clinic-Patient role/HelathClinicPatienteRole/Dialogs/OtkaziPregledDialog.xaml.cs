using System;
using System.Collections.Generic;
using System.Data;
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

namespace HelathClinicPatienteRole.Dialogs
{
    /// <summary>
    /// Interaction logic for OtkaziPregled.xaml
    /// </summary>
    public partial class OtkaziPregled : Window
    {
        public OtkaziPregled()
        {
            InitializeComponent();
        }

        private void PotvrdiButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OtkaziButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
