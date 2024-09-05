using Arkanoid.Base;
using Arkanoid.Hud;
using UnityEngine;

namespace Arkanoid
{
    public class GameContext : Context
    {
        public static GameContext Instance { get; private set; }

        [field: SerializeField]
        public Hud.Hud Hud { get; private set; }

        public HeartFactory HeartFactory { get; private set; }

        private void Awake()
        {
            PrefabRepository prefabRepository = GlobalContext.Instance.PrefabRepository;

            HeartFactory = new HeartFactory(prefabRepository);

            Instance = this;
        }
    }
}
