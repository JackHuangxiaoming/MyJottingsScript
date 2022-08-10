using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Text;
using EditorSceneManager = UnityEditor.SceneManagement.EditorSceneManager;
using System.Collections.Generic;

public class BuildAssetBundles
{
    static AssetInfo _assetInfos;
    static string sourcePath = string.Empty;
    static string target = string.Empty;
    static string dllTarget = string.Empty;
    static int num = 0;

    [MenuItem("Tools/Build_AssetBundleAndEncypt")]
    public static void BuildAssetBundle()
    {
        PlayerSettings.companyName = "XLHY";
        BuildBundleByStyle(1);
    }
    [MenuItem("Tools/CLR_PlatformLobbyCode.dll")]
    public static void TryCLRPlatformLobbyCode()
    {
        Debug.Log("CLRBingding 开始");
        ILRuntimeCLRBinding.GenerateCLRBindingByAnalysis();
        Debug.Log("CLRBingding成功");
    }
    [MenuItem("Tools/Encypt_PlatformLobbyCode.dll")]
    public static void TryEncyptPlatformLobbyCode()
    {
#if UNITY_EDITOR
        try
        {
            string dllPath = Path.Combine(URLFactory.DownLoadHotFilePath, "PlatformLobbyCode/", "PlatformLobbyCode.dll");
            TryEncypt(dllPath, "PlatformLobbyCode.dll");
            string pdbPath = Path.Combine(URLFactory.DownLoadHotFilePath, "PlatformLobbyCode/", "PlatformLobbyCode.pdb");
            TryEncypt(pdbPath, "PlatformLobbyCode.pab");
        }
        catch (Exception e)
        {
            Debug.LogError("加密代码时出错：" + e.Message);
        }
#endif
    }

    private static void TryEncypt(string path, string name = "PlatformLobbyCode.dll")
    {
        if (!File.Exists(path))
            throw new Exception("指定文件不存在");
        string md5 = Utils.GetMD5HashByFile(path);
        Debug.Log(":已加密 MD5 PlayerPrefs.GetString：" + PlayerPrefs.GetString("Encypt_" + name));
        if (PlayerPrefs.GetString("Encypt_" + name) != md5)
        {
            Debug.Log("加密文件：" + path);
            Byte[] temp = File.ReadAllBytes(path);
            AssetManager.Encypt(ref temp);
            File.WriteAllBytes(path, temp);
            PlayerPrefs.SetString("Encypt_" + name, Utils.GetMD5HashByFile(path));
            Debug.Log("加密文件成功 PlayerPrefs.GetString：" + PlayerPrefs.GetString("Encypt_" + name));
        }
    }

    #region 暂时不需要
    //    [MenuItem("Tools/Build AD")]
    //    public static void BuildApkDeveloper()
    //    {
    //        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
    //        PlayerSettings.productName = "游戏名字";
    //        PlayerSettings.applicationIdentifier = "com.hx.test";
    //        PlayerSettings.bundleVersion = "1.0.101";
    //        //项目路径
    //        string BASE_PATH = Application.dataPath + "/../../";
    //        string project_path = BASE_PATH + "PKG";
    //        string project_name = string.Format("{0}/GPP-{1}{2}[T].apk", project_path, DateTime.Now.Month, DateTime.Now.Day);
    //        bool exist = Directory.Exists(project_path);
    //        if (!exist) Directory.CreateDirectory(project_path);
    //        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, project_name, BuildTarget.Android, BuildOptions.None);
    //    }

    //    [MenuItem("Tools/Build AR")]
    //    public static void BuildApkRelease()
    //    {
    //        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
    //        PlayerSettings.productName = "游戏名字";
    //        PlayerSettings.applicationIdentifier = "com.hx.casino";
    //        PlayerSettings.bundleVersion = "1.0.101";
    //        //项目路径
    //        string BASE_PATH = Application.dataPath + "/../../";
    //        string project_path = BASE_PATH + "PKG";
    //        string project_name = string.Format("{0}/GPP-{1}{2}.apk", project_path, DateTime.Now.Month, DateTime.Now.Day);
    //        bool exist = Directory.Exists(project_path);
    //        if (!exist) Directory.CreateDirectory(project_path);
    //        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, project_name, BuildTarget.Android, BuildOptions.None);
    //    }

    //    [MenuItem("Tools/Build EP")]
    //    public static void ExportGoogleProject()
    //    {
    //#if VE
    //        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
    //        PlayerSettings.productName = "游戏名字[VE]";          //APP名称
    //#else
    //        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
    //        PlayerSettings.productName = "游戏名字";              //APP名称
    //#endif
    //        PlayerSettings.applicationIdentifier = "com.ws.casino";      //APP包名(Only ID)
    //        PlayerSettings.bundleVersion = "1.0.101";
    //        //项目路径
    //        string BASE_PATH = Application.dataPath + "/../../";
    //        string project_path = BASE_PATH + "PKG";
    //        bool exist = Directory.Exists(project_path);
    //        if (!exist) Directory.CreateDirectory(project_path);

    //#if UNITY_ANDROID
    //        //编译出 eclipse 项目
    //        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, project_path, BuildTarget.Android, BuildOptions.AcceptExternalModificationsToPlayer);
    //#if VE
    //        //DLL在android工程中对应的位置
    //        string inpath = project_path + "/" + PlayerSettings.productName + "/assets/bin/Data/Managed/Assembly-CSharp.dll";
    //        if (File.Exists(inpath))
    //        {
    //            //先读取没有加密的dll
    //            byte[] bytes = File.ReadAllBytes(inpath);
    ////             //备份
    ////             File.WriteAllBytes(inpath.Replace(".dll",".bak"), bytes);
    //            //字节偏移 DLL就加密了。
    //            bytes[0] += 1;
    //            //在写到原本的位置上
    //            File.WriteAllBytes(inpath, bytes);
    //        }
    //#endif
    //#elif UNITY_IPHONE
    //        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, project_path, BuildTarget.iOS, BuildOptions.AcceptExternalModificationsToPlayer);
    //#endif
    //    }
    #endregion

    public static void BuildBundleByStyle(int style)
    {
        PlayerSettings.bundleVersion = string.Format("1.0.{0}01", style);
        //PlayerSettings.bundleIdentifier = string.Format("com.jbgp.v0{0}01", style);
        switch (style)
        {
            case 1:
                PlayerSettings.productName = "九州山海开发";
                break;
            default:
                Debug.LogError("ProductName unknow =========>>");
                return;
        }

        AssetDatabase.RemoveAssetBundleName("mainmenu", true);
        AssetDatabase.RemoveAssetBundleName("mainmenu_desc", true);
        AssetDatabase.RemoveAssetBundleName("login", true);
        AssetDatabase.RemoveAssetBundleName("login_desc", true);
        AssetDatabase.RemoveAssetBundleName("sharedassets", true);
        AssetDatabase.RemoveAssetBundleName("sharedassets_desc", true);
        AssetDatabase.RemoveAssetBundleName("datatable", true);
        AssetDatabase.RemoveAssetBundleName("item", true);
        AssetDatabase.RemoveAssetBundleName("item_desc", true);
        AssetDatabase.RemoveAssetBundleName("music", true);
        AssetDatabase.RemoveAssetBundleName("music_desc", true);

        string styleStr = style.ToString();
        if (styleStr == "1")
        {
            styleStr = "";
        }

        string stylePrePath = string.Format("{0}/CreatAssetBundle{1}/", Application.dataPath, styleStr);

        CopyBasickRes(stylePrePath);

        Pack(stylePrePath + "PlatformLobby/DataTable", "DataTable");
        Pack(stylePrePath + "PlatformLobby/Login", "Login");
        Pack(stylePrePath + "PlatformLobby/MainMenu", "MainMenu");
        Pack(stylePrePath + "PlatformLobby/SharedAssets", "SharedAssets");
        Pack(stylePrePath + "PlatformLobby/Item", "Item");
        Pack(stylePrePath + "PlatformLobby/Music", "Music");
        AssetDatabase.Refresh();
        BuildAllAssetBundles(style);
    }

    private static void CheckDllPath()
    {

    }

    private static void CopyBasickRes(string styleStr)
    {
        //覆盖  BasicInPackage
        string resPath = styleStr + "BasicPackage";
        string targetPath = string.Format("{0}/Resources/UIPackage/BasicPackage/", Application.dataPath);
        CopyFolder(resPath, targetPath);
        //覆盖  Logo
        string logoResPath = styleStr + "Logo";
        string logoTargetPath = string.Format("{0}/Resources/Logo/", Application.dataPath);
        CopyFolder(logoResPath, logoTargetPath);
        //当前项目用一个场景足够了，这里就不用拷贝场景文件了
        //覆盖  StartScene
        //CopyFile(styleStr + "StartScene.unity", Application.dataPath + "/Scenes/StartScene.unity", true);
        ////        
        //EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/StartScene.unity");
        //覆盖 登录界面背景音乐
        CopyFile(styleStr + "Sound/music-login-bg.mp3", Application.dataPath + "/Resources/Sound/music-login-bg.mp3", true);
        //覆盖 主界面背景音乐
        CopyFile(styleStr + "Sound/BG_Music.mp3", Application.dataPath + "/Resources/Sound/BG_Music.mp3", true);
        //覆盖icon
        CopyFile(styleStr + "Icon/AppIcon.png", Application.dataPath + "/App/AppIcon.png", true);

    }
    /// <summary>
    /// 文件夹拷贝
    /// </summary>
    /// <param name="resPath"></param>
    /// <param name="targetPath"></param>
    private static void CopyFolder(string resPath, string targetPath)
    {
        if (!Directory.Exists(resPath))
        {
            return;
        }
        DirectoryInfo folder = new DirectoryInfo(resPath);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        int length = files.Length;
        for (int i = 0; i < length; i++)
        {
            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
                File.Copy(files[i].FullName, targetPath + files[i].Name, true);
            }
            else
            {
                File.Copy(files[i].FullName, targetPath + files[i].Name, true);
            }
        }
    }
    /// <summary>
    /// 文件拷贝
    /// </summary>
    /// <param name="resFilePath"></param>
    /// <param name="targetPath"></param>
    /// <param name="overwrite"></param>
    private static void CopyFile(string resFilePath, string targetPath, bool overwrite)
    {
        if (File.Exists(resFilePath))
        {
            File.Copy(resFilePath, targetPath, true);
        }
    }

    public static void Pack(string source, string pakageName)
    {
        DirectoryInfo folder = new DirectoryInfo(source);
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        int length = files.Length;
        for (int i = 0; i < length; i++)
        {
            if (files[i] is DirectoryInfo)
            {
                Pack(files[i].FullName, pakageName);
            }
            else
            {
                if (!files[i].Name.EndsWith(".meta"))
                {
                    file(files[i].FullName, pakageName);
                }
            }
        }
    }

    static void file(string source, string pakageName)
    {
        string _source = Replace(source);
        string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
        string _assetPath2 = _source.Substring(Application.dataPath.Length + 1);

        //在代码中给资源设置AssetBundleName
        AssetImporter assetImporter = AssetImporter.GetAtPath(_assetPath);
        string assetName = Path.GetFileName(_assetPath);
        assetName = assetName.Replace(Path.GetExtension(assetName), "");

        assetName = assetName.ToLower();
        if (assetName.EndsWith("_fui"))
        {
            assetName = pakageName + "_desc";
        }
        else
        {
            assetName = pakageName;
        }
        //Debug.Log (assetName);
        assetImporter.assetBundleName = assetName;
    }

    static string Replace(string s)
    {
        return s.Replace("\\", "/");
    }

    public static void BuildAllAssetBundles(int style = 0, AssetInfo assetInfos = null, string _buildPath = null)
    {
        if (!string.IsNullOrEmpty(_buildPath))
            sourcePath = _buildPath;
        else
            sourcePath = Path.Combine(Application.persistentDataPath, "AllPackage");
        target = Path.Combine(Application.persistentDataPath, "files");
        dllTarget = Path.Combine(target, "PlatformLobbyCode");
        if (!Directory.Exists(sourcePath))
        {
            Directory.CreateDirectory(sourcePath);
        }
        if (!Directory.Exists(target))
        {
            Directory.CreateDirectory(target);
        }
        if (!Directory.Exists(dllTarget))
        {
            Directory.CreateDirectory(dllTarget);
        }

#if UNITY_IPHONE
		BuildPipeline.BuildAssetBundles(sourcePath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);
#else
        BuildPipeline.BuildAssetBundles(sourcePath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
#endif
        Debug.Log("打包成功");
        AssetManager.TryEncyptAssetBundle(sourcePath);
#if UNITY_EDITOR_WIN || UNITY_EDITOR_OSX
        Debug.Log("sourcePath::" + sourcePath);
        Debug.Log("target::" + target);
        num = 0;

        #region 使用分文件夹打资源

        CopyFile("particleeffects", "PlatformLobby");//特殊资源，本来是特效文件夹下的 需要单独下载 因为一些原因 直接放平台文件夹下 直接开始就下载好

        //大厅资源 自动拷贝平台文件夹 资源
        string stylePrePath = string.Format("{0}/CreatAssetBundle/PlatformLobby/", Application.dataPath);
        DirectoryInfo folder = new DirectoryInfo(stylePrePath);
        FileSystemInfo[] files = folder.GetDirectories();
        for (int i = 0; i < files.Length; i++)
        {
            CopyFile(files[i].Name.ToLower(), "PlatformLobby");
        }

        #region 其他资源 文件夹名必须和bundle名一样 
        if (assetInfos == null)
        {
            if (_assetInfos == null)
            {
                _assetInfos = new AssetInfo(Application.dataPath, "Assets", false);
            }
            assetInfos = _assetInfos;
            EditorTool.ReadAssetsInChildren(assetInfos);
        }

        string[] bundles = AssetDatabase.GetAllAssetBundleNames();
        for (int i = 0; i < assetInfos.ChildAssetInfo.Count; i++)
        {
            if (assetInfos.ChildAssetInfo[i].AssetName == "CreatAssetBundle")
            {
                List<AssetInfo> infos = assetInfos.ChildAssetInfo[i].ChildAssetInfo;
                AssetInfo info = null;
                for (int q = 0; q < bundles.Length; q++)
                {
                    info = infos.Find((AssetInfo data) =>
                    {
                        return data.AssetName.ToLower() == bundles[q];
                    });
                    if (info != null)
                    {
                        CopyFile(bundles[q], info.AssetName);
                    }
                    info = null;
                }
                break;
            }
        }
        #endregion

        #endregion

        //string[] bundles = AssetDatabase.GetAllAssetBundleNames();
        //for (int q = 0; q < bundles.Length; q++)
        //{
        //    CopyFile(bundles[q], "PlatformLobby");
        //}
        Debug.Log("复制完成，总共复制 " + num + " 个文件");
#endif
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">要复制的资源名</param>
    /// <param name="tar">目的文件夹</param>
    static void CopyFile(string name, string tar)
    {
        string s1 = sourcePath + "/" + name;
        string s2 = target + "/" + tar + "/" + name;
        if (!Directory.Exists(target + "/" + tar))//创建文件夹
        {
            Directory.CreateDirectory(target + "/" + tar);
        }
        if (!CompareTwoFile(s1, s2))
        {
            File.Copy(s1, s2, true);
            Debug.Log("复制成功：" + s2);
            num++;
        }
        if (!CompareTwoFile(s1 + "_desc", s2 + "_desc"))
        {
            File.Copy(s1 + "_desc", s2 + "_desc", true);
            Debug.Log("复制成功：" + s2 + "_desc");
            num++;
        }
        if (!CompareTwoFile(s1 + "scene", s2 + "scene"))
        {
            File.Copy(s1 + "scene", s2 + "scene", true);
            Debug.Log("复制成功：" + s2 + "scene");
            num++;
        }
    }
    /// <summary>
    /// 返回false才代表不同，需要复制
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static bool CompareTwoFile(string s1, string s2)
    {
        if (File.Exists(s1))
        {
            if (File.Exists(s2))
            {
                return GetMD5HashFromFile(s1) == GetMD5HashFromFile(s2);
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public static string GetMD5HashFromFile(string fileName)
    {
        try
        {
            File.Exists(fileName);
            FileStream file = new FileStream(fileName, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("获取文件MD5失败:" + ex.Message);
        }
    }
}