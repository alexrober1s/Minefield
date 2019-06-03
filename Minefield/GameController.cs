using System.Drawing;
using System.Linq;
using System.Text;
using Minefield.Model;

namespace Minefield
{
    //TODO: put strings into a resx so we can auto test output
    public class GameController
    {

        public GameController() : this(new GameState().InitDefault()) {}

        // Added the gamestate arg as in future could be able to load a saved game
        public GameController(GameState gameState)
        {
            GameState = gameState;
        }

        public GameState GameState { get; set; } 
        public bool GameOver => GameState.GameOver;
        public int Score => GameState.Score;
        public Point PlayerPosition => GameState.Player.Position;

        public string Move(Movement movement)
        {
            var message = new StringBuilder();
            bool hit = false;
            bool valid = false;
            var point = CalculateNewPosition(movement);

            if (IsValidMove(point))
            {
                valid = true;
                GameState.Moves++;
                GameState.Player.Position = point;
            }

            if (GameState.GameBoard.Squares.Any(square => square.Position.Equals(point) && square.HasMine))
            {
                hit = true;
                GameState.Player.RemoveLife();
                GameState.GameBoard.RemoveMine(point);
            }

            if (!valid)
            {
                message.AppendLine("Cannot move that way");
                return message.ToString();
            }

            message.AppendLine(string.Format("Moved to: {0}", BoardSquare.ToDisplayPosition(GameState.Player.Position)));

            // valid move but hit 
            if (hit)
            {
                GameState.Score--;
                message.AppendLine("Mine Hit!");
            }
            // valid move no hit increment score
            else
                GameState.Score++;

            message.AppendLine(string.Format("Lives: {0}", GameState.Player.Lives));
            message.AppendLine(string.Format("Moves: {0}", GameState.Moves));

            // check if player has reached the top of the board
            if (GameState.HasWon)
            {
                message.AppendLine();
                message.AppendLine("You Win!");
            }

            return message.ToString();
        }

        Point CalculateNewPosition(Movement movement)
        {
            var x = GameState.Player.Position.X;
            var y = GameState.Player.Position.Y;
            switch (movement)
            {
                case Movement.Up:
                    y = GameState.Player.Position.Y + 1;
                    break;
                case Movement.Down:
                    y = GameState.Player.Position.Y - 1;
                    break;
                case Movement.Left:
                    x = GameState.Player.Position.X - 1;
                    break;
                case Movement.Right:
                    x = GameState.Player.Position.X + 1;
                    break;
            }

            return new Point(x, y);
        }

        bool IsValidMove(Point point)
        {
            return point.X > -1
                && point.X < GameState.GameBoard.BoardSize.Width
                && point.Y > -1
                && point.Y <= GameState.GameBoard.BoardSize.Height;
        }

        public enum Movement
        {
            Up,
            Down,
            Left,
            Right
        }
    }
}
