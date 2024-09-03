using UnityEngine;

namespace Arkanoid
{
    public static class UtilityExtensions
    {
        public static Vector2 WithX(this Vector2 vector, float x)
        {
            return new Vector2(x, vector.y);
        }
    }
}
