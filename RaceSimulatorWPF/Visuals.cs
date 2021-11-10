using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private static int _negativeX;
        private static int _negativeY;
       // private static Bitmap _emptyBitmap;
       // private static Bitmap _background;
       // private static Graphics _graphics;

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
        private const string _isBroken = ".\\Graphics\\isBroken.png";

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
            //Bitmap _emptyBitmap = ImageHandler.CreateEmptyBitmap(200,200);
            
            Bitmap _background = ImageHandler.CreateEmptyBitmap(200,200);
            foreach (Section section in track.Sections)
            {
                var sectionData = CurrentRace.GetSectionData(section);
                Graphics _graphics = Graphics.FromImage(_background);
                if (section.Url != null)
                {
                    _graphics.DrawImage(new Bitmap(ImageHandler.GetBitmap(section.Url)), section.X * 20 + _negativeX, section.Y * 20 + _negativeY);
                    if (sectionData.Left != null)
                    {
                        _graphics.DrawImage(
                            sectionData.Left.IsBroken 
                                ? GetBitmapIsBroken() 
                                : GetBitmapOfParticipants(sectionData.Left),
                            section.X * 20 + _negativeX, section.Y * 20 + _negativeY);
                    }
                    if (sectionData.Right != null)
                    {
                        _graphics.DrawImage(
                            sectionData.Right.IsBroken
                                ? GetBitmapIsBroken()
                                : GetBitmapOfParticipants(sectionData.Right), section.X * 20 + 10 + _negativeX,
                            section.Y * 20 + 10 + _negativeY);
                    }
                }

            }
            return ImageHandler.CreateBitmapSourceFromGdiBitmap(_background);
        }
        private static void DefineUrl(LinkedList<Section> sections)
        {
            foreach (Section section in sections)
                DefineSectionUrl(section);

            DefineOffset();
        }

        public static Bitmap GetBitmapOfParticipants(IParticipant participant)
        {
            var bitmap = ImageHandler.GetBitmap(GetGraphicsOfParticipant(participant.TeamColors.ToString()));
            return bitmap;
        }
        public static string GetGraphicsOfParticipant(string teamColor)
        {
            switch (teamColor)
            {
                case "Blue": return _teamColorBlue;
                case "Red": return _teamColorRed;
                case "Yellow": return _teamColorYellow;
                case "Green": return _teamColorGreen;
                case "Pink": return _teamColorPink;
                default: return null;
            }
        }

        public static Bitmap GetBitmapIsBroken()
        {
            return ImageHandler.GetBitmap(_isBroken);
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
