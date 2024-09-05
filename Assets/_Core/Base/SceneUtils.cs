using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid.Base
{
    public static class SceneUtils
    {
        public static void LoadSceneAdditive(string sceneName, Action callback)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            operation.completed += _ =>
            {
                Scene scene = SceneManager.GetSceneByName(sceneName);
                SceneManager.SetActiveScene(scene);
                callback?.Invoke();
            };
        }

        public static void UnloadScene(string sceneName, Action callback)
        {
            var operation = SceneManager.UnloadSceneAsync(sceneName);
            operation.completed += _ =>
            {
                callback?.Invoke();
            };
        }

        public static void ReloadCurrentScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
