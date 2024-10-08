using Arkanoid.Base.Scenes;
using Arkanoid.Common;
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

        public CoroutineRunner CoroutineRunner { get; private set; }
        public SceneRepository SceneRepository { get; private set; }
        public SceneLoader SceneLoader { get; private set; }
        public GameStateFactory GameStateFactory { get; private set; }
        public GameStateMachine GameStateMachine { get; private set; }
        public Player Player { get; private set; }

        private void Awake()
        {
            Player = new Player();
            CoroutineRunner = CoroutineRunner.Create();
            SceneRepository = new SceneRepository();
            SceneLoader = new SceneLoader(SceneRepository);
            GameStateMachine = new GameStateMachine();
            GameStateFactory = new GameStateFactory(GameStateMachine, SceneLoader);

            Instance = this;
        }
    }
}
