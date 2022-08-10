using UnityEngine;
/// <summary>
/// 优雅的单例
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T single;

    /// <summary>
    /// 对象实例
    /// </summary>
	public static T Single
    {
        get
        {
            if (single == null || single.gameObject == null)
            {
                //instance = (T)FindObjectOfType(typeof(T));
                T[] Tempsingle = FindObjectsOfType<T>();

                if (Tempsingle.Length > 1)
                {
                    Debug.LogErrorFormat("场景中有多个{0}的实例，但是只允许有一个！", typeof(T));
                    return null;
                }
                if (Tempsingle.Length == 1)
                {
                    single = Tempsingle[0];
                    return single;
                }
                if (single == null)
                {
                    GameObject g = new GameObject();
                    g.name = typeof(T).ToString();
                    single = g.AddComponent<T>();
                }
            }
            return single;
        }
    }
}
