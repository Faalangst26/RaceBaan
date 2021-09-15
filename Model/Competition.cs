﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {

        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

       public Competition()
       {
            Participants = new List<IParticipant>();
            Tracks = new Queue<Track>();
            Console.WriteLine("Nieuwe competitie aangemaakt");
       }


        public Track NextTrack()
        {
            if(Tracks.Count != 0)
            {
                return Tracks.Dequeue();
            }
            else
            {
                return null;
            }
        }
    }
}
