using System.Drawing;
using System.Linq;
using Minefield;
using Minefield.Model;
using Moq;
using Xunit;

namespace Tests
{
    public class GameControllerTests
    {
        [Fact]
        public void MoveToWinNoHit()
        {
            var gameController = new GameController();
            foreach(var boardSquare in gameController.GameState.GameBoard.Squares.Where(x => x.HasMine))
                boardSquare.HasMine = false;

            for (var i = 0; i < gameController.GameState.GameBoard.BoardSize.Height; i++)
                gameController.Move(GameController.Movement.Up);
            Assert.True(gameController.GameState.HasWon);
        }

        [Fact]
        public void CannotMoveLeft()
        {
            var gamestate = new Mock<GameState>();
            gamestate.Object.InitDefault();
            var player = new Player();
            var point = new Point(0, 0);
            player.Position = point;
            gamestate.SetupGet(x => x.Player).Returns(player);
            var gameController = new GameController(gamestate.Object);
            gameController.Move(GameController.Movement.Left);
            Assert.True(gameController.PlayerPosition == point);
        }

        [Fact]
        public void CannotMoveRight()
        {
            var gamestate = new Mock<GameState>();
            gamestate.Object.InitDefault();
            var player = new Player();
            var point = new Point(gamestate.Object.GameBoard.BoardSize.Width - 1, 0);
            player.Position = point;
            gamestate.SetupGet(x => x.Player).Returns(player);
            var gameController = new GameController(gamestate.Object);
            gameController.Move(GameController.Movement.Right);
            Assert.True(gameController.PlayerPosition == point);
        }

        [Fact]
        public void CannotMoveDown()
        {
            var gameController = new GameController();
            gameController.Move(GameController.Movement.Down);
            Assert.True(gameController.PlayerPosition.Y == 0);
        }

        [Fact]
        public void HitMineLoseLifeAndMineReduction()
        {
            var gamestate = new Mock<GameState>();
            gamestate.Object.InitDefault();
            gamestate.Object.GameBoard.AddMine(new Point(0, 1));
            var player = new Player();
            var point = new Point(0, 0);
            player.Position = point;
            gamestate.SetupGet(x => x.Player).Returns(player);
            var initialLives = player.Lives;
            var gameController = new GameController(gamestate.Object);
            var originalMineCount = gameController.GameState.GameBoard.Squares.Where(x => x.HasMine).ToList().Count;
            gameController.Move(GameController.Movement.Up);
            Assert.True(gameController.GameState.Player.Lives == initialLives - 1);
            var mineCount = gameController.GameState.GameBoard.Squares.Where(x => x.HasMine).ToList().Count;
            Assert.True(mineCount < originalMineCount);
        }

        [Fact]
        public void HitMineWithOneLifeLeft()
        {
            var gamestate = new Mock<GameState>();
            gamestate.Object.InitDefault();
            gamestate.Object.GameBoard.AddMine(new Point(0, 1));
            var player = new Player();
            var point = new Point(0, 0);
            player.Position = point;
            while (player.Lives != 1)
                player.RemoveLife();
            gamestate.SetupGet(x => x.Player).Returns(player);
            var gameController = new GameController(gamestate.Object);
            var originalMineCount = gameController.GameState.GameBoard.Squares.Where(x => x.HasMine).ToList().Count;
            gameController.Move(GameController.Movement.Up);
            var mineCount = gameController.GameState.GameBoard.Squares.Where(x => x.HasMine).ToList().Count;

            Assert.True(gameController.GameState.Player.Lives == 0);
            Assert.True(mineCount < originalMineCount);
            Assert.True(gameController.GameOver);
        }
    }
}
