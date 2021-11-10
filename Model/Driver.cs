using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColors { get; set; }
        public int TraveledDistance { get; set; }
        public int LapsDriven { get; set; }
        public bool IsBroken { get; set; }
        public TimeSpan Laptime { get; set; }
        public TimeSpan PrevStopwatch { get; set; }
        public TimeSpan PrevLaptime { get; set; }
        public TimeSpan DifferenceLaptime { get; set; }
        public Stopwatch Stopwatch { get; set; }
        

        public Driver(string name, int points, IEquipment equipment, TeamColors teamColors)
        {
            Name = name;
            Points = points;
            Equipment = equipment;
            TeamColors = teamColors;
            TraveledDistance = 0;
            LapsDriven = 0;
            Laptime = new TimeSpan();
            Stopwatch = new Stopwatch();
        }
        public void SetLaptime()
        {
            PrevLaptime = Laptime;
            Laptime = Stopwatch.Elapsed - PrevStopwatch;
            PrevStopwatch = Stopwatch.Elapsed;
            CalculateDifferenceLaptimes();
        }
        public void CalculateDifferenceLaptimes()
        {
            DifferenceLaptime = PrevLaptime - Laptime;
        }
    }
}
