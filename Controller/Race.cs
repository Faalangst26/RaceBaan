using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Model;

namespace Controller
{
    public class Race
    {
        private Random _random;

        private Dictionary<Section, SectionData> _positions;

        private Timer timer;

        public event EventHandler DriversChanged;

        public Track track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        public Race(Track track, List<IParticipant> participants)
        {
            this.track = track;
            this.Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            timer = new Timer(500);
            timer.Elapsed += OnTimedEvent;
            setStartPositions();
        }

        public void Start()
        {
            timer.Start();
        }

        static void OnTimedEvent(object sender, EventArgs e)
        {
            
        }

        

        public void setStartPositions()
        {
            foreach (Section item in track.Sections)//Loop door Alle sections in de baan heen
            {
                if (item.SectionType.Equals(SectionTypes.StartGrid))//Alleen startposities op de startgrid
                {
                    foreach (var participant in Participants)
                    {
                        if(item.SectionData.Left == null)
                        {
                            item.SectionData.Left = participant;
                        } else if (item.SectionData.Right == null)
                        {
                            item.SectionData.Right = participant;
                        }
                        else
                        {
                            return;
                        }                     
                    }
                }
                
            }
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
