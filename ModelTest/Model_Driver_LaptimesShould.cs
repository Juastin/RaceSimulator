using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace ModelTest
{
    [TestFixture]
    public class Model_Driver_LaptimesShould
    {
        private Driver d { get; set; }
        [SetUp]
        public void SetUp()
        {
            d = new Driver("Justin", 0, new Ufo(5, 20, 20, false), TeamColors.Red);
            d.Stopwatch.Start();
        }
        [Test]
        public void SetLaptime_PrevStopWatch_CorrectTime()
        { 
           d.Stopwatch.Stop();
           d.SetLaptime();
           Assert.AreEqual(d.Stopwatch.Elapsed, d.PrevStopwatch);
        }

    }
}
