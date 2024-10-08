using UnityEngine;

namespace Arkanoid.Ball
{
    public class BallFactory
    {
        private const float RacketPadding = 0.1f;

        private readonly PrefabRepository _prefabRepository;

        public BallFactory(PrefabRepository prefabRepository)
        {
            _prefabRepository = prefabRepository;
        }

        public Ball Create(Field field, Racket.Racket racket, BallCollisionProcessor collisionProcessor)
        {
            Ball prefab = _prefabRepository.Ball;
            Ball ball = Object.Instantiate(prefab, field.transform);

            ball.transform.position = new Vector2(
                field.Bounds.center.x,
                racket.transform.position.y + racket.Bounds.extents.y + ball.Bounds.extents.y + RacketPadding);

            ball.Initialize(collisionProcessor);
            return ball;
        }
    }
}
