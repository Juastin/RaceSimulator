using System;
using System.Collections.Generic;
using System.Drawing;
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
        private static Bitmap emptyBitmap;
        private static Bitmap background;
        private static Graphics graphics;

        #region graphics

        private const string _cornerNE = ".\\Graphics\\cornerNE.png";
        private const string _cornerNW = ".\\Graphics\\cornerNW.png";
        private const string _cornerSE = ".\\Graphics\\cornerSE.png";
        private const string _cornerSW = ".\\Graphics\\cornerSW.png";
        private const string _finishHorizontal = ".\\Graphics\\finishHorizontal.png";
        private const string _finishVertical = ".\\Graphics\\finishVertical.png";
        private const string _startGridHorizontal = ".\\Graphics\\startGridHorizontal.png";
        private const string _startGridVertical = ".\\Graphics\\startGridVertical.png";
        private const string _straightHorizontal = ".\\Graphics\\straightHorizontal.png";
        private const string _straightVertical = ".\\Graphics\\straightVertical.png";
        private const string _teamColorBlue = ".\\Graphics\\teamColorBlue.png";
        private const string _teamColorGreen = ".\\Graphics\\teamColorGreen.png";
        private const string _teamColorPink = ".\\Graphics\\teamColorPink.png";
        private const string _teamColorRed = ".\\Graphics\\teamColorRed.png";
        private const string _teamColorYellow = ".\\Graphics\\teamColorYellow.png";

        #endregion

        public static void Initialise(Race currentRace)
        {
            _compass = 1;
            _lastX = 0;
            _lastY = 1;
            _negativeX = 0;
            _negativeY = 0;
            CurrentRace = currentRace;
            DefineUrl(currentRace.Track.Sections);
            //DrawTrack(currentRace.Track);
            //Data.NewVisuals += OnNewVisuals;
        }

        public static BitmapSource DrawTrack(Track track)
        {
            DefineUrl(track.Sections);
            DefineOffset();
            emptyBitmap = ImageHandler.CreateEmptyBitmap(200,200);
            background = ImageHandler.CreateEmptyBitmap(200,200);
            foreach (Section section in track.Sections)
            {
                graphics = Graphics.FromImage(background);
                if (section.Url != null)
                    graphics.DrawImage(new Bitmap(ImageHandler.GetBitmap(section.Url)), section.X * 20 + _negativeX, section.Y * 20 + _negativeY);
            }

            return ImageHandler.CreateBitmapSourceFromGdiBitmap(emptyBitmap);
        }

        //public static void OnNewVisuals(object sender, EventArgs e)
        //{
        //    if (graphics != null)
        //    {
        //        graphics.Clear(Color.White);
        //        graphics = null;
        //    }
        //    ImageHandler.ClearCache();
        //    // Invoke mainwindow.OnDriversChanged
        //    Initialise(Data.CurrentRace);
        //}

        private static void DefineUrl(LinkedList<Section> sections)
        {
            foreach (Section section in sections)
            {
                DefineSectionUrl(section);
            }

            DefineOffset();
        }

        private static void DefineOffset()
        {
            _negativeX = Math.Abs(_negativeX);
            _negativeY = Math.Abs(_negativeY);
        }

        public static void DefineSectionUrl(Section section)
        {
            section.X = _lastX;
            section.Y = _lastY;
            switch (section.SectionTypes)
            {
                case SectionTypes.StartGrid:
                    if (_compass == 0 || _compass == 2)
                        section.Url = _startGridVertical;
                    else
                        section.Url = _startGridHorizontal;
                    break;
                case SectionTypes.RightCorner:
                    if (_compass == 0)
                        section.Url = _cornerSE;
                    if (_compass == 1)
                        section.Url = _cornerSW;
                    if (_compass == 2)
                        section.Url = _cornerNW;
                    if (_compass == 3)
                        section.Url = _cornerNE;
                    _compass++;
                    break;
                case SectionTypes.LeftCorner:
                    if (_compass == 0)
                        section.Url = _cornerSW;
                    if (_compass == 1)
                        section.Url = _cornerNW;
                    if (_compass == 2)
                        section.Url = _cornerNE;
                    if (_compass == 3)
                        section.Url = _cornerSE;
                    _compass--;
                    break;
                case SectionTypes.Straight:
                    if (_compass == 0 || _compass == 2)
                        section.Url = _straightVertical;
                    else
                        section.Url = _straightHorizontal;
                    break;
                case SectionTypes.Finish:
                    if (_compass == 0 || _compass == 2)
                        section.Url = _finishVertical;
                    else
                        section.Url = _finishHorizontal;
                    break;
            }
            if (_compass == 4)
                _compass = 0;
            if (_compass == -1)
                _compass = 3;
            if (_compass == 0)
                _lastY--;
            if (_compass == 1)
                _lastX++;
            if (_compass == 2)
                _lastY++;
            if (_compass == 3)
                _lastX--;

            if (_lastX * 20 < _negativeX)
                _negativeX = _lastX * 20;
            if (_lastY * 20 < _negativeY)
                _negativeY = _lastY * 20;
        }
    }
}
