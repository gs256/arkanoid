using System;

namespace Arkanoid
{
    public class Player
    {
        // TODO: to config
        private const int DefaultLives = 3;

        public int Lives { get; private set; } = DefaultLives;

        public void DecreaseLives()
        {
            if (Lives <= 0)
                throw new InvalidOperationException("Can't decrease 0 lives");

            Lives -= 1;
        }
    }
}
