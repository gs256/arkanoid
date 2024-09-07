using UnityEngine;

namespace Arkanoid
{
    public static class MathUtils
    {
        public static float VectorToAngle(Vector2 vector2)
        {
            return Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
        }

        public static Vector2 AngleToVector(float angleDegrees)
        {
            return new Vector2(
                Mathf.Cos(angleDegrees * Mathf.Deg2Rad),
                Mathf.Sin(angleDegrees * Mathf.Deg2Rad))
                .normalized;
        }

        public static Vector2 GetMirrorVector(Vector2 vector, Vector2 normal)
        {
            if (Vector2.Dot(vector, normal) > 0)
                normal *= -1;
            return vector - 2f * Vector2.Dot(vector, normal) * normal;
        }

        public static float GetMirrorAngle(float angleDegrees, Vector2 normal)
        {
            return VectorToAngle(GetMirrorVector(AngleToVector(angleDegrees), normal));
        }
    }
}
