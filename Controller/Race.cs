using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Controller
{
    public class Race
    {
        public Track Track;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        private Random random;
        private Dictionary<Section, SectionData> positions;
        private Timer timer;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            positions = new Dictionary<Section, SectionData>();
            random = new Random(DateTime.Now.Millisecond);
            timer = new Timer(500);
            
            timer.Elapsed += OnTimedEvent;
            PlaceParticipantsOnStartGrid(Track, Participants);
        }
        protected static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("The threshold was reached.");
        }
        public void Start()
        {
            timer.Start();
        }
        
        public SectionData GetSectionData(Section Section)
        {
            if (positions.TryGetValue(Section, out SectionData SectionData))
            {
                return SectionData;
            }
            SectionData = new SectionData();
            positions.Add(Section, SectionData);
            return SectionData;
        }
        public void RandomizeEquipment()
        {
            foreach (IParticipant Participant in Participants)
            {
                Participant.Equipment.Quality = random.Next(10);
                Participant.Equipment.Performance = random.Next(10);
            }
        }
        public void PlaceParticipantsOnStartGrid(Track track, List<IParticipant> participants)
        {
            LinkedList<Section> startGrids = track.GetStartGridSectionData(track.Sections);
            if (startGrids.Count * 2 >= participants.Count)
            {
                for (int i = 0; i < startGrids.Count; i++)
                {
                    SectionData SectionData = GetSectionData(startGrids.ElementAt(i));
                    for (int y = 2 * i; y <= 2 * i + 1; y++)
                    {
                        if (y < participants.Count)
                        {
                            if (y % 2 == 0)
                                SectionData.Left = Participants[y];
                            else
                                SectionData.Right = Participants[y];
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Not enough startgrids, add some more to your track or remove some participants!");
            }
            
        }
    }
}
