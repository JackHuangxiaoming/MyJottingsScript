using UnityEngine;
/// <summary>
/// 优雅的单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour  where T : MonoBehaviour{

    protected static T single;

    public static T Single
    {
        get {
            if (single == null || single.gameObject == null)
            {
                //instance = (T)FindObjectOfType(typeof(T));
                single = FindObjectOfType<T>();
                if (single == null)
                {
                    Debug.LogWarningFormat("场景中需要一个类型：{0}的实例，但是并没有！",typeof(T));
                }
            }
            return single;
        }
    }
}
