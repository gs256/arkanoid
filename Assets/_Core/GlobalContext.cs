using UnityEngine;

namespace Arkanoid
{
    public class GlobalContext : MonoBehaviour
    {
        public static GlobalContext Instance { get; private set; }

        [field: SerializeField]
        public PrefabRepository PrefabRepository { get; private set; }

        public RacketFactory RacketFactory { get; private set; }

        private void Awake()
        {
            RacketFactory = new RacketFactory(PrefabRepository);

            Instance = this;
        }
    }
}
