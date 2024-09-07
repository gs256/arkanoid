using UnityEngine;

namespace Arkanoid
{
    public class Ball : MonoBehaviour
    {
        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public float Angle { get; set; }

        public Vector2 Position
        {
            get => _rigidbody.position;
            set => _rigidbody.MovePosition(value);
        }

        public Bounds Bounds => _collider.bounds;

        [SerializeField]
        private BoxCollider2D _collider;

        [SerializeField]
        private Rigidbody2D _rigidbody;


        public void UpdatePosition(float deltaTime)
        {
            Position += MathUtils.AngleToVector(Angle) * Speed * deltaTime;
        }

        public void Follow(Transform target)
        {
            float yOffset = transform.position.y - target.position.y;
            Position = new Vector2(target.position.x, target.position.y + yOffset);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
