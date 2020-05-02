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
        }

        private void promenaTaba(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);                                      // Uzimamo index taba koji smo kliknuli

            GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);                     // Pomeranje ikonice koja oznacava na kom smo tabu trenutno

            switch (index)                                                                      // U zavisnosti od taba, prikazujemo drugacije telo nase aplikacije.
            {                                                                                   // Trenutno je to samo promena boje, ali u daljim verzijama cemo tu stavljati 
                case 0:                                                                         // deo koji predstavlja deo tog dela app.
                    GridMain.Background = Brushes.Aquamarine;
                    break;
                case 1:
                    GridMain.Background = Brushes.Beige;
                    break;
                case 2:
                    GridMain.Background = Brushes.CadetBlue;
                    break;
                case 3:
                    GridMain.Background = Brushes.DarkBlue;
                    break;
                case 4:
                    GridMain.Background = Brushes.Firebrick;
                    break;
                case 5:
                    GridMain.Background = Brushes.Gainsboro;
                    break;
                case 6:
                    GridMain.Background = Brushes.HotPink;
                    break;
            }
        }
    }
}
