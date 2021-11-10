using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace Controller
{
    public class Race
    {
        public Track Track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }
        private Random random;
        private Dictionary<Section, SectionData> positions;
        private Timer timer;
        private const int SectionLength = 100;
        private const int MaxLaps = 3;
        public event EventHandler DriversChanged;
        public event EventHandler RaceFinished;
        public event EventHandler CollectGarbage;

        public int AmountFinished { get; set; }

        public Race(Track track, List<IParticipant> participants)
        {
            AmountFinished = 0;
            Track = track;
            Participants = participants;
            positions = new Dictionary<Section, SectionData>();
            random = new Random(DateTime.Now.Millisecond);
            timer = new Timer(300);

            timer.Elapsed += OnTimedEvent;
            
            PlaceParticipantsOnStartGrid(Track, Participants);
            RandomizeEquipment();
            
            
        }
        protected void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            foreach (IParticipant participant in Participants)
            {
                DriversChanged?.Invoke(this, new DriversChangedEventArgs() { Track = Track });
                CalculateTraveledDistance(participant);
            }
        }
        public void CalculateTraveledDistance(IParticipant participant)
        {
            CalculateIsBroken(participant);
            CalculateIsFixed(participant);
            if (participant.IsBroken)
                return;

            participant.TraveledDistance += participant.Equipment.Performance * participant.Equipment.Speed;
            if(participant.TraveledDistance >= SectionLength)
            {
                participant.TraveledDistance -= SectionLength;
                MoveDriver(participant);
            }
        }
        public void CalculateIsBroken(IParticipant participant)
        {
            int quality = participant.Equipment.Quality;
            if (random.Next(1, 10 / quality + quality + 2) == 1)
            {
                participant.IsBroken = true;

                if (Data.IsWpf) return;

                if (participant.Name[0] != '*')
                    participant.Name = "*" + participant.Name;
            }
        }
        public void CalculateIsFixed(IParticipant participant)
        {
            int quality = participant.Equipment.Quality;
            if (random.Next(1, 10 / quality + quality + 2) == 1)
            {
                participant.IsBroken = false;
                if (participant.Name[0] == '*')
                    participant.Name = participant.Name[1..];
            }
        }
        public void CollectEventHandlerGarbage()
        {
            Console.Clear();
            timer.Elapsed -= OnTimedEvent;
            timer = null;
            AmountFinished = 0;
            DriversChanged = null;
            RaceFinished = null;
            
        }
        public void CollectWpfGarbage()
        {
            timer.Stop();
            timer.Elapsed -= OnTimedEvent;
            timer = null;
            AmountFinished = 0;
            CollectGarbage?.Invoke(this, new EventArgs());
            DriversChanged = null;
            RaceFinished = null;
            foreach (var p in Participants)
            {
                p.Stopwatch.Stop();
                p.Stopwatch.Reset();
                p.Laptime = new TimeSpan();
                p.PrevStopwatch = new TimeSpan();
                p.DifferenceLaptime = new TimeSpan();
            }
        }
        // TODO make solid
        public void MoveDriver(IParticipant participant)
        {
            if (participant.LapsDriven >= MaxLaps)
                return;

            Section currentSection = GetSectionByParticipant(participant);
            Section nextSection = GetNextSection(currentSection, participant);
            SectionData currentSectionData = GetSectionData(currentSection);
            GetSectionData(nextSection);

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
            foreach (var p in Participants)
            {
                p.Stopwatch.Start();   
            }
            
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
            participant.Stopwatch.Stop();
            DriversChanged?.Invoke(this, new DriversChangedEventArgs { Track = Track });
            if (AmountFinished >= Participants.Count)
            {
                RaceFinished?.Invoke(this, new EventArgs());
                foreach (IParticipant finishedDriver in Participants)
                {
                    finishedDriver.LapsDriven = 0;
                }
            }

        }
        public SectionData GetSectionData(Section section)
        {
            if (section == null)
                return new SectionData();
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
                if (Track.Sections.Find(currentSection)?.Next != null)
                {
                    var next = Track.Sections.Find(currentSection)?.Next;
                    if (next != null)
                        return next?.Value;
                }
                else
                {
                    participant.LapsDriven++;
                    participant.SetLaptime();

                }
                if (participant.LapsDriven >= MaxLaps)
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
            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Quality = random.Next(5,10);
                participant.Equipment.Performance = random.Next(5,10);
            }
        }
        public void PlaceParticipantsOnStartGrid(Track track, List<IParticipant> participants)
        {
            LinkedList<Section> startGrids = track.GetStartGridSectionData(track.Sections);
            if (startGrids.Count * 2 >= participants.Count)
            {
                for (int i = 0; i < startGrids.Count; i++)
                {
                    SectionData sectionData = GetSectionData(startGrids.ElementAt(i));
                    for (int y = 2 * i; y <= 2 * i + 1; y++)
                    {
                        if (y < participants.Count)
                        {
                            if (y % 2 == 0)
                                sectionData.Left = Participants[y];
                            else
                                sectionData.Right = Participants[y];
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Not enough startGrids, add some more to your track or remove some participants!");
            }
        }
    }
}
