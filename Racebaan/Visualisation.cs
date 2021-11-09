using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Racebaan
{
    public static class Visualisation
    {
        #region graphics
        private static string[] _startHorizontal = { "----", " 1] ", "2 ] ", "----" };
        private static string[] _finishHorizontal = { "----", " 1# ", "2 # ", "----" };
        private static string[] _cornerRight = { "--o ", "   o", " 2 |", "o 1|" };
        private static string[] _cornerLeft = { "o 1|", " 2 |", "   o", "--o " };
        private static string[] _straightHorizontal = { "----", " 1  ", "  2 ", "----" };
        private static string[] _straightVertical = { "|  |", "| 1|", "|2 |", "|  |" };
        
        #endregion

        public static void Initialisation()
        {
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.UpdateRace += UpdatedRace;
        }

        public static void OnDriversChanged(object sender, DriversChangedEventArgs e)
        {
            DrawTrack(e.track);
            
        }

        public static void UpdatedRace(object sender, EventArgs e)
        {
            Initialisation();
        }

        public static string PlaceDriver(string input, IParticipant participant1, IParticipant participant2)
        {

            if(participant1 != null)
            {
                input = input.Replace('1', participant1.Name[0]);
            }
            else
            {
                input = input.Replace('1', ' ');
            }
            if (participant2 != null)
            {
                input = input.Replace('2', participant2.Name[0]);
            }
            else
            {
                input = input.Replace('2', ' ');
                
            }
                
            return input;
        }


        public static void DrawTrack(Track track)
        {
            //Console.BackgroundColor = ConsoleColor.Blue;
            //Teken locatie van het eerste element in track
            int hor = 20;
            int ver = 10;
            int direction = 1; //1 is richting het oosten, 2 richting het zuiden, 3 het westen, en 0 het noorden 
            foreach (Section item in track.Sections)
            {
                
                Console.SetCursorPosition(hor, ver);
                //Kijk wat voor type item het is, om te bepalen wat er getekend moet worden
                if (item.SectionType.Equals(SectionTypes.StartGrid))
                {
                    foreach (string line in _startHorizontal)
                    {
                        ver++;
                        string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                        Console.Write(driverline);
                        Console.SetCursorPosition(hor, ver);
                    }
                    
                }
                //TODO: Bochten naar links implementeren 
                if (item.SectionType.Equals(SectionTypes.LeftCorner))
                {
                    if (direction == 1)//Bocht richting het noorden
                    {
                        foreach (string line in _cornerLeft)
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(driverline);
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                    if (direction == 2)//Bocht richting het oosten
                    {
                        foreach (string line in _cornerLeft)
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(ReverseString(driverline));                            
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                    if (direction == 3)//Bocht richting het zuiden
                    {
                        foreach (string line in _cornerLeft)
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(driverline);
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                    if (direction == 0)//Bocht richting het westen
                    {
                        foreach (string line in _cornerLeft)
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(driverline);
                            Console.SetCursorPosition(hor, ver);
                        }
                    }

                    direction -= 1;
                }
                if (item.SectionType.Equals(SectionTypes.RightCorner))// Checken welke richting we kijken voor het bepalen van de print
                {
                    if (direction == 1)//Bocht richting het zuiden
                    {
                        foreach (string line in _cornerRight)
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(driverline);
                            Console.SetCursorPosition(hor, ver);
                        }
                        

                    }
                    if (direction == 2)//Bocht richting het westen
                    {
                        foreach (string line in _cornerRight.Reverse())//Loop achteruit door de array om hem te spielen over de x as
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(driverline);
                            Console.SetCursorPosition(hor, ver);
                        }
                        
                    }
                    if (direction == 3)//Bocht richting het noorden
                    {
                        foreach (string line in _cornerRight.Reverse())
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(ReverseString(driverline));
                            Console.SetCursorPosition(hor, ver);
                        }
                        
                    }
                    if (direction == 0)//Bocht richting het oosten
                    {
                        foreach (string line in _cornerRight)
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(ReverseString(driverline));
                            Console.SetCursorPosition(hor, ver);
                        }
                        
                    }


                    direction += 1;

                }
                if (item.SectionType.Equals(SectionTypes.Straight))// Checken welke richting we kijken voor het bepalen van de print
                {
                    if (direction == 1 || direction == 3)
                    {
                        foreach (string line in _straightHorizontal)
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(driverline);
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                    else
                    {
                        foreach (string line in _straightVertical)
                        {
                            ver++;
                            string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                            Console.Write(driverline);
                            Console.SetCursorPosition(hor, ver);
                        }
                    }
                    

                }
                if (item.SectionType.Equals(SectionTypes.Finish))
                {
                    foreach (string line in _finishHorizontal)
                    {
                        ver++;
                        string driverline = PlaceDriver(line, item.SectionData.Left, item.SectionData.Right);
                        Console.Write(driverline);
                        Console.SetCursorPosition(hor, ver);
                    }
                    
                }

                if (direction >= 4) direction = 0;
                if (direction <= -1) direction = 3;

                //Richting van de baan bepaald waar het volgende stuk getekend moet worden
                if (direction == 0)
                {
                    ver -= 8;
                }
                else if (direction == 1)
                {
                    hor += 5;
                    ver -= 4;
                }
                else if (direction == 3)
                {
                    hor -= 5;
                    ver -= 4;
                }



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
