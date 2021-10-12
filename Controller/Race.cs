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
        private int maxLaps = 1;
        public event EventHandler DriversChanged;
        public event EventHandler RaceFinished;
        public int AmountFinished = 0;

        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            Participants = participants;
            positions = new Dictionary<Section, SectionData>();
            random = new Random(DateTime.Now.Millisecond);
            timer = new Timer(500);
            leaderboard = new Dictionary<int, IParticipant>();

            timer.Elapsed += OnTimedEvent;
            
            PlaceParticipantsOnStartGrid(Track, Participants);
        }
        protected void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            foreach (IParticipant participant in Participants)
            {
                CalculateTraveledDistance(participant);
            }
        }
        public void CalculateTraveledDistance(IParticipant participant)
        {
            participant.TraveledDistance += participant.Equipment.Performance * participant.Equipment.Speed;
            if(participant.TraveledDistance >= SectionLength)
            {
                participant.TraveledDistance -= SectionLength;
                MoveDriver(participant);
            }
        }
        public void CollectEventHandlerGarbage()
        {
            timer.Elapsed -= OnTimedEvent;
            timer = null;
            DriversChanged = null;
            RaceFinished = null;
        }
        public void MoveDriver(IParticipant participant)
        {
            if (participant.LapsDriven >= maxLaps)
                return;

            Section currentSection = GetSectionByParticipant(participant);
            Section nextSection = GetNextSection(currentSection, participant);
            SectionData currentSectionData = GetSectionData(currentSection);

            if (currentSectionData.Left == participant)
            {
                if (positions[nextSection].Left == null)
                {
                    positions[nextSection].Left = participant;
                    positions[currentSection].Left = null;
                    DriversChanged?.Invoke(this, new DriversChangedEventArgs() { Track = Track });
                }
                else
                {
                    if (positions[nextSection].Right == null)
                    {
                        if (positions[nextSection].Right == null)
                        {
                            positions[nextSection].Right = participant;
                            positions[currentSection].Left = null;
                            DriversChanged?.Invoke(this, new DriversChangedEventArgs() { Track = Track });
                        }
                    }
                }
            }
            if (currentSectionData.Right == participant)
            {
                if(positions[nextSection].Right == null) 
                { 
                    positions[nextSection].Right = participant;
                    positions[currentSection].Right = null;
                    DriversChanged?.Invoke(this, new DriversChangedEventArgs() { Track = Track });
                }
                else
                {
                    if (positions[nextSection].Left == null)
                    {
                        if (positions[nextSection].Left == null)
                        {
                            positions[nextSection].Left = participant;
                            positions[currentSection].Right = null;
                            DriversChanged?.Invoke(this, new DriversChangedEventArgs() { Track = Track });
                        }
                    }
                }
            }
        }
        public void Start()
        {
            timer.Start();
        }
        public void RemoveFromTrack(IParticipant participant)
        {
            SectionData participantSectionData = GetSectionData(GetSectionByParticipant(participant));
            if (participantSectionData.Left == participant)
            {
                participantSectionData.Left = null;

            }
            if (participantSectionData.Right == participant)
            {
                participantSectionData.Right = null;
            }
            AmountFinished++;
            DriversChanged?.Invoke(this, new DriversChangedEventArgs() { Track = Track });
            if (AmountFinished >= Participants.Count)
            {
                RaceFinished?.Invoke(this, new EventArgs());
            }
        }
        public SectionData GetSectionData(Section section)
        {
            if (positions.TryGetValue(section, out SectionData sectionData))
                return sectionData;
            sectionData = new SectionData();
            positions.Add(section, sectionData);
            return sectionData;
        }
        public Section GetNextSection(Section currentSection, IParticipant participant)
        {
            if (Track.Sections.Contains(currentSection)) 
            {
                if (Track.Sections.Find(currentSection).Next != null)
                    return Track.Sections.Find(currentSection).Next.Value;
                else
                    participant.LapsDriven++;
                    if (participant.LapsDriven >= maxLaps)
                    {
                        RemoveFromTrack(participant);
                    }
                return Track.Sections.First.Value;
            }
            return Track.Sections.First.Value;
        }
        public Section GetSectionByParticipant(IParticipant participant)
        {
            foreach (Section section in Track.Sections)
            {
                SectionData sectionData = GetSectionData(section);
                if (sectionData.Left == participant || sectionData.Right == participant)
                    return section;
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
