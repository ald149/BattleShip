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
        [DataRow(true, "a5")]
        [DataRow(false, "(5")]
        [DataRow(false, "a33242")]
        [DataRow(true, "a10")]
        [DataRow(true, "j0")]
        [DataRow(false, "&*(")]

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
        public void Test_UserInput_IsOnBoard(bool expected, string input)
        {
            //Arrange

            //Act
            bool actual = board.IsPlayerInputOnTheBoard(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }


        [DataTestMethod]
        [DataRow(true, "b5", "b9")]
        [DataRow(false, "b5", "b2")]
        [DataRow(true, "a5", "e5")]
        [DataRow(false, "a5", "e3")]
        [DataRow(true, "e5", "a5")]
        [DataRow(true, "i6", "e6")]
        [DataRow(true, "g7", "c7")]
        [DataRow(true, "i9", "i5")]
        [DataRow(true, "a5", "a1")]
        [DataRow(true, "j10", "j6")]
        [DataRow(true, "h10", "h6")]
        [DataRow(true, "g10", "g6")]
        [DataRow(true, "j10", "j6")]
        [DataRow(true, "j10", "j6")]

        public void Test_Ship_Start_And_End_Inputs_Valid_For_AirCraftCarrier(bool expected, string start, string end)
        {
            //Arrange
            AirCraftCarrier airCraftCarrier = new AirCraftCarrier(start, end);

            //Act
            bool isValid = board.ShipStartAndEndInputsValid(airCraftCarrier);

            //Assert
            Assert.AreEqual(expected, isValid);
        }

        /// <summary>
        /// Needs logic added to method, this is junk. 
        /// </summary>
        [DataTestMethod]
        public void Test_Ship_Placement_Does_Not_Overlap()
        {
            //Arrange
            
            AirCraftCarrier airCraftCarrier = new AirCraftCarrier("e3", "e7");

            //Act
            bool isClear = board.DoesShipIntersect(airCraftCarrier);

            //Assert
            Assert.IsFalse(isClear);

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
