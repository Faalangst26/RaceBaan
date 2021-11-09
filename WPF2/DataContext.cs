using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF2
{
    public class DataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _track;
        public string Track
        { 
            get 
            {
                return _track;
            }
            set
            {
                _track = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("track"));
            } 
        }
        private ObservableCollection<string> _racelijst;
        public ObservableCollection<string> racelijst
        {
            get
            {
                return _racelijst;
            }
            set
            {
                _racelijst = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("racelijst"));
            }
        }

        private ObservableCollection<string> _driverlijst;
        public ObservableCollection<string> driverlijst
        {
            get
            {
                return _driverlijst;
            }
            set
            {
                _driverlijst = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("driverlijst"));
            }
        }
        private ObservableCollection<int> _afstandlijst;

        public ObservableCollection<int> afstandlijst
        {
            get
            {
                return _afstandlijst;
            }
            set
            {
                _afstandlijst = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("afstandlijst"));
            }
        }








        public DataContext()
        {

        }



        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            
            

        }

    }
}
