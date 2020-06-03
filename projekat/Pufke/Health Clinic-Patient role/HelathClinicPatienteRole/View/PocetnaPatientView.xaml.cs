using HelathClinicPatienteRole.Model;
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
            btnOtkaziPregledDa.Visibility = Visibility.Hidden;
            btnOtkaziPregledNe.Visibility = Visibility.Hidden;
            txtOtkaziPregled.Visibility = Visibility.Hidden;
        }

        private void OtkaziPregledButton_Click(object sender, RoutedEventArgs e)
        {
            if (btnOtkaziPregledDa.Visibility == Visibility.Hidden)
            {
                btnOtkaziPregledDa.Visibility = Visibility.Visible;
                btnOtkaziPregledNe.Visibility = Visibility.Visible;
                txtOtkaziPregled.Visibility = Visibility.Visible;
            }
            else
            {
                btnOtkaziPregledDa.Visibility = Visibility.Hidden;
                btnOtkaziPregledNe.Visibility = Visibility.Hidden;
                txtOtkaziPregled.Visibility = Visibility.Hidden;
            }
           
        }

        private void btnOtkaziPregledDa_Click(object sender, RoutedEventArgs e)
        {
            //  preglediLV.Items.RemoveAt(preglediLV.Items.IndexOf(preglediLV.SelectedItem));
            // preglediLV.SelectedItem = null;

          
            //   Console.WriteLine(preglediLV.SelectedItem);
 /*             Pregled pregledZaBrisanje = null;

            foreach (Pregled pregled in PocetnaPatientViewModel._PregledList)
            {
                if (preglediLV.SelectedItem.Equals(pregled))
                {
                    Console.WriteLine(preglediLV.SelectedItem);
                    pregledZaBrisanje = pregled;
                }
            }
            PocetnaPatientViewModel._PregledList.Remove(pregledZaBrisanje);*/
         //   preglediLV.Items.RemoveAt(preglediLV.Items.IndexOf(preglediLV.SelectedItem));
            //PocetnaPatientViewModel._PregledList.Add(new Pregled { NazivPregleda = "Specijalisticki pregled", TerminPregleda = "22.06.2020  19:00h", StatusPregleda = "Zakazan" });
        }
    }
}
