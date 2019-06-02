using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Minefield.Model
{
    public class GameBoard
    {
        const int DEF_WIDTH = 8;
        const int DEF_HEIGHT = 8;
        const int _mineCount = 8;

        public readonly Size BoardSize;

        public GameBoard()
        {
            BoardSize = new Size(DEF_WIDTH, DEF_HEIGHT);
            Setup();
        }

        public List<BoardSquare> Squares { get; private set; }

        public void RemoveMine(Point point)
        {
            Squares.Remove(Squares.First(square => square.Position.Equals(point)));
        }

        void Setup()
        {
            var squareCount = BoardSize.Height * BoardSize.Width;

            if (_mineCount >= squareCount)
                throw new Exception("Mine count exceeds board size.");

            Squares = new List<BoardSquare>();
            List<Point> minePositions = GenerateMinePositions(_mineCount, BoardSize);

            for (var y = 0; y < BoardSize.Height; y++)
            {
                for (var x = 0; x < BoardSize.Width; x++)
                {
                    var boardPosition = new Point(x, y);
                    var boardSquare = new BoardSquare
                    {
                        HasMine = minePositions.Any(minePosition => minePosition.Equals(boardPosition)),
                        Position = boardPosition
                    };
                    Squares.Add(boardSquare);
                    if (boardSquare.HasMine)
                        Console.WriteLine(string.Format("Mine at position : {0}", BoardSquare.ToDisplayPosition(boardPosition)));
                }
            }
        }

        List<Point> GenerateMinePositions(int mineCount, Size size)
        {
            var points = new List<Point>();
            var rnd = new Random();
            for (var i = 0; i < mineCount; i++)
            {
                points.Add(CreateUniquePoint(ref rnd, points, size.Width, size.Height));
            }

            return points;
        }

        Point CreateUniquePoint(ref Random rnd, List<Point> points, int maxWidth, int maxHeight)
        {
            var point = new Point(rnd.Next(0, maxWidth), rnd.Next(0, maxHeight));
            return points.Any(x => x.Equals(point)) ? CreateUniquePoint(ref rnd, points, maxWidth, maxHeight) : point;
        }
    }
}