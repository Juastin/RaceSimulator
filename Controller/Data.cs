using Model;
using System;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static event EventHandler NewVisuals;

        public static void Initialise()
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
            NextRace();
            CurrentRace.RaceFinished += OnRaceFinished;
        }

        private static void OnRaceFinished(object sender, System.EventArgs e)
        {
            NextRace();
            CurrentRace.RaceFinished += OnRaceFinished;
        }

        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Justin", 0, new Car(5, 10, 10, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Wouter", 0, new Car(5, 10, 10, false), TeamColors.Yellow));
            Competition.Participants.Add(new Driver("Redmer", 0, new Car(5, 10, 10, false), TeamColors.Blue));
        }
        public static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("HermansSnackCorner", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish }));
            Competition.Tracks.Enqueue(new Track("Windesheim Cross", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish }));
            Competition.Tracks.Enqueue(new Track("HermansSnackCorner Inverted", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCorner, SectionTypes.LeftCorner, SectionTypes.Finish }));
        }
        public static void NextRace()
        {
            CurrentRace?.CollectEventHandlerGarbage();
            Track nextTrack = Competition.NextTrack();
            if (nextTrack != null)
            {
                CurrentRace = new Race(nextTrack, Competition.Participants);
                //CurrentRace.RaceFinished += OnRaceFinished;
                NewVisuals?.Invoke(CurrentRace, new EventArgs());
                CurrentRace.Start();
            }
        }
    }
}