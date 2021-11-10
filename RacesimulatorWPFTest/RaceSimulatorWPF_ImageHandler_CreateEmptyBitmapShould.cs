using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RaceSimulatorWPF;

namespace RaceSimulatorWPFTest
{
    [TestFixture]
    public class RaceSimulatorWPF_ImageHandler_CreateEmptyBitmapShould
    {
        [SetUp]
        public void SetUp()
        {
            ImageHandler.CreateEmptyBitmap(10, 10);
        }

        [Test]
        public void ImageHandler_CreateEmptyBitmap_IsEmpty()
        {
            var empty = ImageHandler.CreateEmptyBitmap(10,10);
            var getEmpty = ImageHandler.GetBitmap("empty");
            Assert.AreEqual(empty, getEmpty);
        }
    }
}
