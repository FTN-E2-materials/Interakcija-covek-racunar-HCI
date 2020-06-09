using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.ViewModels
{
    public class HomeViewModel:ObservableObject
    {
        public HomeViewModel()
        {
            PieChart();
            Cartesian();
            ucitavanjeStanjaLekova();
            ucitavanjeStanjaZaposlenih();
            ucitavanjeStanjaProstorija();
        }

        #region Grafikon PieChart
        public Func<ChartPoint, string> PointLabel { get; set; }

        public void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);


        }

        #endregion

        #region Grafikon Cartesian

        public Func<double, string> yFormatter { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }

        public void Cartesian()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title="Decije", Values = new  ChartValues<double>{100,200,100,200 }
                },
                new LineSeries
                {
                    Title="Hirurgija", Values = new  ChartValues<double>{1000,1200,1300,1900 }
                },
                new LineSeries
                {
                    Title="Oporavak", Values = new  ChartValues<double>{200,600,100,200 }
                },
                new LineSeries
                {
                    Title="Psihijatrija", Values = new  ChartValues<double>{10,3000,10,20 }
                }

            };
        }

        #endregion

        #region Deo vezan za grafikone lekova PieChart - UkupnoX

        private ChartValues<int> _ukupnoAntibiotika;
        private ChartValues<int> _ukupnoAnalgetika;
        private ChartValues<int> _ukupnoKardioVaskularnih;
        private ChartValues<int> _ukupnoAnestetika;


        public ChartValues<int> UkupnoAntibiotika
        {
            get { return _ukupnoAntibiotika; }
            set { _ukupnoAntibiotika = value; OnPropertyChanged("UkupnoAntibiotika"); }
        }

        public ChartValues<int> UkupnoAnalgetika
        {
            get { return _ukupnoAnalgetika; }
            set { _ukupnoAnalgetika = value; OnPropertyChanged("UkupnoAnalgetika"); }
        }

        public ChartValues<int> UkupnoKardioVaskularnih
        {
            get { return _ukupnoKardioVaskularnih; }
            set { _ukupnoKardioVaskularnih = value; OnPropertyChanged("UkupnoKardioVaskularnih"); }
        }

        public ChartValues<int> UkupnoAnestetika
        {
            get { return _ukupnoAnestetika; }
            set { _ukupnoAnestetika = value; OnPropertyChanged("UkupnoAnestetika"); }
        }

        #endregion

        #region Ucitavanje lekova za grafikon
        private void ucitavanjeStanjaLekova()
        {
            UkupnoAnalgetika = new ChartValues<int> { LekoviViewModel.Instance.BrojacAnalgetika };
            UkupnoAnestetika = new ChartValues<int> { LekoviViewModel.Instance.BrojacAnestetika };
            UkupnoKardioVaskularnih = new ChartValues<int> { LekoviViewModel.Instance.BrojacKardioVaskularnih };
            UkupnoAntibiotika = new ChartValues<int> { LekoviViewModel.Instance.BrojacAntibiotika };
        }
        #endregion

        #region Deo vezan za grafikone zaposlenih PieChart - UkupnoY


        private ChartValues<int> _ukupnoLekaraOpstePrakse;
        private ChartValues<int> _ukupnoLekaraSpecijalista;
        private ChartValues<int> _ukupnoOstalihZaposlenih;


        public ChartValues<int> UkupnoLekaraOpstePrakse
        {
            get { return _ukupnoLekaraOpstePrakse; }
            set { _ukupnoLekaraOpstePrakse = value; OnPropertyChanged("UkupnoLekaraOpstePrakse"); }
        }

        public ChartValues<int> UkupnoLekaraSpecijalista
        {
            get { return _ukupnoLekaraSpecijalista; }
            set { _ukupnoLekaraSpecijalista = value; OnPropertyChanged("UkupnoLekaraSpecijalista"); }
        }

        public ChartValues<int> UkupnoOstalihZaposlenih
        {
            get { return _ukupnoOstalihZaposlenih; }
            set { _ukupnoOstalihZaposlenih = value; OnPropertyChanged("UkupnoOstalihZaposlenih"); }
        }

        #endregion

        #region Ucitavanje stanja zaposlenih za grafikon
        private void ucitavanjeStanjaZaposlenih()
        {

            UkupnoLekaraOpstePrakse = new ChartValues<int> { ZaposleniViewModel.Instance.BrojacLekaraOpstePrakse };
            UkupnoLekaraSpecijalista = new ChartValues<int> { ZaposleniViewModel.Instance.BrojacLekaraSpecijalista };
            UkupnoOstalihZaposlenih = new ChartValues<int> { ZaposleniViewModel.Instance.BrojacOstalihZaposlenih };
        }
        #endregion

        #region Deo vezan za grafikon prostorija PieChart - UkupnoZ

        private ChartValues<int> _ukupnoSobaZaPacijente;
        private ChartValues<int> _ukupnoSobaZaPreglede;
        private ChartValues<int> _ukupnoOperacionihSala;


        public ChartValues<int> UkupnoSobaZaPacijente
        {
            get { return _ukupnoSobaZaPacijente; }
            set { _ukupnoSobaZaPacijente = value; OnPropertyChanged("UkupnoSobaZaPacijente"); }
        }

        public ChartValues<int> UkupnoSobaZaPreglede
        {
            get { return _ukupnoSobaZaPreglede; }
            set { _ukupnoSobaZaPreglede = value; OnPropertyChanged("UkupnoSobaZaPreglede"); }
        }

        public ChartValues<int> UkupnoOperacionihSala
        {
            get { return _ukupnoOperacionihSala; }
            set { _ukupnoOperacionihSala = value; OnPropertyChanged("UkupnoOperacionihSala"); }
        }


        #endregion

        #region Ucitavanje stanja prostorija za grafikon
        private void ucitavanjeStanjaProstorija()
        {
            UkupnoSobaZaPacijente = new ChartValues<int> { ProstorijeViewModel.Instance.BrojacSobaZaPacijente };
            UkupnoSobaZaPreglede = new ChartValues<int> { ProstorijeViewModel.Instance.BrojacSobaZaPreglede };
            UkupnoOperacionihSala = new ChartValues<int> { ProstorijeViewModel.Instance.BrojacOperacionihSala };
        }
        #endregion
    }
}
