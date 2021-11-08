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
        private Race CurrentRace;
        private Track NextTrack;
        Competition competition;
        [SetUp]
        public void SetUp()
        {
            competition = new Competition();
            competition.Participants.Add(new Driver("Justin", 0, new Ufo(5, 10, 10, false), TeamColors.Red));
            competition.Participants.Add(new Driver("Redmer", 0, new Ufo(8, 8, 7, false), TeamColors.Blue));
            competition.Participants.Add(new Driver("Wouter", 0, new Ufo(5, 10, 10, false), TeamColors.Yellow));
            competition.Tracks.Enqueue(new Track("TestTrack", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish }));
            NextTrack = competition.NextTrack();
            CurrentRace = new Race(NextTrack, competition.Participants);
        }
        [Test]
        public void PlaceParticipantsOnStartGrid_NotEnoughStartGrids_ReturnException()
        {
            competition.Participants.Add(new Driver("A", 0, new Ufo(5, 10, 10, false), TeamColors.Red));
            competition.Participants.Add(new Driver("B", 0, new Ufo(8, 8, 7, false), TeamColors.Blue));

            Assert.Throws<Exception>(
            () => { CurrentRace.PlaceParticipantsOnStartGrid(CurrentRace.Track, competition.Participants); });
        }
    }
}
