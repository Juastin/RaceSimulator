using NUnit.Framework;
using Model;
using Controller;
using System.Collections.Generic;
using System;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_PlaceParticipantsShould
    {
        private Race _race;
        private Race CurrentRace;
        private Track NextTrack;
        [SetUp]
        public void SetUp()
        {
            Competition Competition = new Competition();
            Competition.Participants.Add(new Driver("Justin", 0, new Car(5, 10, 10, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("Redmer", 0, new Car(8, 8, 7, false), TeamColors.Blue));
            Competition.Participants.Add(new Driver("Wouter", 0, new Car(5, 10, 10, false), TeamColors.Yellow));
            Competition.Tracks.Enqueue(new Track("TestTrack", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish }));
            NextTrack = Competition.NextTrack();
            CurrentRace = new Race(NextTrack, Competition.Participants);
        }
        [Test]
        public void PlaceParticipants_NotEnoughStartGrids_ReturnException()
        {
            Competition Competition = new Competition();
            Competition.Participants.Add(new Driver("A", 0, new Car(5, 10, 10, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("B", 0, new Car(8, 8, 7, false), TeamColors.Blue));
            Competition.Participants.Add(new Driver("C", 0, new Car(5, 10, 10, false), TeamColors.Yellow));
            Competition.Participants.Add(new Driver("D", 0, new Car(5, 10, 10, false), TeamColors.Red));
            Competition.Participants.Add(new Driver("E", 0, new Car(8, 8, 7, false), TeamColors.Blue));
            Competition.Participants.Add(new Driver("F", 0, new Car(5, 10, 10, false), TeamColors.Yellow));
            Assert.Throws<Exception>(
            () => { Race.PlaceParticipants(); });
        }
    }
}
