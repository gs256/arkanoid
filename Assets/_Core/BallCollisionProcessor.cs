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
                {
                    Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
                    return;
                }

                float steerFactor = -1 * (ball.Position.x - racket.Position.x) / racket.Bounds.extents.x;
                steerFactor = Mathf.Clamp(steerFactor, -1f, 1f);
                float steerAngle = MathUtils.Remap(steerFactor, -1, 1, 10, 170);
                ball.Angle = (mirrorAngle + steerAngle) / 2f;
            }
            else
            {
                ball.Angle = mirrorAngle;
            }
        }

        private bool ShouldIgnoreRacketCollision(Ball ball, Vector2 normal)
        {
            bool collidedWithSide = normal != Vector2.up;
            bool movingUpwards = Vector2.Dot(normal, MathUtils.AngleToVector(ball.Angle)) > 0;
            return collidedWithSide || movingUpwards;
        }
    }
}
