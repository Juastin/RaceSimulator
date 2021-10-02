using Model;
using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Race_RandomizeEquipmentShould
    {
        private Race race;
        [SetUp]
        public void SetUp()
        {
            
            List<IParticipant> Participants = new List<IParticipant>
            {
                new Driver("A", 0, new Car(5, 10, 10, false), TeamColors.Red),
                new Driver("B", 0, new Car(5, 10, 10, false), TeamColors.Red),
                new Driver("C", 0, new Car(5, 10, 10, false), TeamColors.Red),
                new Driver("D", 0, new Car(5, 10, 10, false), TeamColors.Red)
            }; 
            race = new Race
                (
                new Track("TestTrack", 
                new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Finish })
                , Participants
                );
        }
        [Test]
        public void RandomizeEquipment_NotBiggerThanint10()
        {
            race.RandomizeEquipment();
            foreach(IParticipant participant in race.Participants)
            {
                Assert.Less(participant.Equipment.Quality, 11);
                Assert.Less(participant.Equipment.Performance, 11);
            }
        }
    }
}
