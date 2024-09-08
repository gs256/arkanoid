using UnityEngine;

namespace Arkanoid.Common
{
    public static class UtilityExtensions
    {
        public static Vector2 WithX(this Vector2 vector, float x)
        {
            return new Vector2(x, vector.y);
        }

        public static bool HasComponent<T>(this GameObject unityObject, out T component) where T : Component
        {
            component = unityObject.GetComponent<T>();
            return component != null;
        }
    }
}
