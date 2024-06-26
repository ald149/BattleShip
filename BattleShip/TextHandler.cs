using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public static class TextHandler
    {
        public static void printInputInstructions(string shipType,  string shipSize)
        {
            Console.WriteLine($"You will now be prompted to enter coordinates for a {shipType}, the ship should cover {shipSize } places.");
        }

        public static void printStartInput(string shipType)
        {
            Console.WriteLine($"Please enter a start coordinate for the {shipType}");
        }

        public static void printEndInput(string shipType)
        {
            Console.WriteLine($"Please enter an end coordinate for the {shipType}");
        }
    }
}
