using BattleShip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipTests
{
    [TestClass]
    public class OpponentControllerUnitTests
    {
        private Board board;

        [TestInitialize]
        public void TestInit()
        {
            board = new Board();
        }



        [DataTestMethod]
        [DataRow(3, 4, -1)] // Aircraft carrier -- offset of 4
        [DataRow(8, 4, 4)]
        [DataRow(10, 4, -1)]
        [DataRow(11, 4, -1)]
        [DataRow(12, 4, -1)]
        [DataRow(13, 4, -1)]
        [DataRow(14, 4, 10)]
        [DataRow(24, 4, 20)]
        [DataRow(23, 4, -1)]
        [DataRow(34, 4, 30)]
        [DataRow(33, 4, -1)]
        [DataRow(94, 4, 90)]
        [DataRow(93, 4, -1)]
        public void Test_OpponentController_GetWestCoordinate(int startCoordinate, int offset, int expected)
        {
            //Arrange
            var opponentController = new OpponentController(board);

            //Act
            int westCoordinate = opponentController.GetWestCoordinate(startCoordinate, offset);

            //Assert
            Assert.AreEqual(expected, westCoordinate);


        }

        [DataTestMethod]
        [DataRow(31, 4, -1)] // Aircraft carrier -- offset of 4
        [DataRow(44, 4, 4)]
        [DataRow(0, 4, -1)]
        [DataRow(21, 4, -1)]
        [DataRow(12, 4, -1)]
        [DataRow(43, 4, 3)]
        [DataRow(54, 4, 14)]
        [DataRow(64, 4, 24)]
        [DataRow(73, 4, 33)]
        [DataRow(49, 4, 9)]
        [DataRow(99, 4, 59)]
        [DataRow(4, 4, -1)]
        [DataRow(83, 4, 43)]
        public void Test_OpponentController_GetNorthCoordinate(int startCoordinate, int offset, int expected)
        {
            //Arrange
            var opponentController = new OpponentController(board);

            //Act
            int westCoordinate = opponentController.GetNorthCoordinate(startCoordinate, offset);

            //Assert
            Assert.AreEqual(expected, westCoordinate);


        }

    }
}
