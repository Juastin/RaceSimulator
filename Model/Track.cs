using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Section { get; set; }
        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Section = new LinkedList<Section>();
            foreach (SectionTypes type in sections)
            {
                Section.AddLast(new Section(type));
            }
        }
    }
}
