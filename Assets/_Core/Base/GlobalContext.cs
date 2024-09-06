using Arkanoid.Levels;
using Arkanoid.Menu;
using UnityEngine;

namespace Arkanoid.Base
{
    public class GlobalContext : Context
    {
        public static GlobalContext Instance { get; private set; }

        [field: SerializeField]
        public PrefabRepository PrefabRepository { get; private set; }

        [field: SerializeField]
        public LevelRepository LevelRepository { get; private set; }

        public RacketFactory RacketFactory { get; private set; }
        public BallFactory BallFactory { get; private set; }
        public CoroutineRunner CoroutineRunner { get; private set; }
        public SceneRepository SceneRepository { get; private set; }
        public SceneLoader SceneLoader { get; private set; }
        public GameStateFactory GameStateFactory { get; private set; }
        public GameStateMachine GameStateMachine { get; private set; }
        public MainMenuProvider MainMenuProvider { get; private set; }
        public Player Player { get; private set; }

        private void Awake()
        {
            Player = new Player();
            CoroutineRunner = CoroutineRunner.Create();
            RacketFactory = new RacketFactory(PrefabRepository);
            BallFactory = new BallFactory(PrefabRepository);
            SceneRepository = new SceneRepository();
            SceneLoader = new SceneLoader(SceneRepository);
            MainMenuProvider = new MainMenuProvider();
            GameStateMachine = new GameStateMachine();
            GameStateFactory = new GameStateFactory(GameStateMachine, SceneLoader, MainMenuProvider);

            Instance = this;
        }
    }
}
