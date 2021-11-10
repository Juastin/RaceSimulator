using Controller;
using Model;
using System;
using System.Collections.Generic;

namespace RaceSimulator
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
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Console.WriteLine(Data.CurrentRace.Track.Name);
            DrawTrack(currentRace.Track);
            Data.NewVisuals += OnNewVisuals;
        }
        #region graphics
        private static string[] _finishHorizontal = {
            "ooooo",
            "   1#",
            "  2 #",
            "ooooo"
        };
        private static string[] _finishVertical = {
            "o###o",
            "o1  o",
            "o  2o",
            "o   o"
        };
        private static string[] _startGridHorizontal =
        {
            "ooooo",
            "   1 ",
            "  2  ",
            "ooooo"

        };
        private static string[] _startGridVertical =
        {
            "o   o",
            "o1  o",
            "o  2o",
            "o   o"

        };
        private static string[] _straightHorizontal = {
            "ooooo",
            "   1 ",
            "   2 ",
            "ooooo"
        };
        private static string[] _straightVertical = {
            "o   o",
            "o1 2o",
            "o   o",
            "o   o"
        };
        private static string[] _cornerSW = {
            "oooo",
            "   oo",
            "1 2 o",
            "o   o"
        };
        private static string[] _cornerNW = {
            "o   o",
            "1 2 o",
            "   oo",
            "oooo "
        };
        private static string[] _cornerNE = {
            "o   o",
            "o 1 2",
            "oo   ",
            " oooo"
        };
        private static string[] _cornerSE = {
             " oooo",
             "oo   ",
             "o 1 2",
             "o   o"
         };

        #endregion
        
        public static void OnDriversChanged(object sender, EventArgs e)
        {
            DriversChangedEventArgs driversChanged = (DriversChangedEventArgs)e;
            DrawTrack(driversChanged.Track);
        }
        public static void OnNewVisuals(object sender, EventArgs e)
        {
            Initialise(Data.CurrentRace);
            Data.CurrentRace.Start();
        }
        public static void DrawTrack(Track track)
        {
            DefineGraphics(track.Sections);

            foreach (Section section in track.Sections)
            {
                string[] Visuals = DrawParticipantsOnTrack(section);
                for (int i = 0; i < section.Visuals.Length; i++)
                {
                    Console.SetCursorPosition(section.X * _sectionSize[0] + _negativeX, section.Y * _sectionSize[1] + _negativeY + i);
                    Console.Write(Visuals[i]);
                }
            }
        }
        private static void DefineGraphics(LinkedList<Section> sections)
        {
            foreach (Section section in sections)
            {
                DefineSection(section);
            }
            DefineOffset();
        }
        public static string[] DrawParticipantsOnTrack(Section section)
        {
            string[] clone = (string[])section.Visuals.Clone();

            for (int i = 0; i < section.Visuals.Length; i++)
            {
                SectionData sectionData = CurrentRace.GetSectionData(section);
                
                if (sectionData.Left != null)
                    clone[i] = clone[i].Replace('1', sectionData.Left.Name[0]);
                if(sectionData.Right != null)
                    clone[i] = clone[i].Replace('2', sectionData.Right.Name[0]);

                clone[i] = clone[i].Replace('1', ' ');
                clone[i] = clone[i].Replace('2', ' ');
            }
            return clone;
        }
        private static void DefineOffset()
        {
            _negativeX = Math.Abs(_negativeX);
            _negativeY = Math.Abs(_negativeY);
        }
        public static void DefineSection(Section section)
        {
            section.X = _lastX;
            section.Y = _lastY;
            switch (section.SectionTypes)
            {
                case SectionTypes.StartGrid:
                    if (_compass == 0 || _compass == 2)
                        section.Visuals = _startGridVertical;
                    else
                        section.Visuals = _startGridHorizontal;
                    break;
                case SectionTypes.RightCorner:
                    if (_compass == 0)
                        section.Visuals = _cornerSE;
                    if (_compass == 1)
                        section.Visuals = _cornerSW;
                    if (_compass == 2)
                        section.Visuals = _cornerNW;
                    if (_compass == 3)
                        section.Visuals = _cornerNE;
                    _compass++;
                    break;
                case SectionTypes.LeftCorner:
                    if (_compass == 0)
                        section.Visuals = _cornerSW;
                    if (_compass == 1)
                        section.Visuals = _cornerNW;
                    if (_compass == 2)
                        section.Visuals = _cornerNE;
                    if (_compass == 3)
                        section.Visuals = _cornerSE;
                    _compass--;
                    break;
                case SectionTypes.Straight:
                    if (_compass == 0 || _compass == 2)
                        section.Visuals = _straightVertical;
                    else
                        section.Visuals = _straightHorizontal;
                    break;
                case SectionTypes.Finish:
                    if (_compass == 0 || _compass == 2)
                        section.Visuals = _finishVertical;
                    else
                        section.Visuals = _finishHorizontal;
                    break;
            }
            ChangeCompass();
            CalculateNegativeCords();
        }
        public static void ChangeCompass()
        {
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
        }
        public static void CalculateNegativeCords()
        {
            if (_lastX * _sectionSize[0] < _negativeX)
                _negativeX = _lastX * _sectionSize[0];
            if (_lastY * _sectionSize[1] < _negativeY)
                _negativeY = _lastY * _sectionSize[1];
        }
    }
}
