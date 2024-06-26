using System;
using System.Collections.Generic;
using System.Drawing;

namespace BattleShip
{
    public class Ship
    {
        public enum ShipName
        {
            Cruiser,
            Destroyer,
            Submarine,
            BattleShip,
            Aircraft_Carrier

        }

        private int _shipSize;
        public const int AIRCRAFTCARRIERSIZE = 5;
        public const int BATTLESHIPSIZE = 4;
        public const int DESTROYERSIZE = 3;
        public const int SUBMARINESIZE = 3;
        public const int CRUISERSIZE = 2;
        private ShipName _shipName;
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

        public ShipName Name
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

        public Ship(string start, string end, ShipName shipName)
        {
            _start = start.ToLower();
            _end = end.ToLower();
            _shipName = shipName;
            Init();

        }

        private void Init()
        {
            SetShipSize();
            ComputeCoorindates();

        }

        private void SetShipSize()
        {
            switch (_shipName)
            {
                case ShipName.Aircraft_Carrier:
                    _shipSize = AIRCRAFTCARRIERSIZE;
                    break;
                case ShipName.BattleShip:
                    _shipSize = BATTLESHIPSIZE;
                    break;
                case ShipName.Submarine:
                    _shipSize = SUBMARINESIZE;
                    break;
                case ShipName.Destroyer:
                    _shipSize = DESTROYERSIZE;
                    break;
                case ShipName.Cruiser:
                    _shipSize = CRUISERSIZE;
                    break;
                default:
                    break;
            }
                
        }

        /// <summary>
        /// This method computes the missing coordinates between the start and the end
        /// </summary>
        /// <returns></returns>
        public List<string> ComputeCoorindates()
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