using Arkanoid.Tags;
using UnityEngine;

namespace Arkanoid
{
    public class BallCollisionProcessor
    {
        // TODO: to config
        private const float RandomBounceAngleScattering = 2f;
        public void ProcessCollision(Ball ball, Collision2D collision)
        {
            if (collision.contacts.Length < 1)
                return;

            if (IsCollidable(collision.gameObject))
            {
                ProcessBounce(ball, collision);

                if (collision.gameObject.HasComponent<Block>(out Block block))
                    block.Hit();
            }
        }

        private void ProcessBounce(Ball ball, Collision2D collision)
        {
            if (CollidedWithRacketSide(collision))
            {
                Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
                return;
            }

            float mirrorAngle = MathUtils.GetMirrorAngle(ball.Angle, GetNormal(collision));
            float scattering = Random.Range(-RandomBounceAngleScattering, RandomBounceAngleScattering);

            ball.Angle = mirrorAngle + scattering;
        }

        private bool IsCollidable(GameObject gameObject)
        {
            return gameObject.HasTag(ObjectTag.Collidable);
        }

        private bool IsRacket(GameObject gameObject)
        {
            return gameObject.HasTag(ObjectTag.Racket);
        }

        private bool CollidedWithRacketSide(Collision2D collision)
        {
            return IsRacket(collision.gameObject) && GetNormal(collision).y <= 0;
        }

        private Vector2 GetNormal(Collision2D collision)
        {
            DebugAssertExpectedCollisionData(collision);
            return collision.contacts[0].normal;
        }

        private void DebugAssertExpectedCollisionData(Collision2D collision)
        {
            Debug.Assert(collision.contacts.Length > 0, "At least one contact point expected");
            Vector2 firstNormal = collision.contacts[0].normal;
            foreach (var contact in collision.contacts)
                Debug.Assert(contact.normal == firstNormal, $"Expected equal normals for each contact point, got {firstNormal} and {contact.normal}");
        }
    }
}
