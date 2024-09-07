using System;
using System.Collections;
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

        public Coroutine CallAfterDelay(float delaySeconds, Action callback)
        {
            return StartCoroutine(RunAfterDelayCoroutine(delaySeconds, callback));
        }

        private IEnumerator RunAfterDelayCoroutine(float delaySeconds, Action callback)
        {
            yield return new WaitForSeconds(delaySeconds);
            callback?.Invoke();
        }
    }
}
