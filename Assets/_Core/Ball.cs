using Arkanoid.Tags;
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

        [SerializeField]
        private float _randomAngleScattering;

        public void UpdatePosition(float deltaTime)
        {
            Position += AngleToDirection(Angle) * Speed * deltaTime;
        }

        private Vector2 AngleToDirection(float angleDegrees)
        {
            return new Vector2(
                Mathf.Cos(angleDegrees * Mathf.Deg2Rad),
                Mathf.Sin(angleDegrees * Mathf.Deg2Rad))
                .normalized;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.HasTag(ObjectTag.Collidable))
            {
                Angle = AngleFromVector(GetMirrorVector(AngleToDirection(Angle), other.contacts[0].normal)) +
                        UnityEngine.Random.Range(-_randomAngleScattering, _randomAngleScattering);
            }
        }

        public static float AngleFromVector(Vector2 vector2)
        {
            return Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
        }

        private Vector2 GetMirrorVector(Vector2 vector, Vector2 normal)
        {
            return vector - 2f * Vector2.Dot(vector, normal) * normal;
        }
    }
}
