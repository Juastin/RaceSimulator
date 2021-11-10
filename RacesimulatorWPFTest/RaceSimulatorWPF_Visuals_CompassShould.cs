using System;
using System.Collections.Generic;
using System.Text;
using Controller;
using Model;
using NUnit.Framework;
using RaceSimulatorWPF;

namespace RaceSimulatorWPFTest
{
    [TestFixture]
    public class RaceSimulatorWPF_Visuals_CompassShould
    {
        [SetUp]
        public void SetUp()
        {
            Data.Initialise(true);
            Visuals.Initialise(Data.CurrentRace);
        }

        [Test]
        public void ChangeCompass_ShouldChangeXorY()
        {
            if (Data.CurrentRace.Track.Sections.First != null)
            {
                Section section = Data.CurrentRace.Track.Sections.First.Value;
                var x1 = section.X;
                    Visuals.DefineSectionUrl(section);
                    Visuals.DefineSectionUrl(section);
                Assert.AreNotEqual(x1,section.X);
            }
        }
    }
}
