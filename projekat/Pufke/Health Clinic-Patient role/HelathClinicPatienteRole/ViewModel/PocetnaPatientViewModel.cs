using HelathClinicPatienteRole.Dialogs;
using HelathClinicPatienteRole.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelathClinicPatienteRole.ViewModel
{
    class PocetnaPatientViewModel 
    {
        public PocetnaPatientViewModel()
        {
            PirkaziIzmeniPregledDialogCommand = new RelayCommand(PirkaziIzmeniPregledDialog);
            PirkaziOtkaziPregledDialogCommand = new RelayCommand(PirkaziOtkaziPregledDialog);
            PirkaziAnketaLekaraDialogCommand = new RelayCommand(PirkaziAnketaLekaraDialog);
        }

        #region Anketa lekara 

        public RelayCommand PirkaziAnketaLekaraDialogCommand { get; private set; }

        public void PirkaziAnketaLekaraDialog(object obj)
        {
            var s = new AnketaPregledaDialog();
            s.ShowDialog();

        }

        #endregion

        #region Izmeni pregled Dialog 

        public RelayCommand PirkaziIzmeniPregledDialogCommand { get; private set; }

        public void PirkaziIzmeniPregledDialog(object obj)
        {
            var s = new IzmenaPregledaDialog();
            s.ShowDialog();

        }

        #endregion


        #region Otkazi pregled Dialog 

        public RelayCommand PirkaziOtkaziPregledDialogCommand { get; private set; }

        public void PirkaziOtkaziPregledDialog(object obj)
        {
            var s = new OtkaziPregled();
            s.ShowDialog();

        }

        #endregion

    }
}
