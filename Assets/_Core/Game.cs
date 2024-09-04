using UnityEngine;

namespace Arkanoid
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private Level _level;

        private void Start()
        {
            _level.Initialize();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _level.StartLevel();
            }
        }
    }
}
