using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZTools.SingletonNS
{
    public sealed class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner instance;
        private Dictionary<string, Coroutine> tracking;

        public static MonoBehaviour GetRunner()
        {
            if (instance == null)
            {
                var obj = new GameObject("[COROUTINE RUNNER]");
                instance = obj.AddComponent<CoroutineRunner>();
                instance.tracking = new Dictionary<string, Coroutine>();
                DontDestroyOnLoad(obj);
            }

            return instance;
        }

        public static bool IsRunning(string _trackingName)
        {
            return instance != null && instance.tracking.ContainsKey(_trackingName);
        }

        public static Coroutine Run(IEnumerator _function, string _trackingName = null)
        {
            if (instance == null)
            {
                var obj = new GameObject("[COROUTINE RUNNER]");
                instance = obj.AddComponent<CoroutineRunner>();
                instance.tracking = new Dictionary<string, Coroutine>();
                DontDestroyOnLoad(obj);
            }

            if (!string.IsNullOrEmpty(_trackingName))
            {
                Stop(_trackingName);
                var c = instance.StartCoroutine(_function);
                instance.tracking.Add(_trackingName, c);
                return c;
            }
            else
            {
                return instance.StartCoroutine(_function);
            }
        }

        public static void Stop(Coroutine _coroutine)
        {
            if (instance != null)
                instance.StopCoroutine(_coroutine);
        }

        public static void Stop(string _name)
        {
            if (instance != null)
            {
                if (instance.tracking.TryGetValue(_name, out var c))
                {
                    instance.tracking.Remove(_name);
                    if (c != null)
                    {
                        instance.StopCoroutine(c);   
                    }
                }
            }
        }

        private void OnDestroy()
        {
            tracking = null;
            StopAllCoroutines();
        }
    }
}