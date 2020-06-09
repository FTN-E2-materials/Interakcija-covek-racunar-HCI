using HelathClinicPatienteRole.Model;
using HelathClinicPatienteRole.ViewModel;
using Microsoft.Build.Framework.XamlTypes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelathClinicPatienteRole.View
{
    /// <summary>
    /// Interaction logic for PocetnaPatientView.xaml
    /// </summary>
    public partial class PocetnaPatientView : UserControl
    {
        public PocetnaPatientView()
        {
            InitializeComponent();

        
        }

        private void OdjaviSeButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }


        
    
    }
}
