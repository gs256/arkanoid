using Arkanoid.Common;
using UnityEngine;

namespace Arkanoid.Ball
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
        private Collider2D _collider;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        private BallCollisionProcessor _ballCollisionProcessor;

        public void Initialize(BallCollisionProcessor ballCollisionProcessor)
        {
            _ballCollisionProcessor = ballCollisionProcessor;
        }

        public void UpdatePosition(float deltaTime)
        {
            Position += MathUtils.AngleToVector(Angle) * Speed * deltaTime;
        }

        public void Follow(Transform target)
        {
            float yOffset = transform.position.y - target.position.y;
            Position = new Vector2(target.position.x, target.position.y + yOffset);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _ballCollisionProcessor.ProcessCollision(this, other);
        }
    }
}
