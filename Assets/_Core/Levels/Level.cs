using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid.Levels
{
    public class Level : MonoBehaviour
    {
        private const float MouseSensitivity = 0.01f;

        public event Action Died;
        public event Action Completed;

        [SerializeField]
        private Field _field;

        private RacketFactory _racketFactory;
        private BallFactory _ballFactory;
        private Racket _racket;
        private Ball _ball;
        private bool _started;
        private bool _died;
        private bool _completed;
        private List<Block> _blocks;
        private Player _player;

        public void Initialize(Player player, RacketFactory racketFactory,
            BallFactory ballFactory)
        {
            BallCollisionProcessor ballCollisionProcessor = new();
            _player = player;
            _racketFactory = racketFactory;
            _ballFactory = ballFactory;

            _racket = _racketFactory.Create(_field);
            _ball = _ballFactory.Create(_field, _racket);
            _ball.Initialize(ballCollisionProcessor);
            SetupBlocks();
        }

        public void StartLevel()
        {
            _started = true;
        }

        private void Update()
        {
            UpdateRacketPosition();

            // TODO: level state
            if (!_started)
                _ball.Follow(_racket.transform);
            else
                _ball.UpdatePosition(Time.deltaTime);

            if (!_completed)
                CheckDieCondition();
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
            _completed = true;
            Completed?.Invoke();
        }

        private void UpdateRacketPosition()
        {
            _racket.Position = _racket.Position.WithX((Input.mousePosition.x - Screen.width / 2f) * MouseSensitivity);
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
            if (_died)
                return;

            if (_ball.Position.y < _field.Bounds.min.y)
            {
                _died = true;
                Died?.Invoke();
            }
        }
    }
}
