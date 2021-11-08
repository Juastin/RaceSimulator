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
    class Controller_Race_GetNextSectionShould
    {
        private List<IParticipant> Participants;
        private Track Track;
        private Section CurrentSection;
        private Race Race;
        private Section TestSection;

        [SetUp]
        public void SetUp()
        {
            Track = new Track("TestTrack", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Finish });
            CurrentSection = Track.Sections.First.Value;
            TestSection = new Section(SectionTypes.Straight);
            Participants = new List<IParticipant>();
            Participants.Add(new Driver("test", 0, new Ufo(5, 10, 10, false), TeamColors.Red));
            Race = new Race(Track, Participants);
            
        }
        [Test]
        public void GetNextSection_ShouldBeNextSection() 
        {
            Section nextSection = Race.GetNextSection(CurrentSection, Participants[0]);
            Assert.AreEqual(nextSection, Track.Sections.ElementAt(1));
        }
        [Test]
        public void GetNextSection_IfnotNextSectionReturnFirstSection()
        {
            Section firstSection = Race.GetNextSection(TestSection, Participants[0]);
            Assert.AreEqual(firstSection, Track.Sections.ElementAt(0));
        }
    }
}
