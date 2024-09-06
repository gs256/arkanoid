using System;
using UnityEngine;

namespace Arkanoid
{
    public class Block : MonoBehaviour
    {
        // TODO: to config
        private const int DefaultPoints = 10;

        public event Action<Block> Destroyed;

        public int Points => DefaultPoints;

        public void Hit()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
