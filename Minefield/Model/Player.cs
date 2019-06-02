using System.Drawing;

namespace Minefield.Model
{
    public class Player
    {
        const int DEF_LIVES = 5;

        public int Lives { get; private set; } = 5;

        public Point Position { get; set; }

        public bool IsAlive => Lives > 0;

        public Player(int lives = DEF_LIVES)
        {
            Lives = lives;
        }

        public void RemoveLife()
        {
            Lives--;
        }
    }
}
