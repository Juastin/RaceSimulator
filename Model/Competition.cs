using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public Track NextTrack()
        {
            return Tracks.Dequeue();
        }
        public Competition(List<IParticipant> participants, Queue<Track> tracks)
        {
            participants = Participants;
            tracks = Tracks;
        }
    }
}
