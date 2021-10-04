using System;
using System.Threading;
using Controller;

namespace Racebaan
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            
            Data.NextRace();
            //Data.NextRace();
            Visualisation.Initialisation();
            //Visualisation.DrawTrack(Data.CurrentRace.track);
            for (; ; )
            {
                Thread.Sleep(100);
            }
            
        }
    }
}
