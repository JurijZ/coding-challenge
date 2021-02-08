using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ships
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files", "Input.csv");

            string mapSize = GetMapSize(filePath);
            List<Ship> ships = GetShips(filePath);

            List<string> outputs = new List<string>();

            foreach(var ship in ships)
            {
                foreach(char step in ship.steps)
                {
                    ship.position = CalculateNewPosition(ship.position, step);
                }

                // final position
                outputs.Add(ship.position);
            }
            
            Console.WriteLine("Completed");            
        }

        public static string Forward(string position)
        {
            StringBuilder sb = new StringBuilder(position);

            if (position[4] == 'E')
            {                
                sb[0] = (char)((int)position[0] + 1);
            }
            else if (position[4] == 'N')
            {
                sb[3] = (char)((int)position[3] + 1);
            }
            else if (position[4] == 'W')
            {
                sb[0] = (char)((int)position[0] - 1);
            }
            else
            {
                sb[3] = (char)((int)position[3] - 1);
            }

            return sb.ToString();
        }

        public static string TurnLeft(string position)
        {
            if (position[4] == 'E')
            {
                position = position.Replace('E', 'N');
            }
            else if (position[4] == 'N')
            {
                position = position.Replace('N', 'W');
            }
            else if (position[4] == 'W')
            {
                position = position.Replace('W', 'S');
            }
            else
            {
                position = position.Replace('S', 'E');
            }

            return position;
        }

        public static string TurnRight(string position)
        {
            if (position[4] == 'E')
            {
                position = position.Replace('E', 'S');
            }
            else if (position[4] == 'S')
            {
                position = position.Replace('S', 'W');
            }
            else if (position[4] == 'W')
            {
                position = position.Replace('W', 'N');
            }
            else
            {
                position = position.Replace('N', 'E');
            }

            return position;
        }

        public static string CalculateNewPosition(string position, char step)
        {
            string newPosition;

            if (step == 'L')
            {
                newPosition = TurnLeft(position);
            }
            else if (step == 'R')
            {
                newPosition = TurnRight(position);
            }
            else
            {
                // goes forward
                newPosition = Forward(position);
            }

            return newPosition;
        }

        public static string GetMapSize(string filePath)
        {
            try
            {
                string mapSize;

                using (StreamReader sr = new StreamReader(filePath))
                {
                    mapSize = sr.ReadLine();
                }

                return mapSize;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read");
                throw;
            }
        }

        public static List<Ship> GetShips(string filePath)
        {
            try
            {
                List<Ship> ships = new List<Ship>();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string mapSize = sr.ReadLine(); // skip

                    bool lineType = true;
                    string line;
                    Ship newShip = new Ship();

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Trim() == "")
                        {
                            continue;
                        }

                        if (lineType)
                        {
                            newShip = new Ship();
                            newShip.position = line;
                            lineType = false;
                        }
                        else
                        {
                            newShip.steps = line;
                            ships.Add(newShip);
                            lineType = true;
                        }
                    }
                }

                return ships;
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read");
                throw;
            }
        }
    }
}
