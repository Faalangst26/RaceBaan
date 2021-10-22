using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Driver : IParticipant
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public int Quality { get; set; }
        public bool isBroken { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
        public int DistanceTravelled { get; set; }

        public int LapsDone { get; set; }
        public int Speed { get; set; }
        public int Performance { get; set; }

        public Driver(string name, int speed, int performance, TeamColors color)
        {
            Name = name;
            Speed = speed;
            Performance = performance;
            Quality = new Random().Next(5, 10);//Random kwaliteit
            TeamColor = color;
        }

    }
}
