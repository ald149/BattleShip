

namespace BattleShip
{
    public class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.DisplayUserShips();
            board.DisplayHitsAndMissesOnEnemy();

        }
    }
}
