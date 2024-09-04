using UnityEngine;

namespace Arkanoid
{
    public class Ball : MonoBehaviour
    {
        [field: SerializeField]
        public float Speed { get; private set; }

        [field: SerializeField]
        public float Angle { get; set; }

        private Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private BallCollisionProcessor _collisionProcessor;

        public void Initialize(BallCollisionProcessor collisionProcessor)
        {
            _collisionProcessor = collisionProcessor;
        }

        public void UpdatePosition(float deltaTime)
        {
            Position += MathUtils.AngleToVector(Angle) * Speed * deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _collisionProcessor.ProcessCollision(this, other);
        }
    }
}
