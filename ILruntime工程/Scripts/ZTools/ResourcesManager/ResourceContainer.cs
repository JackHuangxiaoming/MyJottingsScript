using UnityEngine;
using ZTools.SingletonNS;
using System.Collections.Generic;
using System;
using System.CodeDom;
using System.Collections;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using ZTools.DataStructures;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ZTools.ResourceManagerNS
{
    public sealed class ResourceContainer : Singletons<ResourceContainer>
    {
        private static readonly List<Action<(string path, Object asset, Exception error)>> externalEventsQueryBuffer =
            new List<Action<(string path, Object asset, Exception error)>>();

        private Dictionary<string, (AsyncOperationHandle handle, Action<(string path, Object asset, Exception error)> callback)> loadingRequest;
        private Dictionary<string, AudioClip[]> audioWarmingProcess;
        private List<string> toRemoveAudioPreloadingCache;
        private HashSet<string> callbacksFastPath;
        private IDPackedObjectList<string, Action<(string path, Object asset, Exception error)>> callbacks;
        private float intentionalDelay;

        public override void FixedUpdate()
        {
        }

        public override void LateUpdate()
        {
        }

        public override void Load()
        {
            try
            {
                if (Application.isEditor)
                {
#if UNITY_EDITOR
                    intentionalDelay = EditorPrefs.GetFloat("intentional_res_delay", 0f);
#endif
                }
                else
                {
                    intentionalDelay = 0f;
                    var args = Environment.GetCommandLineArgs();

                    for (var i = 0; i < args.Length; i++)
                    {
                        if (args[i].Equals("-res_delay"))
                        {
                            intentionalDelay = float.Parse(args[i + 1]);
                            break;
                        }
                    }
                }
            }
            catch
            {
                intentionalDelay = 0f;
            }

            loadingRequest =
                new Dictionary<string, (AsyncOperationHandle handle,
                    Action<(string path, Object asset, Exception error)> callback)>();

            callbacks = new IDPackedObjectList<string, Action<(string path, Object asset, Exception error)>>(64);
            callbacksFastPath = new HashSet<string>();
            audioWarmingProcess = new Dictionary<string, AudioClip[]>();
            toRemoveAudioPreloadingCache = new List<string>(10);
        }

        public override void Reload()
        {
            UnloadAll();
        }

        public override void Unload()
        {
            UnloadAll();
        }

        public override void Update()
        {
            foreach (var pair in audioWarmingProcess)
            {
                var allLoaded = true;
                
                foreach (var clip in pair.Value)
                {
                    if (clip.loadState == AudioDataLoadState.Loading)
                    {
                        allLoaded = false;
                        break;
                    }
                }

                if (allLoaded)
                {
                    toRemoveAudioPreloadingCache.Add(pair.Key);
                }
            }

            foreach (var key in toRemoveAudioPreloadingCache)
            {
                audioWarmingProcess.Remove(key);
                
                try
                {
                    FlushGameObjectEventsAfterAudioLoaded(key);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }

            toRemoveAudioPreloadingCache.Clear();
        }

        public bool IsLoaded(string _path)
        {
            //如果资源已经加载完毕
            //但是正在预热音效，也当作加载中，而不算加载完毕
            return loadingRequest.TryGetValue(_path, out var r) && r.handle.IsDone &&
                   !audioWarmingProcess.ContainsKey(_path);
        }

        public bool IsLoading(string _path)
        {
            return loadingRequest.ContainsKey(_path);
        }

        public bool IsLoadingOrLoaded(string _path)
        {
            return IsLoaded(_path) || IsLoading(_path);
        }

        #region Resource

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_path"></param>
        /// <param name="_onAssetLoaded"></param>
        public void LoadFromResources<T>(string _path,
            Action<(string path, Object asset, Exception error)> _onAssetLoaded = null, bool _mergeRequestEvent = false)
            where T : Object
        {
            if (_mergeRequestEvent && _onAssetLoaded != null)
            {
                if (loadingRequest.TryGetValue(_path, out var x))
                {
                    if (x.handle.IsDone)
                    {
                        _onAssetLoaded.Invoke((_path, x.handle.Result as Object, x.handle.OperationException));
                    }
                    else
                    {
                        callbacksFastPath.Add(_path);
                        callbacks.Add(_path, _onAssetLoaded);
                    }

                    return;
                }
            }
            else
            {
                if (IsLoadingOrLoaded(_path))
                {
                    throw new ArgumentException($"不能对同一个路径进行多次请求。错误路径{_path}");
                }
            }

            var request = Addressables.LoadAssetAsync<T>(_path);
            loadingRequest.Add(_path, (request, _onAssetLoaded));
            
            //如果是音效
            if (typeof(T) == typeof(GameObject))
            {
                var p = _path;

                if (_onAssetLoaded != null)
                {
                    callbacksFastPath.Add(p);
                    callbacks.Add(p, _onAssetLoaded);
                }

                request.Completed += (handle) =>
                {
                    if (DirectInstance == null) return;

                    if (!DirectInstance.loadingRequest.ContainsKey(p)) return;

                    var toPreloadAudioClips = GetAudioClipsFromGameObject(handle.Result as GameObject);
                    if (toPreloadAudioClips != null)
                    {
                        DirectInstance.audioWarmingProcess.Add(p, toPreloadAudioClips);
                    }
                    else
                    {
                        FlushGameObjectEventsAfterAudioLoaded(p);
                    }
                };
            }
            else
            {
                if (_onAssetLoaded != null)
                {
                    var p = _path;
                    var c = _onAssetLoaded;

                    request.Completed += (handle) =>
                    {
                        if (DirectInstance == null) return;

                        if (DirectInstance.loadingRequest.TryGetValue(p, out var r))
                        {
                            if (r.callback != null)
                            {
                                r.callback.Invoke((p, handle.Result, handle.OperationException));
                                DirectInstance.loadingRequest[p] = (r.handle, null);
                            }
                        }

                        if (DirectInstance.callbacksFastPath.Contains(p))
                        {
                            DirectInstance.callbacksFastPath.Remove(p);

                            externalEventsQueryBuffer.Clear();

                            DirectInstance.callbacks.FindAllValuesOfKey(p, externalEventsQueryBuffer);
                            DirectInstance.callbacks.RemoveKey(p);

                            foreach (var callback in externalEventsQueryBuffer)
                            {
                                callback?.Invoke((p, handle.Result, handle.OperationException));
                            }

                            externalEventsQueryBuffer.Clear();
                        }
                    };
                }
            }
        }

        private static readonly HashSet<AudioClip> clipBuffer = new HashSet<AudioClip>();
        private static readonly List<AudioSource> sourceBuffer = new List<AudioSource>(10);

        /// <summary>
        /// 从指定GameObject身上获取对应的尚未加载完毕的AudioClip数组
        /// 如果GameObject为空，则返回空。
        /// 如果没有找到符合条件的AudioClip也返回空。
        /// 如果返回的是空，则不会有任何GC。
        /// </summary>
        /// <param name="_target"></param>
        /// <returns></returns>
        private static AudioClip[] GetAudioClipsFromGameObject(GameObject _target)
        {
            if (_target == null) return null;

            _target.GetComponentsInChildren(true, sourceBuffer);
            foreach (var b in sourceBuffer)
            {
                var c = b.clip;
                if (c != null && c.loadType != AudioClipLoadType.Streaming && c.loadState != AudioDataLoadState.Loaded)
                {
                    if (c.loadState == AudioDataLoadState.Unloaded)
                        c.LoadAudioData();

                    clipBuffer.Add(b.clip);
                }
            }

            sourceBuffer.Clear();

            if (clipBuffer.Count > 0)
            {
                var result = new AudioClip[clipBuffer.Count];

                var index = 0;
                foreach (var clip in clipBuffer)
                {
                    result[index] = clip;
                    ++index;
                }
                
                clipBuffer.Clear();

                return result;
            }

            return null;
        }

        private void FlushGameObjectEventsAfterAudioLoaded(string _path)
        {
            //这种情况下，所有事件都存在了callbacks里面
            if (DirectInstance.callbacksFastPath.Contains(_path) && loadingRequest.TryGetValue(_path, out var r))
            {
                var handle = r.handle;
                DirectInstance.callbacksFastPath.Remove(_path);

                externalEventsQueryBuffer.Clear();

                DirectInstance.callbacks.FindAllValuesOfKey(_path, externalEventsQueryBuffer);
                DirectInstance.callbacks.RemoveKey(_path);

                foreach (var callback in externalEventsQueryBuffer)
                {
                    callback?.Invoke((_path, handle.Result as GameObject, handle.OperationException));
                }

                externalEventsQueryBuffer.Clear();
            }
        }

        #endregion

        public T GetAsset<T>(string _path) where T : Object
        {
            if (loadingRequest.TryGetValue(_path, out var r))
            {
                if (r.handle.IsValid() && r.handle.IsDone)
                {
                    if (r.handle.Result is T result)
                    {
                        return result;
                    }
                    else
                    {
                        Debug.LogWarning($"资源{_path}加载完毕，但类型并不是{typeof(T).Name}，因此返回空。");
                        return null;
                    }
                }
                else
                {
                    Debug.LogWarning($"资源{_path}尚未加载完毕，因此返回空。");
                    return null;
                }
            }
            else
            {
                Debug.LogWarning($"资源{_path}没有加载，因此返回空。");
                return null;
            }
        }

        /// <summary>
        /// 释放指定Addressable路径的资源，请求，以及合并的请求
        /// </summary>
        /// <param name="_path">Addressable路径</param>
        /// <param name="_unloadByDesign">如果该资源涉及合并请求，卸载其中一个指令会导致其他事件都被释放。如果是故意这么做(例如UnloadAll)，则设置为TRUE</param>
        public void Unload(string _path, bool _unloadByDesign = false)
        {
            if (loadingRequest.TryGetValue(_path, out var r))
            {
                loadingRequest.Remove(_path);

                if (r.handle.IsValid())
                {
                    Addressables.Release(r.handle);
                }
            }

            if (callbacksFastPath.Contains(_path))
            {
                if (!_unloadByDesign)
                {
                    Debug.LogWarning("如果一个资源需要手动独立释放，则暂时不要调用多事件回调。为了避免出现错误，已经将所有相关事件全部清理。");
                }

                callbacks.RemoveKey(_path);
                callbacksFastPath.Remove(_path);
            }

            audioWarmingProcess.Remove(_path);
        }

        private void UnloadAll()
        {
            var loadingArray = loadingRequest.Values.ToArray();
            loadingRequest.Clear();
            callbacks.Clear();
            callbacksFastPath.Clear();
            audioWarmingProcess.Clear();

            foreach (var r in loadingArray)
            {
                if (r.handle.IsValid())
                    Addressables.Release(r.handle);
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("功能入口/查询/已加载资源清单")]
        public static void LogCurrentExistedObjects()
        {
            if (DirectInstance != null)
            {
                foreach (var loaded in DirectInstance.loadingRequest)
                {
                    if (loaded.Value.handle.IsDone)
                        Debug.Log(loaded.Key);
                }
            }
        }
#endif
    }
}