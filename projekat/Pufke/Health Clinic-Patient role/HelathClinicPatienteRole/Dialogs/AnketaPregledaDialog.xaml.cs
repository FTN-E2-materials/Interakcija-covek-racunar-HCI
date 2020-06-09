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

namespace HelathClinicPatienteRole.Dialogs
{
    /// <summary>
    /// Interaction logic for AnketaPregledaDialog.xaml
    /// </summary>
    public partial class AnketaPregledaDialog : Window
    {
        public AnketaPregledaDialog()
        {
            InitializeComponent();
        }

        private void AnketaButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Anketa uspešno poslata! Hvala!");
        }
    }
}
