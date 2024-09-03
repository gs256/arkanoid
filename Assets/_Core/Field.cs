using UnityEngine;

namespace Arkanoid
{
    public class Field : MonoBehaviour
    {
        public Bounds Bounds => _collider.bounds;

        [SerializeField]
        private BoxCollider2D _collider;
    }
}
