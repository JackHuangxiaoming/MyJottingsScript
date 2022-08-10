using UnityEngine;
using System.IO;
using System;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;

public class BuildAssetBundles
{
    static string sourcePath = @"C:\Users\Administrator\AppData\LocalLow\WS\皇家娱乐场\AllPackage";
    static string target = @"C:\Users\Administrator\AppData\LocalLow\WS\皇家娱乐场\files";
    static int num = 0;


    [MenuItem("Tools/BuildStyle1")]
    public static void BuildStyle1()
    {
        BuildBundleByStyle(1);
    }

    [MenuItem("Tools/BuildStyle2")]
    public static void BuildStyle2()
    {
        BuildBundleByStyle(2);
    }

    [MenuItem("Tools/BuildStyle3")]
    public static void BuildStyle3()
    {
        BuildBundleByStyle(3);
    }

    [MenuItem("Tools/BuildStyle4")]
    public static void BuildStyle4()
    {
        BuildBundleByStyle(4);
    }

    private static void BuildBundleByStyle(int style)
    {
        PlayerSettings.bundleVersion = string.Format("1.0.0.{0}01", style);
        PlayerSettings.bundleIdentifier = string.Format("com.redao.v0{0}01", style);
        //switch (style)
        //{
        //    //case 1:
        //    //    PlayerSettings.productName = "皇家娱乐场";
        //    //    break;
        //    //case 2:
        //    //    PlayerSettings.productName = "维加斯电玩城";

        //    //    break;
        //    //case 3:
        //    //    PlayerSettings.productName = "德州电玩城";
        //    //    break;
        //    //case 4:
        //    //    PlayerSettings.productName = "大富豪娱乐场";
        //    //    break;
        //    //default:
        //    //    PlayerSettings.productName = string.Format("皇家娱乐场{0}", style);
        //    //    break;
        //}

        //  AssetDatabase.RemoveAssetBundleName("mainmenu", true);
        // AssetDatabase.RemoveAssetBundleName("mainmenu_desc", true);
        // AssetDatabase.RemoveAssetBundleName("login", true);
        // AssetDatabase.RemoveAssetBundleName("login_desc", true);
        AssetDatabase.RemoveAssetBundleName("NiuNiu", true);
        AssetDatabase.RemoveAssetBundleName("NiuNiu", true);
        string styleStr = style.ToString();
        if (styleStr == "1")
        {
            styleStr = "";
        }

        string stylePrePath = string.Format("{0}/CreatAssetBundle{1}/", Application.dataPath, styleStr);
        //拷贝基础资源
        CopyBasickRes(stylePrePath);


        //Pack(stylePrePath + "PlatformLobby/Login", "Login");
        //Pack(stylePrePath + "PlatformLobby/MainMenu", "MainMenu");
        Pack(stylePrePath + "PlatformLobby/NiuNiu", "NiuNiu");

        AssetDatabase.Refresh();
        BuildAllAssetBundles(style);
    }

    private static void CopyBasickRes(string styleStr)
    {
        //覆盖  BasicInPackage
        //C:\Users\Administrator\Desktop\TempNiuNiuClient\HuangmingfuTempNiuNiuClient\TempNiuNiuClientUnity\Assets\CreatAssetBundle\
        string resPath = styleStr + "BasicInPackage";

        string targetPath = string.Format("{0}/Resources/UIPackage/BasicInPackage/", Application.dataPath);
        CopyFolder(resPath, targetPath);

        //覆盖  Logo
        string logoResPath = styleStr + "Logo";
        string logoTargetPath = string.Format("{0}/Resources/Logo/", Application.dataPath);
        CopyFolder(logoResPath, logoTargetPath);

        //覆盖  StartScene
        CopyFile(styleStr + "StartScene.unity", Application.dataPath + "/Scenes/usedScene/StartScene.unity", true);

        //EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/usedScene/StartScene.unity");

        //覆盖 登录界面背景音乐
        CopyFile(styleStr + "Sound/music-login-bg.mp3", Application.dataPath + "/Resources/Sound/music-login-bg.mp3", true);

        //覆盖 主界面背景音乐
        CopyFile(styleStr + "Sound/BG_Music.mp3", Application.dataPath + "/Resources/Sound/BG_Music.mp3", true);

        //覆盖icon
        CopyFile(styleStr + "Icon/AppIcon.png", Application.dataPath + "/App/AppIcon.png", true);



    }
    /// <summary>
    /// 拷贝文件夹
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
            File.Copy(files[i].FullName, targetPath + files[i].Name, true);
        }
    }
    /// <summary>
    /// 复制文件
    /// </summary>
    /// <param name="resFilePath"></param>
    /// <param name="targetPath"></param>
    /// <param name="overwrite"></param>
    private static void CopyFile(string resFilePath, string targetPath, bool overwrite)
    {
        if (File.Exists(resFilePath))
        {
            string[] s = targetPath.Split('/');
            string path = targetPath.Substring(0, (targetPath.Length - s[s.Length - 1].Length) - 1);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.Copy(resFilePath, targetPath, true);
        }
    }

    static void Pack(string source, string pakageName)
    {
        //source = //Application.dataPath/CreatAssetBundle/
        //pakage = 包名
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

    /// <summary>
    /// 拷贝文件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="pakageName"></param>
    static void file(string source, string pakageName)
    {
        //文件全路径 Application.dataPath/CreatAssetBundle/StartScene.unity
        string _source = Replace(source);

        //Assets + /CreatAssetBundle/StartScene.unity
        string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
        string _assetPath2 = _source.Substring(Application.dataPath.Length + 1);

        //在代码中给资源设置AssetBundleName
        AssetImporter assetImporter = AssetImporter.GetAtPath(_assetPath);
        string assetName = Path.GetFileName(_assetPath);
        assetName = assetName.Replace(Path.GetExtension(assetName), "");

        assetName = assetName.ToLower();
        if (assetName == pakageName.ToLower())
        {
            assetName = pakageName + "_desc";
        }
        else
        {
            assetName = pakageName;
        }
        //Debug.Log(assetName);
        assetImporter.assetBundleName = assetName;
    }

    static string Replace(string s)
    {
        return s.Replace("\\", "/");
    }

    [MenuItem("Tools/Build AssetBundles")]
    static void BuildAllAssetBundles(int style = 0)
    {
        sourcePath = Path.Combine(Application.persistentDataPath, "AllPackage");
        target = Path.Combine(Application.persistentDataPath, "files");
        if (!Directory.Exists(sourcePath))
        {
            Directory.CreateDirectory(sourcePath);
        }
        if (!Directory.Exists(target))
        {
            Directory.CreateDirectory(target);
        }


#if UNITY_IPHONE
		BuildPipeline.BuildAssetBundles(sourcePath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);
#else
        BuildPipeline.BuildAssetBundles(sourcePath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
#endif
        Debug.Log("打包成功");
#if UNITY_EDITOR_WIN
        Debug.Log("sourcePath::" + sourcePath);
        Debug.Log("target::" + target);


        num = 0;
        //大厅资源
        //CopyFile("mainmenu", "PlatformLobby");
        //CopyFile("login", "PlatformLobby");
        //CopyFile("login_desc", "PlatformLobby");
        //opyFile("sharedassets", "PlatformLobby");
        // CopyFile("bird", "Bird");
        CopyFile("niuniu", "yueJuNiuNiu");
        // CopyFile("poker", "DouDiZhu");
        // CopyFile("bjl", "BJL");
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
        //sourcePath = LocalLow/redaoPlatform/YueJuNiuNiu\AllPackage
        //target = LocalLow/redaoPlatform/YueJuNiuNiu\files
        string s1 = sourcePath + @"\" + name;
        string s2 = target + @"\" + tar + @"\" + name;
        if (!Directory.Exists(target + @"\" + tar))//创建文件夹
        {
            Directory.CreateDirectory(target + @"\" + tar);
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