using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Points { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEquipment Equipment { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TeamColors TeamColors { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Driver(string name, int points, IEquipment equipment, TeamColors teamColors)
        {
            name = Name;
            points = Points;
            equipment = Equipment;
            teamColors = TeamColors;
        }
        // public Driver() {   }
    }
}
