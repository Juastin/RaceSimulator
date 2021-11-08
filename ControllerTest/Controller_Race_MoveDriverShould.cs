using Controller;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControllerTest
{
    [TestFixture]
    class Controller_Race_MoveDriverShould
    {
        private Race Race;
        private List<IParticipant> Participants;


        [SetUp]
        public void SetUp()
        {
            Participants = new List<IParticipant>();
            Participants.Add(new Driver("Test1", 0, new Ufo(5, 10, 10, false), TeamColors.Red));
            Participants.Add(new Driver("Test2", 0, new Ufo(5, 10, 10, false), TeamColors.Red));
            Participants.Add(new Driver("Test3", 0, new Ufo(5, 10, 10, false), TeamColors.Red));
            Race = new Race(track: new Track("TestTrack", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Finish }), Participants);
        }
        [Test]
        public void MoveDriver_DontMoveWhenCannotOvertake()
        {
            Race.MoveDriver(Participants[2]);
            Race.GetSectionByParticipant(Participants[2]);
            Assert.AreEqual(Race.GetSectionByParticipant(Participants[2]), Race.Track.Sections.First.Value);
        }
        [Test]
        public void MoveDriver_MoveDriverToNextSection()
        {
            Race.MoveDriver(Participants[0]);
            Race.GetSectionByParticipant(Participants[0]);
            Assert.AreNotEqual(Race.GetSectionByParticipant(Participants[0]), Race.Track.Sections.ElementAt(1));
        }
        [Test]
        public void MoveDriver_DriverOvertakes()
        {
            Race.MoveDriver(Participants[0]);
            Race.MoveDriver(Participants[2]);
            Race.MoveDriver(Participants[2]);
            SectionData sectionData = Race.GetSectionData(Race.GetSectionByParticipant(Participants[2]));
            Assert.AreEqual(sectionData.Right, Participants[2]);

        }
    }
}
