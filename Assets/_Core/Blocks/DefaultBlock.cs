using System;

namespace Arkanoid.Blocks
{
    public class DefaultBlock : Block
    {
        public override event Action<Block> Destroyed;
        public override int Points => 10;

        public override void Hit()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
