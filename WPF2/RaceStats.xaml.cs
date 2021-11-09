using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF2
{
    /// <summary>
    /// Interaction logic for RaceStats.xaml
    /// </summary>
    public partial class RaceStats : Window
    {
        public RaceStats()
        {
            InitializeComponent();
            Update();
            Data.CurrentRace.DriversChanged += OnDriversChangedStats;


        }

        public void OnDriversChangedStats(object sender, DriversChangedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            this.Dispatcher.Invoke(() =>
            {
                var datacontext = (DataContext)this.RaceGrid.DataContext;
                datacontext.racelijst = new System.Collections.ObjectModel.ObservableCollection<string>();
                //Voeg alle tracknamen toe aan de lijst
                datacontext.racelijst.Add(Data.CurrentRace.track.Name);
                foreach (var item in Data.Competitie.Tracks)
                {
                    datacontext.racelijst.Add(item.Name);
                }
                datacontext.driverlijst = new System.Collections.ObjectModel.ObservableCollection<string>();
                datacontext.afstandlijst = new System.Collections.ObjectModel.ObservableCollection<int>();
                //Voeg alle drivers toe aan de lijst
                foreach (var item in Data.Competitie.Participants)
                {
                    datacontext.driverlijst.Add(item.Name);
                    datacontext.afstandlijst.Add(item.DistanceTravelled);
                }
                datacontext.Track = Data.CurrentRace.track.Name;

            });
        }



    }
}
