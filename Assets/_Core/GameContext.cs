using Arkanoid.Base;
using Arkanoid.Ui;
using Arkanoid.Ui.Lives;
using UnityEngine;

namespace Arkanoid
{
    public class GameContext : Context
    {
        public static GameContext Instance { get; private set; }

        [field: SerializeField]
        public UiController UiController { get; private set; }

        [field: SerializeField]
        public Game Game { get; private set; }

        public HeartFactory HeartFactory { get; private set; }

        private void Awake()
        {
            PrefabRepository prefabRepository = GlobalContext.Instance.PrefabRepository;

            HeartFactory = new HeartFactory(prefabRepository);

            Instance = this;
        }
    }
}
