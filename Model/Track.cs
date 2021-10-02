using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = AddSections(sections);
        }

        // Fix this
        public LinkedList<Section> AddSections(SectionTypes[] sections)
        {
            LinkedList<Section> Section = new LinkedList<Section>();
            foreach (SectionTypes type in sections)
            {
                Section.AddLast(new Section(type));
            }
            return Section;
        }


        // Fix this
        public LinkedList<Section> GetStartGridSectionData(LinkedList<Section> sections)
        {
            LinkedList<Section> SectionDataList = new LinkedList<Section>();
            foreach (Section section in sections)
            {
                if (section.SectionTypes == SectionTypes.StartGrid)
                    SectionDataList.AddFirst(section);
            }
            return SectionDataList;
        }
    }
}
