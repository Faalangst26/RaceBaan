using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Racebaan
{
    static class Visualisation
    {
        #region graphics
        private static string[] _startHorizontal = { "----", "  ] ", "  ] ", "----"};
        private static string[] _finishHorizontal = { "----", "  # ", "  # ", "----" };
        private static string[] _cornerRight = { "--o ", "   o", "   |", "o  |"};
        private static string[] _cornerLeft = { "o  |", "   |", "   o", "--o "};
        private static string[] _straightHorizontal = { "----", "    ", "    ", "----"};
        private static string[] _straightVertical = { "|  |", "|  |", "|  |", "|  |" };

        #endregion

        public static void Initialisation()
        {

        }

        public static void DrawTrack(Track track)
        {
            int hor = 0;
            int direction = 1; //1 is richting het oosten, 2 richting het zuiden, 3 het westen, en 0 het noorden 
            foreach (Section item in track.Sections)
            {
                if (direction >= 4) direction = 0;
                if (direction <= -1) direction = 3;

                int ver = 0;
                Console.SetCursorPosition(hor, 0);
                if (item.SectionType.Equals(SectionTypes.StartGrid))
                {
                    foreach (string line in _startHorizontal)
                    {
                        ver++;
                        Console.Write(line);
                        Console.SetCursorPosition(hor, ver);
                    }
                }
                if (item.SectionType.Equals(SectionTypes.LeftCorner))
                {
                    direction -= 1; 
                    foreach (string line in _cornerLeft)
                    {
                        ver++;
                        Console.Write(line);
                        Console.SetCursorPosition(hor, ver);
                    }
                }
                if (item.SectionType.Equals(SectionTypes.RightCorner))// Checken welke richting we kijken voor het bepalen van de print
                {
                    if(direction == 1)//Bocht richting het zuiden
                    {
                        foreach (string line in _cornerRight)
                        {
                            ver++;
                            Console.Write(line);
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                    if (direction == 2)//Bocht richting het westen
                    {
                        foreach (string line in _cornerRight.Reverse())//Loop achteruit door de array om hem te spielen over de x as
                        {
                            ver++;
                            Console.Write(line);
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                    if (direction == 3)//Bocht richting het noorden
                    {
                        foreach (string line in _cornerRight.Reverse())
                        {
                            ver++;
                            Console.Write(ReverseString(line));
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                    if (direction == 0)//Bocht richting het oosten
                    {
                        foreach (string line in _cornerRight)
                        {
                            ver++;
                            Console.Write(ReverseString(line));
                            Console.SetCursorPosition(hor, ver);
                        }
                    }


                    direction += 1;
                    
                }
                if (item.SectionType.Equals(SectionTypes.Straight))// Checken welke richting we kijken voor het bepalen van de print
                {
                    if(direction == 1 || direction == 3)
                    {
                        foreach (string line in _straightHorizontal)
                        {
                            ver++;
                            Console.Write(line);
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                    else
                    {
                        foreach (string line in _straightVertical)
                        {
                            ver++;
                            Console.Write(line);
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                   
                }
                if (item.SectionType.Equals(SectionTypes.Finish))
                {
                    foreach (string line in _finishHorizontal)
                    {
                        ver++;
                        Console.Write(line);
                        Console.SetCursorPosition(hor, ver);
                    }
                }
                hor += 5;


            }
        }

        public static string ReverseString(string inputstring) //String reverser voor het spiegelen van de bochten over de y as
        {
            char[] charArray = inputstring.ToArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


    }
}
