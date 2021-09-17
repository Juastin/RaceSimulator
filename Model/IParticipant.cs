using System;
using System.Collections.Generic;
using System.Text;
public enum TeamColors
{
    Red,
    Green,
    Yellow,
    Grey,
    Blue
}
namespace Model
{
    interface IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColors { get; set; }

    }
}
