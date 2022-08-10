using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using UnityEngine.Jobs;
using Unity.Collections;

namespace ZTools.PoolingNS
{
    public class GameObjectPool
    {
        private struct DelayedReturnCommand
        {
            public int minReturnFrame;
            public GameObject gameObject;
        }


        private GameObject asset;
        private Transform root;
        private int totalCount;
        private int activeCount;
        private bool oneFrameDelay;
        private bool hideInEditor;
        private List<GameObject> objectInsideThisPool;
        private Queue<DelayedReturnCommand> delayedCommands;

        private TransformAccessArray transformAccess;

        public TransformAccessArray TransformArray
        {
            get
            {
                if (!transformAccess.isCreated)
                {
                    transformAccess = new TransformAccessArray(objectInsideThisPool.Count);
                    foreach (var obj in objectInsideThisPool)
                    {
                        transformAccess.Add(obj.transform);
                    }
                }

                return transformAccess;
            }
        }

        /// <summary>
        /// 获取一个临时的针对所有Active物体的TransformAccessArray
        /// 每次访问都会生成一个
        /// 用完之后务必Dispose掉！
        /// </summary>
        public TransformAccessArray TempActiveTransformArray
        {
            get
            {
                var activeTransformAccess = new TransformAccessArray(activeCount);

                var counter = activeCount;
                foreach (var obj in objectInsideThisPool)
                {
                    if (obj.activeSelf)
                    {
                        --counter;
                        activeTransformAccess.Add(obj.transform);

                        if (counter <= 0)
                            break;
                    }
                }

                return activeTransformAccess;
            }
        }

        public bool AutoExpand
        {
            get;
            set;
        }

        public int AutoExpandCount
        {
            get;
            set;
        }

        public bool AssetOnly
        {
            get; private set;
        }

        public bool DontReparent
        {
            get; private set;
        }

        public bool OneFrameDelay
        {
            set
            {
                oneFrameDelay = value;
                if (value)
                {
                    delayedCommands = new Queue<DelayedReturnCommand>();
                }
                else
                    delayedCommands = null;
            }
        }

        public int Count
        {
            get
            {
                { return totalCount; }
            }
        }

        public int ActiveCount
        {
            get
            {
                return activeCount;
            }
        }

        public int SleepCount
        {
            get
            {
                return totalCount - activeCount;
            }
        }

        public GameObject Asset { get { return asset; } }

        /// <summary>
        /// 创建一个对象池，传入的GameObject必须是内存当中的Asset
        /// </summary>
        /// <param name="_asset"></param>
        /// <param name="_initCount"></param>
        public GameObjectPool(GameObject _asset, int _initCount, bool _assetOnly = false, bool _dontReparent = false, GameObject _parent = null, bool _hideInEditor = false)
        {
            if (_asset == null)
                throw new System.Exception("传入的资源为空，不能生成对象池");

            if (!_assetOnly)
            {
                objectInsideThisPool = new List<GameObject>();
                var poolName = "Pool_" + _asset.name;
                var rootObject = _parent == null ? new GameObject(poolName) : _parent;
                root = rootObject.transform;
                //root.hierarchyCapacity = _initCount;
#if UNITY_EDITOR
                hideInEditor = _hideInEditor;
                root.hideFlags = hideInEditor ? HideFlags.HideInHierarchy : HideFlags.None;
#endif
            }

            asset = _asset;

            AssetOnly = _assetOnly;
            DontReparent = _dontReparent;

            Expand(_initCount);
        }

        public void MoveToScene(Scene _scene)
        {
            if (!AssetOnly)
                SceneManager.MoveGameObjectToScene(root.gameObject, _scene);
        }

        public void DontDestroyOnLoad()
        {
            if (!AssetOnly)
                GameObject.DontDestroyOnLoad(root.gameObject);
        }

        public void Destroy()
        {
            if (transformAccess.isCreated)
                transformAccess.Dispose();

            activeCount = 0;

            if (!AssetOnly)
            {
                foreach (var obj in objectInsideThisPool)
                {
                    if (obj != null)
                    {
                        GameObject.Destroy(obj);
                    }
                }

                objectInsideThisPool = null;
                if (root != null && root.gameObject != null)
                    GameObject.Destroy(root.gameObject);
            }

            asset = null;
            delayedCommands = null;
            oneFrameDelay = false;
        }

        /// <summary>
        /// 强制重新回收全部子元素
        /// </summary>
        public void RecollectAll()
        {
            if (!AssetOnly)
            {
                if (DontReparent)
                {
                    foreach (var obj in objectInsideThisPool)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
                else
                {
                    foreach (var obj in objectInsideThisPool)
                    {
                        if (obj != null && obj.transform.parent != root)
                        {
                            obj.SetActive(false);
                            obj.transform.parent = root;
                        }
                    }
                }

                activeCount = 0;
            }
        }

        public void Expand(int _count)
        {
            if (!AssetOnly)
            {
                var name = asset.name;
                var writeIntoArray = transformAccess.isCreated;

                for (int i = 0; i < _count; ++i)
                {
                    var newObject = GameObject.Instantiate(asset, root);
                    newObject.SetActive(false);
                    newObject.name = string.Concat(name, '_', totalCount);
#if UNITY_EDITOR
                    newObject.hideFlags = hideInEditor ? HideFlags.HideInHierarchy : HideFlags.None;
#endif
                    objectInsideThisPool.Add(newObject);
                    ++totalCount;

                    if (writeIntoArray)
                        transformAccess.Add(newObject.transform);
                }
            }
        }

        public void Return(GameObject _object)
        {
            if (!AssetOnly)
            {
                if (oneFrameDelay)
                {
                    delayedCommands.Enqueue(new DelayedReturnCommand() { minReturnFrame = Time.frameCount, gameObject = _object });
                }

                if (DontReparent)
                {
                    _object.SetActive(false);
                    --activeCount;
                }
                else
                {
                    _object.SetActive(false);
                    _object.transform.parent = root;
                    --activeCount;
                }
            }
        }

        public GameObject Get()
        {
            if (AssetOnly)
            {
                return asset;
            }
            else
            {
                if (oneFrameDelay)
                {
                    var currentFrame = Time.frameCount;

                    //一直清理Queue，直到发现一个不满足的GameObject
                    while (delayedCommands.Count > 0 && currentFrame > delayedCommands.Peek().minReturnFrame)
                    {
                        delayedCommands.Dequeue();
                    }
                }

                if (DontReparent)
                {
                    var lastIndex = -1;

                    if (oneFrameDelay)
                    {
                        for (int i = 0, count = root.childCount; i < count; ++i)
                        {
                            var obj = root.GetChild(i).gameObject;

                            if (!obj.activeSelf && !IsGameobjectInsideDelayedCommands(obj))
                            {
                                lastIndex = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0, count = root.childCount; i < count; ++i)
                        {
                            var obj = root.GetChild(i).gameObject;

                            if (!obj.activeSelf)
                            {
                                lastIndex = i;
                                break;
                            }
                        }
                    }


                    if (lastIndex == -1)
                    {
                        if (AutoExpand)
                        {
                            Expand(AutoExpandCount);
                            return Get();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var obj = root.GetChild(lastIndex);
                        ++activeCount;
                        return obj.gameObject;
                    }
                }
                else
                {

                    var lastIndex = -1;

                    if (oneFrameDelay)
                    {
                        for (int i = root.childCount - 1; i >= 0; --i)
                        {
                            if (!this.IsGameobjectInsideDelayedCommands(root.GetChild(i).gameObject))
                            {
                                lastIndex = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        lastIndex = root.childCount - 1;
                    }

                    if (lastIndex == -1)
                    {
                        if (AutoExpand)
                        {
                            Expand(AutoExpandCount);
                            return Get();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        var obj = root.GetChild(lastIndex);
                        obj.SetParent(null);
                        ++activeCount;
                        return obj.gameObject;
                    }
                }
            }
        }

        public GameObject GetFirstObjectWithoutPooling()
        {
            return AssetOnly ? asset : objectInsideThisPool[0];
        }

        public void Foreach(System.Action<GameObject> action, bool _effectAsset)
        {
            if (!AssetOnly)
            {
                foreach (var obj in objectInsideThisPool)
                {
                    action(obj);
                }
            }

            if (_effectAsset)
            {
                action(asset);
            }
        }

        private bool IsGameobjectInsideDelayedCommands(GameObject _gameObject)
        {
            foreach (var command in delayedCommands)
            {
                if (command.gameObject == _gameObject)
                    return true;
            }

            return false;
        }
    }
}