using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition { get; set; }
        public static Race CurrentRace { get; set; }

        public static void Initialise()
        {
            Competition = new Competition();
            AddParticipants();
            AddTracks();
        }
        public static void AddParticipants()
        {
            Competition.Participants.Add(new Driver("Justin", 0, new Car(5, 10, 10, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Redmer", 0, new Car(8, 8, 7, false), TeamColors.Blue));
            Competition.Participants.Add(new Driver("Wouter", 0, new Car(5, 10, 10, false), TeamColors.Yellow));
        }
        public static void AddTracks()
        {
            Competition.Tracks.Enqueue(new Track("HermansSnackCorner", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish }));
            Competition.Tracks.Enqueue(new Track("Blokje om", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Finish }));
            Competition.Tracks.Enqueue(new Track("Parkeerplaats Cross", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Finish }));
        }
        public static void NextRace()
        {
            Track NextTrack = Competition.NextTrack();
            if (NextTrack != null)
            {
                CurrentRace = new Race(NextTrack, Competition.Participants);
            }
        }
    }
}