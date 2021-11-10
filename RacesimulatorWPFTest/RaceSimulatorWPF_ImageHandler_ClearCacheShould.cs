using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RaceSimulatorWPF;

namespace RaceSimulatorWPFTest
{
    [TestFixture]
    public class RaceSimulatorWPF_ImageHandler_ClearCacheShould
    {

        [Test]
        public void ImageHandler_ClearCache_Works()
        {
            var test = ImageHandler.CreateEmptyBitmap(10, 10);
            ImageHandler.ClearCache();
            var test2 = ImageHandler.CreateEmptyBitmap(20,20);
            Assert.AreNotEqual(test.Width, test2.Width);
        }
        [Test]
        public void ImageHandler_GetBitmap_ReturnbitmapinCache()
        {
            var test = ImageHandler.CreateEmptyBitmap(10, 10);
            var test2 = ImageHandler.CreateEmptyBitmap(20, 20);
            Assert.AreEqual(test.Width, test2.Width);
        }
    }
}
