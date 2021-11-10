﻿using Model;
using System;
using System.Threading;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static event EventHandler NewVisuals;
        public static bool IsWpf { get; set; }

        public static void Initialise(bool isWpf)
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
            NextRace();
            CurrentRace.RaceFinished += OnRaceFinished;
            IsWpf = isWpf;
        }

        private static void OnRaceFinished(object sender, System.EventArgs e)
        {
            NextRace();
            CurrentRace.RaceFinished += OnRaceFinished;
        }

        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Justin", 0, new Ufo(5, 20, 20, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Wouter", 0, new Ufo(5, 20, 20, false), TeamColors.Yellow));
            Competition.Participants.Add(new Driver("Redmer", 0, new Ufo(5, 20, 20, false), TeamColors.Blue));
        }
        public static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("HermansSnackCorner", new[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish }));
            Competition.Tracks.Enqueue(new Track("HermansSnackCorner Inverted", new[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Finish }));
            Competition.Tracks.Enqueue(new Track("Windesheim Cross", new[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish }));
        }
        public static void NextRace()
        {
            if (IsWpf == false)
            {
                CurrentRace?.CollectEventHandlerGarbage();
            }
            if (IsWpf)
            {
                CurrentRace?.CollectWpfGarbage();
            }
            Track nextTrack = Competition.NextTrack();
            if (nextTrack == null)
                NewVisuals?.Invoke(null,EventArgs.Empty);
            if (nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants);
                NewVisuals?.Invoke(CurrentRace, EventArgs.Empty);
            }
        }
    }
}