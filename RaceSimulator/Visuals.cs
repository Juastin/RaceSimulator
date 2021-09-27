using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Model;

namespace RaceSimulator
{
    public static class Visuals
    {
        public static int X;
        public static int Y;
        private static int lastX;
        private static int lastY;
        public static bool IsHorizontal;
        public static bool IsBackwards;
        public static string[] CurrentSection;
        private static int compass;
        public static void Initialise()
        {
            X = 0;
            Y = 0;
            compass = 1;
            lastX = 10;
            lastY = 0;
        }
        #region graphics
        private static string[] _finishHorizontal = {
            "ooooo",
            "    #",
            "    #",
            "ooooo"
        };
        private static string[] _finishVertical = {
            "o###o",
            "o   o",
            "o   o",
            "o   o"
        };
        private static string[] _startGridHorizontal =
        {
            "ooooo",
            "|   |",
            "  |  ",
            "ooooo"

        };
        private static string[] _startGridVertical =
        {
            "o   o",
            "o---o",
            "o   o",
            "o   o"

        };
        private static string[] _straightHorizontal = {
            "ooooo",
            "     ",
            "     ",
            "ooooo"
        };
        private static string[] _straightVertical = {
            "o   o",
            "o   o",
            "o   o",
            "o   o"
        };
        private static string[] _cornerSW = {
            "oooo",
            "   oo",
            "    o",
            "o   o"
        };
        private static string[] _cornerNW = {
            "o   o",
            "    o",
            "   oo",
            "oooo "
        };
        private static string[] _cornerNE = {
            "o   o",
            "o    ",
            "oo   ",
            " oooo"
        };
        private static string[] _cornerSE = {
             " oooo",
             "oo   ",
             "o    ",
             "o   o"
         };
        #endregion

        public static void DrawTrack(Track track)   
        {
            foreach (Section section in track.Section) { 
                DefineSection(section);
                for (int i = 0; i < section.Visuals.Length; i++)
                {
                    Console.SetCursorPosition(section.X * 5, section.Y * 4 + i);
                    Console.Write(section.Visuals[i]);
                    Thread.Sleep(25);
                }
            }
        }
        public static void DefineSection(Section section)
        {
            
            section.X = lastX;
            section.Y = lastY;
            switch (section.SectionTypes)
            {
                case SectionTypes.StartGrid:
                    if (compass == 0 || compass == 2)
                        section.Visuals = _startGridVertical;
                    else
                        section.Visuals = _startGridHorizontal;
                    break;
                case SectionTypes.RightCorner:
                    if (compass == 0)
                        section.Visuals = _cornerSE;
                    if (compass == 1)
                        section.Visuals = _cornerSW;
                    if (compass == 2)
                        section.Visuals = _cornerNW;
                    if (compass == 3)
                        section.Visuals = _cornerNE;
                    compass++;
                    break;
                case SectionTypes.LeftCorner:
                    if (compass == 0)
                        section.Visuals = _cornerSW;
                    if (compass == 1)
                        section.Visuals = _cornerNW;
                    if (compass == 2)
                        section.Visuals = _cornerNE;
                    if (compass == 3)
                        section.Visuals = _cornerSE;

                    compass--;
                    break;
                case SectionTypes.Straight:
                    if (compass == 0 || compass == 2)
                        section.Visuals = _straightVertical;
                    else
                        section.Visuals = _straightHorizontal;
                    break;
                case SectionTypes.Finish:
                    if (compass == 0 || compass == 2)
                        section.Visuals = _finishVertical;
                    else
                        section.Visuals = _finishHorizontal;
                    break;
            }
            if (compass == 0)
               lastY--;
            if (compass == 1)
                lastX++;
            if (compass == 2)
                lastY++;
            if (compass == 3)
                lastX--;
            if (compass == 4 || compass == -1)
                compass = 0;

        }
    }
}
