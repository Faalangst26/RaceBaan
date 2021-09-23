﻿using System;
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
            Console.WriteLine("Initialiseren!");
            AddDrivers();
            AddTracks();
        }

        public static void NextRace()
        {
            Track NextTrack = Competitie.NextTrack();

            if(NextTrack != null)
            {
                CurrentRace = new Race(NextTrack, Competitie.Participants);
            }
            
        }


        public static void AddDrivers()
        {
            Competitie.Participants.Add(new Driver());
            Competitie.Participants.Add(new Driver());
            Competitie.Participants.Add(new Driver());
        }

        public static void AddTracks()
        {
            Competitie.Tracks.Enqueue(new Track("Baan 1", null));
            Competitie.Tracks.Enqueue(new Track("Baan 2", null));
            Competitie.Tracks.Enqueue(new Track("Baan 3", null));
        }



    }
}