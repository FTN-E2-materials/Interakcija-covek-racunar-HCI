using HelathClinicPatienteRole.Dialogs;
using HelathClinicPatienteRole.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelathClinicPatienteRole.ViewModel
{
    class PocetnaPatientViewModel
    {
        public PocetnaPatientViewModel()
        {
            PrikaziDialogCommand = new RelayCommand(PirkaziDialog);
        }

        #region Dialog 

        public RelayCommand PrikaziDialogCommand { get; private set; }

        public void PirkaziDialog(object obj)
        {
            var s = new IzmenaPregledaDialog();
            s.ShowDialog();

        }

        #endregion
    }
}
