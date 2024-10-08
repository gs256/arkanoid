using Arkanoid.Ball;
using Arkanoid.Common;
using Arkanoid.Racket;
using UnityEngine;

namespace Arkanoid.Levels
{
    public class LevelFactory
    {
        private readonly LevelRepository _levelRepository;
        private readonly Player _player;
        private readonly RacketFactory _racketFactory;
        private readonly BallFactory _ballFactory;
        private readonly CoroutineRunner _coroutineRunner;

        public LevelFactory(Player player, LevelRepository levelRepository, RacketFactory racketFactory,
            BallFactory ballFactory, CoroutineRunner coroutineRunner)
        {
            _levelRepository = levelRepository;
            _player = player;
            _racketFactory = racketFactory;
            _ballFactory = ballFactory;
            _coroutineRunner = coroutineRunner;
        }

        public Level Create(int levelIndex)
        {
            Level prefab = _levelRepository.Levels[levelIndex];
            Level level = Object.Instantiate(prefab);
            level.Initialize(_player, _racketFactory, _ballFactory, _coroutineRunner);
            return level;
        }
    }
}
