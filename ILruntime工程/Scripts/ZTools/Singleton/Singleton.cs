using System;

namespace ZTools.SingletonNS
{
    public abstract class SingletonBase
    {
        /// <summary>
        /// 是否已经加载, 可以使用
        /// 如果已经被卸载, 该值会返回False
        /// </summary>
        public bool Loaded { get; set; }
        public abstract void Load();
        public abstract void Unload();
        public abstract void Reload();
        public abstract void Update();
        public abstract void LateUpdate();
        public abstract void FixedUpdate();

        public virtual void ReleaseMemories() { }
        public virtual void OnApplicationFocused() { }
        public virtual void OnApplicationGoesIntoBackground() { }

#if UNITY_EDITOR
        public virtual void OnDrawGizmos() { }
#endif
    }

    public abstract class Singletons<Type> : SingletonBase, IDisposable where Type : Singletons<Type>, new()
    {
        /// <summary>
        /// 单例, 如果不存在, 会自动创建
        /// </summary>
        public static Type Instance
        {
            get
            {
                if (instance == null)
                {
                    Construct();
                }

                return instance;
            }
        }

        /// <summary>
        /// 自动创建
        /// </summary>
        private static void Construct()
        {
            instance = new Type();
            SingletonManager.Regist(instance);
            instance.Load();
            instance.Loaded = true;
        }

        /// <summary>
        /// 直接引用, 不会自动创建, 在OnDestroy时调用最为保险
        /// </summary>
        public static Type DirectInstance
        {
            get
            {
                if (instance != null && instance.Loaded)
                    return instance;
                else
                    return null;
            }
        }

        private static Type instance;

        void IDisposable.Dispose()
        {
            instance = null;
        }
    }
}
