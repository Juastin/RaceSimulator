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
        private Dictionary<int, IParticipant> leaderboard;
        private Timer timer;
        private int SectionLength = 100;
        public event EventHandler DriversChanged;
        public Section CurrentSection;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            positions = new Dictionary<Section, SectionData>();
            random = new Random(DateTime.Now.Millisecond);
            timer = new Timer(500);
            leaderboard = new Dictionary<int, IParticipant>();

            //CurrentSection = GetStartLine(track);
            timer.Elapsed += OnTimedEvent;
            
            PlaceParticipantsOnStartGrid(Track, Participants);
            Console.WriteLine();
            //foreach (KeyValuePair<int, IParticipant> keyValuePair in leaderboard)
            //{
            //    Console.WriteLine($"{keyValuePair}");
            //}
            foreach (IParticipant participant in Participants)
            {
                
                Console.WriteLine($"{GetNextSection(GetSectionByParticipant(participant)).SectionTypes}");
                
                if (GetSectionByParticipant(participant) == null)
                {
                    Console.WriteLine("isnull");
                }
            }
            


        }
        protected void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine("This is being executed after every 500ms after Start(); has been called.");
            MoveDrivers();
        }
        public void MoveDrivers()
        {
            //GetnextSection();
            //position[nextSection].Left == participant;
        }
        public void Start()
        {
            timer.Start();
        }
        public SectionData GetSectionData(Section section)
        {
            if (positions.TryGetValue(section, out SectionData sectionData))
            {
                return sectionData;
            }
            sectionData = new SectionData();
            positions.Add(section, sectionData);
            return sectionData;
        }
        public Section GetNextSection(Section currentSection)
        {
            if (Track.Sections.Find(currentSection).Next.Value != null)
            {
                Section p;
                return p = Track.Sections.Find(currentSection).Next.Value;
            }
            else
            {
                return Track.Sections.First.Value;
            }
            


        }
        public Section GetSectionByParticipant(IParticipant participant)
        {
            foreach (Section section in Track.Sections)
            {
                SectionData sectionData = GetSectionData(section);
                if (sectionData.Left == participant || sectionData.Right == participant)
                {
                    return section;
                }
            }
            return null;
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


                            leaderboard.Add(y +1, participants[y]);
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
