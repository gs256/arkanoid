using System;
using UnityEngine;

namespace Arkanoid.Blocks
{
    public class FlyingBlock : Block
    {
        private const float FieldBorderOffset = 0.1f;

        public override event Action<Block> Destroyed;
        public override int Points => 25;

        [SerializeField, Range(0.1f, 10f)]
        private float _speed;

        [SerializeField]
        private Field _field;

        [SerializeField]
        private Collider2D _collider;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        private int _directionMultiplier = 1;

        public override void Hit()
        {
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (_field.Bounds.max.x - _collider.bounds.max.x <= FieldBorderOffset)
                _directionMultiplier = -1;
            if (_collider.bounds.min.x - _field.Bounds.min.x <= FieldBorderOffset)
                _directionMultiplier = 1;

            Vector2 delta = Vector2.right * _directionMultiplier * _speed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + delta);
        }
    }
}
