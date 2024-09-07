using Arkanoid.Tags;
using UnityEngine;

namespace Arkanoid
{
    public class BallCollisionProcessor
    {
        // TODO: to config
        private const float RandomBounceAngleScattering = 0f;
        private const float RayExtraDistance = 0.02f;

        public void ProcessCollision(Ball ball)
        {
            if (TryGetHit(ball, out Collider2D collider, out Vector2 normal))
            {
                ProcessBounce(ball, normal);
                if (collider.gameObject.HasComponent<Block>(out Block block))
                    block.Hit();
            }
        }

        private void ProcessBounce(Ball ball, Vector2 normal)
        {
            if (Vector2.Dot(normal, MathUtils.AngleToVector(ball.Angle)) > 0)
                return;

            float mirrorAngle = MathUtils.GetMirrorAngle(ball.Angle, normal);
            float scattering = Random.Range(-RandomBounceAngleScattering, RandomBounceAngleScattering);

            ball.Angle = mirrorAngle + scattering;
        }

        private bool IsCollidable(Collider2D collider)
        {
            return collider?.gameObject.HasTag(ObjectTag.Collidable) ?? false;
        }

        private bool IsRacket(Collider2D collider)
        {
            return collider?.gameObject.HasTag(ObjectTag.Racket) ?? false;
        }

        private bool TryGetHit(Ball ball, out Collider2D collider, out Vector2 normal)
        {
            Bounds ballBounds = ball.Bounds;
            RaycastHit2D upHit = Physics2D.Raycast(ball.Position, Vector2.up, ballBounds.extents.y + RayExtraDistance);
            RaycastHit2D rightHit = Physics2D.Raycast(ball.Position, Vector2.right, ballBounds.extents.x + RayExtraDistance);
            RaycastHit2D downHit = Physics2D.Raycast(ball.Position, Vector2.down, ballBounds.extents.y + RayExtraDistance);
            RaycastHit2D leftHit = Physics2D.Raycast(ball.Position, Vector2.left, ballBounds.extents.x + RayExtraDistance);
            normal = Vector2.zero;
            collider = null;

            if (IsCollidable(upHit.collider))
            {
                normal += Vector2.down;
                collider = upHit.collider;
            }

            if (IsCollidable(downHit.collider))
            {
                normal += Vector2.up;
                collider = downHit.collider;
            }

            if (IsCollidable(rightHit.collider))
            {
                normal += Vector2.left;
                collider = rightHit.collider;
            }

            if (IsCollidable(leftHit.collider))
            {
                normal += Vector2.right;
                collider = leftHit.collider;
            }

            if (collider == null)
                return false;

            normal.Normalize();
            return true;
        }
    }
}
