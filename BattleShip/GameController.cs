using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class GameController
    {
        private AirCraftCarrier _aircraftCarrier;
        private BigBattleship _battleship;
        private Cruiser _cruiser;
        private Submarine _submarine;
        private Destroyer _destroyer;
        private Board _board;

        public GameController(Board board)
        {
            _board = board;
        }

    }
}
