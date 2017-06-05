using UnityEngine;
/// <summary>
/// 优雅的单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class Single<T> : MonoBehaviour  where T : MonoBehaviour{

    protected static T instance;

	public static T Instance
    {
        get {
            if (instance == null || instance.gameObject == null)
            {
                //instance = (T)FindObjectOfType(typeof(T));
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogWarningFormat("场景中需要一个类型：{0}的实例，但是并没有！",typeof(T));
                }
            }
            return instance;
        }
    }
}
