using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using Model;

namespace Controller
{
    public class Race
    {
        private Random _random;

        private Dictionary<Section, SectionData> _positions;

        private System.Timers.Timer timer;
        public int NumofLaps { get; set; }

        private int _NumberOfDrivers;

        public event EventHandler<DriversChangedEventArgs> DriversChanged;



        //private DriversChangedEventArgs DCEA = new DriversChangedEventArgs();

        public Track track { get; set; }
        public List<IParticipant> Participants { get; set; }
        public DateTime StartTime { get; set; }

        public Race(Track track, int numoflaps, List<IParticipant> participants)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Race constructor!");
            Thread.Sleep(1000);
            this.track = track;
            this.Participants = participants;
            NumofLaps = numoflaps;
            _NumberOfDrivers = Participants.Count;
            foreach (var participant in Participants)
            {
                participant.DistanceTravelled = 0;
                participant.LapsDone = 0;
            }
            _random = new Random(DateTime.Now.Millisecond);
            timer = new System.Timers.Timer(500);
            timer.Elapsed += OnTimedEvent;
            setStartPositions();
        }

        public void Start()
        {
            timer.Start();

        }

        public void Stop()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Race voorbij!");
            timer.Stop();
            timer.Elapsed -= OnTimedEvent;
            foreach (EventHandler<DriversChangedEventArgs> Eh in DriversChanged.GetInvocationList())
            {
                DriversChanged -= Eh;
            }
            Console.Clear();
            Data.NextRace();
        }

        public void OnTimedEvent(object sender, EventArgs e)
        {




            if (DriversChanged != null) DriversChanged(this, new DriversChangedEventArgs(track)); //Drivers changed event afvuren
            _NumberOfDrivers = Participants.Count;
            foreach (IParticipant participant in Participants)
            {
                var driver = (Driver)participant;
                participant.DistanceTravelled += driver.Performance * driver.Speed;
                //Random failures
                if (_random.Next(0, 9) > driver.Quality && driver.isBroken == false)
                {
                    //Failure
                    driver.isBroken = true;
                    driver.Speed = 0;
                    driver.Name = driver.Name.Insert(0, "X");
                }
                if (driver.isBroken)
                {

                    if (_random.Next(0, 9) < driver.Quality)//Auto gerepareerd!
                    {
                        driver.isBroken = false;
                        driver.Name = driver.Name.Remove(0, 1);

                        driver.Speed = _random.Next(1, 20);
                    }
                } 
                else if (driver.LapsDone >= NumofLaps)
                {

                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine($" {participant.Name} is klaar met de race!");
                    RemoveDriver(participant);
                    _NumberOfDrivers--;
                }
                else if (participant.DistanceTravelled % 100 >= 1)//Bij elke 100 meter afgelegd
                {
                    MoveDriver(participant);

                }
                if (_NumberOfDrivers <= 0)
                {

                    Stop();
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

        public void RemoveDriver(IParticipant participant)
        {
            foreach (var section in track.Sections)
            {
                if (section.SectionData.Left != null)
                {
                    if (section.SectionData.Left.Equals(participant))
                    {
                        section.SectionData.Left = null;

                    }
                }
                else if (section.SectionData.Right != null)
                {
                    if (section.SectionData.Right.Equals(participant))
                    {
                        section.SectionData.Right = null;

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

                }
            }
            if (next == true)//Als next nogsteeds true is, hebben we een rondje gecomplete en moet de driver naar het 1e slot op de baan en is er een ronde afgerond     
            {
                if (track.Sections.First.Value.SectionData.Left == null)
                {
                    track.Sections.First.Value.SectionData.Left = participant;
                    participant.LapsDone++;      
                }
                else
                {
                    track.Sections.First.Value.SectionData.Right = participant;
                    participant.LapsDone++;
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
