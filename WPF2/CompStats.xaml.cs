using Controller;
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
    /// Interaction logic for CompStats.xaml
    /// </summary>
    public partial class CompStats : Window
    {
        public CompStats()
        {
            InitializeComponent();
            this.Dispatcher.Invoke(() =>
            {
                var datacontext = (DataContext)this.CompGrid.DataContext;
                datacontext.racelijst = new System.Collections.ObjectModel.ObservableCollection<string>();
                //Voeg alle tracknamen toe aan de lijst
                datacontext.racelijst.Add(Data.CurrentRace.track.Name);
                foreach (var item in Data.Competitie.Tracks)
                {
                    datacontext.racelijst.Add(item.Name);
                }
                datacontext.driverlijst = new System.Collections.ObjectModel.ObservableCollection<string>();
                //Voeg alle drivers toe aan de lijst
                foreach (var item in Data.Competitie.Participants)
                {
                    datacontext.driverlijst.Add(item.Name);
                }

            });

        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
