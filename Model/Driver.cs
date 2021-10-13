using System;
using System.Collections.Generic;
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

        public Driver(string name, int points, IEquipment equipment, TeamColors teamColors)
        {
            Name = name;
            Points = points;
            Equipment = equipment;
            TeamColors = teamColors;
            TraveledDistance = 0;
            LapsDriven = 0;
        }
    }
}
