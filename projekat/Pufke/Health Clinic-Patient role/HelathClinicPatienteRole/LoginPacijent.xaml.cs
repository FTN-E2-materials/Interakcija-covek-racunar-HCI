using HelathClinicPatienteRole.View;
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
    /// Interaction logic for LoginPacijent.xaml
    /// </summary>
    public partial class LoginPacijent : Window
    {

       
        public LoginPacijent()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }
    }
}
