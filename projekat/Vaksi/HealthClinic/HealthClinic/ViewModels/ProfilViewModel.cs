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
    public class ProfilViewModel : ObservableObject
    {

        public ProfilViewModel()
        {
            IzmenaDijalogCommand = new RelayCommand(PrikaziDijalog);
            // kao parametar se ocekuje delegat, posto je delegat pokazivac na funkciju
            // prosledjujem funkciju i onda se ona okida, u ovom slucaju bez uslova pod kojim se okida
            // ali moze se proslediti i uslov pod kojim se okida

            // inicijalno podaci profila ne mogu da se menjaju
            IzmenaProfila = false;

            IzmeniProfilCommand = new RelayCommand(IzmeniProfil);
            PotvrdiIzmeneProfilaCommand = new RelayCommand(PotvrdiIzmeneProfila);
            OdustaniOdIzmeneCommand = new RelayCommand(OdustaniOdIzmene);

            ucitavanjeUpravnikaPosleLogina();

        }

        #region Upravnikov profil

        private Upravnik _upravnik;

        public Upravnik Upravnik
        {
            get { return _upravnik; }
            set { _upravnik = value; OnPropertyChanged("Upravnik"); }
        }


        #endregion

        #region Upravnik za cuvanje podataka
        private Upravnik _upravnikZaCuvanjePodataka;

        /// <summary>
        /// Kada se klikne na izmenu profila, sacuvam trenutke podatke
        /// koje imam o upravniku, da u slucaju opcije 'Odustani'
        /// ovi podaci budu sacuvani a ne oni koji su menjani.
        /// </summary>
        public Upravnik UpravnikZaCuvanjePodataka
        {
            get { return _upravnikZaCuvanjePodataka; }
            set { _upravnikZaCuvanjePodataka = value; OnPropertyChanged("UpravnikZaCuvanjePodataka"); }
        }

        #endregion

        #region Komande

        public RelayCommand IzmenaDijalogCommand { get; private set; }
        public RelayCommand IzmeniProfilCommand { get; private set; }
        public RelayCommand PotvrdiIzmeneProfilaCommand { get; private set; }
        public RelayCommand OdustaniOdIzmeneCommand { get; private set; }


        #endregion

        #region Funkcije koje komande koriste
        public void PrikaziDijalog(object obj)
        {
            var dijalog = new IzmenaProfilaDijalog();
            dijalog.ShowDialog();
        }

        public void IzmeniProfil(object obj)
        {
            
            if(Upravnik is null)
                Upravnik = new Upravnik();

            if(UpravnikZaCuvanjePodataka is null)
                UpravnikZaCuvanjePodataka = new Upravnik();

            // u temp upravniku cuvam trenutne podatke
            UpravnikZaCuvanjePodataka.AdresaStanovanja = Upravnik.AdresaStanovanja;
            UpravnikZaCuvanjePodataka.Biografija = Upravnik.Biografija;
            UpravnikZaCuvanjePodataka.Ime = Upravnik.Ime;
            UpravnikZaCuvanjePodataka.KontaktTelefon = Upravnik.KontaktTelefon;
            UpravnikZaCuvanjePodataka.Prezime = Upravnik.Prezime;
            UpravnikZaCuvanjePodataka.Sifra = Upravnik.Sifra;
            UpravnikZaCuvanjePodataka.Struka = Upravnik.Struka;
            UpravnikZaCuvanjePodataka.KorisnickoIme = Upravnik.KorisnickoIme;
            UpravnikZaCuvanjePodataka.DatumRodjenja = Upravnik.DatumRodjenja;

            // sada je moguca izmena profila
            IzmenaProfila = true;
        }

        public void PotvrdiIzmeneProfila(object obj)
        {
            IzmenaProfila = false;
        }

        public void OdustaniOdIzmene(object obj)
        {
            // kada se odustane, podaci se vracaju na stare
            Upravnik.AdresaStanovanja = UpravnikZaCuvanjePodataka.AdresaStanovanja;
            Upravnik.Biografija = UpravnikZaCuvanjePodataka.Biografija;
            Upravnik.Ime = UpravnikZaCuvanjePodataka.Ime;
            Upravnik.KontaktTelefon = UpravnikZaCuvanjePodataka.KontaktTelefon;
            Upravnik.Prezime = UpravnikZaCuvanjePodataka.Prezime;
            Upravnik.Sifra = UpravnikZaCuvanjePodataka.Sifra;
            Upravnik.Struka = UpravnikZaCuvanjePodataka.Struka;
            Upravnik.KorisnickoIme = UpravnikZaCuvanjePodataka.KorisnickoIme;
            Upravnik.DatumRodjenja = UpravnikZaCuvanjePodataka.DatumRodjenja;

            IzmenaProfila = false;
        }
        #endregion


        #region Izmena profila 
        private bool _izmenaProfila;

        /// <summary>
        /// Odredjujem da li neka polja mogu ili ne mogu da se prikazuju
        /// trenutno
        /// </summary>
        public bool IzmenaProfila
        {
            get { return _izmenaProfila; }
            set { _izmenaProfila = value; OnPropertyChanged("IzmenaProfila"); }
        }

        #endregion

        #region Ucitavanje nakon logina
        private void ucitavanjeUpravnikaPosleLogina()
        {
            Upravnik = new Upravnik()
            {
                Ime = "Dusan",
                Prezime = "Marjanski",
                Sifra = "Upravnik1",
                DatumRodjenja = new DateTime(1990, 01, 01)
            };
        }

        #endregion
    }
}
