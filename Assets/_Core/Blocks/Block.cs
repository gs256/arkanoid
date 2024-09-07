using System;
using UnityEngine;

namespace Arkanoid.Blocks
{
    public abstract class Block : MonoBehaviour
    {
        public abstract event Action<Block> Destroyed;
        public abstract int Points { get; }
        public abstract void Hit();
    }
}
