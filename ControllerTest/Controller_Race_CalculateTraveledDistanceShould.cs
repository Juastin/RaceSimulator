using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using Model;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Race_CalculateTraveledDistanceShould
    {
        private Race race;
        private List<IParticipant> participants;
        private Driver test1;
        private Driver test2;

        [SetUp]
        public void SetUp()
        {
            Track track = new Track("TestTrack", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Finish });
            test1 = new Driver("test1", 0, new Car(5, 5, 5, false), TeamColors.Red);
            test2 = new Driver("test2", 0, new Car(5, 5, 5, false), TeamColors.Yellow);
            participants = new List<IParticipant>();
            race = new Race(track, participants);

            participants.Add(test1);
            participants.Add(test2);
            
        }
        [Test]
        public void CalculateTraveledDistanceShould_ParticipantTraveledDistanceEqual()
        {
            race.CalculateTraveledDistance(test1);
            int traveledDistance = test1.Equipment.Performance * test1.Equipment.Speed;
            Assert.AreEqual(test1.TraveledDistance, traveledDistance);
        }
    }
}
