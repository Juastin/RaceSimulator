using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LapFinishedEventArgs : EventArgs
    {
        public IParticipant Participant { get; set; }
    }
}
