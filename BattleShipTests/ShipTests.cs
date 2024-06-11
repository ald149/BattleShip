using BattleShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipTests
{
    [TestClass]
    public class ShipTests
    {
  
        [TestInitialize]
        public void TestInit()
        {
        }


        /// <summary>
        /// Test scenario where ship is placed horizontally. Figure out how to make this method take many inputs for testing edge cases. 
        /// </summary>
        [DataTestMethod]
        [DataRow("A10", "A6", new string[] { "a6", "a7", "a8", "a9", "a10" })]
        public void Test_ComputeCoordinates_AirCraftCarrier(string start, string end, string[] expectedCoordinatesArray)
        {
            //Arrange
            List<string> expectedCoordinates = expectedCoordinatesArray.ToList();
            AirCraftCarrier airCraftCarrier = new AirCraftCarrier(start, end);

            //Act
            var actualCoordinates = airCraftCarrier.ComputeCoorindates();

            //Assert
            CollectionAssert.AreEqual(expectedCoordinates, actualCoordinates);

        }

        [DataTestMethod]    
        [DataRow("a7", "a3")]
        [DataRow("j7", "j3")]
        [DataRow("F5", "B5")]
        [DataRow("J8", "F8")]
        public void Test_Reorder_Coordinates_AirCraft_Carrier(string start, string end) 
        { 
            //Arrange
            var ship = new AirCraftCarrier(start, end);

            //Act
            ship.ReorderCoordinates();

            //Assert
            Assert.AreEqual(ship.End, start.ToLower());
            Assert.AreEqual(ship.Start, end.ToLower());
        }

    }
}
