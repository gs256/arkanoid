using System;
using System.Collections.Generic;
using System.Linq;
using Arkanoid.Ball;
using Arkanoid.Blocks;
using Arkanoid.Common;
using Arkanoid.Racket;
using UnityEngine;

namespace Arkanoid.Levels
{
    public class Level : MonoBehaviour
    {
        private const float DelayBeforeDeath = 1f;
        private const float DelayBeforeComplete = 1f;
        private const float RacketMovementSmoothFactor = 45f;

        public event Action Lost;
        public event Action Completed;

        [SerializeField]
        private Field _field;

        private RacketFactory _racketFactory;
        private BallFactory _ballFactory;
        private Racket.Racket _racket;
        private Ball.Ball _ball;
        private List<Block> _blocks;
        private Player _player;
        private CoroutineRunner _coroutineRunner;
        private BallCollisionProcessor _ballCollisionProcessor;
        private Coroutine _routine;
        private Camera _camera;

        private LevelState _currentState = LevelState.Waiting;
        private LevelState _stateBeforePause = LevelState.Waiting;

        public void Initialize(Player player, RacketFactory racketFactory, BallFactory ballFactory,
            CoroutineRunner coroutineRunner)
        {
            _player = player;
            _racketFactory = racketFactory;
            _ballFactory = ballFactory;
            _coroutineRunner = coroutineRunner;
            _camera = Camera.main;

            _ballCollisionProcessor = new BallCollisionProcessor();
            _racket = _racketFactory.Create(_field);
            SetupBall();
            SetupBlocks();
        }

        private void OnDestroy()
        {
            if (_routine != null && _coroutineRunner != null)
                _coroutineRunner.StopCoroutine(_routine);
        }

        public void StartLevel()
        {
            if (_currentState is LevelState.Waiting)
                _currentState = LevelState.Playing;
        }

        private void Update()
        {
            if (_currentState is not LevelState.Pause)
                UpdateRacketPosition(Time.deltaTime);

            if (_currentState is LevelState.Waiting)
                _ball.Follow(_racket.transform);
            else if (_currentState is not LevelState.Pause)
                _ball.UpdatePosition(Time.deltaTime);

            if (_currentState is LevelState.Playing)
                CheckDieCondition();

        }

        private void FixedUpdate()
        {
            if (_currentState is LevelState.Waiting)
                _ball.Follow(_racket.transform);
            else if (_currentState is not LevelState.Pause)
                _ball.UpdatePosition(Time.fixedDeltaTime);
        }

        public void Revive()
        {
            _currentState = LevelState.Waiting;
            SetupBall();
        }

        public void Pause()
        {
            _stateBeforePause = _currentState;
            _currentState = LevelState.Pause;
        }

        public void Resume()
        {
            _currentState = _stateBeforePause;
        }

        private void SetupBall()
        {
            if (_ball != null)
                Destroy(_ball.gameObject);
            _ball = _ballFactory.Create(_field, _racket, _ballCollisionProcessor);
        }

        private void SetupBlocks()
        {
            _blocks = GetComponentsInChildren<Block>().ToList();
            Debug.Assert(_blocks.Count > 0, "There should be at least one block on a level");
            foreach (var block in _blocks)
                block.Destroyed += OnBlockDestroyed;
        }

        private void OnBlockDestroyed(Block block)
        {
            block.Destroyed -= OnBlockDestroyed;
            _blocks.Remove(block);
            _player.AddScore(block.Points);

            if (_blocks.Count == 0)
                OnAllBlocksDestroyed();
        }

        private void OnAllBlocksDestroyed()
        {
            _currentState = LevelState.Completed;
            _routine = _coroutineRunner.CallAfterDelay(DelayBeforeComplete, () =>
            {
                _currentState = LevelState.Pause;
                Completed?.Invoke();
            });
        }

        private void UpdateRacketPosition(float deltaTime)
        {
            float targetX = _camera.ScreenToWorldPoint(Input.mousePosition).x;
            _racket.Position = Vector2.MoveTowards(_racket.Position, _racket.Position.WithX(targetX),
                RacketMovementSmoothFactor * deltaTime);

            ClampRacketPosition();
        }

        private void ClampRacketPosition()
        {
            float racketHalfSize = _racket.Bounds.extents.x;
            float racketMax = _racket.Position.x + racketHalfSize;
            float fieldMax = _field.Bounds.max.x;
            float racketMin = _racket.Position.x - racketHalfSize;
            float fieldMin = _field.Bounds.min.x;

            if (racketMax > fieldMax)
                _racket.Position = _racket.Position.WithX(fieldMax - racketHalfSize);
            else if (racketMin < fieldMin)
                _racket.Position = _racket.Position.WithX(fieldMin + racketHalfSize);
        }

        private void CheckDieCondition()
        {
            if (_ball.Position.y < _field.Bounds.min.y)
            {
                _currentState = LevelState.Died;

                _routine = _coroutineRunner.CallAfterDelay(DelayBeforeDeath, () =>
                {
                    _currentState = LevelState.Pause;
                    _player.DecreaseLives();

                    if (_player.Lives > 0)
                        Revive();
                    else
                        Lost?.Invoke();
                });
            }
        }
    }
}
