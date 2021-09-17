using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Section { get; set; }
        public Track(string name, SectionTypes[] sections)
        {
            name = Name;
            Section = new LinkedList<Section>();
            foreach (SectionTypes type in sections)
            {
                Section.AddLast(new Section(type));
            }
        }
    }
}
