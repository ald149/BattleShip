using System;

namespace BattleShip
{
    public class GameController
    {
        private Board _board;
        private Ship aircraftCarrier;
        private Ship bigBattleship;
        private Ship destroyer;
        private Ship submarine;
        private Ship cruiser;
        private string[] startEndCoordinates;
        private OpponentController opponentController;


        public GameController(Board board)
        {
            _board = board;
            BeginGame();
        }

        public void BeginGame()
        {
            Console.WriteLine("Welcome to Battleship!! ");
            _board.DisplayUserShips(ConsoleColor.Gray);
            _board.DisplayHitsAndMissesOnEnemy();
            //SetupAirCraftCarrier();
            //SetupBigBattleShip();
            //SetupDestroyer();
            //SetupSubmarine();
            //SetupCruiser();
            SetupEnemyBoard();
        }

        private void SetupEnemyBoard()
        {
            opponentController = new OpponentController(_board);
            _board.OpponentBoard = opponentController.PlaceOpponentShips();
        }

        /// <summary>
        /// Takes in and verifies user input and places on board.
        /// </summary>
        private void SetupAirCraftCarrier()
        {
            bool setUpComplete = false;
            string startCoordinate = string.Empty;
            string endCoordinate = string.Empty;
            bool isStartValid = false;
            bool isEndValidFormat = false;
            bool isValidStartAndEnd = false;
            bool isInline = false;
            int shipSize = 5;
            Console.ForegroundColor = ConsoleColor.White;

            TextHandler.printInputInstructions("Aircraft Carrier", "5");

            while (!setUpComplete)
            {
                TextHandler.printStartInput("Aircraft Carrier");
                startCoordinate = Console.ReadLine();
                isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                while (!isStartValid)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    startCoordinate = Console.ReadLine();
                    isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                }

                TextHandler.printEndInput("Aircraft Carrier");
                endCoordinate = Console.ReadLine();
                isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);      
                while (!isEndValidFormat)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    endCoordinate = Console.ReadLine();
                    isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);
                }

                isInline = _board.AreCoordinatesInline(startCoordinate, endCoordinate);
                if (isInline)
                {
                    startEndCoordinates = _board.ReorderCoordinates(startCoordinate, endCoordinate);
                    startCoordinate = startEndCoordinates[0];
                    endCoordinate = startEndCoordinates[1];
                    isValidStartAndEnd = _board.ShipStartAndEndInputsValid(startCoordinate, endCoordinate, shipSize);
                    if (isValidStartAndEnd)
                    {
                        aircraftCarrier = new Ship(startCoordinate, endCoordinate, Ship.ShipName.Aircraft_Carrier);
                        setUpComplete = true;
                    }
                    else
                    {
                        Console.WriteLine("These coordinates do not match the size of 5 places for an Aircraft Carrier.");
                    }
                }
                else
                {
                    Console.WriteLine("These coordinates are not inline with each other, please re-enter coordinates.");
                }

            }

            _board.AddShip(aircraftCarrier.ShipCoordinates);
            _board.DisplayUserShips(ConsoleColor.DarkBlue);
        }

        /// <summary>
        /// Takes in and verifies user input and places on board.
        /// </summary>
        private void SetupBigBattleShip()
        {
            bool setUpComplete = false;
            string startCoordinate = string.Empty;
            string endCoordinate = string.Empty;
            bool isStartValid = false;
            bool isEndValidFormat = false;
            bool isValidStartAndEnd = false;
            bool isInline = false;
            bool shipCorrectSize = false;
            int shipSize = 4;
            Console.ForegroundColor = ConsoleColor.White;

            TextHandler.printInputInstructions("Battleship", "4");

            while (!setUpComplete)
            {
                TextHandler.printStartInput("Battleship");
                startCoordinate = Console.ReadLine();
                isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                while (!isStartValid)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    startCoordinate = Console.ReadLine();
                    isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                }

                TextHandler.printEndInput("Battleship");
                endCoordinate = Console.ReadLine();
                isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);
                while (!isEndValidFormat)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    endCoordinate = Console.ReadLine();
                    isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);
                }

                isInline = _board.AreCoordinatesInline(startCoordinate, endCoordinate);
                if (isInline)
                {
                    startEndCoordinates = _board.ReorderCoordinates(startCoordinate, endCoordinate);
                    startCoordinate = startEndCoordinates[0];
                    endCoordinate = startEndCoordinates[1];
                    isValidStartAndEnd = _board.ShipStartAndEndInputsValid(startCoordinate, endCoordinate, shipSize);
                    if (isValidStartAndEnd)
                    {
                        bigBattleship = new Ship(startCoordinate, endCoordinate, Ship.ShipName.BattleShip);
                        setUpComplete = true;
                        shipCorrectSize = true;
                    }
                    else
                    {
                        Console.WriteLine("These coordinates do not match the size of 4 places for a BattleShip.");
                    }
                }
                else
                {
                    Console.WriteLine("These coordinates are not inline with each other, please re-enter coordinates.");
                }

                // The ship coordinates are inline and the correct size, then check if they overlap.
                if(isInline && shipCorrectSize)
                {
                    setUpComplete = !_board.DoesShipIntersect(bigBattleship);
                }

          
                if(!setUpComplete && shipCorrectSize)
                {
                    Console.WriteLine("These coordinates overlap another ship and are not valid.");
                }
            }

            _board.AddShip(bigBattleship.ShipCoordinates);
            _board.DisplayUserShips(ConsoleColor.DarkGreen);
        }

        /// <summary>
        /// Takes in and verifies user input and places on board.
        /// </summary>
        private void SetupDestroyer()
        {
            bool setUpComplete = false;
            string startCoordinate = string.Empty;
            string endCoordinate = string.Empty;
            bool isStartValid = false;
            bool isEndValidFormat = false;
            bool isValidStartAndEnd = false;
            bool isInline = false;
            int shipSize = 3;
            bool shipCorrectSize = false;
            Console.ForegroundColor = ConsoleColor.White;

            TextHandler.printInputInstructions("Destroyer", "3");

            while (!setUpComplete)
            {
                TextHandler.printStartInput("Destroyer");
                startCoordinate = Console.ReadLine();
                isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                while (!isStartValid)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    startCoordinate = Console.ReadLine();
                    isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                }

                TextHandler.printEndInput("Destroyer");
                endCoordinate = Console.ReadLine();
                isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);
                while (!isEndValidFormat)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    endCoordinate = Console.ReadLine();
                    isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);
                }

                isInline = _board.AreCoordinatesInline(startCoordinate, endCoordinate);
                if (isInline)
                {
                    startEndCoordinates = _board.ReorderCoordinates(startCoordinate, endCoordinate);
                    startCoordinate = startEndCoordinates[0];
                    endCoordinate = startEndCoordinates[1];
                    isValidStartAndEnd = _board.ShipStartAndEndInputsValid(startCoordinate, endCoordinate, shipSize);
                    if (isValidStartAndEnd)
                    {
                        destroyer = new Ship(startCoordinate, endCoordinate, Ship.ShipName.Destroyer);
                        setUpComplete = true;
                        shipCorrectSize = true;
                    }
                    else
                    {
                        Console.WriteLine("These coordinates do not match the size of 3 places for a Destroyer.");
                        
                    }
                }
                else
                {
                    Console.WriteLine("These coordinates are not inline with each other, please re-enter coordinates.");
                }
                // The ship coordinates are inline and the correct size, then check if they overlap.
                if (isInline && shipCorrectSize)
                {
                    setUpComplete = !_board.DoesShipIntersect(destroyer);
                }


                if (!setUpComplete && shipCorrectSize)
                {
                    Console.WriteLine("These coordinates overlap another ship and are not valid.");
                }
            }

            _board.AddShip(destroyer.ShipCoordinates);
            _board.DisplayUserShips(ConsoleColor.DarkMagenta);
        }


        /// <summary>
        /// Takes in and verifies user input and places on board.
        /// </summary>
        private void SetupSubmarine()
        {
            bool setUpComplete = false;
            string startCoordinate = string.Empty;
            string endCoordinate = string.Empty;
            bool isStartValid = false;
            bool isEndValidFormat = false;
            bool isValidStartAndEnd = false;
            bool isInline = false;
            bool shipCorrectSize = false;
            int shipSize = 3;
            Console.ForegroundColor = ConsoleColor.White;

            TextHandler.printInputInstructions("Submarine", "3");

            while (!setUpComplete)
            {
                TextHandler.printStartInput("Submarine");
                startCoordinate = Console.ReadLine();
                isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                while (!isStartValid)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    startCoordinate = Console.ReadLine();
                    isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                }

                TextHandler.printEndInput("Submarine");
                endCoordinate = Console.ReadLine();
                isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);
                while (!isEndValidFormat)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    endCoordinate = Console.ReadLine();
                    isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);
                }

                isInline = _board.AreCoordinatesInline(startCoordinate, endCoordinate);
                if (isInline)
                {
                    startEndCoordinates = _board.ReorderCoordinates(startCoordinate, endCoordinate);
                    startCoordinate = startEndCoordinates[0];
                    endCoordinate = startEndCoordinates[1];
                    isValidStartAndEnd = _board.ShipStartAndEndInputsValid(startCoordinate, endCoordinate, shipSize);
                    if (isValidStartAndEnd)
                    {
                        submarine = new Ship(startCoordinate, endCoordinate, Ship.ShipName.Submarine);
                        setUpComplete = true;
                        shipCorrectSize = true;

                    }
                    else
                    {
                        Console.WriteLine("These coordinates do not match the size of 3 places for a Submarine.");

                    }
                }
                else
                {
                    Console.WriteLine("These coordinates are not inline with each other, please re-enter coordinates.");
                }
                // The ship coordinates are inline and the correct size, then check if they overlap.
                if (isInline && shipCorrectSize)
                {
                    setUpComplete = !_board.DoesShipIntersect(submarine);
                }


                if (!setUpComplete && shipCorrectSize)
                {
                    Console.WriteLine("These coordinates overlap another ship and are not valid.");
                }
            }

            _board.AddShip(submarine.ShipCoordinates);
            _board.DisplayUserShips(ConsoleColor.DarkMagenta);
        }

        /// <summary>
        /// Takes in and verifies user input and places on board.
        /// </summary>
        private void SetupCruiser()
        {
            bool setUpComplete = false;
            string startCoordinate = string.Empty;
            string endCoordinate = string.Empty;
            bool isStartValid = false;
            bool isEndValidFormat = false;
            bool isValidStartAndEnd = false;
            bool isInline = false;
            bool shipCorrectSize = false;
            int shipSize = 2;
            Console.ForegroundColor = ConsoleColor.White;

            TextHandler.printInputInstructions("Cruiser", "2");

            while (!setUpComplete)
            {
                TextHandler.printStartInput("Cruiser");
                startCoordinate = Console.ReadLine();
                isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                while (!isStartValid)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    startCoordinate = Console.ReadLine();
                    isStartValid = _board.IsPlayerInputCorrectFormat(startCoordinate);
                }

                TextHandler.printEndInput("Cruiser");
                endCoordinate = Console.ReadLine();
                isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);
                while (!isEndValidFormat)
                {
                    Console.WriteLine("Invalid input format or placement, please re-enter a coordinate:");
                    endCoordinate = Console.ReadLine();
                    isEndValidFormat = _board.IsPlayerInputCorrectFormat(endCoordinate);
                }

                isInline = _board.AreCoordinatesInline(startCoordinate, endCoordinate);
                if (isInline)
                {
                    startEndCoordinates = _board.ReorderCoordinates(startCoordinate, endCoordinate);
                    startCoordinate = startEndCoordinates[0];
                    endCoordinate = startEndCoordinates[1];
                    isValidStartAndEnd = _board.ShipStartAndEndInputsValid(startCoordinate, endCoordinate, shipSize);
                    if (isValidStartAndEnd)
                    {
                        cruiser = new Ship(startCoordinate, endCoordinate, Ship.ShipName.Cruiser);
                        setUpComplete = true;
                        shipCorrectSize = true;

                    }
                    else
                    {
                        Console.WriteLine("These coordinates do not match the size of 2 places for a Cruiser.");

                    }
                }
                else
                {
                    Console.WriteLine("These coordinates are not inline with each other, please re-enter coordinates.");
                }
                // The ship coordinates are inline and the correct size, then check if they overlap.
                if (isInline && shipCorrectSize)
                {
                    setUpComplete = !_board.DoesShipIntersect(cruiser);

                }


                if (!setUpComplete && shipCorrectSize)
                {
                    Console.WriteLine("These coordinates overlap another ship and are not valid.");
                }
            }

            _board.AddShip(cruiser.ShipCoordinates);
            _board.DisplayUserShips(ConsoleColor.DarkMagenta);
        }




    }
}
