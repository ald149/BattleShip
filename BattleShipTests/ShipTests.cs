using BattleShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipTests
{
    [TestClass]
    public class ShipTests
    {
        private string[] startEndCoordinates;
        private Board board;

        [TestInitialize]
        public void TestInit()
        {
            board = new Board();
        }


        /// <summary>
        /// Test scenario where ship is placed horizontally. Figure out how to make this method take many inputs for testing edge cases. --- needs to reorder 1st
        /// </summary>
        [DataTestMethod]
        [DataRow("A10", "A6", new string[] { "a6", "a7", "a8", "a9", "a10" })]
        public void Test_ComputeCoordinates_AirCraftCarrier(string start, string end, string[] expectedCoordinatesArray)
        {
            //Arrange
            startEndCoordinates =  board.ReorderCoordinates(start, end);
            start = startEndCoordinates[0];
            end = startEndCoordinates[1];
            List<string> expectedCoordinates = expectedCoordinatesArray.ToList();
            Ship airCraftCarrier = new Ship(start, end, Ship.ShipName.Aircraft_Carrier);

            //Act

            var actualCoordinates = airCraftCarrier.ShipCoordinates;

            //Assert
            CollectionAssert.AreEqual(expectedCoordinates, actualCoordinates);

        }

        /// <summary>
        /// Test scenario where ship is placed horizontally. Figure out how to make this method take many inputs for testing edge cases. 
        /// </summary>
        [DataTestMethod]
        [DataRow("B10", "B7", new string[] { "b7", "b8", "b9", "b10" })]
        public void Test_ComputeCoordinates_BigBattleShip(string start, string end, string[] expectedCoordinatesArray)
        {
            //Arrange
            startEndCoordinates = board.ReorderCoordinates(start, end);
            start = startEndCoordinates[0];
            end = startEndCoordinates[1];
            List<string> expectedCoordinates = expectedCoordinatesArray.ToList();
            Ship bigBattleship = new Ship(start, end, Ship.ShipName.BattleShip);

            //Act
            var actualCoordinates = bigBattleship.ComputeCoorindates();

            //Assert
            CollectionAssert.AreEqual(expectedCoordinates, actualCoordinates);

        }

    }
}
