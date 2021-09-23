using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }
        public Competition() 
        {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
        }
        public Competition(List<IParticipant> participants, Queue<Track> tracks)
        {
            Participants = participants;
            Tracks = tracks;
        }
        public Track NextTrack()
        {
            if(Tracks.TryDequeue(out Track Track))
            {
                return Track;
            }
            return null;
        }
    }
}
