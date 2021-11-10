using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RaceFinishedEventArgs : EventArgs
    { 
        public bool RaceFinished { get; set; }
        public Dictionary<int, IParticipant> Leaderboard { get; set; }
    }
}
