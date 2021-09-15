using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Track
    {
        public String Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(String name, SectionTypes[] sections)
        {
            Name = name;
           
        }
    }
}
