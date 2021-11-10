using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Controller;
using Model;
using NUnit.Framework;
using RaceSimulator;

namespace RaceSimulatorTest
{
    class RaceSimulator_Visuals_CompassShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialise(false);
            Visuals.Initialise();
        }

        [Test]
        public void ChangeCompass_ShouldChangeXorY()
        {
            if (Data.CurrentRace.Track.Sections.First != null)
            { 
                Section section = Data.CurrentRace.Track.Sections.First.Value;
                var x1 = section.X;
                Visuals.DefineSection(section);
                Visuals.DefineSection(section);
                Assert.AreNotEqual(x1, section.X);
            }
        }
    }
}
