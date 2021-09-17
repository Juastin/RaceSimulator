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
    class Section
    {
        public SectionTypes SectionTypes { get; set; }

        public Section(SectionTypes sectionTypes)
        {
            sectionTypes = SectionTypes;
        }
    }
}
