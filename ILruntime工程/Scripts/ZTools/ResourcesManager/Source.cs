using System.Collections;
using System.Collections.Generic;
using ZTools.SingletonNS;
using System;
using Unity.Collections;
using UnityEngine;

namespace ZTools.ResourceManagerNS
{
    /// <summary>
    /// 适用于加载Unity单项资源的通用管理器
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TResource"></typeparam>
    public abstract class Source<TClass, TKey, TResource> : Singletons<TClass>
        where TResource : UnityEngine.Object
        where TClass : Source<TClass, TKey, TResource>, new()
    {
        #region 变量定义

        public static int maxStreamingCount = 5;

        /// <summary>
        /// （已加载的）资源管理列表
        /// </summary>
        private Dictionary<TKey, KeyValuePair<string, TResource>> resources;

        /// <summary>
        /// 不同ID对应着同一个Path的请求映射表
        /// 当其中任何一个ID加载完毕时，会自动填充其他的
        /// 这样解决几个异步请求的问题
        /// </summary>
        private Dictionary<string, HashSet<TKey>> samePathDifferentKeyRequests;

        private Queue<TKey> streamingAssets;

        #endregion

        #region 事件定义

        /// <summary>
        /// 当库中有新的资源完成加载时
        /// </summary>
        public event Action<TKey> onNewAssetLoaded;

        #endregion

        #region 实现 Singleton

        public override void FixedUpdate()
        {
        }

        public override void LateUpdate()
        {
        }

        public override void Load()
        {
            resources = new Dictionary<TKey, KeyValuePair<string, TResource>>();
            samePathDifferentKeyRequests = new Dictionary<string, HashSet<TKey>>();
            streamingAssets = new Queue<TKey>();
        }

        public override void Reload()
        {
            UnloadAll();
        }

        public override void Unload()
        {
            UnloadAll();

            resources = null;
            samePathDifferentKeyRequests = null;
            streamingAssets = null;
        }

        public override void Update()
        {
        }

        #endregion

        #region 加载与卸载

        /// <summary>
        /// 根据键值进行加载
        /// </summary>
        /// <param name="_key">键值</param>
        /// <param name="_async">是否异步</param>
        public void Load(TKey _key)
        {
            if (!IsLoadingOrCompetelyLoaded(_key))
            {
                Load(_key, GetPathByKey(_key));
            }
        }

        public IEnumerator LoadAllAsync(ICollection<TKey> _keys)
        {
            if (_keys == null || _keys.Count == 0)
                yield break;

            var toLoad = new List<TKey>(_keys);

            var counter = 0;
            var targetCount = _keys.Count;

            while (counter < targetCount)
            {
                for (var i = 0; i < toLoad.Count; ++i)
                {
                    if (IsCompetelyLoaded(toLoad[i]))
                    {
                        ++counter;
                        //toLoad.RemoveAtSwapBack(i);
                        --i;
                    }
                    else
                    {
                        Load(toLoad[i]);
                    }
                }

                yield return null;
            }
        }

        /// <summary>
        /// 加载键值与路径共同确定的资源
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_path"></param>
        /// <param name="_async"></param>
        protected void Load(TKey _key, string _path)
        {
            if (_path == null)
                return;

            //如果从来没有加载过，则加载
            if (!resources.ContainsKey(_key))
            {
                //占位
                resources.Add(_key, new KeyValuePair<string, TResource>(_path, null));

                if (ResourceContainer.Instance.IsLoaded(_path))
                {
                    OnLoadComplete(_key, _path, ResourceContainer.Instance.GetAsset<TResource>(_path));
                }
                else if (ResourceContainer.Instance.IsLoading(_path))
                {
                    if (!samePathDifferentKeyRequests.ContainsKey(_path))
                        samePathDifferentKeyRequests.Add(_path, new HashSet<TKey>());

                    samePathDifferentKeyRequests[_path].Add(_key);
                }
                else
                {
                    var k = _key;

                    ResourceContainer.Instance.LoadFromResources<TResource>(_path, (x) =>
                    {
                        //如果本脚本尚未被清理掉
                        //（避免发生抢占线程的情况）
                        if (resources != null && resources.ContainsKey(k))
                        {
                            OnLoadComplete(k, x.path, x.asset as TResource);
                        }
                    });
                }
            }
        }

        /// <summary>
        /// 根据键值进行卸载
        /// </summary>
        /// <param name="_key"></param>
        public void Unload(TKey _key)
        {
            if (resources.TryGetValue(_key, out var v))
            {
                var path = v.Key;
                resources.Remove(_key);
                ResourceContainer.DirectInstance?.Unload(path);
            }
        }

        /// <summary>
        /// 卸载全部资源
        /// </summary>
        public void UnloadAll()
        {
            if (resources != null)
            {
                if (ResourceContainer.DirectInstance != null)
                {
                    var res = ResourceContainer.DirectInstance;
                    foreach (var loaded in resources)
                    {
                        res.Unload(loaded.Value.Key, true);
                    }
                }

                resources.Clear();
            }

            streamingAssets.Clear();
            samePathDifferentKeyRequests.Clear();
            onNewAssetLoaded = null;
        }

        /// <summary>
        /// 键值所对应的资源是否已经加载完毕
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public bool IsCompetelyLoaded(TKey _key)
        {
            return resources.TryGetValue(_key, out var v) && v.Value != null;
        }

        /// <summary>
        /// 键值所对应的资源是否正在加载
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public bool IsLoading(TKey _key)
        {
            return resources.TryGetValue(_key, out var v) && v.Value == null;
        }

        /// <summary>
        /// 键值所对应的资源是否正在加载，或者已经加载完毕
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public bool IsLoadingOrCompetelyLoaded(TKey _key)
        {
            return resources.ContainsKey(_key);
        }

        private void OnLoadComplete(TKey _key, string _path, TResource _asset)
        {
            if (samePathDifferentKeyRequests.TryGetValue(_path, out var list))
            {
                samePathDifferentKeyRequests.Remove(_path);
                list.Add(_key);
                foreach (var key in list)
                {
                    OnAssetLoaded(key, _path, _asset);
                }
            }
            else
            {
                OnAssetLoaded(_key, _path, _asset);
            }
        }

        /// <summary>
        /// 当资源加载完毕后的回调函数
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_path"></param>
        /// <param name="_asset"></param>
        protected virtual void OnAssetLoaded(TKey _key, string _path, TResource _asset)
        {
            resources[_key] = new KeyValuePair<string, TResource>(_path, _asset);
            // var oldPair = resources[_key];
            // var newPair = new KeyValuePair<string, TResource>(oldPair.Key, _asset);
            // resources[_key] = newPair;

            onNewAssetLoaded?.Invoke(_key);
        }

        /// <summary>
        /// 返回键值对应的路径，以支持只根据键值，就能加载的功能
        /// 每个子类必须实现
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        protected abstract string GetPathByKey(TKey _key);

        #endregion

        #region 访问函数

        /// <summary>
        /// 根据键值获取资源
        /// </summary>
        /// <param name="_key">键值</param>
        /// <param name="_resources">资源，根据Config的配置，可能返回NULL</param>
        /// <returns>返回是否获取成功</returns>
        public virtual bool Get(TKey _key, out TResource _resources)
        {
            if (resources.TryGetValue(_key, out var v))
            {
                _resources = v.Value;
                return _resources != null;
            }

            _resources = null;
            return false;
        }

        #endregion
    }
}