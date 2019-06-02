using System.Drawing;
using System.Linq;
using System.Text;
using Minefield.Model;

namespace Minefield
{
    public class GameController
    {
        readonly GameState _gameState;

        public GameController()
        {
            _gameState = new GameState();
        }

        public bool GameOver => _gameState.GameOver;
        public int Score => _gameState.Score;
        public Point PlayerPosition => _gameState.Player.Position;

        public string Move(Movement movement)
        {
            var message = new StringBuilder();
            bool hit = false;
            bool valid = false;
            var point = CalculateNewPosition(movement);

            if (IsValidMove(point))
            {
                valid = true;
                _gameState.Moves++;
                _gameState.Player.Position = point;
            }

            if (_gameState.GameBoard.Squares.Any(square => square.Position.Equals(point) && square.HasMine))
            {
                hit = true;
                _gameState.Player.RemoveLife();
                _gameState.GameBoard.RemoveMine(point);
            }

            if (!valid)
            {
                message.AppendLine("Cannot move that way");
                return message.ToString();
            }

            message.AppendLine(string.Format("Moved to: {0}", BoardSquare.ToDisplayPosition(_gameState.Player.Position)));

            // valid move but hit 
            if (hit)
            {
                _gameState.Score--;
                message.AppendLine("Mine Hit!");
                message.AppendLine(string.Format("You have {0} lives left", _gameState.Player.Lives));
            }
            // valid move no hit increment score
            else
                _gameState.Score++;

            // check if player has reached the top of the board
            if (_gameState.HasWon)
            {
                message.AppendLine();
                message.AppendLine("You Win!");
            }

            return message.ToString();
        }

        Point CalculateNewPosition(Movement movement)
        {
            var x = _gameState.Player.Position.X;
            var y = _gameState.Player.Position.Y;
            switch (movement)
            {
                case Movement.Up:
                    y = _gameState.Player.Position.Y + 1;
                    break;
                case Movement.Down:
                    y = _gameState.Player.Position.Y - 1;
                    break;
                case Movement.Left:
                    x = _gameState.Player.Position.X - 1;
                    break;
                case Movement.Right:
                    x = _gameState.Player.Position.X + 1;
                    break;
            }

            return new Point(x, y);
        }

        bool IsValidMove(Point point)
        {
            return point.X > -1
                && point.X < _gameState.GameBoard.BoardSize.Width
                && point.Y > -1
                && point.Y <= _gameState.GameBoard.BoardSize.Height;
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
