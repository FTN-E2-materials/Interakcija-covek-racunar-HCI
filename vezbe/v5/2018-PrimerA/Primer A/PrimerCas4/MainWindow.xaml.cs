using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrimerCas4.Layouts;
using PrimerCas4.Binding;
using PrimerCas4.Validation;
using PrimerCas4.Table;
using PrimerCas4._2DG;

namespace PrimerCas4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            var s = new ValidationExample();
            s.Show();
        }
    }
}
