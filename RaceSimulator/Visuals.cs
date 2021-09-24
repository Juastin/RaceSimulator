using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace RaceSimulator
{
    public static class Visuals
    {
        public static int X;
        public static int Y;
        public static bool IsHorizontal;
        public static bool IsBackwards;
        public static string[] CurrentSection;
        public static int compass;
        public static void Initialise()
        {
            X = 0;
            Y = 0;
            compass = 1;
        }
        #region graphics
        private static string[] _finishHorizontal = {
            "oooo",
            "  # ",
            "  # ",
            "oooo"
        };
        private static string[] _finishVertical = {
            "o  o",
            "o  o",
            "o##o",
            "o  o"
        };
        private static string[] _startGridHorizontal =
        {
            "oooo",
            "   |",
            "   |",
            "oooo"

        };
        private static string[] _startGridVertical =
        {
            "o  o",
            "o--o",
            "o  o",
            "o  o"

        };
        private static string[] _straightHorizontal = {
            "oooo",
            "    ",
            "    ",
            "oooo"
        };
        private static string[] _straightVertical = {
            "o  o",
            "o  o",
            "o  o",
            "o  o"
        };
        private static string[] _cornerSW = {
            "ooo",
            "  oo",
            "   o",
            "o  o"
        };
        private static string[] _cornerNW = {
            "o  o",
            "   o",
            "  oo",
            "ooo "
        };
        private static string[] _cornerNE = {
            "o  o",
            "o   ",
            "oo  ",
            " ooo"
        };
        private static string[] _cornerSE = {
             " ooo",
             "oo  ",
             "o   ",
             "o  o"
         };
        #endregion

        public static void DrawTrack(Track track)   
        {
            
        }
        public static void DefineSection(Section section)
        {

            switch (section.SectionTypes)
            {
                case SectionTypes.StartGrid:
                    if (compass == 0 || compass == 2)
                        CurrentSection = _startGridVertical;
                    else
                        CurrentSection = _startGridHorizontal;
                    break;
                case SectionTypes.RightCorner:
                    if (compass == 0)
                        CurrentSection = _cornerSE;
                    if (compass == 1)
                        CurrentSection = _cornerSW;
                    if (compass == 2)
                        CurrentSection = _cornerNW;
                    if (compass == 3)
                        CurrentSection = _cornerNE;
                    break;
                case SectionTypes.LeftCorner:
                    if (compass == 0)
                        CurrentSection = _cornerSW;
                    if (compass == 1)
                        CurrentSection = _cornerNW;
                    if (compass == 2)
                        CurrentSection = _cornerNE;
                    if (compass == 3)
                        CurrentSection = _cornerSE;
                    break;
                case SectionTypes.Straight:
                    if (compass == 0 || compass == 2)
                        CurrentSection = _straightVertical;
                    else
                        CurrentSection = _straightHorizontal;
                    break;
                case SectionTypes.Finish:
                    if (compass == 0 || compass == 2)
                        CurrentSection = _finishVertical;
                    else
                        CurrentSection = _finishHorizontal;
                    break;
            }
        }
    }
}
