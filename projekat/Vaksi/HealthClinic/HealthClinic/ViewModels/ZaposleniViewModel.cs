using HealthClinic.Models;
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
