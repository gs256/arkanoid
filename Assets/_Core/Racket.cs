using UnityEngine;

namespace Arkanoid
{
    public class Racket : MonoBehaviour
    {
        [field: SerializeField]
        public float Speed { get; private set; }

        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Bounds Bounds => _collider.bounds;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private BoxCollider2D _collider;
    }
}
