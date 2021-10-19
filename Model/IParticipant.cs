using System;
using System.Collections.Generic;
using System.Text;
public enum TeamColors
{
    Red,
    Green,
    Yellow,
    Pink,
    Blue
}
namespace Model
{
    public interface IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColors { get; set; }
        public int TraveledDistance { get; set; }
        public int LapsDriven { get; set; }
        public bool IsBroken { get; set; }
    }
}
