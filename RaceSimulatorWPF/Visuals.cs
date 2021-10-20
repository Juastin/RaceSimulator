using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Controller;
using Model;
using Track = Model.Track;

namespace RaceSimulatorWPF
{
    public static class Visuals
    {
        private static int _lastX;
        private static int _lastY;
        public static Race CurrentRace { get; set; }
        private static int _compass;
        private static int[] _sectionSize;
        private static int _negativeX;
        private static int _negativeY;
        public static void Initialise(Race currentRace)
        {
            _compass = 1;
            _lastX = 0;
            _lastY = 1;
            _sectionSize = new[] { 5, 4 };
            _negativeX = 0;
            _negativeY = 0;
            CurrentRace = currentRace;
            DrawTrack(currentRace.Track);
            Data.NewVisuals += OnNewVisuals;
        }
        public static BitmapSource DrawTrack(Track track)
        {
            return ImageHandler.CreateBitmapSourceFromGdiBitmap(ImageHandler.CreateEmptyBitmap(800, 400));
        }
        
        public static void OnNewVisuals(object sender, EventArgs e)
        {
            Initialise(Data.CurrentRace);
        }
    }
}
