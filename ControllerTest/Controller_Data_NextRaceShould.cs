using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using NUnit.Framework;

namespace ControllerTest
{
    [TestFixture]
    public class Controller_Data_NextRaceShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialise(true);
        }

        [Test]
        public void NextRace_NotEqualAsLast()
        {
            var race1 = Data.CurrentRace;
            Data.NextRace();
            var race2 = Data.CurrentRace;
            Assert.AreNotEqual(race1,race2);
        }
    }
}
