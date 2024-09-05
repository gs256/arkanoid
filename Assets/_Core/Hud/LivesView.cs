using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid.Hud
{
    public class LivesView : MonoBehaviour
    {
        private readonly List<GameObject> _hearts = new();
        private HeartFactory _heartFactory;

        public void Initialize(HeartFactory heartFactory)
        {
            _heartFactory = heartFactory;
        }

        public void SetLives(int lives)
        {
            Debug.Assert(lives > 0);

            int hearts = _hearts.Count;
            if (hearts < lives)
                for (int i = 0; i < lives - hearts; i++)
                    CreateHeart();
            else if (hearts > lives)
                for (int i = 0; i < hearts - lives; i++)
                    RemoveHeart();
        }

        private void CreateHeart()
        {
            GameObject heart = _heartFactory.Create(transform);
            _hearts.Add(heart);
        }

        private void RemoveHeart()
        {
            _hearts.Remove(_hearts.Last());
        }
    }
}
