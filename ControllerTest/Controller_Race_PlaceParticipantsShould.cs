using NUnit.Framework;
using Model;
using Controller;
using System.Collections.Generic;

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_PlaceParticipantsShould
    {
        private Race _race;
        private Competition Competition;
        private Race CurrentRace;
        private Track NextTrack;
        [SetUp]
        public void SetUp()
        {
            Competition = new Competition();
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

        }
    }
}
