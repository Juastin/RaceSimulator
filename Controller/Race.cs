using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public class Race
    {
        public Track Track;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
        }
        public SectionData GetSectionData(Section Section)
        {
            if (_positions.TryGetValue(Section, out SectionData SectionData))
            {
                return SectionData;
            }
            SectionData = new SectionData();
            _positions.Add(Section, SectionData);
            return SectionData;
        }
        public void RandomizeEquipment()
        {
            foreach (IParticipant Participant in Participants)
            {
                Participant.Equipment.Quality = _random.Next(10);
                Participant.Equipment.Performance = _random.Next(10);
            }
        }
    }
}
