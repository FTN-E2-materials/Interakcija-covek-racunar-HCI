using HealthClinic.Dialogs;
using HealthClinic.Models;
using HealthClinic.Utilities;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HealthClinic.ViewModels
{
    public class ProfilViewModel
    {

        public ProfilViewModel()
        {
            IzmenaDijalogCommand = new RelayCommand(PrikaziDijalog);            
            // kao parametar se ocekuje delegat, posto je delegat pokazivac na funkciju
            // prosledjujem funkciju i onda se ona okida, u ovom slucaju bez uslova pod kojim se okida
            // ali moze se proslediti i uslov pod kojim se okida
        }

        #region Komande

        public RelayCommand IzmenaDijalogCommand { get; private set; }

        public void PrikaziDijalog(object obj)
        {
            var dijalog = new IzmenaProfilaDijalog();
            dijalog.ShowDialog();
        }


        #endregion

    }
}
