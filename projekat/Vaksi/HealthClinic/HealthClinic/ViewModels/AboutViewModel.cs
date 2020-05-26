using HealthClinic.Dialogs;
using HealthClinic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HealthClinic.ViewModels
{
    public class AboutViewModel:ObservableObject
    {   // nasledjujem observable-object kako bi dobio on-property-change
        private System.Uri videoLink = new Uri("C:\\Users\\Vaxi\\Desktop\\6-semestar\\HCI\\projekat\\Vaksi\\HealthClinic\\HealthClinic\\videos\\thanks.mp4");
        private DispatcherTimer timer;

        public AboutViewModel()
        {
            _trenutniProgresKlipa = 1.0;
            _trenutniProgresZvuka = 0.5;

            /*
             * Ovako se moze praviti bilo koja komponenta i s njom mozemo upravljati iz ovog dela.
             */
            MediaElementObject = new MediaElement();                // instanciranje video klipa
            MediaElementObject.Source = videoLink;
            
            
            
            // Omogucenje promene value slajdera vremena videa nakon nekoliko milisekundi klipa.
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            MediaElementObject.Volume = TrenutniProgresZvuka;

            otvaranje();
        }

        #region Komande



        #endregion

        #region Video klip deo

        private double _maxVreme;

        public double MaxVreme
        {
            get { return _maxVreme; }
            set { _maxVreme = value; OnPropertyChanged("MaxVreme"); }
        }


        private void otvaranje()
        {
            //TimeSpan ts = MediaElementObject.NaturalDuration.TimeSpan;
            MaxVreme = 60.0;
            timer.Start();
        }



        // TODO: Proveriti da li je ovo okej[s ovim parametrima]
        private void Timer_Tick(object sender, EventArgs e)
        {
            TrenutniProgresKlipa = MediaElementObject.Position.TotalSeconds;
        }

        private double _trenutniProgresZvuka;

        public double TrenutniProgresZvuka
        {
            get { return _trenutniProgresZvuka; }
            set { _trenutniProgresZvuka = value; MediaElementObject.Volume = _trenutniProgresZvuka ; OnPropertyChanged("TrenutniProgresZvuka"); }
        }



        private double _trenutniProgresKlipa;

        public double TrenutniProgresKlipa
        {
            get { return _trenutniProgresKlipa; }
            set
            {
                _trenutniProgresKlipa = value;
                MediaElementObject.Position = TimeSpan.FromSeconds(_trenutniProgresKlipa);
                OnPropertyChanged("TrenutniProgresKlipa");

                //MessageBox.Show(_trenutniProgresKlipa.ToString());
            }
        }

        private MediaElement _mediaElementObject;

        public MediaElement MediaElementObject
        {
            get { return _mediaElementObject; }
            set { _mediaElementObject = value; OnPropertyChanged("MediaElementObject"); }
        }
        #endregion
    }
}
