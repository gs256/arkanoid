using System;
using UnityEngine;

namespace Arkanoid
{
    public class Player
    {
        // TODO: to config
        private const int DefaultLives = 2;
        private const int LivesAfterRevive = 1;

        public event Action<int> LivesChanged;
        public event Action<int> ScoreChanged;

        public int Lives { get; private set; } = DefaultLives;
        public int Score { get; private set; }

        public void Initialize()
        {
            Lives = DefaultLives;
            Score = 0;

            LivesChanged?.Invoke(Lives);
            ScoreChanged?.Invoke(Score);
        }

        public void Revive()
        {
            Lives = LivesAfterRevive;
            LivesChanged?.Invoke(Lives);
        }

        public void DecreaseLives()
        {
            Debug.Assert(Lives > 0, "Can't decrease 0 lives");
            Lives -= 1;
            LivesChanged?.Invoke(Lives);
        }

        public void AddScore(int points)
        {
            Debug.Assert(points >= 0, "Can't add negative score");
            Score += points;
            ScoreChanged?.Invoke(Score);
        }
    }
}
