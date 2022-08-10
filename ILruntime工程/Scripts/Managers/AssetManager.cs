using UnityEngine;
using System.Collections.Generic;
using FairyGUI;
using System.IO;
using System;
using System.Xml;
using System.Threading.Tasks;

public class AssetManager : MonoBehaviour
{
    private static AssetManager _instance;
    public static AssetManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                _instance = obj.AddComponent<AssetManager>();
                obj.hideFlags = HideFlags.HideInHierarchy;
                GameObject.DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public static String BasicPackage = "BasicPackage";
    public static String SharedAssets = "SharedAssets";
    public static String PlatformLobby = "PlatformLobby";

    private static Dictionary<string, AssetBundle> m_AssetBundleDictionary = new Dictionary<string, AssetBundle>();


    static Dictionary<string, Action<XmlReader>> callBackPool_XR = new Dictionary<string, Action<XmlReader>>();
    #region FairyGUI获取相关
    public T CreatFGUIFromResouce<T>(string pkgName, string resName) where T : GComponent
    {
        pkgName = RemoveBundleSuffix(pkgName);

        if (!CheckAndAddFGUIPackge("BasicPackage", pkgName, false))
        {
            return null;
        }
        return (T)UIPackage.CreateObject(pkgName, resName, typeof(T));
    }
    public T CreateFGUIFromResName<T>(string pkgDirectory, string bundleName, string resName) where T : GComponent
    {
        CheckAndAddFGUIPackge(PlatformLobby, SharedAssets);//检查添加公共包
        bundleName = RemoveBundleSuffix(bundleName);
        bool b = CheckAndAddFGUIPackge(pkgDirectory, bundleName);
        if (!b) return null;
        return (T)UIPackage.CreateObject(bundleName, resName, typeof(T));
    }
    public GObject CreateFGUIFromResName(string pkgDirectory, string bundleName, string resName)
    {
        CheckAndAddFGUIPackge(PlatformLobby, SharedAssets);//检查添加公共包
        bundleName = RemoveBundleSuffix(bundleName);
        bool b = CheckAndAddFGUIPackge(pkgDirectory, bundleName);
        if (!b) return null;
        return UIPackage.CreateObject(bundleName, resName);
    }

    public object GetItemAssetFromResName(string pkgDirectory, string bundleName, string resName)
    {
        CheckAndAddFGUIPackge(PlatformLobby, SharedAssets);//检查添加公共包
        bundleName = RemoveBundleSuffix(bundleName);
        bool b = CheckAndAddFGUIPackge(pkgDirectory, bundleName);
        if (!b) return null;
        return UIPackage.GetItemAsset(bundleName, resName);
    }



    public string GetFGUIItemUrl(string pkgDirectory, string bundleName, string resName)
    {
        CheckAndAddFGUIPackge(PlatformLobby, SharedAssets);//检查添加公共包
        bundleName = RemoveBundleSuffix(bundleName);
        bool b = CheckAndAddFGUIPackge(pkgDirectory, bundleName);
        if (!b) return "";
        return UIPackage.GetItemURL(bundleName, resName);
    }

    public GObject CreateFGUIFromURL(string pkgDirectory, string bundleName, string url)
    {
        CheckAndAddFGUIPackge(PlatformLobby, SharedAssets);//检查添加公共包
        bundleName = RemoveBundleSuffix(bundleName);
        bool b = CheckAndAddFGUIPackge(pkgDirectory, bundleName);
        if (!b) return null;
        return UIPackage.CreateObjectFromURL(url);
    }

    public bool CheckAndAddFGUIPackge(string pkgDirectory, string pkgName, bool isBundle = true)
    {
        if (string.IsNullOrEmpty(pkgDirectory) || string.IsNullOrEmpty(pkgName))
        {
            Debug.LogError("AssetManager load pkgName is null or len=0 ！！!");
            return false;
        }
        if (UIPackage.GetByName(pkgName) == null)
        {
            if (!isBundle)
            {
                ////////////////////////////////////下面是resource方式的加载/////////////////////////////////////
                string path = Path.Combine("UIPackage", pkgDirectory, pkgName);
                path = path.Replace("\\", "/");
                UIPackage.AddPackage(path);
            }
            else
            {
                ////////////////////////////////////下面是bundle方式的加载/////////////////////////////////////

                string pkgLowerName = pkgName.ToLower();//unity 5.5打包的bundle 包名全为小写
                string path = FormatBundlePath(pkgLowerName, pkgDirectory);
                //AssetBundle manifestBundle = AssetBundle.LoadFromFile(AddBundleSuffix(path));
                AssetBundle manifestBundle = LoadMyAssetBundle(AddBundleSuffix(path));
                if (manifestBundle == null) return false;
                //AssetBundle bundle = AssetBundle.LoadFromFile(path);
                AssetBundle bundle = LoadMyAssetBundle(path);
                if (bundle == null) return false;
                pkgName = string.Format("{0}_fui", pkgName);
                UIPackage.AddPackage(manifestBundle, bundle, pkgName);
            }
        }
        return true;
    }

    public static string FormatBundlePath(string pkgName, string gameHotDirectory)
    {
        pkgName = RemoveBundleSuffix(pkgName);
        string url = URLFactory.DownLoadHotFilePath;
        url = Path.Combine(url, gameHotDirectory, pkgName);
        url = url.Replace("\\", "/");
        return url;
    }

    public static string RemoveBundleSuffix(string pkgName)
    {
        if (pkgName.EndsWith("_desc"))
        {
            pkgName = pkgName.Remove(pkgName.LastIndexOf("_desc"));

        }
        return pkgName;
    }

    public static string AddBundleSuffix(string pkgName)
    {
        if (!pkgName.EndsWith("_desc"))
            pkgName += "_desc";
        return pkgName;
    }

    #endregion


    #region 普通获取相关

    public void UnloadAllAssets(bool unloadAllLoadedAssets = true)
    {
        List<string> unloadList = new List<string>();
        foreach (string assetBundleName in m_AssetBundleDictionary.Keys)
        {
            unloadList.Add(assetBundleName);
            if (m_AssetBundleDictionary[assetBundleName] != null)
                m_AssetBundleDictionary[assetBundleName].Unload(unloadAllLoadedAssets);
        }
        for (int i = 0; i < unloadList.Count; i++)
        {
            m_AssetBundleDictionary.Remove(unloadList[i]);
        }
    }

    public void UnloadAssetBundle(string assetBundleName, bool unloadAsset = true)
    {
        if (m_AssetBundleDictionary.ContainsKey(assetBundleName))
        {
            if (m_AssetBundleDictionary[assetBundleName] != null)
                m_AssetBundleDictionary[assetBundleName].Unload(unloadAsset);
            m_AssetBundleDictionary.Remove(assetBundleName);
        }
    }

    public static AssetBundle LoadAssetBundleFromLocal(string bundleName, string bundleDirectory)
    {
        lock (m_AssetBundleDictionary)
        {
            if (m_AssetBundleDictionary.ContainsKey(bundleName) && m_AssetBundleDictionary[bundleName] != null)
            {
                return m_AssetBundleDictionary[bundleName];
            }
            else
            {
                string bundlePath = FormatBundlePath(bundleName, bundleDirectory);
                //m_AssetBundleDictionary.Add(bundleName, AssetBundle.LoadFromFile(bundlePath));
                m_AssetBundleDictionary.Add(bundleName, LoadMyAssetBundle(bundlePath));
                return m_AssetBundleDictionary[bundleName];
            }
        }
    }
    //async public static Task<AssetBundle> LoadAssetBundleFromLocalAsync(string bundleName, string bundleDirectory)
    //{
    //    if (m_AssetBundleDictionary.ContainsKey(bundleName) && m_AssetBundleDictionary[bundleName] != null)
    //    {
    //        return m_AssetBundleDictionary[bundleName];
    //    }
    //    else
    //    {
    //        string bundlePath = FormatBundlePath(bundleName, bundleDirectory);
    //        m_AssetBundleDictionary[bundleName] = await LoadMyAssetBundleByAsync(bundlePath, bundleName);
    //        return m_AssetBundleDictionary[bundleName];
    //    }
    //}


    public static UnityEngine.Object LoadPrefabeOnlyFormResourcesByResourcesPath(string resourcesPath, Type type)
    {
        UnityEngine.Object prefab = Resources.Load(resourcesPath, type);
        return prefab;
    }

    private static UnityEngine.Object LoadPrefabeOnlyFromBundleByBundlePath(string bundleDirectory, string bundleName, string prefabName, Type type)
    {
        if (string.IsNullOrEmpty(bundleName)) return null;
        AssetBundle ab = LoadAssetBundleFromLocal(bundleName, bundleDirectory);
        if (ab == null)
        {
            Debug.LogError("resource error! unity3d==>>" + bundleName + ",not found!");
            return null;
        }
        else
        {
            UnityEngine.Object obj = ab.LoadAsset(prefabName, type);
            if (obj == null)
                Debug.LogError("prefabe is null:" + prefabName);
            return obj;
        }
    }

    public static UnityEngine.Object GetInstanceByBundlePath(string bundleDirectory, string bundleName, string prefabName, Type type)
    {
#if UNITY_EDITOR
        /** 改变Shader */
        GameObject obj = (GameObject)GetInstanceByBundlePath(bundleDirectory, bundleName, prefabName, type, Vector3.zero, Quaternion.identity);
        ReFindShader(obj);
        return obj;
#else        
        return GetInstanceByBundlePath(bundleDirectory, bundleName, prefabName, type, Vector3.zero, Quaternion.identity);
#endif
    }

    public static UnityEngine.Object GetInstanceByBundlePath(string bundleDirectory, string bundleName, string prefabName, Type type, Vector3 position, Quaternion rotation)
    {
        UnityEngine.Object prefab = LoadPrefabeOnlyFromBundleByBundlePath(bundleDirectory, bundleName, prefabName, type);
        if (prefab == null)
            return null;
        UnityEngine.Object obj = GameObject.Instantiate(prefab, position, rotation);
        return obj;
    }

    public static UnityEngine.Object GetPrefab(string prefabPath, string bundleName = "")
    {
        UnityEngine.Object prefab = Resources.Load(prefabPath);
        if (prefab == null)
        {
            if (m_AssetBundleDictionary.ContainsKey(bundleName))
            {
                prefab = m_AssetBundleDictionary[bundleName].LoadAsset(prefabPath);
            }
        }
        if (prefab == null)
        {
            Debug.LogError("没有找到预制件" + prefabPath);
        }
        return prefab;
    }

    public GameObject GetGameObject(string prefabName, string bundleName = "")
    {
        UnityEngine.Object obj = GetPrefab(prefabName, bundleName);
        GameObject go = GameObject.Instantiate(obj) as GameObject;
        return go;
    }

    #endregion

    #region 加载其他
    /// <summary>
    /// 加载xml 
    /// </summary>
    /// <returns></returns>
    public static void LoadXMLByBundle(string xmlName, Action<XmlDocument> callBack, string bundleName, string bundleDirectory)
    {
        AssetBundle ab = LoadAssetBundleFromLocal(bundleName, bundleDirectory);
        TextAsset textAsset = ab.LoadAsset<TextAsset>(xmlName);
        XmlDocument doc = null;
        if (null != textAsset)
        {
            doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            TextReader textReader = new StringReader(textAsset.text);
            XmlReader xmlReader = XmlReader.Create(textReader, settings);
            doc.Load(xmlReader);
            xmlReader.Dispose();
        }
        else
        {
            Debug.LogError("null == textAsset, xmlName: " + xmlName);
        }
        if (callBack != null)
        {
            callBack(doc);
        }
    }
    public static XmlDocument LoadXMLByBundle(string xmlName, string bundleName, string bundleDirectory)
    {
        AssetBundle ab = LoadAssetBundleFromLocal(bundleName, bundleDirectory);
        TextAsset textAsset = ab.LoadAsset<TextAsset>(xmlName);
        XmlDocument doc = null;
        if (null != textAsset)
        {
            doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            TextReader textReader = new StringReader(textAsset.text);
            XmlReader xmlReader = XmlReader.Create(textReader, settings);
            doc.Load(xmlReader);
            xmlReader.Dispose();
        }
        else
        {
            Utils.LogError("null == textAsset, xmlName: " + xmlName);
        }
        return doc;
    }
    public async static void LoadXMLByBundleAsync(string xmlName, string bundleName, string bundleDirectory, Action<XmlReader> callBack)
    {
        lock (callBackPool_XR)
        {
            if (callBackPool_XR.ContainsKey(xmlName))
                return;
            callBackPool_XR[xmlName] = callBack;
        }
        //LoadAssetBundleFromLocalAsync(bundleName, bundleDirectory);
        AssetBundle ab = LoadAssetBundleFromLocal(bundleName, bundleDirectory);
        XmlReader xr = null;
        TextAsset textAsset = ab?.LoadAsset<TextAsset>(xmlName);
        if (null != textAsset)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            settings.IgnoreWhitespace = true;
            settings.Async = true;
            TextReader textReader = new StringReader(textAsset.text);
            xr = XmlReader.Create(textReader, settings);
        }
        else
        {
            Utils.LogError("null == textAsset, xmlName: " + xmlName);
            return;
        }
        await System.Threading.Tasks.Task.Run(async () =>
        {
            await Task.Delay(1);
            lock (callBackPool_XR)
            {
                if (callBackPool_XR.ContainsKey(xmlName))
                    callBackPool_XR[xmlName](xr);
                callBackPool_XR[xmlName] = null;
            }
        });
    }
    #endregion

    #region 加密 解密

    const byte m_key = 127;
    /// <summary>
    /// 加密/解密
    /// </summary>
    public static void Encypt(ref byte[] targetData)
    {
        int dataLength = targetData.Length;
        for (int i = 0; i < dataLength; ++i)
        {
            targetData[i] = (byte)(targetData[i] ^ m_key);
        }
    }
    public static byte[] EncyptByte(byte[] targetData)
    {
        int dataLength = targetData.Length;
        byte[] data = new byte[dataLength];
        for (int i = 0; i < dataLength; ++i)
        {
            data[i] = (byte)(targetData[i] ^ m_key);
        }
        return data;
    }

    /// <summary>
    /// 尝试加密全部Bundle
    /// </summary>
    /// <param name="sourcePath"></param>
    public static void TryEncyptAssetBundle(string sourcePath)
    {
        Debug.Log("==========================================开始加密...");
        //遍历streamingAssets目录下所有的ab包，逐个进行加密
        foreach (var f in new DirectoryInfo(sourcePath).GetFiles())
        {
            if (string.IsNullOrEmpty(f.Extension) && f.Name != "AllPackage")
            {
                Debug.Log(f.Name + ":已加密 MD5 PlayerPrefs.GetString：" + PlayerPrefs.GetString("Encypt_" + f.Name));
                if (PlayerPrefs.GetString("Encypt_" + f.Name) == Utils.GetMD5HashByFile(f.FullName))
                    continue;
                Debug.Log("加密文件：" + f.FullName);
                Byte[] temp = File.ReadAllBytes(f.FullName);
                Encypt(ref temp);
                File.WriteAllBytes(f.FullName, temp);
                PlayerPrefs.SetString("Encypt_" + f.Name, Utils.GetMD5HashByFile(f.FullName));
                Debug.Log("加密文件成功 PlayerPrefs.GetString：" + PlayerPrefs.GetString("Encypt_" + f.Name));
            }
        }
        Debug.Log("==========================================加密完成...");
    }

    #endregion

    /// <summary>
    /// 自己的方式加载Bundle
    /// </summary>
    /// <returns></returns>
    public static AssetBundle LoadMyAssetBundle(string bundlePath)
    {
        byte[] stream = File.ReadAllBytes(bundlePath);
        //解密
        Encypt(ref stream);
        return AssetBundle.LoadFromMemory(stream);
    }

    public async static Task<byte[]> LoadMyAssetBundleByAsync(string bundlePath)
    {
        if (!File.Exists(bundlePath))
        {
            return null;
        }
        List<byte> datas = new List<byte>();
        byte[] buffer = new byte[0x1000];
        using (FileStream fs = new FileStream(bundlePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
        {
            while (await fs.ReadAsync(buffer, 0, buffer.Length) != 0)
            {
                datas.AddRange(buffer);
            }
        }
        buffer = datas.ToArray();
        //解密
        Encypt(ref buffer);
        return buffer;
    }


#if UNITY_EDITOR
    /** 改变Shader */
    public static void ReFindShader(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return;
        }
        List<Renderer> renderers = new List<Renderer>();
        gameObject.transform.GetComponentsInChildren(true, renderers);
        Renderer renderer;
        Material[] shareMaterials;
        int length;
        Material material;
        for (int i = 0, count = renderers.Count; i < count; i++)
        {
            renderer = renderers[i];
            shareMaterials = renderer.sharedMaterials;
            length = shareMaterials == null ? 0 : shareMaterials.Length;
            for (int j = 0; j < length; j++)
            {
                material = shareMaterials[j];
                if (material != null)
                {
                    Shader shader = material.shader;
                    int rendererQueue = material.renderQueue;
                    if (shader != null)
                    {
                        material.shader = Shader.Find(shader.name);
                        material.renderQueue = 3000;
                    }
                }
            }
        }
    }
#endif
}