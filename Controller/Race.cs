using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public class Race
    {
        private Random _random;

        private Dictionary<Section, SectionData> _positions;

        public Track track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        public Race(Track track, List<IParticipant> participants)
        {
            this.track = track;
            this.Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
        }

        public void RandomizeEquipment()
        {
            foreach (var participant in Participants)
            {
                participant.Equipment.Quality = _random.Next();
                participant.Equipment.Performance = _random.Next();
            }
        }

        public SectionData GetSectionData(Section section)
        {

            if (_positions[section] != null)
            {
                return _positions[section];
            }
            else
            {
                _positions.Add(section, new SectionData());
                return _positions[section];
            }
        }

     
    }
}
