using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.Models
{
    public class Oprema : ObservableObject
    {
		private string _nazivOpreme;
        private int _kolicinaOpreme;

        public string NazivOpreme
		{
			get { return _nazivOpreme; }
            set
            {
                if (value != _nazivOpreme)
                {
                    _nazivOpreme = value;
                    OnPropertyChanged("NazivOpreme");
                }
            }
        }

        public int KolicinaOpreme
        {
            get { return _kolicinaOpreme; }
            set
            {
                if (value != _kolicinaOpreme)
                {
                    _kolicinaOpreme = value;
                    OnPropertyChanged("KolicinaOpreme");
                }
            }
        }

    }
}
