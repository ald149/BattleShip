using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class OpponentController
    {
        private Board _board;
        private Ship _aircraftCarrier;
        private Ship _bigBattleship;
        private Ship _destroyer;
        private Ship _submarine;
        private Ship _cruiser;
        private string[] _startEndCoordinates;
        private char[,] _opponentBoard;
        private char[,] _opponentShootingBoard;
        private List<int> _possibleEndCoordinates;

        public OpponentController(Board gameBoard)
        {
            _board = gameBoard;
            _opponentBoard = new char[10, 10];
            _opponentShootingBoard = new char[10, 10];
        }

        /// <summary>
        /// Randomly pick coordinates to place opponent's ships on the board and validate the coordinates are on the board and don't overlap previous ships.
        /// </summary>
        /// <returns></returns>
        public char[,] PlaceOpponentShips()
        {
            // Place ships on opponents board
            Console.WriteLine("\nPlacing opponents ships.......");
            SetOpponentAircraftCarrier();

            return _opponentBoard;

        }

        /// <summary>
        /// 1. Choose a starting point -- Random int from 00 to 99, no need to verify open
        /// 2. Choose an end point  -- Collect the possible end 
        /// 3. Verify it is open
        /// 4. Fill in middle coordinates
        /// 5. Verify there are no collisions with other placements
        /// </summary>
       private void SetOpponentAircraftCarrier()
        {
            bool setUpComplete = false;

            // Even if Start and endCoords are ok, the overlap may fail
            while (!setUpComplete)
            {
                SetCoordinates(5);
            }


        }

        private void SetCoordinates(int shipSize)
        {
            int startCoordinate = SetStartCoordinate();                 // -- This is good. 
            _possibleEndCoordinates = GetPossibleEndCoordinates(startCoordinate, shipSize);
            int endCoordinate = SetEndCoordinate(startCoordinate);
            // Now, do we pick the end coordinate and then test for overlap.... or do we check each posssible direction for overlap, and then choose? 
            // Ok... this is the way...
            // Get the 4 possible end points, then check which ones are viable. Then use random picker to choose from the possible choices. 
            // Yes... this... because if none are viable, then we return and pick a new start point immediately
        }

        /// <summary>
        /// Set the start Coordinate 
        /// </summary>
        /// <returns></returns>
        private int SetStartCoordinate()
        {
            bool startCoordinateSet = false;
            int startCoordinate = 0;

            while (!startCoordinateSet)
            {
                Random randStart = new Random();
                int xCoordinate = randStart.Next(0, 9);
                int yCoordinate = randStart.Next(0, 9);
                if(_opponentBoard[xCoordinate, yCoordinate] != 'C')
                {
                    _opponentBoard[xCoordinate, yCoordinate] = 'C';
                    startCoordinateSet = true;
                    int xInt = xCoordinate * 10;
                    startCoordinate = xInt + yCoordinate;
                }

            }

            return startCoordinate;
        }

        /// <summary>
        /// Checks each possible end coordinate, and returns the first one that doesn't overlap. 
        /// </summary>
        /// <param name="startCoordinate"></param>
        /// <returns></returns>
        private int SetEndCoordinate(int startCoordinate)
        {
            // -------------this can be better, more efficient somehow. Probably works fine at this point, but sloppy.
            bool endCoordinateSet = false;
            while (!endCoordinateSet)
            {
                int endCoordinateCount = _possibleEndCoordinates.Count;
                bool[] isCoordinateChecked = new bool[endCoordinateCount];
                Random randStart = new Random();
                int endCoordIndex = randStart.Next(0, endCoordinateCount);
                if (!isCoordinateChecked[endCoordIndex])
                {
                    int tryCoordinate = _possibleEndCoordinates.ElementAt(endCoordIndex);
                    endCoordinateSet = DoesEndCoordinateOverlap(startCoordinate, tryCoordinate);
                    if (endCoordinateSet)
                        return tryCoordinate;
                }
                foreach (bool value in isCoordinateChecked)
                {
                    int checkedCount = 0;
                    if(value == true)
                    { checkedCount++; }
                    if (checkedCount == endCoordinateCount)
                        return -1;
                }
            }

            return -1;
        }

        /// <summary>
        /// Checks a x
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool DoesEndCoordinateOverlap(int startCoordinate, int endCoordinate)
        {

            // now we just need to do a check for overlap and return


            return false;
        }

        /// <summary>
        /// Method takes in the 
        /// </summary>
        /// <param name="startCoordinate"></param>
        /// <returns></returns>
        public List<int>  GetPossibleEndCoordinates(int startCoordinate, int shipSize)
        {
            List<int> endCoords = new List<int>();
            int offset = shipSize - 1;
            endCoords.Add(GetWestCoordinate(startCoordinate, offset));
            endCoords.Add( GetNorthCoordinate(startCoordinate, offset));
            endCoords.Add( GetEastCoordinate(startCoordinate, offset));
            endCoords.Add( GetSouthCoordinate(startCoordinate, offset));
            foreach(int coord in endCoords)
            {
                if (coord == -1)
                    endCoords.Remove(coord);
            }
            
            return endCoords;
        }

        /// <summary>
        /// Get the possible end coordinate to the west of the start coordinate.
        /// </summary>
        /// <param name="startCoordinate"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int GetWestCoordinate(int startCoordinate, int offset)
        {
            int secondDigit = startCoordinate % 10;
            if(secondDigit - offset < 0)
            {
                return -1;
            }
            else
            {
                return startCoordinate - offset;
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startCoordinate"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int GetEastCoordinate(int startCoordinate, int offset)
        {
            int secondDigit = startCoordinate % 10;
            if(secondDigit + offset > 9)
            {
                return -1;
            }
            else
            {
                return startCoordinate + offset;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startCoordinate"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int GetNorthCoordinate(int startCoordinate, int offset)
        {
            int firstDigit = startCoordinate / 10;
            if(firstDigit - offset < 0)
            {
                return -1;
            }
            else
            {
                return startCoordinate - (offset * 10);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startCoordinate"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public int GetSouthCoordinate(int startCoordinate, int offset)
        {
            int firstDigit = startCoordinate / 10;
            if(firstDigit + offset > 9)
            {
                return -1;
            }
            else
            {
                return startCoordinate + (offset * 10);
            }

        }



        //public bool ShipStartAndEndInputsValid(int start, int end, int shipSize)
        //{
        //    bool isValid = false;

        //    // These algorithms or checks do all the heavy lifting After the coordinates are converted to simple integers, the math is 
        //    // rather simple to check if the ships start and end coordinates are valid in relation to each other. 
        //    if (endCoordinate - startCoordinate == (shipSize - 1))
        //    {
        //        isValid = true;
        //        return isValid;
        //    }
        //    else if (endCoordinate - startCoordinate == ((shipSize - 1) * 10))
        //    {
        //        isValid = true;
        //        return isValid;
        //    }
        //    else
        //        return isValid;

        //}


    }
}
