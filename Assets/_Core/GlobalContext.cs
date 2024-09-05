using UnityEngine;

namespace Arkanoid
{
    // FIXME
    [DefaultExecutionOrder(-1)]
    public class GlobalContext : MonoBehaviour
    {
        public static GlobalContext Instance { get; private set; }

        [field: SerializeField]
        public PrefabRepository PrefabRepository { get; private set; }

        [field: SerializeField]
        public LevelRepository LevelRepository { get; private set; }

        public RacketFactory RacketFactory { get; private set; }
        public BallFactory BallFactory { get; private set; }
        public LevelManager LevelManager { get; private set; }
        public CoroutineRunner CoroutineRunner { get; private set; }

        private void Awake()
        {
            CoroutineRunner = CoroutineRunner.Create();
            RacketFactory = new RacketFactory(PrefabRepository);
            BallFactory = new BallFactory(PrefabRepository);
            LevelManager = new LevelManager(LevelRepository, CoroutineRunner);

            Instance = this;
        }
    }
}
