using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// 1. Check format of input.
/// 2. Reorder coordinates if needed. 
/// 3. Check coordinates on board. 
/// 4. Check if coordinates match ship size.
/// 5. Compute the coordinates
/// 6. Check for overlap placement
/// 7. If all pass, then create the ship object
/// Why have all the different classes for ships just to store a constant ship size int?? Dumb. 
/// </summary>


namespace BattleShip
{
    public class Board
    {
        /**
         * Explanations for players ship board.  
         * C = Covered coordinate
         * O = Open coordinate 
         * Explanations for players shooting board. 
         * H = Hit 
         * M = Miss
         **/

        public enum Letters
        {
            a,
            b,
            c,
            d,
            e,
            f,
            g,
            h,
            i,
            j
        }


        public enum Directions
        {
            up,
            down,
            left,
            right
        }

        private char[,] playerBoard;
        private char[,] playerShootingBoard;
        private char[,] opponentBoard;
        private char[,] opponentShootingBoard;

        public char[,] PlayerBoard
        {
            get { return playerBoard; }
            set { playerBoard = value; }
        }
        public Board()
        {
            playerBoard = new char[10, 10];
            playerShootingBoard = new char[10, 10];
            opponentBoard = new char[10, 10];
            opponentShootingBoard = new char[10, 10];
           
            Initialize_PlayerBoard();
            Initialize_ShootingBoard();
            Initialize_OpponentBoard();
            Initialize_OpponentShootingBoard();

        }

        /// <summary>
        /// Method takes in a coordinate and converts it from string form such as "a1" and returns it as an int such as 01
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public int ConvertCoordinateToInt(string coordinate)
        {

            // Convert coordinate to integer form
            Letters row = (Letters)Enum.Parse(typeof(Letters), coordinate.Substring(0, 1).ToLower());
            int column = int.Parse(coordinate.Substring(1));
            int convertedCoordinate = ((int)row) * 10 + column - 1;

            return convertedCoordinate;
        }

        public void AddShip(List<string> coordinateList)
        {
            foreach (string coordinate in coordinateList)
            {
                int row = coordinate[0] - 'a';
                int col = int.Parse(coordinate.Substring(1)) - 1;
                playerBoard[row, col] = 'C';
            }
        }


        #region Validation Methods

        //public bool IsHit(Ship ship)
        //{
        //    return true;
        //}

        /// <summary>
        /// Tests if user input is valid by checking for inbounds 
        /// Converts all input to lowerCase to avoid errors 
        /// </summary>
        /// <param name="playerShipPositionIn"> the user's inputted coordinate .</param>
        public bool IsPlayerInputOnTheBoard(string playerShipPositionInput)
        {
            // Check if the first character is a valid letter
            if (!Enum.IsDefined(typeof(Letters), playerShipPositionInput.Substring(0, 1).ToLower()))
            {
                return false;
            }

            int column = 0;

            // Parse the input string to get the row and column indices

            Letters row = (Letters)Enum.Parse(typeof(Letters), playerShipPositionInput.Substring(0, 1).ToLower());
            if (playerShipPositionInput.Length > 2)
            {
                column = int.Parse(playerShipPositionInput.Substring(1, playerShipPositionInput.Length - 1)) - 1;
            }
            else
                column = int.Parse(playerShipPositionInput.Substring(1, 1)) - 1;


            if (row >= Letters.a && row <= Letters.j && column >= 0 && column < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Tests to see if ship placement will intersect another existing ship. Checks that ship placement will not overlap with a coordinate currently marked "C" for covered vs 
        /// "O" for open
        /// </summary>
        /// <returns></returns>
        public bool DoesShipIntersect(Ship ship)
        {
            bool intersects = false;

            foreach (string coordinate in ship.ShipCoordinates)
            {
                int col = 0;
                int row = 0;
                
                if (coordinate.Length == 2)
                {
                    col = coordinate[1] - '1';
                    row = coordinate[0] - 'a';
                }
                else if (coordinate.Length == 3)
                {
                    col = 9;
                    row = coordinate[0] - 'a';
                }

                if (playerBoard[row, col] == 'C')
                {
                    intersects = true;
                    return intersects;
                }
                
            }
            return intersects;
        }

        /// <summary>
        /// Validates that the players input is a valid input. Uppercase letters are allowed and accounted for. 
        /// </summary>
        /// <param name="playerShipPositionInput"></param>
        /// <returns></returns>
        public bool IsPlayerInputCorrectFormat(string playerShipPositionInput)
        {
            bool isCorrectFormat = false;
            if (playerShipPositionInput == null)
                return isCorrectFormat;

            // Check if input contains only letters and numbers
            if (!Regex.IsMatch(playerShipPositionInput, @"^[a-jA-J][1-9][0]?$"))
            {
                return false;
            }

            // need to check that first element is a-j and second elementis 1-9 and third element, if it exists must be a zero
            if (!char.IsLetter(playerShipPositionInput[0]))
            {
                return false;
            }

            if (playerShipPositionInput[0] < 'a' || playerShipPositionInput[0] > 'j')
            {
                return false;
            }
            if (!int.TryParse(playerShipPositionInput.Substring(1), out int num))
            {
                return false;
            }
            if (num < 1 || num > 10)
            {
                return false;
            }
            if (playerShipPositionInput.Length == 3 && playerShipPositionInput[2] != '0')
            {
                return false;
            }


            Letters row;
            if (Enum.TryParse(playerShipPositionInput.Substring(0, 1).ToLower(), out row))
            {
                bool onBoard = IsPlayerInputOnTheBoard(playerShipPositionInput);
                if (playerShipPositionInput.Length > 3 || onBoard == false)
                {
                    isCorrectFormat = false;
                }
                else
                {
                    isCorrectFormat = true;
                }
            }
            else
            {
                isCorrectFormat = false;
            }

            return isCorrectFormat;

        }

        /// <summary>
        /// Method tests whether the start and end inputs for the ship are valid given the size of the ship. Requires reorder already performed. 
        /// We are considering that inbounds is already checked by IsPlayerInputOnTheBoard
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public bool ShipStartAndEndInputsValid(string start, string end, int shipSize)
        {
            bool isValid = false;

            int endCoordinate = ConvertCoordinateToInt(end);
            int startCoordinate = ConvertCoordinateToInt(start);

            // These algorithms or checks do all the heavy lifting After the coordinates are converted to simple integers, the math is 
            // rather simple to check if the ships start and end coordinates are valid in relation to each other. 
            if (endCoordinate - startCoordinate == (shipSize - 1))
            {
                isValid = true;
                return isValid;
            }
            else if (endCoordinate - startCoordinate == ((shipSize - 1) * 10))
            {
                isValid = true;
                return isValid;
            }
            else
                return isValid;

        }

        /// <summary>
        /// Takes in start and end coordinates... if they end coordinates are before start, it reverses them
        /// </summary>
        /// <param name="startCoordinates"></param>
        /// <param name="endCoordinates"></param>
        /// <returns></returns>
        public string[] ReorderCoordinates(string startCoordinates, string endCoordinates)
        {
            string[] reorderedCoordinates = new string[2];
            // Split the start and end coordinates into their row and column components
            int startRow = int.Parse(startCoordinates.Substring(1));
            int endRow = int.Parse(endCoordinates.Substring(1));
            char startCol = startCoordinates[0];
            char endCol = endCoordinates[0];

            // Check if the end coordinate comes before the start coordinate horizontally or vertically
            if (endCol < startCol || endRow < startRow)
            {
                // Swap the start and end coordinates
                string temp = startCoordinates;
                startCoordinates = endCoordinates;
                endCoordinates = temp;
            }

            reorderedCoordinates[0] = startCoordinates;
            reorderedCoordinates[1] = endCoordinates;

            return reorderedCoordinates;
        }

        /// <summary>
        /// Method takes in start and end coordinates and verifies they are inline vertically or horizontally.
        /// </summary>
        /// <param name="startCoordinate"></param>
        /// <param name="endCoordinate"></param>
        /// <returns></returns>
        public bool AreCoordinatesInline(string startCoordinate, string endCoordinate)
        {
            // Convert the start and end coordinates to row and column indices
            int startRow = startCoordinate[1] - '1';
            int startCol = startCoordinate[0] - 'a';
            int endRow = endCoordinate[1] - '1';
            int endCol = endCoordinate[0] - 'a';

            // Check if the coordinates are in the same row or column
            return startRow == endRow || startCol == endCol;
        }

        #endregion


        /// <summary>
        /// Displays the user board and shows location of their ships with x and y labels
        /// </summary>
        /// <param name="color"></param>
        public void DisplayUserShips(ConsoleColor color)
        {
            Console.WriteLine(" Player's GameBoard:\n");
            Console.WriteLine(" 1 2 3 4 5 6 7 8 9 10");
            Console.WriteLine(" --------------------");
            for (int i = 0; i < playerBoard.GetLength(0); i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
     
                Console.Write((char)('a' + i));
                Console.Write(" ");
                for (int j = 0; j < playerBoard.GetLength(1); j++)
                {
                    if (playerBoard[i, j] == 'C')
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = color;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }


                    Console.Write(playerBoard[i, j]);
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("C = Covered by Ship.");
            Console.WriteLine("O = Open spot.");
        }

        /// <summary>
        /// Displays board for user to track hits, misses, and open spots
        /// </summary>
        public void DisplayHitsAndMissesOnEnemy()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Player's Hits and Misses:\n");
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10 ");
            Console.WriteLine("  --------------------");
            for (int i = 0; i < playerShootingBoard.GetLength(0); i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("" + (char)('a' + i));
                Console.Write(" ");
                for (int j = 0; j < playerShootingBoard.GetLength(1); j++)
                {
                    if (playerShootingBoard[i, j] == 'H')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (playerShootingBoard[i, j] == 'M')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(playerShootingBoard[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("H = Hit on enemy ship.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("M = Miss on enemy ship");
        }



        /// <summary>
        /// Intiializes players gameBoard to all be open places
        /// </summary>
        private void Initialize_PlayerBoard()
        {
            for (int i = 0; i < playerBoard.GetLength(0); i++)
            {
                for (int j = 0; j < playerBoard.GetLength(1); j++)
                {
                    playerBoard[i, j] = 'O';
                }
            }
        }

        /// <summary>
        /// Initializes players Hits and Misses Board to all open spots
        /// </summary>
        private void Initialize_ShootingBoard()
        {
            for (int i = 0; i < playerShootingBoard.GetLength(0); i++)
            {
                for (int j = 0; j < playerShootingBoard.GetLength(1); j++)
                {
                    playerShootingBoard[i, j] = 'O';
                }
            }
        }

        /// <summary>
        /// Intiializes opponents gameBoard to all be open places
        /// </summary>
        private void Initialize_OpponentBoard()
        {
            for (int i = 0; i < opponentBoard.GetLength(0); i++)
            {
                for (int j = 0; j < opponentBoard.GetLength(1); j++)
                {
                    opponentBoard[i, j] = 'O';
                }
            }
        }

        /// <summary>
        /// Initializes opponents Hits and Misses Board to all open spots
        /// </summary>
        private void Initialize_OpponentShootingBoard()
        {
            for (int i = 0; i < opponentShootingBoard.GetLength(0); i++)
            {
                for (int j = 0; j < opponentShootingBoard.GetLength(1); j++)
                {
                    opponentShootingBoard[i, j] = 'O';
                }
            }
        }

        public void PlaceEnemyShips()
        {


        }

    }
}