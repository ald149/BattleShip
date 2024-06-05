using System;
using System.Text.RegularExpressions;

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
        private char[,] shootingBoard;

        public char[,] PlayerBoard
        {
            get { return playerBoard; }
            set { playerBoard = value; }
        }
        public Board()
        {
            playerBoard = new char[10, 10];
            shootingBoard = new char[10, 10];
            Initialize_PlayerBoard();
            Initialize_ShootingBoard();
            playerBoard[0, 0] = 'C';  // a1
            playerBoard[0, 1] = 'C';  // a2 
            playerBoard[4, 7] = 'C';
            playerBoard[2, 4] = 'C';
            playerBoard[3, 4] = 'C';
            playerBoard[4, 4] = 'C';
            playerBoard[5, 4] = 'C';
            shootingBoard[5, 3] = 'H';
            shootingBoard[3, 4] = 'M';

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


        #region Validation Method

        ///***********************  this is our algorithm for our checks of user input and beginning the game. 
        //ask the user to enter a start position and whether they want to go up, down, left, right, then check the validity of those options. 
        // first check if the input is a correct length and format. 
        // next we check if the coordinate is on the board
        // finally we run the validation for the start and end points. They are obviously now both on the board, we must only at this point determine proper length and orientation. 
        // now we ensure the coordinates do not cover any that are already taken --- need a method that lays out all the coordinates covered and compares to current user board. 
        // next we will update the playerboard to include the new ship. 

        public bool IsHit(Ship ship)
        {
            return true;
        }

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
        /// Tests to see if ship placement will intersect another existing ship. Checks that ship placement will not overlap with a coordinate currently marked "C" for covered vs "O"
        /// for open
        /// </summary>
        /// <returns></returns>
        public bool DoesShipIntersect(Ship ship)
        {


            return false;
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
            if (!Regex.IsMatch(playerShipPositionInput, @"^[a-zA-Z0-9]+$"))
            {
                return false;
            }

            Letters row = (Letters)Enum.Parse(typeof(Letters), playerShipPositionInput.Substring(0, 1).ToLower());

            if (playerShipPositionInput.Length > 3 || !Enum.IsDefined(typeof(Letters), playerShipPositionInput.Substring(0, 1).ToLower()))
            {
                isCorrectFormat = false;
                return isCorrectFormat;
            }
            else
                isCorrectFormat = true;

            return isCorrectFormat;

        }

        /// <summary>
        /// Method tests whether the start and end inputs for the ship are valid given the size of the ship and the open/closed spots on the board. 
        /// We are considering that inbounds is already checked by IsPlayerInputOnTheBoard
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public bool ShipStartAndEndInputsValid(Ship ship)
        {
            bool isValid = false;

            int endCoordinate = ConvertCoordinateToInt(ship.End);
            int startCoordinate = ConvertCoordinateToInt(ship.Start);

            // These algorithms or checks do all the heavy lifting After the coordinates are converted to simple integers, the math is 
            // rather simple to check if the ships start and end coordinates are valid in relation to each other. 
            if (endCoordinate - startCoordinate == (ship.ShipSize - 1) || startCoordinate - endCoordinate == (ship.ShipSize - 1))
            {
                isValid = true;
                return isValid;
            }
            else if (endCoordinate - startCoordinate == ((ship.ShipSize - 1) * 10) || (startCoordinate - endCoordinate == (ship.ShipSize -1) * 10) )
            {
                isValid = true;
                return isValid;
            }
            else
                return isValid;

        }
        #endregion



        /// <summary>
        /// Displays the user board and shows location of their ships with x and y labels
        /// </summary>
        public void DisplayUserShips()
        {
            Console.WriteLine("Player's GameBoard:\n");
            Console.WriteLine("  1 2 3 4 5 6 7 8 ");
            Console.WriteLine("  ---------------");
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
                        Console.BackgroundColor = ConsoleColor.Gray;
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
        }

        /// <summary>
        /// Displays board for user to track hits, misses, and open spots
        /// </summary>
        public void DisplayHitsAndMissesOnEnemy()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPlayer's Hits and Misses:\n");
            Console.WriteLine("  1 2 3 4 5 6 7 8 ");
            Console.WriteLine("  ---------------");
            for (int i = 0; i < shootingBoard.GetLength(0); i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write((char)('a' + i));
                Console.Write(" ");
                for (int j = 0; j < shootingBoard.GetLength(1); j++)
                {
                    if (shootingBoard[i, j] == 'H')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (shootingBoard[i, j] == 'M')
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(shootingBoard[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }



        /// <summary>
        /// Intiializes players gameBoard to all be open places
        /// </summary>
        public void Initialize_PlayerBoard()
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
        /// Initializes Hits and Misses Board to all open spots
        /// </summary>
        public void Initialize_ShootingBoard()
        {
            for (int i = 0; i < shootingBoard.GetLength(0); i++)
            {
                for (int j = 0; j < shootingBoard.GetLength(1); j++)
                {
                    shootingBoard[i, j] = 'O';
                }
            }
        }
    }
}