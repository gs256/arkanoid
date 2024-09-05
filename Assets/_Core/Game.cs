using Arkanoid.Base;
using UnityEngine;

namespace Arkanoid
{
    public class Game : MonoBehaviour
    {
        private LevelManager _levelManager;

        private void Awake()
        {
            _levelManager = GlobalContext.Instance.LevelManager;
        }

        private void Start()
        {
            _levelManager.LoadFirstLevel();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _levelManager.StartLevel();
            }
        }
    }
}
