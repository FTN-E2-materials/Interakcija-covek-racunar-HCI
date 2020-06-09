using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.Models
{
    public class Upravnik:Zaposlen
    {
		private string _kontaktTelefon;
		private string _adresaStanovanja;
		private string _biografija;
		private DateTime _datumRodjenja;

		
		public string KontaktTelefon
		{
			get { return _kontaktTelefon; }
			set
			{
				if (value != _kontaktTelefon)
				{
					_kontaktTelefon = value;
					OnPropertyChanged("KontaktTelefon");
				}
			}
		}
		public string AdresaStanovanja
		{
			get { return _adresaStanovanja; }
			set
			{
				if (value != _adresaStanovanja)
				{
					_adresaStanovanja = value;
					OnPropertyChanged("AdresaStanovanja");
				}
			}
		}
		public string Biografija
		{
			get { return _biografija; }
			set
			{
				if (value != _biografija)
				{
					_biografija = value;
					OnPropertyChanged("Biografija");
				}
			}
		}
		public DateTime DatumRodjenja
		{
			get { return _datumRodjenja; }
			set
			{
				if (value != _datumRodjenja)
				{
					_datumRodjenja = value;
					OnPropertyChanged("DatumRodjenja");
				}
			}
		}



	}
}
