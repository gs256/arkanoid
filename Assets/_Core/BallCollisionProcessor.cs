using Arkanoid.Tags;
using UnityEngine;

namespace Arkanoid
{
    public class BallCollisionProcessor
    {
        // TODO: to config
        private const float RandomBounceAngleScattering = 0f;
        private const float RayDistance = 0.06f;

        public void ProcessCollision(Ball ball)
        {
            if (TryGetHit(ball.Bounds, out Collider2D collider, out Vector2 normal))
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
            if (collider == null)
                return false;
            return collider.gameObject.HasTag(ObjectTag.Collidable);
        }

        private bool TryGetHit(Bounds ballBounds, out Collider2D collider, out Vector2 normal)
        {
            RaycastHit2D upHit = Physics2D.Raycast(new Vector2(ballBounds.center.x, ballBounds.max.y), Vector2.up, RayDistance);
            RaycastHit2D rightHit = Physics2D.Raycast(new Vector2(ballBounds.max.x, ballBounds.center.y), Vector2.right, RayDistance);
            RaycastHit2D downHit = Physics2D.Raycast(new Vector2(ballBounds.center.x, ballBounds.min.y), Vector2.down, RayDistance);
            RaycastHit2D leftHit = Physics2D.Raycast(new Vector2(ballBounds.min.x, ballBounds.center.y), Vector2.left, RayDistance);

            if (IsCollidable(upHit.collider))
            {
                normal = Vector2.down;
                collider = upHit.collider;
                return true;
            }

            if (IsCollidable(downHit.collider))
            {
                normal = Vector2.up;
                collider = downHit.collider;
                return true;
            }

            if (IsCollidable(rightHit.collider))
            {
                normal = Vector2.left;
                collider = rightHit.collider;
                return true;
            }

            if (IsCollidable(leftHit.collider))
            {
                normal = Vector2.right;
                collider = leftHit.collider;
                return true;
            }

            normal = Vector2.zero;
            collider = null;
            return false;
        }
    }
}
