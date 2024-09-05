using UnityEngine;

namespace Arkanoid
{
    public class Level : MonoBehaviour
    {
        private const float MouseSensitivity = 0.01f;

        [SerializeField]
        private Field _field;

        private RacketFactory _racketFactory;
        private BallFactory _ballFactory;
        private Racket _racket;
        private Ball _ball;
        private bool _started;

        public void Initialize()
        {
            _racketFactory = GlobalContext.Instance.RacketFactory;
            _ballFactory = GlobalContext.Instance.BallFactory;
            BallCollisionProcessor ballCollisionProcessor = new();

            _racket = _racketFactory.Create(_field);
            _ball = _ballFactory.Create(_field, _racket);
            _ball.Initialize(ballCollisionProcessor);
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
            {
                _ball.Follow(_racket.transform);
            }
            else
            {
                _ball.UpdatePosition(Time.deltaTime);
            }
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
    }
}
