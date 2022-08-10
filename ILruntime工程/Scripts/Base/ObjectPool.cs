using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理非UI对象(包括预制件和已生成物体)
/// </summary>
public class ObjectPool 
{
    /// <summary>
    /// 保存的已读取预制件个数
    /// </summary>
    public const int SaveObjNum = 50;
    /// <summary>
    /// 每种生成的物体保存个数
    /// </summary>
    public const int SaveGameobjNum = 30;

    public static ObjectPool Instance{
        get{
            if (_instane == null)
                _instane = new ObjectPool();
            return _instane;
        }
    }
    /// <summary>
    /// 预制件列表(除去子弹)
    /// </summary>
    public Dictionary<string, Object> objPool;
    public List<string> objNames;
    /// <summary>
    /// 生成的物体列表
    /// </summary>
    public Dictionary<string, Queue<Transform>> GameobjectDic;
    private static ObjectPool _instane;
    private ObjectPool()
    { 
        objPool = new Dictionary<string, Object>();
        GameobjectDic = new Dictionary<string, Queue<Transform>>();
        objNames = new List<string>();
    }


    public void AddPrefabToPool(string path , Object prefab)
    {
        if (!objPool.ContainsKey(path))
        {
            objPool[path] = prefab;
            objNames.Add(path);

            if (objNames.Count > SaveObjNum)
            {
                objPool.Remove(objNames[0]);
                objNames.RemoveAt(0);
            }
        }
    }

    public Object GetPrefab(string path , string bundleName = "")
    {
        if(string.IsNullOrEmpty(bundleName))
        {
            if (!objPool.ContainsKey(path))
            {
                Object prefab = AssetManager.GetPrefab(path, bundleName);
                AddPrefabToPool(path,prefab);
            }
            try
            {
                return objPool[path];
            }
            catch (System.Exception)
            {
                return objPool[path];
            }
        }
        return null;
    }

    public GameObject GetGameobj(string prefabName , string bundleName = "")
    {
        GameObject go = null;
        if (GameobjectDic.ContainsKey(prefabName) && GameobjectDic[prefabName].Count > 0)
        {
            go = GameobjectDic[prefabName].Dequeue().gameObject;
            go.gameObject.SetActive(true);
        }
        else
        {
            if (!GameobjectDic.ContainsKey(prefabName))
            {
                GameobjectDic[prefabName] = new Queue<Transform>();
            }
            Object prefab = GetPrefab(prefabName, bundleName);
            if (prefab != null)
            {
                go =  GameObject.Instantiate(prefab) as GameObject;
            }
        }
        return go;
    }

    public void ReturnGameobj(Transform trans, string path = "")
    {
        string key = trans.name;
        if (!string.IsNullOrEmpty(path))
            key = path;
        if (GameobjectDic.ContainsKey(key))
        {
            if (GameobjectDic[key].Count < SaveGameobjNum)
            {
                trans.position = Vector3.down * 1000;//移到看不到的位置
                trans.gameObject.SetActive(false);
                if (!GameobjectDic[key].Contains(trans))
                {
                    GameobjectDic[key].Enqueue(trans);
                }
            }
            else
                GameObject.Destroy(trans.gameObject);
        }
        else
        {
            GameobjectDic[key] = new Queue<Transform>();
            GameobjectDic[key].Enqueue(trans);
        }
    }

    public void ReturnGameobj(GameObject go, string path = "")
    {
        Transform trans = go.transform;
        ReturnGameobj(trans, path);
    }

    public void Clear()
    {
        objPool.Clear();
        objNames.Clear();
        GameobjectDic.Clear();
    }


}
