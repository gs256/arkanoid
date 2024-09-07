using Arkanoid.Tags;
using UnityEngine;

namespace Arkanoid
{
    public class BallCollisionProcessor
    {
        private const float RayExtraDistance = 0.02f;
        private const float AngleEps = 0.01f;

        public void ProcessCollision(Ball ball)
        {
            if (TryGetHit(ball, out Collider2D collider, out Vector2 normal))
            {
                ProcessBounce(ball, collider, normal);

                if (collider.gameObject.HasComponent<Block>(out Block block))
                    block.Hit();
            }
        }

        private void ProcessBounce(Ball ball, Collider2D collider, Vector2 normal)
        {
            float mirrorAngle = MathUtils.GetMirrorAngle(ball.Angle, normal);

            if (collider.gameObject.HasComponent<Racket>(out Racket racket))
            {
                if (normal != Vector2.up)
                    return;

                if (Vector2.Dot(normal, MathUtils.AngleToVector(ball.Angle)) > 0)
                    return;

                float factor = -1 * (ball.Position.x - racket.Position.x) / racket.Bounds.extents.x;
                factor = Mathf.Clamp(factor, -1f, 1f);
                float steerAngle = MathUtils.Remap(factor, -1, 1, 10, 170);
                ball.Angle = (mirrorAngle + steerAngle) / 2f;
            }
            else
            {
                if (IsBlock(collider) && Mathf.Abs(mirrorAngle - ball.Angle) < AngleEps)
                    ball.Angle = MathUtils.VectorToAngle(normal - MathUtils.AngleToVector(ball.Angle));
                else
                    ball.Angle = mirrorAngle;
            }
        }

        private bool IsCollidable(Collider2D collider)
        {
            return collider?.gameObject.HasTag(ObjectTag.Collidable) ?? false;
        }

        public bool IsBlock(Collider2D collider)
        {
            return collider?.gameObject.HasTag(ObjectTag.Block) ?? false;
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
                normal = Vector2.down;
                collider = upHit.collider;
            }

            if (IsCollidable(downHit.collider))
            {
                normal = Vector2.up;
                collider = downHit.collider;
            }

            if (IsCollidable(rightHit.collider))
            {
                normal = Vector2.left;
                collider = rightHit.collider;
            }

            if (IsCollidable(leftHit.collider))
            {
                normal = Vector2.right;
                collider = leftHit.collider;
            }

            if (collider == null)
                return false;

            normal.Normalize();
            return true;
        }
    }
}
