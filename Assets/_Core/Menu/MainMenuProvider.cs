namespace Arkanoid.Menu
{
    public class MainMenuProvider
    {
        public MainMenu MainMenu { get; private set; }

        public void Initialize(MainMenu mainMenu)
        {
            MainMenu = mainMenu;
        }
    }
}
