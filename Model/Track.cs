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
            Sections = ArrayToLinkedList(sections);
            
        }

        public LinkedList<Section> ArrayToLinkedList(SectionTypes[] sections)
        {
            LinkedList<Section> Templist = new LinkedList<Section>();
            foreach (var trackpiece in sections)
            {
                Templist.AddLast(new Section(trackpiece));
            }
            return Templist;
        }

    }
}
