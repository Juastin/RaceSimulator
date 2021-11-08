using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Controller;

namespace RaceSimulatorWPF
{
    public class MainWindowDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TrackName => Data.CurrentRace.Track.Name;

        public MainWindowDataContext()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.NewVisuals += OnNewVisuals;
        }

        private void OnNewVisuals(object? sender, EventArgs e)
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }

        private void OnDriversChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
