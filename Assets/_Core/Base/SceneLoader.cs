using System;

namespace Arkanoid.Base
{
    public class SceneLoader
    {
        private readonly SceneRepository _sceneRepository;

        public SceneLoader(SceneRepository sceneRepository)
        {
            _sceneRepository = sceneRepository;
        }

        public void LoadMenu(Action callback)
        {
            SceneUtils.LoadSceneAdditive(_sceneRepository.MenuSceneName, callback);
        }

        public void UnloadMenu(Action callback)
        {
            SceneUtils.UnloadScene(_sceneRepository.MenuSceneName, callback);
        }

        public void LoadGame(Action callback)
        {
            SceneUtils.LoadSceneAdditive(_sceneRepository.GameSceneName, callback);
        }

        public void UnloadGame(Action callback)
        {
            SceneUtils.UnloadScene(_sceneRepository.GameSceneName, callback);
        }
    }
}
