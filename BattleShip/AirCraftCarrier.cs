using System;
using System.Collections.Generic;
using System.Drawing;

namespace BattleShip
{
    public class AirCraftCarrier : Ship
    {
        private const int _shipSize = 5;
        private Color color = Color.Gray;
        private List<string> _shipCoordinates;

        public AirCraftCarrier(string start, string end)
            : base(start, end, _shipSize)
        {
            ReorderCoordinates();
        }

        public override List<string> ComputeCoorindates()
        {
            List<string> shipFullCoordinates = new List<string>();
            shipFullCoordinates.Capacity = _shipSize;

            // Convert start and end coordinates to integers
            int startX = Convert.ToInt32(Start.Substring(1));
            int startY = Start[0] - 'a' + 1;
            int endX = Convert.ToInt32(End.Substring(1));
            int endY = End[0] - 'a' + 1;

            // Determine the direction of the ship placement
            bool isHorizontal = startX == endX;

            // Fill in the missing coordinates
            for (int i = 0; i < _shipSize; i++)
            {
                int x = isHorizontal ? startX : startX + i;
                int y = isHorizontal ? startY + i : startY;
                string coordinate = $"{(char)(y + 'a' - 1)}{x}";
                shipFullCoordinates.Add(coordinate);
            }

            ShipCoordinates = shipFullCoordinates;

            return shipFullCoordinates;
        }
    }
}
