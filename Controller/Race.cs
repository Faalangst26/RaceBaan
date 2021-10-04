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

        private int StartCountdown = 1; //5 Seconden start countdown

        public event EventHandler<DriversChangedEventArgs> DriversChanged;



        //private DriversChangedEventArgs DCEA = new DriversChangedEventArgs();

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
            foreach (var item in track.Sections)
            {
                Console.WriteLine($"Sectiondata: {item.SectionData.Left}");
            }
        }

        public void OnTimedEvent(object sender, EventArgs e)
        {
            StartCountdown--;
            if (DriversChanged != null) DriversChanged(this, new DriversChangedEventArgs(track));

            if (StartCountdown < 0)//Race is gestart
            {
                MoveDriver(Participants[0]);
                StartCountdown = 4;
            }

        }



        public void setStartPositions()
        {
            foreach (Section item in track.Sections)//Loop door Alle sections in de baan heen
            {
                if (item.SectionType.Equals(SectionTypes.StartGrid))//Alleen startposities op de startgrid
                {
                    foreach (var participant in Participants)
                    {
                        if (item.SectionData.Left == null)
                        {
                            item.SectionData.Left = participant;
                        }
                        else if (item.SectionData.Right == null)
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

        public void MoveDriver(IParticipant participant)
        {
            Boolean next = false;
            foreach (var section in track.Sections)
            {
                if (section.SectionData.Left != null)
                {
                    if (section.SectionData.Left.Equals(participant))
                    {
                        section.SectionData.Left = null;
                        next = true;
                    }
                }
                else if (next)
                {
                    section.SectionData.Left = participant;
                    next = false;
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
