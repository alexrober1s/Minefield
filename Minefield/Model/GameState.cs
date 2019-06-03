using System.Drawing;

namespace Minefield.Model
{
    public class GameState
    {
        public GameBoard GameBoard { get; set; }

        public GameState InitDefault()
        {
            GameBoard = new GameBoard();
            Player = new Player
            {
                Position = new Point((GameBoard.BoardSize.Width - 1) / 2, 0) // center bottom
            };
            return this;
        }

        public int Moves { get; set; }
        public int Score { get; set; }
        public virtual Player Player { get; set; }
        public bool HasWon => Player.Position.Y == GameBoard.BoardSize.Height;
        public bool GameOver => !Player.IsAlive || HasWon;
    }
}
