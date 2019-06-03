using System.Drawing;
using Minefield.Extensions;

namespace Minefield.Model
{
    public class BoardSquare
    {
        public Point Position;
        public bool HasMine;

        public static string ToDisplayPosition(Point position)
        {
            return string.Format("{0}{1}", position.X.ToChar(), position.Y + 1);
        }
    }
}
