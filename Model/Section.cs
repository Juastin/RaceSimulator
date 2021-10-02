using System;
using System.Collections.Generic;
using System.Text;

public enum SectionTypes
{
    Straight,
    LeftCorner,
    RightCorner,
    StartGrid,
    Finish
}

namespace Model
{
    public class Section
    {
        public SectionTypes SectionTypes { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string[] Visuals { get; set; }

        public Section(SectionTypes sectionTypes)
        {
            SectionTypes = sectionTypes;
        }
    }
}
