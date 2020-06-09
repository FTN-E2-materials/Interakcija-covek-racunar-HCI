using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.Models
{
    public class FizickiRad : ObservableObject
    {
		private string _nazivRada;
		private DateTime _odDatuma;
		private DateTime _dotDatuma;

		
		public string NazivRada
		{
			get { return _nazivRada; }
			set { _nazivRada = value; OnPropertyChanged("NazivRada"); }
		}
		
		public DateTime OdDatuma
		{
			get { return _odDatuma; }
			set { _odDatuma = value; OnPropertyChanged("OdDatuma"); }
		}

		public DateTime DoDatuma
		{
			get { return _dotDatuma; }
			set { _dotDatuma = value; OnPropertyChanged("DoDatuma"); }
		}
	}
}
