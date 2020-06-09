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

namespace HelathClinicPatienteRole.View
{
    /// <summary>
    /// Interaction logic for PocetnaView.xaml
    /// </summary>
    public partial class PocetnaView : UserControl
    {

        public PocetnaView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginPacijent loginPacijent = new LoginPacijent();
            loginPacijent.Show();
        }
        private void PersonalizovaniLogin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Personalizovana aplikacija je trenutno dostupna samo za Pacijenta!");
        }
     
    }
}
