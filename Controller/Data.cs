using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class Data
    {
        static Competition competition;

        public static void Initialise()
        {
            competition = new Competition();
            AddParticipants();
            AddTracks();
        }
        public static void AddParticipants()
        {
            competition.Participants.Add(new Driver("Justin", 0, new Car(10, 10, 10, false), TeamColors.Red));
            competition.Participants.Add(new Driver("Redmer", 0, new Car(8, 8, 7, false), TeamColors.Blue));
            competition.Participants.Add(new Driver("Wouter", 0, new Car(5, 10, 10, false), TeamColors.Yellow));
        }
        public static void AddTracks()
        {
            competition.Tracks.Enqueue(new Track("HermanCorner", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Finish }));
            competition.Tracks.Enqueue(new Track("Blokje om", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Finish }));
            competition.Tracks.Enqueue(new Track("Parkeerplaats Cross", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Finish }));
        }
    }
}
