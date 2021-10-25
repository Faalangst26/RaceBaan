using Controller;
using Model;
using System;
using System.Collections.Generic;
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
        public DataContext()
        {

        }



        public void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            
            

        }

    }
}
