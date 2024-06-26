using BattleShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipTests
{
    [TestClass]
    public class BoardTests
    {
        private Board board;

        [TestInitialize]
        public void TestInit()
        {
            board = new Board();
        }

        [DataTestMethod]
        [DataRow(false, "b5", "b9")]
        [DataRow(false, "b5", "b2")]
        [DataRow(true, "e5", "e9")]
        [DataRow(true, "a1", "a4")]
        [DataRow(true, "a5", "e5")]
        public void Test_Ship_Placement_Does_Not_Intersect_Aircraft_Carrier(bool expectedIntersects, string start, string end)
        {
            //Arrange
            Ship airCraftCarrier = new Ship(start, end, Ship.ShipName.Aircraft_Carrier);

            //Act
            bool actualIntersects = board.DoesShipIntersect(airCraftCarrier);

            //Assert
            Assert.AreEqual(expectedIntersects, actualIntersects);

        }


        [DataTestMethod]
        [DataRow(false, "b5", "b8")]
        [DataRow(false, "b5", "b2")]
        [DataRow(true, "e5", "e8")]
        [DataRow(true, "a1", "a3")]
        [DataRow(true, "a5", "d5")]
        public void Test_Ship_Placement_Does_Not_Intersect_Big_BattleShip(bool expectedIntersects, string start, string end)
        {
            //Arrange
            var bigBattleship = new Ship(start, end, Ship.ShipName.Aircraft_Carrier);

            //Act
            bool actualIntersects = board.DoesShipIntersect(bigBattleship);

            //Assert
            Assert.AreEqual(expectedIntersects, actualIntersects);

        }

        [DataTestMethod]
        [DataRow("a1", "c3", false)]
        [DataRow("c8", "f3", false)]
        [DataRow("c8", "c4", true)] 
        [DataRow("a4", "e4", true)]
        public void BoardTests_Are_Coordinates_Inline(string startCoordinate, string endCoordinate, bool expectedInline)
        {
            //Arrange

            //Act
            bool actualInline = board.AreCoordinatesInline(startCoordinate, endCoordinate);

            //Assert
            Assert.AreEqual(expectedInline, actualInline);

        }


        [DataTestMethod]
        [DataRow(false, "b5", "b7")]
        [DataRow(false, "b4", "b2")]
        [DataRow(true, "e6", "e8")]
        [DataRow(true, "a1", "a3")]
        [DataRow(true, "a5", "c5")]
        public void Test_Ship_Placement_Does_Not_Intersect_Destroyer(bool expectedIntersects, string start, string end)
        {
            //Arrange
            var destroyer = new Ship(start, end, Ship.ShipName.Aircraft_Carrier);

            //Act
            bool actualIntersects = board.DoesShipIntersect(destroyer);

            //Assert
            Assert.AreEqual(expectedIntersects, actualIntersects);

        }


        [DataTestMethod]
        [DataRow(true, "a5")]
        [DataRow(false, "(5")]
        [DataRow(false, "a33242")]
        [DataRow(true, "a10")]
        [DataRow(false, "j0")]
        [DataRow(false, "&*(")]
        [DataRow(false, "x5td")]
        [DataRow(false, "a0")]
        [DataRow(false, "c0")]
        [DataRow(false, "c")]
        [DataRow(false, "r10")]

        public void Test_IsPlayerInputInCorrectFormatMethod(bool expected, string playerInput)
        {
            //Arrange

            //Act
            var actual = board.IsPlayerInputCorrectFormat(playerInput);

            //Assert
            Assert.AreEqual(expected, actual);
        }


        [DataTestMethod]
        [DataRow(false, "x4")]
        [DataRow(false, "e2352515")]
        [DataRow(true, "j10")]
        [DataRow(true, "a1")]
        [DataRow(false, "j0")]
        [DataRow(false, "a0")]
        [DataRow(true, "a10")]
        public void Test_UserInput_IsOnBoard(bool expected, string input)
        {
            //Arrange

            //Act
            bool actual = board.IsPlayerInputOnTheBoard(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// This method requires that the reordering has been performed already. 
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        [DataTestMethod]
        [DataRow(true, "b5", "b9")]
        [DataRow(false, "b5", "b2")]
        [DataRow(true, "a5", "e5")]
        [DataRow(false, "a5", "e3")]
        public void Test_Ship_Start_And_End_Inputs_Valid_For_AirCraftCarrier(bool expected, string start, string end)
        {
            //Arrange
            int shipSize = 5;

            //Act
            bool isValid = board.ShipStartAndEndInputsValid(start, end, shipSize);

            //Assert
            Assert.AreEqual(expected, isValid);
        }

        [DataTestMethod]
        [DataRow(true, "b5", "b8")]
        [DataRow(false, "b4", "b2")]
        [DataRow(true, "a5", "d5")]
        [DataRow(false, "a5", "e3")]
        [DataRow(false, "c3", "j7")]
        public void Test_Ship_Start_And_End_Inputs_Valid_For_BigBattleship(bool expected, string start, string end)
        {
            //Arrange
            int shipSize = 4;

            //Act
            bool isValid = board.ShipStartAndEndInputsValid(start, end, shipSize);

            //Assert
            Assert.AreEqual(expected, isValid);
        }

        [DataTestMethod]
        [DataRow( "b6", 15)]
        [DataRow("h5", 74)]
        [DataRow("i7", 86)]
        [DataRow("g10", 69)]
        [DataRow("j10", 99)]
        [DataRow("a1", 0)]
        [DataRow("b10", 19)]
        public void Test_Coordinate_To_Int_Conversion_Method(string coordinate, int expected)
        {
            //Arrange

            //Act
            int actual = board.ConvertCoordinateToInt(coordinate);

            //Assert
            Assert.AreEqual(expected, actual);

        }

        private char [,] Player_ShootingBoard_For_Testing()
        {
            char[,] dummyBoard = new char[10, 10];


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    dummyBoard[i, j] = 'o';
                }
            }

            dummyBoard[3, 4] = 'x';
            dummyBoard[4, 4] = 'x';
            dummyBoard[5, 4] = 'x';
            dummyBoard[6, 4] = 'x';
            return dummyBoard;
        }

    }
}
