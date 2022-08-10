using UnityEngine;
using System.Collections.Generic;
using ZTools.PoolingNS;
using UnityEngine.SceneManagement;
using System;
using UnityEditor;
using Object = UnityEngine.Object;

namespace ZTools.ResourceManagerNS
{
    /// <summary>
    /// 对象池资源管理
    /// </summary>
    /// <typeparam name="Tkey">键值类型，不允许是可为空的类型，string除外</typeparam>
    public abstract class PooledSource<TClass, Tkey> : Source<TClass, Tkey, GameObject>
        where TClass : PooledSource<TClass, Tkey>, new()
    {
        #region 变量定义

        private Dictionary<Tkey, GameObjectPool> pooling;
        private Scene resourcesScene;
        private GameObject copyedAssetsRoot;

        protected int defaultPoolCount;
        protected int autoExpandCount;
        protected bool autoExpand;
        protected bool allowNullReturn;
        protected bool dontReparent;
        protected bool copyAssets;
        protected bool shouldHidePools;

        #endregion

        #region 复写 Singleton 生命周期

        /// <summary>
        /// 复写加载函数
        /// 在执行完基础的加载函数上，
        /// 初始化PooledSource所需的额外列表与字典
        /// 同时设置初始大小等属性
        /// </summary>
        public override void Load()
        {
            base.Load();

            //尝试获取PooledResourcesConfigAttribute信息
            var attributes = GetType().GetCustomAttributes(typeof(PooledSourceConfigAttribute), false);

            string resourcesSceneName = null;

            if (attributes.Length > 0)
            {
                (bool mobileOverride, int defaultCount, int expandCount) expand = (false, 0, 0);
                
#if USE_URP
                var mobile = GetType().GetCustomAttributes(typeof(MobileSourceConfigAttribute), false);
                if (mobile.Length > 0)
                {
                    var mobileConfig = (MobileSourceConfigAttribute) mobile[0];
                    expand = (true, mobileConfig.OverridePoolCount, mobileConfig.OverrideExpandCount);
                }
#endif
                
                var config = ((PooledSourceConfigAttribute) attributes[0]);
                defaultPoolCount = expand.mobileOverride ? expand.defaultCount : config.DefaultPoolCount;
                autoExpand = config.AutoExpand;
                autoExpandCount = expand.mobileOverride ? expand.expandCount : config.AutoExpandCount;
                allowNullReturn = config.AllowNullReturn;
                resourcesSceneName = config.ResoucesScene;
                dontReparent = config.DontReparent;
                copyAssets = config.CopyAssets;

#if UNITY_EDITOR
                if (config.HideFlagNum == 0)
                    shouldHidePools = false;
                else
                    shouldHidePools = (EditorPrefs.GetInt("pool_hide_flag", 0) & config.HideFlagNum) ==
                                      config.HideFlagNum;
#endif
            }
            else
            {
                defaultPoolCount = 8;
                autoExpand = true;
                autoExpandCount = 1;
                allowNullReturn = false;
                resourcesSceneName = "Rs";
                dontReparent = false;
                copyAssets = false;
                shouldHidePools = false;
            }

            pooling = new Dictionary<Tkey, GameObjectPool>();
            Scene s = SceneManager.GetSceneByName(resourcesSceneName);
            if (s.IsValid())
                resourcesScene = s;
            else
            {
                Scene activeScene = SceneManager.GetActiveScene();
                resourcesScene = SceneManager.CreateScene(resourcesSceneName);

                if (activeScene.isLoaded)
                {
                    SceneManager.SetActiveScene(activeScene);
                }
            }

            if (copyAssets)
            {
                copyedAssetsRoot = new GameObject("COPY_ASSETS_OF_" + this.GetType().Name);
                SceneManager.MoveGameObjectToScene(copyedAssetsRoot, resourcesScene);
            }
        }

        public override void Reload()
        {
            if (copyAssets)
            {
                foreach (var pool in pooling.Values)
                {
                    var ast = pool.Asset;
                    pool.Destroy();

                    if (ast != null)
                        GameObject.Destroy(ast);
                }
            }
            else
            {
                foreach (var pool in pooling.Values)
                {
                    pool.Destroy();
                }
            }

            pooling.Clear();
            base.Reload();
        }

        public override void Unload()
        {
            if (copyAssets)
            {
                foreach (var pool in pooling.Values)
                {
                    if (pool != null)
                    {
                        var ast = pool.Asset;
                        pool.Destroy();

                        if (ast != null)
                            GameObject.Destroy(ast);
                    }
                }
            }
            else
            {
                foreach (var pool in pooling.Values)
                {
                    pool?.Destroy();
                }
            }

            pooling = null;

            if(copyedAssetsRoot != null)
            {
                GameObject.Destroy(copyedAssetsRoot);
            }

            base.Unload();
        }

        #endregion

        #region 功能函数

        /// <summary>
        /// 把旗下所有的资源移动到某个特定场景中
        /// </summary>
        /// <param name="_scene"></param>
        public void MoveToScene(Scene _scene)
        {
            resourcesScene = _scene;
            foreach (var pool in pooling.Values)
                pool.MoveToScene(resourcesScene);
        }

        /// <summary>
        /// 复写获取资源
        /// 不再返回原始资源
        /// 而是从对象池里面取出指定的对象
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public override bool Get(Tkey _key, out GameObject _resources)
        {
            if (pooling.TryGetValue(_key, out var pool) && pool != null)
            {
                _resources = pool.Get();
                return _resources != null || allowNullReturn;
            }

            _resources = null;
            return false;
        }

        /// <summary>
        /// 查询指定某个Key所对应的对象池大小
        /// </summary>
        /// <param name="_key"></param>
        /// <returns></returns>
        public (int usingCount, int totalCount) QueryPoolUsage(Tkey _key)
        {
            return pooling.TryGetValue(_key, out var pool) ? (pool.ActiveCount, pool.Count) : (0, 0);
        }

        /// <summary>
        /// 获取队列中的资源，但是又不触发对象池的移除和回收
        /// 和GetAssetOnly类似，只是不会直接操纵Asset，避免Editor换景下的错误
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_resources"></param>
        /// <returns></returns>
        public bool GetFirstWithoutPool(Tkey _key, out GameObject _resources)
        {
            if (pooling.ContainsKey(_key))
            {
                var pool = pooling[_key];
                if (pool == null)
                {
                    _resources = null;
                    return false;
                }
                else
                {
                    _resources = pooling[_key].GetFirstObjectWithoutPooling();

                    if (_resources == null)
                    {
                        //如果允许返回空值，那么即便是为空，也应当返回true
                        return allowNullReturn;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                _resources = null;
                return false;
            }
        }

        /// <summary>
        /// 仅仅获取资源
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_resources"></param>
        /// <returns></returns>
        protected bool GetAssetOnly(Tkey _key, out GameObject _resources)
        {
            return base.Get(_key, out _resources);
        }

        /// <summary>
        /// 对象池专属的返还资源
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_gameObject"></param>
        public virtual void Return(Tkey _key, GameObject _gameObject)
        {
            pooling[_key].Return(_gameObject);
        }

        #endregion

        #region 资源加载与卸载

        /// <summary>
        /// 复写卸载资源的函数
        /// 在基本的卸载函数之前
        /// 要销毁指定的对象池
        /// </summary>
        /// <param name="_key"></param>
        public new void Unload(Tkey _key)
        {
            if (pooling.TryGetValue(_key, out var pool))
            {
                pool.Destroy();
                pooling.Remove(_key);
            }

            base.Unload(_key);
        }

        public new void UnloadAll()
        {
            foreach (var pool in pooling.Values)
            {
                pool.Destroy();
            }

            pooling.Clear();

            base.UnloadAll();
        }

        protected virtual int GetOverridePoolCount(Tkey _key)
        {
            return 1;
        }

        /// <summary>
        /// 复写当资源加载完毕后的回调函数
        /// 需要额外执行创建对象池的功能
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_path"></param>
        /// <param name="_asset"></param>
        protected override void OnAssetLoaded(Tkey _key, string _path, GameObject _asset)
        {
            base.OnAssetLoaded(_key, _path, _asset);
            try
            {
                var poolSize = defaultPoolCount;

                if (poolSize < 0)
                {
                    poolSize = GetOverridePoolCount(_key);
                }

                if (copyAssets)
                {
                    var newAsset = Object.Instantiate(_asset, copyedAssetsRoot.transform);
                    OnAssetsCopyed(ref newAsset);
                    newAsset.SetActive(false);

                    var pool = new GameObjectPool(newAsset, poolSize, false, dontReparent,
                        _hideInEditor: shouldHidePools) {AutoExpand = autoExpand, AutoExpandCount = autoExpandCount};
                    pool.MoveToScene(resourcesScene);
                    pooling.Add(_key, pool);
                }
                else
                {
                    var pool = new GameObjectPool(_asset, poolSize, false, dontReparent, _hideInEditor: shouldHidePools)
                    {
                        AutoExpand = autoExpand, AutoExpandCount = autoExpandCount
                    };
                    pool.MoveToScene(resourcesScene);
                    pooling.Add(_key, pool);
                }
            }
            catch (Exception _e)
            {
                Debug.LogException(_e);
                Debug.LogError($"创建物体时发生错误，资源不存在。类型{this.GetType().ToString()}, ID{_key}, 资源路径{_path}");
            }
        }

        protected GameObjectPool GetPool(Tkey _key)
        {
            return pooling[_key];
        }

        protected virtual void OnAssetsCopyed(ref GameObject _assets)
        {

        }

        #endregion
    }

    /// <summary>
    /// 描述对象池的基本数据
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class PooledSourceConfigAttribute : Attribute
    {
        /// <summary>
        /// 默认的对象池大小
        /// </summary>
        public int DefaultPoolCount { get; private set; }

        /// <summary>
        /// 当数量不足时，自动扩展的池子大小
        /// </summary>
        public int AutoExpandCount { get; private set; }

        /// <summary>
        /// 是否允许自动扩展
        /// </summary>
        public bool AutoExpand
        {
            get
            {
                return AutoExpandCount > 0;
            }
        }

        /// <summary>
        /// 是否允许返回NULL值
        /// </summary>
        public bool AllowNullReturn { get; private set; }

        /// <summary>
        /// 对象池存放的场景名称
        /// </summary>
        public string ResoucesScene { get; private set; }

        /// <summary>
        /// 对象池是否采用DontReparent模式
        /// </summary>
        public bool DontReparent { get; private set; }

        /// <summary>
        /// 是否实例化一份Assets，并保持监听
        /// </summary>
        public bool CopyAssets { get; private set; }
        
        /// <summary>
        /// 隐藏对象池的枚举编号。（只针对编辑器）
        /// </summary>
        public int HideFlagNum { get; private set; }

        /// <summary>
        /// 构造一个对象池资源管理器的限制条件
        /// </summary>
        /// <param name="_defaultPoolCount">默认的对象池大小</param>
        /// <param name="_autoExpandCount">数量不足时的对象池扩展大小，使得对象池总是有足够的元素可以使用。如果为0，则表示不允许扩展</param>
        /// <param name="_allowNullReturn">是否允许返回NULL值。</param>
        /// <param name="_resourcesScene">放置的场景名称</param>
        /// <param name="_dontReparent">对象池是否采用DontReparent模式，如果对象取出后就没移动过，那么应该采取这样的模式</param>
        public PooledSourceConfigAttribute(int _defaultPoolCount, int _autoExpandCount, bool _allowNullReturn, string _resourcesScene = "Rs", bool _dontReparent = false, bool _copyAssets = false, int _hideFlagNum = 0)
        {
            DefaultPoolCount = _defaultPoolCount;
            AutoExpandCount = _autoExpandCount;
            AllowNullReturn = _allowNullReturn;
            ResoucesScene = _resourcesScene;
            DontReparent = _dontReparent;
            CopyAssets = _copyAssets;
            HideFlagNum = _hideFlagNum;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class MobileSourceConfigAttribute : Attribute
    {
        public int OverridePoolCount;
        public int OverrideExpandCount;

        public MobileSourceConfigAttribute(int overridePoolCount, int overrideExpandCount)
        {
            OverridePoolCount = overridePoolCount;
            OverrideExpandCount = overrideExpandCount;
        }
    }
}
