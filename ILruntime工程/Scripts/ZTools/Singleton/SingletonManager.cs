using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ZTools.SingletonNS
{
    /// <summary>
    /// 单例管理器
    /// 存放当前场景中所有存活的单例
    /// 
    /// 该脚本的销毁顺序放在所有脚本的最后，避免卸载的时候找不到单例的情况
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SingletonManager : MonoBehaviour
    {
#if UNITY_EDITOR

        //[UnityEditor.MenuItem("Tools/Log Data Size")]
        //public static void LogSize()
        //{
        //    if (managerObj != null)
        //    {
        //        Debug.Log($"{System.Runtime.InteropServices.Marshal.SizeOf(managerObj.GetComponent<SingletonManager>())}");
        //    }
        //}

        [UnityEditor.MenuItem("功能入口/查询/系统单例清单")]
        private static void LogSingleton()
        {
            foreach(var singleton in allSingletons)
            {
                Debug.Log(singleton.GetType().ToString());
            }
        }
        
#endif

        /// <summary>
        /// Singleton Manager 是寄存在一个GameObject上的
        /// 因此可以选择该物体在层级面板中是否可视
        /// </summary>
        private static bool shouldThisObjectHide = false;

        /// <summary>
        /// Singleton Manager 所寄存的GameObject
        /// </summary>
        private static GameObject managerObj;

        /// <summary>
        /// 当前所有注册的单例
        /// </summary>
        private static HashSet<SingletonBase> allSingletons;

        /// <summary>
        /// 缓存上述Hash列表的迭代器
        /// 避免每一帧都产生GC
        /// </summary>
        private static SingletonBase[] allSingletonsArray;

        /// <summary>
        /// 注册一个单例
        /// </summary>
        /// <param name="_singleton">要注册的单例</param>
        public static void Regist(SingletonBase _singleton)
        {
            //Debug.Log(_singleton.GetType().ToString());
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            {
                Debug.LogError("正在关闭，但是却创建了单例");
            }
#endif
            if (allSingletons == null)
            {
                managerObj = new GameObject("[Singleton Manager]");
                managerObj.AddComponent<SingletonManager>();
                DontDestroyOnLoad(managerObj);
                managerObj.hideFlags = shouldThisObjectHide ? (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor) : HideFlags.None;
                allSingletons = new HashSet<SingletonBase>();
            }

            if (allSingletons.Contains(_singleton)) throw new System.Exception(string.Format("重复注册单例 : {0}", _singleton.GetType().FullName));

            allSingletons.Add(_singleton);
            allSingletonsArray = allSingletons.ToArray();
        }

        /// <summary>
        /// 注销一个单例
        /// </summary>
        /// <param name="_singleton">要注销的单例</param>
        public static void UnRegist(SingletonBase _singleton, bool _callReleaseMemory = true)
        {
            if (_singleton == null)
                return;

            if (allSingletons == null)
                throw new System.Exception(string.Format("单例管理器SingletonManager尚未初始化，不允许注销单例 ： {0}", _singleton.GetType().FullName));

            if (!allSingletons.Contains(_singleton))
                throw new System.Exception(string.Format("单例{0}没有注册过，不允许注销", _singleton.GetType().FullName));

            if (_callReleaseMemory)
            {
                _singleton.ReleaseMemories();
            }
            
            _singleton.Unload();
            _singleton.Loaded = false;
            allSingletons.Remove(_singleton);
            allSingletonsArray = allSingletons.ToArray();

            ((System.IDisposable)_singleton).Dispose();
        }

        /// <summary>
        /// 注销所有的单例
        /// </summary>
        public static void UnRegistAll(bool _callReleaseMemory = true)
        {
            //Debug.Log("Start Unregist All Singletons");

            List<string> names = new List<string>(allSingletons.Count);

            foreach (var singleton in allSingletons)
                names.Add(singleton.GetType().Name);

            var name = string.Empty;

            try
            {
                foreach (var singleton in allSingletons)
                {
                    name = singleton.GetType().Name;

                    if(_callReleaseMemory)
                        singleton.ReleaseMemories();
                    
                    singleton.Unload();
                    singleton.Loaded = false;
                    ((System.IDisposable)singleton).Dispose();
                    //Debug.Log($"正进行到{name}");
                }
            }
            catch
            {
                Debug.Log($"卸载时候出现了错误，正进行到{name}, 以下是当前所有存在的Singleton");
                foreach (var n in names)
                {
                    Debug.Log(n);
                }

                throw;
            }

            allSingletons = null;
            allSingletonsArray = null;
        }

        public static void Destroy()
        {
            DestroyImmediate(managerObj);
            managerObj = null;
        }

        #region 更新函数 Updates

        /// <summary>
        /// 检查是否应该更新，避免报错
        /// </summary>
        /// <returns></returns>
        private bool ShouldUpdate()
        {
            return allSingletonsArray != null && allSingletonsArray.Length > 0;
        }

        /// <summary>
        /// Unity默认回调，每帧更新
        /// </summary>
        private void Update()
        {
            if (!ShouldUpdate())
                return;

#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
                return;
#endif

            for (int i = 0; i < allSingletonsArray.Length; ++i)
            {
                if (allSingletonsArray[i] == null)
                    throw new System.Exception("存在一个为空值的单例!");

                //UnityEngine.Profiling.Profiler.BeginSample(allSingletonsArray[i].GetType().ToString());

                allSingletonsArray[i].Update();

                //UnityEngine.Profiling.Profiler.EndSample();
            }
        }

        /// <summary>
        /// Unity默认回调，固定频率更新
        /// </summary>
        private void FixedUpdate()
        {
            if (!ShouldUpdate())
                return;

            for (int i = 0; i < allSingletonsArray.Length; ++i)
            {
                if (allSingletonsArray[i] == null)
                    throw new System.Exception("存在一个为空值的单例!");

                //UnityEngine.Profiling.Profiler.BeginSample(allSingletonsArray[i].GetType().ToString());

                allSingletonsArray[i].FixedUpdate();

                //UnityEngine.Profiling.Profiler.EndSample();
            }
        }

        /// <summary>
        /// Unity默认回调，帧尾更新
        /// </summary>
        private void LateUpdate()
        {
            if (!ShouldUpdate())
                return;

            for (int i = 0; i < allSingletonsArray.Length; ++i)
            {
                if (allSingletonsArray[i] == null)
                    throw new System.Exception("存在一个为空值的单例!");

                //UnityEngine.Profiling.Profiler.BeginSample(allSingletonsArray[i].GetType().ToString());

                allSingletonsArray[i].LateUpdate();

                //UnityEngine.Profiling.Profiler.EndSample();
            }
        }

#endregion

        private void OnDisable()
        {
            foreach (var manager in allSingletonsArray)
            {
                manager.ReleaseMemories();
                manager.Loaded = false;
            }

            bool isDisabledByQuitEditor;
#if UNITY_EDITOR
            isDisabledByQuitEditor = !UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode;
#else
            isDisabledByQuitEditor = applicationIsQuiting;
            //isDisabledByQuitEditor = false;
#endif

            if (isDisabledByQuitEditor)
            {
                foreach (var manager in allSingletonsArray)
                {
                    ((System.IDisposable)manager).Dispose();
                }
                return;
            }
            else
            {
                UnRegistAll(false);
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            try
            {
                if (focus)
                {
                    foreach (var singleton in allSingletons)
                    {
                        try
                        {
                            singleton.OnApplicationFocused();
                        }
                        catch (System.Exception _e)
                        {
                            Debug.LogError($"恢复焦点时，{singleton.GetType().Name}出现了一些问题。");
                            Debug.LogException(_e);
                        }
                    }
                }
                else
                {
                    foreach (var singleton in allSingletons)
                    {
                        try
                        {
                            singleton.OnApplicationGoesIntoBackground();
                        }
                        catch (System.Exception _e)
                        {
                            Debug.LogError($"进入背景程序时，{singleton.GetType().Name}出现了一些问题。");
                            Debug.LogException(_e);
                        }
                    }
                }
            }
            catch (System.Exception _e)
            {
                Debug.LogException(_e);
            }
        }

#pragma warning disable 414
        private bool applicationIsQuiting = false;
#pragma warning restore 414
        private void OnApplicationQuit()
        {
            applicationIsQuiting = true;
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            foreach(var singleton in allSingletons)
            {
                singleton.OnDrawGizmos();
            }
        }

#endif
    }
}
