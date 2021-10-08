using System;
using System.Collections.Generic;
using System.Text;
using Model;


namespace Controller
{
    public static class Data
    {
        public static Competition Competitie { get; set; }
        public static Race CurrentRace { get; set; }

        
    

        public static void Initialize()
        {
            Competitie = new Competition();
            AddDrivers();
            AddTracks();
            
        }

        public static void NextRace()
        {
            Track NextTrack = Competitie.NextTrack();

            if(NextTrack != null)
            {
                CurrentRace = new Race(NextTrack, Competitie.Participants);
                CurrentRace.Start();
            }
            
        }


        public static void AddDrivers()
        {
            Competitie.Participants.Add(new Driver("Gerard", 10, 5));
            Competitie.Participants.Add(new Driver("Henk", 15, 5));
        
        }

        public static void AddTracks()
        {
            Competitie.Tracks.Enqueue(new Track("Baan 1", new SectionTypes[] {SectionTypes.Finish, SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner, SectionTypes.Straight,SectionTypes.RightCorner }));
            Competitie.Tracks.Enqueue(new Track("Baan 2", new SectionTypes[] {SectionTypes.Finish, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.LeftCorner, SectionTypes.RightCorner, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight,SectionTypes.Straight,SectionTypes.Straight,SectionTypes.RightCorner,SectionTypes.Straight, SectionTypes.RightCorner}));
        }



    }
}
