#if UNITY_ADDRESSABLE


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ZTools.ResourceManagerNS
{
#pragma warning disable 649

    public delegate void AssetReferenceGameObjectTrackerCallbackDelegate(AssetReferenceGameObjectTracker tracker);

    public struct AssetReferenceGameObjectTracker
    {
        private struct Pair
        {
            public AssetReferenceGameObjectTracker tracker;
            public AssetReferenceGameObjectTrackerCallbackDelegate callback;

            public Pair(AssetReferenceGameObjectTracker _tracker, AssetReferenceGameObjectTrackerCallbackDelegate _callback)
            {
                tracker = _tracker;
                callback = _callback;
            }
        }
        private static Dictionary<AsyncOperationHandle, Pair> loadingTrackers = new Dictionary<AsyncOperationHandle, Pair>();

        private static void OnCompleted(AsyncOperationHandle<GameObject> _handle)
        {
            _handle.Completed -= OnCompleted;

            if (loadingTrackers.TryGetValue(_handle, out var pair))
            {
                pair.callback?.Invoke(pair.tracker);
                loadingTrackers.Remove(_handle);
            }
        }

        private AsyncOperationHandle<GameObject> handle;

        internal AssetReferenceGameObjectTracker(AssetReferenceGameObject _reference, AssetReferenceGameObjectTrackerCallbackDelegate _callback, Transform _parent)
        {
            handle = _reference.InstantiateAsync(_parent);

            loadingTrackers.Add(handle, new Pair(this, _callback));
            handle.Completed += OnCompleted;
        }

        internal AssetReferenceGameObjectTracker(string _key, AssetReferenceGameObjectTrackerCallbackDelegate _callback, Transform _parent)
        {
            handle = Addressables.InstantiateAsync(_key, _parent);

            loadingTrackers.Add(handle, new Pair(this, _callback));
            handle.Completed += OnCompleted;
        }

        public void Release()
        {
            if (handle.IsValid())
            {
                var temp = handle;

                if (loadingTrackers.TryGetValue(handle, out var pair))
                {
                    loadingTrackers.Remove(handle);

                    handle.Completed -= OnCompleted;
                    handle = default;

                    pair.callback?.Invoke(this);
                }
                else
                {
                    handle.Completed -= OnCompleted;
                    handle = default;
                }

                Addressables.ReleaseInstance(temp);
            }
        }

        public GameObject AsGameObject()
        {
            return handle.IsValid() ? handle.Result : null;
        }

        public bool IsValid
        {
            get
            {
                return handle.IsValid();
            }
        }

        public bool IsReady
        {
            get
            {
                return handle.IsValid() && handle.Status != AsyncOperationStatus.None;
            }
        }
    }

    public static class AssetReferenceGameObjectExtension
    {
        /// <summary>
        /// 异步加载一个实例，并创建一个用于跟踪的Tracker。
        /// 一旦通过这个方法创建了Tracker，则必须使用该Tracker进行Release。
        /// </summary>
        /// <param name="_reference">GameObject Addressable资源引用</param>
        /// <param name="_callback">回调</param>
        /// <param name="_parent">父节点</param>
        /// <returns></returns>
        public static AssetReferenceGameObjectTracker InstantiateAsyncAndTrack(this AssetReferenceGameObject _reference, AssetReferenceGameObjectTrackerCallbackDelegate _callback = null, Transform _parent = null)
        {
            return new AssetReferenceGameObjectTracker(_reference, _callback, _parent);
        }

        /// <summary>
        /// 异步加载一个实例，并创建一个用于跟踪的Tracker。
        /// 一旦通过这个方法创建了Tracker，则必须使用该Tracker进行Release。
        /// </summary>
        /// <param name="_reference">GameObject Addressable资源引用</param>
        /// <param name="_callback">回调</param>
        /// <param name="_parent">父节点</param>
        /// <returns></returns>
        public static AssetReferenceGameObjectTracker InstantiateAsyncAndTrack(this string _reference, AssetReferenceGameObjectTrackerCallbackDelegate _callback = null, Transform _parent = null)
        {
            return new AssetReferenceGameObjectTracker(_reference, _callback, _parent);
        }
    }
    #pragma warning restore 649
}


#endif