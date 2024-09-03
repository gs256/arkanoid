using UnityEngine;

namespace Arkanoid
{
    public class Game : MonoBehaviour
    {
        private const float MouseSensitivity = 0.01f;

        [SerializeField]
        private Racket _racket;

        [SerializeField]
        private Field _field;

        [SerializeField]
        private Ball _ball;

        private void Start()
        {
            _ball.Angle = UnityEngine.Random.Range(0f, 90f);
        }

        private void Update()
        {
            _ball.UpdatePosition(Time.deltaTime);
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
