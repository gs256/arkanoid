using System;
using UnityEngine;

namespace Arkanoid.Blocks
{
    public class DoubleHitBlock : Block
    {
        public override event Action<Block> Destroyed;
        public override int Points => 20;

        [SerializeField]
        private Color _completeColor;

        [SerializeField]
        private Color _brokenColor;

        [SerializeField, HideInInspector]
        private SpriteRenderer _spriteRenderer;

        private bool _alreadyHit;

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Debug.Assert(_spriteRenderer != null);
            _spriteRenderer.color = _completeColor;
        }

        public override void Hit()
        {
            if (_alreadyHit)
            {
                Destroyed?.Invoke(this);
                Destroy(gameObject);
            }

            _spriteRenderer.color = _brokenColor;
            _alreadyHit = true;
        }
    }
}
