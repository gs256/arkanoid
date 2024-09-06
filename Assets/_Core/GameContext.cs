using System;
using Arkanoid.Base;
using Arkanoid.Levels;
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
        public LevelManager LevelManager { get; private set; }
        public RacketFactory RacketFactory { get; private set; }
        public BallFactory BallFactory { get; private set; }
        public LevelFactory LevelFactory { get; private set; }
        public PlayerView PlayerView { get; set; }

        private void Awake()
        {
            Player player = GlobalContext.Instance.Player;
            PlayerView = new PlayerView(player, UiController);
            PrefabRepository prefabRepository = GlobalContext.Instance.PrefabRepository;
            LevelRepository levelRepository = GlobalContext.Instance.LevelRepository;
            CoroutineRunner coroutineRunner = GlobalContext.Instance.CoroutineRunner;

            HeartFactory = new HeartFactory(prefabRepository);
            RacketFactory = new RacketFactory(prefabRepository);
            BallFactory = new BallFactory(prefabRepository);
            LevelFactory = new LevelFactory(player, levelRepository, RacketFactory, BallFactory);
            LevelManager = new LevelManager(levelRepository, coroutineRunner, UiController, LevelFactory);

            Instance = this;
        }

        private void OnDestroy()
        {
            PlayerView.Dispose();
        }
    }
}
