using UnityEngine;

namespace Arkanoid
{
    public class CoroutineRunner : MonoBehaviour
    {
        public static CoroutineRunner Create()
        {
            GameObject gameObject = new GameObject("Coroutine runner");
            CoroutineRunner runner = gameObject.AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(gameObject);
            return runner;
        }
    }
}
