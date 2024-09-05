using UnityEngine;

namespace Arkanoid
{
    [CreateAssetMenu]
    public class PrefabRepository : ScriptableObject
    {
        [field: SerializeField]
        public Racket Racket { get; private set; }

        [field: SerializeField]
        public Ball Ball { get; private set; }

        [field: SerializeField]
        public GameObject Heart { get; private set; }
    }
}
