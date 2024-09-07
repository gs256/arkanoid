using Arkanoid.Blocks;
using UnityEngine;

namespace Arkanoid
{
    public class BallCollisionProcessor
    {
        public void ProcessCollision(Ball ball, Collision2D collision)
        {
            ProcessBounce(ball, collision);

            if (collision.gameObject.HasComponent<Block>(out Block block))
                block.Hit();
        }

        private void ProcessBounce(Ball ball, Collision2D collision)
        {
            Vector2 normal = collision.contacts[0].normal;
            float mirrorAngle = MathUtils.GetMirrorAngle(ball.Angle, normal);

            if (collision.gameObject.HasComponent<Racket>(out Racket racket))
            {
                if (ShouldIgnoreRacketCollision(ball, normal))
                    return;

                float factor = -1 * (ball.Position.x - racket.Position.x) / racket.Bounds.extents.x;
                factor = Mathf.Clamp(factor, -1f, 1f);
                float steerAngle = MathUtils.Remap(factor, -1, 1, 10, 170);
                ball.Angle = (mirrorAngle + steerAngle) / 2f;
            }
            else
            {
                ball.Angle = mirrorAngle;
            }
        }

        private bool ShouldIgnoreRacketCollision(Ball ball, Vector2 normal)
        {
            return (normal != Vector2.up) || Vector2.Dot(normal, MathUtils.AngleToVector(ball.Angle)) > 0;
        }
    }
}
