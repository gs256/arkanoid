using Arkanoid.Base;
using UnityEngine;

namespace Arkanoid.Menu
{
    public class MenuContext : Context
    {
        [SerializeField]
        private MainMenu _mainMenu;

        private void Awake()
        {
            var gameStateMachine = GlobalContext.Instance.GameStateMachine;
            _mainMenu.Initialize(gameStateMachine);
        }
    }
}
