#if UNITY_ADDRESSABLE


using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ZTools.ResourceManagerNS
{
#pragma warning disable 649

    public delegate void AssetReferenceTrackerCallbackDelegate<TObject>(AssetReferenceTracker<TObject> tracker) where TObject : UnityEngine.Object;

    public struct AssetReferenceTracker<TObject> where TObject : UnityEngine.Object
    {
        private struct Pair
        {
            public AssetReferenceTracker<TObject> tracker;
            public AssetReferenceTrackerCallbackDelegate<TObject> callback;

            public Pair(AssetReferenceTracker<TObject> _tracker, AssetReferenceTrackerCallbackDelegate<TObject> _callback)
            {
                tracker = _tracker;
                callback = _callback;
            }
        }
        private static Dictionary<AsyncOperationHandle, Pair> loadingTrackers = new Dictionary<AsyncOperationHandle, Pair>();

        private AsyncOperationHandle<TObject> handle;

        private static void OnCompleted(AsyncOperationHandle<TObject> _handle)
        {
            _handle.Completed -= OnCompleted;

            if (loadingTrackers.TryGetValue(_handle, out var pair))
            {
                pair.callback?.Invoke(pair.tracker);
                loadingTrackers.Remove(_handle);
            }
        }

        internal AssetReferenceTracker(AssetReferenceT<TObject> _reference, AssetReferenceTrackerCallbackDelegate<TObject> _callback)
        {
            handle = _reference.LoadAssetAsync();

            loadingTrackers.Add(handle, new Pair(this, _callback));
            handle.Completed += OnCompleted;
        }

        internal AssetReferenceTracker(string _key, AssetReferenceTrackerCallbackDelegate<TObject> _callback)
        {
            handle = Addressables.LoadAssetAsync<TObject>(_key);

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

                Addressables.Release(temp);
            }
        }

        public TObject AsAsset()
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

    public static class AssetReferenceTrackerExtension
    {
        public static AssetReferenceTracker<TObject> LoadAssetAsyncAndTrack<TObject>(this AssetReferenceT<TObject> _reference, AssetReferenceTrackerCallbackDelegate<TObject> _callback = null) where TObject : UnityEngine.Object
        {
            return new AssetReferenceTracker<TObject>(_reference, _callback);
        }

        public static AssetReferenceTracker<TObject> LoadAssetAsyncAndTrack<TObject>(this string _key, AssetReferenceTrackerCallbackDelegate<TObject> _callback = null) where TObject : UnityEngine.Object
        {
            return new AssetReferenceTracker<TObject>(_key, _callback);
        }
    }
    #pragma warning restore 649
}


#endif