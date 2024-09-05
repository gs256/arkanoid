using System;
using UnityEngine;

namespace Arkanoid
{
    public class Block : MonoBehaviour
    {
        public event Action<Block> Destroyed;

        public void Hit()
        {
            Destroy(gameObject);
            Destroyed?.Invoke(this);
        }
    }
}
