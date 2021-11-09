using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using Controller;
using Model;

namespace RaceSimulatorWPF
{
    public class CompStatsDataContext : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Track> TrackList => Data.Competition.Tracks.ToList();
        public List<IParticipant> DriverList => Data.Competition.Participants.ToList();

        public CompStatsDataContext()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.NewVisuals += OnNewVisuals;
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
