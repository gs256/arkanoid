using UnityEngine;

namespace Arkanoid.Levels
{
    public class LevelFactory
    {
        private readonly LevelRepository _levelRepository;
        private readonly Player _player;
        private readonly RacketFactory _racketFactory;
        private readonly BallFactory _ballFactory;

        public LevelFactory(Player player, LevelRepository levelRepository, RacketFactory racketFactory,
            BallFactory ballFactory)
        {
            _levelRepository = levelRepository;
            _player = player;
            _racketFactory = racketFactory;
            _ballFactory = ballFactory;
        }

        public Level Create(int levelIndex)
        {
            Level prefab = _levelRepository.Levels[levelIndex];
            Level level = Object.Instantiate(prefab);
            level.Initialize(_player, _racketFactory, _ballFactory);
            return level;
        }
    }
}
