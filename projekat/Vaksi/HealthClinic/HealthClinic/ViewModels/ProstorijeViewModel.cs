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
    public class ProstorijeViewModel
    {

        public ProstorijeViewModel()
        {
            PieChart();
            ucitavanjeProstorija();

            
            DodajProstorijuCommand = new RelayCommand(PrikaziDijalogDodavanjaProstorije);
            IzmeniProstorijuCommand = new RelayCommand(PrikaziDijalogIzmeneProstorije);
            GenerisiIzvestajProstorijaCommand = new RelayCommand(PrikaziDijalogGenerisanjaIzvestaja);
        }

        #region Komande

        public RelayCommand GenerisiIzvestajProstorijaCommand { get; private set; }
        public void PrikaziDijalogGenerisanjaIzvestaja(object obj)
        {
            var dijalog = new GenerisiIzvestajProstorijaDijalog();
            dijalog.ShowDialog();
        }
        public RelayCommand DodajProstorijuCommand { get; private set; }

        public void PrikaziDijalogDodavanjaProstorije(object obj)
        {
            var dijalog = new DodajProstorijuDijalog();
            dijalog.ShowDialog();
        }

        public RelayCommand IzmeniProstorijuCommand { get; private set; }

        public void PrikaziDijalogIzmeneProstorije(object obj)
        {
            var dijalog = new IzmeniProstorijuDijalog();
            dijalog.ShowDialog();
        }


        #endregion 

        #region Tabela
        private void ucitavanjeProstorija()
        {
            Prostorije = new ObservableCollection<Prostorija>();
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "12", Namena = "operaciona sala", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "9", Namena = "soba", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "13", Namena = "soba", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "decije", BrojSobe = "8", Namena = "soba", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "decije", BrojSobe = "10", Namena = "operaciona sala", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "otorinolaringologija", BrojSobe = "2", Namena = "soba", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
            Prostorije.Add(new Prostorija() { Odeljenje = "interno", BrojSobe = "7", Namena = "operaciona sala", UvidZauzetosti = "otvori uvid", SpisakOpreme = "prikazi spisak" });
        }

        public ObservableCollection<Prostorija> Prostorije { get; set; }

        #endregion

        #region Grafikon
        public Func<ChartPoint, string> PointLabel { get; set; }

        private void PieChart()
        {
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
        }

        #endregion
    }
}
