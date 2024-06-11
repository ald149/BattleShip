using System.Collections.Generic;
using System.Drawing;

namespace BattleShip
{
    public abstract class Ship
    {
        public enum ShipName
        {
            PatrolBoat,
            Destroyer,
            Submarine,
            BattleShip,
            Aircraft_Carrier

        }

        private int _shipSize;
        private string _shipName;
        private Color _shipColor;
        private string _start;
        private string _end;
        private List<string> _shipCoordinates;

        public List<string> ShipCoordinates
        {
            get { return _shipCoordinates; }

            set { _shipCoordinates = value; }
        }

        public int ShipSize
        {
            get { return _shipSize; }

            set { _shipSize = value; }
        }

        public string Name
        {
            get { return _shipName; }
            set { _shipName = value; }
        }

        public Color Color
        {
            get { return _shipColor; }
            set { _shipColor = value; }
             
        }
        
        public string Start
        {
            get { return _start; }
        }

        public string End
        {
            get { return _end; }
        }

        public Ship(string start, string end, int shipSize)
        {
            _start = start.ToLower();
            _end = end.ToLower();
            _shipSize = shipSize;
        }

        public abstract List<string> ComputeCoorindates();

        /// <summary>
        /// This method reorders the coordinates so that the start is always a lower value than the end
        /// </summary>
        public void ReorderCoordinates()
        {
            // Split the start and end coordinates into their row and column components
            int startRow = int.Parse(_start.Substring(1));
            int endRow = int.Parse(_end.Substring(1));
            char startCol = _start[0];
            char endCol = _end[0];

            // Check if the end coordinate comes before the start coordinate horizontally or vertically
            if (endCol < startCol || endRow < startRow)
            {
                // Swap the start and end coordinates
                string temp = _start;
                _start = _end;
                _end = temp;
            }

        }
        
    }
}