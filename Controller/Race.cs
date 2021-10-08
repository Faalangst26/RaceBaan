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

        private int _RaceTime = 0; //Hoelang de race al bezig is

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
            if (DriversChanged != null) DriversChanged(this, new DriversChangedEventArgs(track)); //Drivers changed event afvuren

            foreach (IParticipant participant in Participants)
            {
                var driver = (Driver)participant;
                participant.DistanceTravelled += driver.Performance * driver.Speed;
                if(participant.DistanceTravelled % 100 == 0)//Bij elke 100 meter afgelegd
                {
                    MoveDriver(participant);
                }
            }

            //if (StartCountdown < 0)//Race is gestart
            //{
            //    MoveDriver(Participants[0]);
            //    MoveDriver(Participants[1]);
            //    StartCountdown = 4;
            //}

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

            foreach (var section in track.Sections)//Loop door de track heen en kijk waar de driver staat
            {
                if (section.SectionData.Left != null)
                {
                    if (section.SectionData.Left.Equals(participant))
                    {
                        section.SectionData.Left = null;
                        next = true;
                    }
                }
                else if (section.SectionData.Right != null)
                {
                    if (section.SectionData.Right.Equals(participant))
                    {
                        section.SectionData.Right = null;
                        next = true;
                    }
                }
                else if (next)//Verplaats de driver naar het volgende trackpiece
                {
                    if (section.SectionData.Left == null)
                    {
                        section.SectionData.Left = participant;
                        next = false;
                    }
                    else if (section.SectionData.Right == null)
                    {
                        section.SectionData.Right = participant;
                        next = false;
                    }
                    else
                    {

                    }

                }
            }
            if (next == true)//Als next nogsteeds true is, hebben we een rondje gecomplete en moet de driver naar het 1e slot op de baan
            {
                if(track.Sections.First.Value.SectionData.Left == null)
                {
                    track.Sections.First.Value.SectionData.Left = participant;
                } else
                {
                    track.Sections.First.Value.SectionData.Right = participant;
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
