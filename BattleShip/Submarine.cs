using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Submarine : Ship
    {
        private const int _shipSize = 3;
        private Color color = Color.Gray;
        private List<string> _shipCoordinates;

        public Submarine(string start, string end)
        : base(start, end, _shipSize)
        {
            ReorderCoordinates();
            _shipCoordinates = ComputeCoorindates();
        }

        /// <summary>
        /// This method computes the missing coordinates between the start and the end
        /// </summary>
        /// <returns></returns>
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
