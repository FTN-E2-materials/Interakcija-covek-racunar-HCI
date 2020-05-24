using HealthClinic.Dialogs;
using HealthClinic.Models;
using HealthClinic.Utilities;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.ViewModels
{
    public class ZaposleniViewModel
    {
        public ObservableCollection<Zaposlen> Zaposleni { get; set; }
        
        public ZaposleniViewModel()
        {
            this.PieChart();                // init piecharta

            ucitavanjePodatakaUTabelu();

            DodajZaposlenogCommand = new RelayCommand(PrikaziDijalogDodavanjaZaposlenog);
            IzmeniZaposlenogCommand = new RelayCommand(PrikaziDijalogIzmeneZaposlenog);
            GenerisiIzvestajZaposlenogCommand = new RelayCommand(PrikaziDijalogGenerisanjaIzvestaja);
        }


        #region Komande

        public RelayCommand GenerisiIzvestajZaposlenogCommand { get; private set; }

        public void PrikaziDijalogGenerisanjaIzvestaja(object obj)
        {
            var dijalog = new GenerisiIzvestajZaposlenihDijalog();
            dijalog.ShowDialog();
        }

        public RelayCommand DodajZaposlenogCommand { get; private set; }

        public void PrikaziDijalogDodavanjaZaposlenog(object obj)
        {
            var dijalog = new DodajZaposlenogDijalog();
            dijalog.ShowDialog();
        }

        public RelayCommand IzmeniZaposlenogCommand { get; private set; }

        public void PrikaziDijalogIzmeneZaposlenog(object obj)
        {
            var dijalog = new IzmeniZaposlenogDijalog();
            dijalog.ShowDialog();
        }

        #endregion

        private void ucitavanjePodatakaUTabelu()
        {
            //Tabela - popunjavanje
            Zaposleni = new ObservableCollection<Zaposlen>();
            Zaposleni.Add(new Zaposlen() { Ime = "Zika", Prezime = "Vojvodic", Struka = "Otorinolaringolog", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "10" });
            Zaposleni.Add(new Zaposlen() { Ime = "Nikola", Prezime = "Zigic", Struka = "Oftamolog", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "8" });
            Zaposleni.Add(new Zaposlen() { Ime = "Marko", Prezime = "Bogdanovic", Struka = "Kardio hirurg", Sifra = "*****", RadniKalendar = "slobodan", BrojOperacijaOveNedelje = "18" });
            Zaposleni.Add(new Zaposlen() { Ime = "Boban", Prezime = "Jokic", Struka = "Pedijatar", Sifra = "*****", RadniKalendar = "slobodan", BrojOperacijaOveNedelje = "9" });
            Zaposleni.Add(new Zaposlen() { Ime = "Nikola", Prezime = "Marjanovic", Struka = "Doktor opste prakse", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "12" });
            Zaposleni.Add(new Zaposlen() { Ime = "Zika", Prezime = "Vojvodic", Struka = "Otorinolaringolog", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "10" });
            Zaposleni.Add(new Zaposlen() { Ime = "Nikola", Prezime = "Zigic", Struka = "Oftamolog", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "8" });
            Zaposleni.Add(new Zaposlen() { Ime = "Marko", Prezime = "Bogdanovic", Struka = "Kardio hirurg", Sifra = "*****", RadniKalendar = "slobodan", BrojOperacijaOveNedelje = "18" });
            Zaposleni.Add(new Zaposlen() { Ime = "Boban", Prezime = "Jokic", Struka = "Pedijatar", Sifra = "*****", RadniKalendar = "slobodan", BrojOperacijaOveNedelje = "9" });
            Zaposleni.Add(new Zaposlen() { Ime = "Nikola", Prezime = "Marjanovic", Struka = "Doktor opste prakse", Sifra = "*****", RadniKalendar = "popunjen", BrojOperacijaOveNedelje = "12" });
        }


        #region Grafikon
        public Func<ChartPoint, string> PointLabel { get; set; }

        public void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
       

        }
        #endregion

    }
}
