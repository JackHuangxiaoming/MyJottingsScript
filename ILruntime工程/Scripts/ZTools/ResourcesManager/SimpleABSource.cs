using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZTools.SingletonNS;
using UnityEngine.AddressableAssets;

namespace ZTools.ResourceManagerNS
{
    public enum Existence
    {
        NotSure,
        NotExist,
        Exist
    }

    internal static class SimpleABSourceCache
    {
        public static readonly List<string> removeCache = new List<string>(4);
        public static int prevLifeID = 0;
    }

    public abstract class SimpleABSource<TClass, TResource> : Singletons<TClass>
        where TResource : Object
        where TClass : SimpleABSource<TClass, TResource>, new()
    {
        protected abstract string GetAddressableKey(string _path);
        protected abstract string ErrorKey { get; }
        protected abstract int StreamingCapacity { get; }

        private HashSet<string> requestedKeys;

        protected Dictionary<string, Existence> existences;
        private Queue<ArrayList> existenceQueryCache;
        protected int lifeID { get; private set; }

        public override void Load()
        {
            requestedKeys = new HashSet<string>();
            existences = new Dictionary<string, Existence>();
            existenceQueryCache = new Queue<ArrayList>();
            lifeID = ++SimpleABSourceCache.prevLifeID;
        }

        public override void Reload()
        {
            foreach (var key in requestedKeys)
            {
                ResourceContainer.Instance.Unload(key);
            }

            requestedKeys.Clear();
        }

        public override void ReleaseMemories()
        {
            if (ResourceContainer.DirectInstance != null)
            {
                foreach (var key in requestedKeys)
                {
                    ResourceContainer.DirectInstance.Unload(key);
                }
            }

            requestedKeys = null;
        }

        public Existence TryGetExist(string _addressableKey)
        {
            _addressableKey = GetAddressableKey(_addressableKey);
            
            if (existences.TryGetValue(_addressableKey, out var result))
            {
                return result;
            }
            else
            {
                existences[_addressableKey] = Existence.NotSure;
                
                var queryList = existenceQueryCache.Count > 0 ? existenceQueryCache.Dequeue() : new ArrayList(1);
                queryList.Add(_addressableKey);
                var k = _addressableKey;
                var lifeIDCache = lifeID;
                
                Addressables.LoadResourceLocationsAsync(queryList, Addressables.MergeMode.Union, typeof(TResource))
                        .Completed +=
                    _handle =>
                    {
                        if (DirectInstance != null && DirectInstance.lifeID == lifeIDCache &&
                            DirectInstance.existences.ContainsKey(k))
                        {
                            DirectInstance.existences[k] =
                                (_handle.IsValid() && _handle.Result != null && _handle.Result.Count > 0)
                                    ? Existence.Exist
                                    : Existence.NotExist;

                            DirectInstance.existenceQueryCache.Enqueue(queryList);
                        }

                        Addressables.Release(_handle);
                        queryList.Clear();
                    };

                return Existence.NotSure;
            }
        }

        public TResource TryGetAsset(string _addressableKey)
        {
            _addressableKey = GetAddressableKey(_addressableKey);

            //如果为空，返回空
            if (string.IsNullOrEmpty(_addressableKey))
                return null;

            if (existences.TryGetValue(_addressableKey, out var exist) && exist != Existence.Exist)
                return null;

            if (ResourceContainer.Instance.IsLoaded(_addressableKey))
            {
                return ResourceContainer.DirectInstance.GetAsset<TResource>(_addressableKey);
            }

            if (ResourceContainer.DirectInstance.IsLoading(_addressableKey))
            {
                return null;
            }

            if (!requestedKeys.Contains(_addressableKey))
            {
                requestedKeys.Add(_addressableKey);

                //大于0表示是一个LOOP
                //否则就不是
                if (StreamingCapacity > 0)
                {
                    var removeCache = SimpleABSourceCache.removeCache;
                    removeCache.Clear();

                    var toRemoveCount = StreamingCapacity - requestedKeys.Count + 1;

                    foreach (var key in requestedKeys)
                    {
                        if (toRemoveCount > 0)
                        {
                            ResourceContainer.DirectInstance.Unload(key);
                            SimpleABSourceCache.removeCache.Add(key);
                            --toRemoveCount;
                        }
                        else
                        {
                            break;
                        }
                    }

                    foreach (var cache in removeCache)
                    {
                        requestedKeys.Remove(cache);
                    }

                    removeCache.Clear();
                }
            }

            ResourceContainer.DirectInstance.LoadFromResources<TResource>(_addressableKey);
            return null;
        }
        
        public override void LateUpdate() { }

        public override void Unload()
        {
            existences = null;
        }
        public override void FixedUpdate() { }
        public override void Update() { }
    }
}