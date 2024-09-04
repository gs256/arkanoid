using UnityEngine;

namespace Arkanoid
{
    public class Level : MonoBehaviour
    {
        private const float MouseSensitivity = 0.01f;

        [SerializeField]
        private Field _field;

        [SerializeField]
        private Ball _ball;

        private bool _started;
        private RacketFactory _racketFactory;
        private Racket _racket;

        public void Initialize()
        {
            _racketFactory = GlobalContext.Instance.RacketFactory;
            BallCollisionProcessor ballCollisionProcessor = new();

            _racket = _racketFactory.Create(_field);
            _ball.Initialize(ballCollisionProcessor);
        }

        public void StartLevel()
        {
            _started = true;
        }

        private void Update()
        {
            UpdateRacketPosition();

            if (!_started)
                return;

            _ball.UpdatePosition(Time.deltaTime);
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
