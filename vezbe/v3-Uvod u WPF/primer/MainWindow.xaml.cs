using System.Windows;
using PrimerCas4.Layouts;
using PrimerCas4.Binding;

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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var s = new StackPanelExample();
            s.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var s = new GridExample();
            s.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var s = new DockPanelExample();
            s.ShowDialog();
            
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            var s = new WrapPanelExample();
            s.ShowDialog();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var s = new ComplexLayoutExample();
            s.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            var s = new SimpleBinding();
            s.Show();
        }
    }
}
