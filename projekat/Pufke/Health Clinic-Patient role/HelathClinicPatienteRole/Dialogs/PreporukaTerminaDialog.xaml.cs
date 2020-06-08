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

namespace HelathClinicPatienteRole.Dialogs
{
    /// <summary>
    /// Interaction logic for PreporukaTerminaDialog.xaml
    /// </summary>
    public partial class PreporukaTerminaDialog : Window
    {
		private static bool izabranPrioritet;

		public static bool IzabranPrioritet { get => izabranPrioritet; set => izabranPrioritet = value; }

		public PreporukaTerminaDialog()
        {
            InitializeComponent();
        }

		private void lekarChecked(object sender, RoutedEventArgs e)
		{
		
			if (lekarCheckedCB.IsChecked == true)
			{
                izabranPrioritet = true;
            }
            if (lekarCheckedCB.IsChecked == false)
            {
                izabranPrioritet = false;

            }
        }

		private void datumChecked(object sender, RoutedEventArgs e)
		{

			if (datumCheckedCB.IsChecked == true)
			{
                izabranPrioritet = true;

            }
            if (datumCheckedCB.IsChecked == false)
            {
                izabranPrioritet = false;

            }
        }

        #region Singlton
        private static PreporukaTerminaDialog instance = null;
        private static readonly object padlock = new object();


        public static PreporukaTerminaDialog Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PreporukaTerminaDialog();
                    }
                    return instance;
                }
            }
        }
        #endregion
    }
}
