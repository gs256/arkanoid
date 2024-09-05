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
            MainMenuProvider mainMenuProvider = GlobalContext.Instance.MainMenuProvider;
            mainMenuProvider.Initialize(_mainMenu);
        }
    }
}
