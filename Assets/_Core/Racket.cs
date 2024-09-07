using UnityEngine;

namespace Arkanoid
{
    public class Racket : MonoBehaviour
    {
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
