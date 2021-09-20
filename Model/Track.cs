using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public String Name { get; set; }
        public LinkedList<Section> Sections = new LinkedList<Section>();


        public Track(String name, SectionTypes[] sections)
        {
            
            Name = name;
            foreach (var trackpiece in sections)
            {
                Sections.AddLast(new Section(trackpiece));
            }
            
        }
    }
}
