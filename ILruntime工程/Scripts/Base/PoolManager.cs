using FairyGUI;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 管理UI对象
/// </summary>
public class PoolManager {
    private static PoolManager inst;
    public static PoolManager Inst
    {
        get
        {
            if (inst == null) inst = new PoolManager();
            return inst;
        }
    }
    private Dictionary<string, List<object>> objPool = new Dictionary<string, List<object>>();
    private GObjectPool gPool;
    /// <summary>
    /// FGUI自带对象池
    /// </summary>
    public GObjectPool GPool
    {
        get
        {
            if (gPool == null)
            {
                gPool = new GObjectPool(new GameObject("GObjectPool").transform);
            }
            return gPool;
        }
    }
    public void Init(Func<int> f)
    {

    }
    /// <summary>
    /// 获取池中对象
    /// </summary>
    /// <param name="strPrefabName"></param>
    /// <returns></returns>
    public object Get(string strPrefabName)
    {
        GameObject go = null;
        if (objPool.ContainsKey(strPrefabName))
        {
            List<object> objList = objPool[strPrefabName];
            if (objList.Count > 0)
            {
                go = objList[0] as GameObject;
                objList.Remove(go);
            }
        }
        if (go == null)
        {
            go = GameObject.Instantiate(Resources.Load(strPrefabName)) as GameObject;
            go.name = strPrefabName;
        }
        go.SetActive(true);
        return go;
    }
    /// <summary>
    /// FGUI获取
    /// </summary>
    /// <param name="strPkgName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public object Get(string strPkgName,string resName)
    {
        GObject go = null;
        if (objPool.ContainsKey(resName))
        {
            List<object> objList = objPool[resName];
            if (objList.Count > 0)
            {
                go = objList[0] as GObject;
                objList.Remove(go);
            }
        }
        if (go == null)
        {
            if (UIPackage.GetByName(strPkgName) == null)
            {
                Debug.LogError(strPkgName+"包未加载");
                return null;
            }
            go = UIPackage.CreateObject(strPkgName, resName);
            go.name = resName;
        }
        if (go == null)
        {
            Debug.LogError(resName + "资源不存在");
        }
        return go;
    }
    /// <summary>
    /// 将对象返还给对象池
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public object Push(GameObject go)
    {
        if (!objPool.ContainsKey(go.name))
        {
            objPool[go.name] = new List<object>();
        }
        objPool[go.name].Add(go);

        go.SetActive(false);
        return go;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public GObject Push(GObject go)
    {
        if (!objPool.ContainsKey(go.name))
        {
            objPool[go.name] = new List<object>();
        }
        objPool[go.name].Add(go);

        go.visible = false;
        return go;
    }
    /// <summary>
    /// 
    /// </summary>
    private void Reset()
    {

    }
}
