using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    


    public partial class MainWindow : Window
    {
        private RaceStats _racestats;
        private CompStats _compstats;

        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRace();
            Initialisation();
        }

        public void Initialisation()
        {
            
            Data.CurrentRace.DriversChanged += OnDriversChangedWPF;
            Data.UpdateRace += UpdatedRace;
           

            
        }
        public void UpdatedRace(object sender, EventArgs e)
        {
            Initialisation();
            ImageHandler.ClearCache();
           

        }

        public void OnDriversChangedWPF(object sender, DriversChangedEventArgs e)
        {
            this.MainImage.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() =>
            {
                this.MainImage.Source = null;
                this.MainImage.Source = VisualizeWPF.DrawTrack(e.track);
            }));
            this.Dispatcher.Invoke(() =>
            {
                var datacontext = (DataContext)this.MijnGrid.DataContext;
                datacontext.Track = Data.CurrentRace.track.Name;
            });
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Open_Competitie(object sender, RoutedEventArgs e)
        {
            _compstats = new CompStats();
            _compstats.Show();
        }

        private void Open_Race(object sender, RoutedEventArgs e)
        {
            _racestats = new RaceStats();
            _racestats.Show();
        }
    }
}
