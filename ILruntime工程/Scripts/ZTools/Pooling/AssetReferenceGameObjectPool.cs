#if UNITY_ADDRESSABLE

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
using UnityEngine.SceneManagement;
using ZTools.ResourceManagerNS;

namespace ZTools.PoolingNS
{
    public class AssetReferenceGameObjectPool : IDisposable
    {
        protected static void Swap<T>(List<T> _list, int _indexA, int _indexB)
        {
            var temp = _list[_indexA];
            _list[_indexA] = _list[_indexB];
            _list[_indexB] = temp;
        }

        private int expandCount;
        private int aliveCount;
        private int currentUsingIndex;
        private bool parentCreatedAtRuntime;
        private Transform parent;
        private AssetReferenceGameObject reference;
        private string referenceKey;
        private List<AssetReferenceGameObjectTracker> list;

        private AssetReferenceGameObjectPool(AssetReferenceGameObject _reference, string _referenceKey, int _capacity, Transform _parent, Scene _scene, string _rootName)
        {
            if (_rootName != null)
            {
                var root = new GameObject(_rootName);
                SceneManager.MoveGameObjectToScene(root, _scene);
                parent = root.transform;
                parentCreatedAtRuntime = true;
            }
            else
            {
                parent = _parent;
                parentCreatedAtRuntime = false;
            }

            reference = _reference;
            referenceKey = _referenceKey;
            aliveCount = 0;
            currentUsingIndex = 0;
            expandCount = Mathf.Max(1, _capacity / 2);

            list = new List<AssetReferenceGameObjectTracker>(_capacity);

            if (reference != null)
            {
                for (int i = 0; i < _capacity; ++i)
                {
                    list.Add(reference.InstantiateAsyncAndTrack(OnInstantiateDone, parent));
                }
            }
            else
            {
                for (int i = 0; i < _capacity; ++i)
                {
                    list.Add(referenceKey.InstantiateAsyncAndTrack(OnInstantiateDone, parent));
                }
            }

            OnListExpanded(list.Count);
        }

        public AssetReferenceGameObjectPool(AssetReferenceGameObject _reference, int _capacity, Transform _parent) : this(_reference, null, _capacity, _parent, default, null) { }

        public AssetReferenceGameObjectPool(AssetReferenceGameObject _reference, int _capacity, Scene _scene, string _parentName) : this(_reference, null, _capacity, null, _scene, _parentName) { }

        public AssetReferenceGameObjectPool(string _key, int _capacity, Transform _parent) : this(null, _key, _capacity, _parent, default, null) { }

        public AssetReferenceGameObjectPool(string _key, int _capacity, Scene _scene, string _parentName) : this(null, _key, _capacity, null, _scene, _parentName) { }

        public void Dispose()
        {
            if (list != null)
            {
                var temp = list;
                list = null;

                expandCount = 0;
                aliveCount = 0;
                currentUsingIndex = 0;
                parent = null;
                reference = null;

                foreach (var l in temp)
                {
                    l.Release();
                }

                if (parentCreatedAtRuntime)
                {
                    if (parent != null && parent.gameObject != null)
                    {
                        GameObject.Destroy(parent.gameObject);
                    }
                }
            }
        }

        public GameObject TryGet()
        {
            if (list == null) return null;

            if (currentUsingIndex >= aliveCount)
            {
                if (aliveCount == list.Count)
                {
                    //EXPAND
                    if (reference != null)
                    {
                        for (int i = 0; i < expandCount; ++i)
                        {
                            list.Add(reference.InstantiateAsyncAndTrack(OnInstantiateDone, parent));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < expandCount; ++i)
                        {
                            list.Add(referenceKey.InstantiateAsyncAndTrack(OnInstantiateDone, parent));
                        }
                    }

                    OnListExpanded(list.Count);
                }

                return null; //还没加载完毕
            }
            else
            {
                ++currentUsingIndex;
                return list[currentUsingIndex - 1].AsGameObject();
            }
        }

        public void Return(GameObject _gameObject)
        {
            if (_gameObject == null) return;
            if (list == null) return;

            for (int i = 0; i < currentUsingIndex; ++i)
            {
                if (list[i].AsGameObject().Equals(_gameObject))
                {
                    //和CurrentUsingIndex - 1调换位置
                    --currentUsingIndex;
                    Swap(list, i, currentUsingIndex);
                    OnSwapped(i, currentUsingIndex);
                    return;
                }
            }

            Debug.LogWarning($"{_gameObject.name} 不属于此对象池管理", _gameObject);
        }

        protected virtual void OnListExpanded(int _newSize) { }

        protected virtual void OnInstantiated(int _index, GameObject _gameObject) { }

        protected virtual void OnSwapped(int _indexA, int _indexB) { }

        protected virtual void OnRemoved(int _index) { }

        protected virtual void OnDisposed() { }

        protected int CurrentUsingIndex { get { return currentUsingIndex; } }

        private void OnInstantiateDone(AssetReferenceGameObjectTracker tracker)
        {
            if (list == null) return;

            //成功
            if (tracker.IsValid)
            {
                var index = list.IndexOf(tracker);

                if (index != aliveCount)
                {
                    Swap(list, aliveCount, index);
                    OnSwapped(aliveCount, index);
                }

                OnInstantiated(aliveCount, tracker.AsGameObject());

                ++aliveCount;
            }
            //失败，则从队列中移除， 不用管AliveCount
            else
            {
                var index = list.IndexOf(tracker);
                var lastIndex = list.Count - 1;

                Swap(list, index, lastIndex);
                OnSwapped(index, lastIndex);

                list.RemoveAt(lastIndex);
                OnRemoved(lastIndex);
            }
        }
    }

    public class AssetReferenceGameObjectPool<T> : AssetReferenceGameObjectPool where T : Component
    {
        private List<Transform> transformList;
        private List<T> componentList;

        public AssetReferenceGameObjectPool(AssetReferenceGameObject _reference, int _capacity, Transform _parent) : base(_reference, _capacity, _parent) { }
        public AssetReferenceGameObjectPool(AssetReferenceGameObject _reference, int _capacity, Scene _scene, string _parentName) : base(_reference, _capacity, _scene, _parentName) { }
        public AssetReferenceGameObjectPool(string _key, int _capacity, Transform _parent) : base(_key, _capacity, _parent) { }
        public AssetReferenceGameObjectPool(string _key, int _capacity, Scene _scene, string _parentName) : base(_key, _capacity, _scene, _parentName) { }

        protected override void OnListExpanded(int _newSize)
        {
            if (transformList == null)
            {
                transformList = new List<Transform>(_newSize);
                for (int i = 0; i < _newSize; ++i)
                {
                    transformList.Add(null);
                }
            }
            else
            {
                for (int i = 0, count = _newSize - transformList.Count; i < count; ++i)
                {
                    transformList.Add(null);
                }
            }

            if (componentList == null)
            {
                componentList = new List<T>(_newSize);
                for (int i = 0; i < _newSize; ++i)
                {
                    componentList.Add(null);
                }
            }
            else
            {
                for (int i = 0, count = _newSize - componentList.Count; i < count; ++i)
                {
                    componentList.Add(null);
                }
            }
        }

        protected override void OnInstantiated(int _listIndex, GameObject _gameObject)
        {
            transformList[_listIndex] = _gameObject.transform;
            _gameObject.TryGetComponent<T>(out var component);
            componentList[_listIndex] = component;
        }

        protected override void OnRemoved(int _index)
        {
            transformList.RemoveAt(_index);
            componentList.RemoveAt(_index);
        }

        protected override void OnSwapped(int _indexA, int _indexB)
        {
            Swap(transformList, _indexA, _indexB);
            Swap(componentList, _indexA, _indexB);
        }

        protected override void OnDisposed()
        {
            transformList = null;
            componentList = null;
        }

        public GameObject TryGet(out Transform _transform, out T _component)
        {
            var result = base.TryGet();
            if (result != null)
            {
                var index = CurrentUsingIndex - 1;
                _transform = transformList[index];
                _component = componentList[index];
                return result;
            }
            else
            {
                _transform = null;
                _component = null;
                return result;
            }
        }
    }
}

#endif