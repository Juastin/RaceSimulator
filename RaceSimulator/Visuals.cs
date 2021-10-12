using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RaceSimulator
{
    public static class Visuals
    {
        private static int lastX;
        private static int lastY;
        public static Race CurrentRace;
        private static int compass;
        private static int[] sectionSize;
        private static int negativeX;
        private static int negativeY;
        public static void Initialise(Race currentRace)
        {
            compass = 1;
            lastX = 0;
            lastY = 1;
            sectionSize = new int[] { 5, 4 };
            negativeX = 0;
            negativeY = 0;
            CurrentRace = currentRace;
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            DrawTrack(currentRace.Track);
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
            DriversChangedEventArgs DriversChanged = (DriversChangedEventArgs)e;
            DrawTrack(DriversChanged.Track);
        }
        public static void DrawTrack(Track track)
        {
                DefineGraphics(track.Sections);

            foreach (Section section in track.Sections)
            {
                string[] Visuals = DrawParticipantsOnTrack(section);
                for (int i = 0; i < section.Visuals.Length; i++)
                {
                    Console.SetCursorPosition(section.X * sectionSize[0] + negativeX, section.Y * sectionSize[1] + negativeY + i);
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
            string[] Visuals = (string[])section.Visuals.Clone();

            for (int i = 0; i < section.Visuals.Length; i++)
            {
                SectionData SectionData = CurrentRace.GetSectionData(section);

                if (SectionData.Left != null)
                    Visuals[i] = Visuals[i].Replace('1', SectionData.Left.Name[0]);
                if(SectionData.Right != null)
                    Visuals[i] = Visuals[i].Replace('2', SectionData.Right.Name[0]);

                Visuals[i] = Visuals[i].Replace('1', ' ');
                Visuals[i] = Visuals[i].Replace('2', ' ');
            }
            return Visuals;
        }
        private static void DefineOffset()
        {
            negativeX = Math.Abs(negativeX);
            negativeY = Math.Abs(negativeY);
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
            if (compass == 4 || compass == -1)
                compass = 0;
            if (compass == 0)
                lastY--;
            if (compass == 1)
                lastX++;
            if (compass == 2)
                lastY++;
            if (compass == 3)
                lastX--;

            if (lastX * sectionSize[0] < negativeX)
                negativeX = lastX * sectionSize[0];
            if (lastY * sectionSize[1] < negativeY)
                negativeY = lastY * sectionSize[1];
        }

    }
}
