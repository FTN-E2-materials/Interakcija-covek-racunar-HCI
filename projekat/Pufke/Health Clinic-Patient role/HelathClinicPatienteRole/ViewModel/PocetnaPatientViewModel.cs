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
            PrikaziDialogCommand = new IzmeniPregledCommand(PirkaziDialog);
        }

        #region Dialog 

        public IzmeniPregledCommand PrikaziDialogCommand { get; private set; }

        public void PirkaziDialog(string dialog)
        {
            var s = new IzmenaPregledaDialog();
            s.ShowDialog();

        }

        #endregion
    }
}
