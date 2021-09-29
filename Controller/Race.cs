using Model;
using System;
using System.Collections.Generic;
using System.Linq;

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
            _positions = new Dictionary<Section, SectionData>();
            _random = new Random(DateTime.Now.Millisecond);
            PlaceParticipants(Track, Participants);
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
        public void PlaceParticipants(Track track, List<IParticipant> participants)
        {
            LinkedList<Section> startGrids = track.GetSectionDataList(track.Sections);

            if (startGrids.Count >= participants.Count)
            {
                for (int i = 0; i < participants.Count; i++)
                {
                    SectionData SectionData = GetSectionData(startGrids.ElementAt(i));

                    if (i % 2 == 0)
                        SectionData.Left = participants[i];
                    else
                        SectionData.Right = participants[i];
                }
            }
            else
            {
                throw new Exception("There are no startgrids left! Make some more for your track.");
            }
        }
    }
}
