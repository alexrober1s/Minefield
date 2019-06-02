using System.Drawing;

namespace Minefield.Model
{
    public class GameState
    {
        public GameBoard GameBoard { get; }

        public GameState()
        {
            GameBoard = new GameBoard();
            Player = new Player();
            Player.Position = new Point((GameBoard.BoardSize.Width - 1) / 2, 0); // center bottom
        }

        public int Moves { get; set; }
        public int Score { get; set; }
        public Player Player { get; }
        public bool HasWon => Player.Position.Y == GameBoard.BoardSize.Height - 1;
        public bool GameOver => !Player.IsAlive || HasWon;
    }
}
