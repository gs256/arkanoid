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
            get => transform.position;
            set => transform.position = value;
        }

        public Bounds Bounds => _collider.bounds;

        [SerializeField]
        private BoxCollider2D _collider;

        private BallCollisionProcessor _collisionProcessor;

        public void Initialize(BallCollisionProcessor collisionProcessor)
        {
            _collisionProcessor = collisionProcessor;
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
            _collisionProcessor.ProcessCollision(this, other);
        }
    }
}
