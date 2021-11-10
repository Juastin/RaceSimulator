using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using Controller;
using Model;

namespace RaceSimulatorWPF
{
    public class RaceStatsDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<IParticipant> Participants { get => new ObservableCollection<IParticipant>(Data.CurrentRace.Participants);  }
        public RaceStatsDataContext()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.NewVisuals += OnNewVisuals;
            //Participants = Data.CurrentRace.Participants;
        }
        private void OnNewVisuals(object sender, EventArgs e)
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
        }
        private void OnDriversChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
