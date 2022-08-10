using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using FairyGUI;
using DG.Tweening;
using System;
using ILRuntime.CLR.TypeSystem;

public class CustSceneManager : MonoBehaviour
{
    private static CustSceneManager _instance;
    public static CustSceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("CustSceneManager");
                GameObject.DontDestroyOnLoad(go);
                _instance = go.AddComponent<CustSceneManager>();
            }
            return _instance;
        }
    }

    private List<string> _unLoadingSceneList = new List<string>();
    private Dictionary<string, AsyncOperation> _SceneAsyncOperationList = new Dictionary<string, AsyncOperation>();

    private CustSceneManager()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;

    }

    /// <summary>
    /// 跳转场景
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <param name="viewName"> 显示界面名  为空串时 不切换显示界面</param>
    /// <param name="loadingPrefabName">加载界面资源名   为空串时候 显示默认加载界面</param>
    public void GoNextSceneAndShowView(string sceneName, string viewName, string loadingPrefabName = "", bool autoRemoveLoadingUI = true, bool notShowLoading = false)
    {
        UIManager.Instance.CloseWaitingPopup();
        if (_SceneAsyncOperationList.ContainsKey(sceneName))
        {
            return;
        }
        StopCoroutine("LoadingCoroutine");
        Hashtable hash = new Hashtable();
        hash.Add("loadingPrefabName", loadingPrefabName);
        hash.Add("sceneName", sceneName);
        hash.Add("viewName", viewName);
        hash.Add("autoRemoveLoadingUI", autoRemoveLoadingUI);
        hash.Add("notShowLoading", notShowLoading);
        StartCoroutine(LoadingCoroutine(hash));
    }

    /// <summary>
    /// 跳转游戏场景
    /// </summary>
    /// <param name="gameName">游戏代码名 CustGameName</param>
    /// <param name="sceneName">场景名</param>
    /// <param name="viewName"> 显示界面名 CustViewName  为空串时 不切换显示界面</param>
    /// <param name="loadingPrefabName">加载界面资源名   为空串时候 显示默认加载界面</param>
    public void EnterGameScene(string gameName, string sceneName, string viewName, string loadingPrefabName, bool autoRemoveLoadingUI = true)
    {
        UIManager.Instance.CloseWaitingPopup();
        string sceneLowerName = sceneName.ToLower();
        AssetBundle bundle = AssetManager.LoadAssetBundleFromLocal(sceneLowerName, gameName);
        if (bundle == null)
        {
            Utils.LogError("=====场景资源包不存在==" + " 文件夹：" + gameName + "   场景名：" + sceneLowerName);
        }
        else
        {
            GoNextSceneAndShowView(sceneName, viewName, loadingPrefabName, autoRemoveLoadingUI);
        }
    }

    IEnumerator LoadingCoroutine(Hashtable hash)
    {
        yield return new WaitForEndOfFrame();
        float startTime = Time.time;
        Utils.LogError("Load Scene :" + startTime);
        string sceneName = (string)hash["sceneName"];
        bool isShowLoding = !(bool)hash["notShowLoading"];
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            DOTween.Clear(true);
            ObjectPool.Instance.Clear();
            if (isShowLoding)
            {
                UIManager.Instance.ShowLoadingUI((string)hash["loadingPrefabName"]);
            }
            //AsyncOperation ao = SceneManager.LoadSceneAsync((string)hash["sceneName"], LoadSceneMode.Single);
            //_SceneAsyncOperationList[sceneName] = ao;
            AsyncOperation ao = StartLoadScene((string)hash["sceneName"], LoadSceneMode.Single);
            if (isShowLoding)
            {
                UIManager.Instance.StartUpdateLoadingUI(ao);
            }
            yield return ao;
        }

        yield return null;//场景加载完成后 添加一帧的间隔 用于场景物体的Awake初始化

        Resources.UnloadUnusedAssets();//回收无引用资源

        yield return null;//场景加载完成后 添加一帧的间隔 用于场景物体的Start初始化

        var viewName = hash["viewName"];
        if (null != viewName)
        {
            UIManager.Instance.ShowView((string)viewName);
            //UIManager.Instance.ShowView("SettingPopup");
        }
        float dsTime = Time.time - startTime;
        if (isShowLoding)
        {
            if (dsTime < 1)
            {
                yield return new WaitForSeconds(1 - dsTime);
            }
        }
        Utils.LogError("Load Scene Done ：" + Time.time);
        yield return null;//添加一帧的间隔        
        if (!hash.ContainsKey("autoRemoveLoadingUI") || (bool)hash["autoRemoveLoadingUI"])
        {
            UIManager.Instance.RemoveLoadingUI();
        }
    }

    public AsyncOperation StartLoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        if (_SceneAsyncOperationList.ContainsKey(sceneName))
            return _SceneAsyncOperationList[sceneName];
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
            return null;
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName, mode);
        //ao.allowSceneActivation = false;
        _SceneAsyncOperationList[sceneName] = ao;
        return ao;
    }


    public void StartUnloadScene(string sceneName)
    {
        if (_unLoadingSceneList.Contains(sceneName))
            return;
        _unLoadingSceneList.Add(sceneName);
        SceneManager.UnloadSceneAsync(sceneName);
    }

    private void SceneManager_sceneUnloaded(Scene scene)
    {
        _unLoadingSceneList.Remove(scene.name);
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _SceneAsyncOperationList.Remove(scene.name);
    }

    private void Update()
    {
        //if (_SceneAsyncOperationList.Count > 0)
        //{
        //    foreach (var item in _SceneAsyncOperationList)
        //    {
        //        if (item.Value.progress >= 0.9f)
        //        {
        //            item.Value.allowSceneActivation = true;
        //        }
        //    }
        //}
    }
}
