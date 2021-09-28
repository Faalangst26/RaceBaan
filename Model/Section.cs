using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{

    public enum SectionTypes
    {
        Straight,
        LeftCorner,
        RightCorner,
        StartGrid,
        Finish
    }
    
    public class Section
    {
        public SectionData SectionData { get; set; }
        public SectionTypes SectionType { get; set; }
        
        public Section(SectionTypes sectiontype)
        {
            SectionType = sectiontype;
            SectionData = new SectionData();
        }

    }
}
