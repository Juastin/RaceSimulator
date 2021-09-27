﻿using System;
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
            Section = AddSections(sections);
        }
        public LinkedList<Section> AddSections(SectionTypes[] sections)
        {
            LinkedList<Section> Section = new LinkedList<Section>();
            foreach (SectionTypes type in sections)
            {
                Section.AddLast(new Section(type));
            }
            return Section;
        }
        public LinkedList<Section> GetSectionDataList(LinkedList<Section> sections)
        {
            LinkedList<Section> listSections = new LinkedList<Section>();
            foreach (Section section in sections)
            {
                if (section.SectionTypes == SectionTypes.StartGrid)
                    listSections.AddFirst(section);
            }
            return listSections;
        }
    }
}
