using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;
public class BundleAtlasInfo
{
    private TextureImporterPlatformSettings platformSettings = new TextureImporterPlatformSettings();//平台压缩设置        
    public TextureImporterFormat format = TextureImporterFormat.ASTC_4x4;//图集压缩方式
    public TextureCompressionQuality compressionQuality = TextureCompressionQuality.Normal;//图集压缩质量
    public int maxTextureSize = 2048;
    public bool overridden = true;


    public TextureImporterType spriteType = TextureImporterType.Sprite;//图集类型
    public TextureImporterType defaultType = TextureImporterType.Default;//图集类型
    public bool mipmapEnabled = false; //是否开启mipmap 否则会模糊
    public bool ignorePngGamma = false;//
    public bool isReadable = false;  //是否可以读写纹理 会存在2份内存
    public bool alphaIsTransparency = true; //是否开启 alpha = 透明度
    public bool sRGBTexture = true;//
    public int spritePixelsPerUnit = 100;
    public bool autoSize = false;
    public bool isValid = false;

    public BundleAtlasInfo()
    {
        UpdateInfo();
    }
    public TextureImporterPlatformSettings GetPlatformSettings()
    {
        UpdateInfo();
        return platformSettings;
    }
    public void UpdateInfo()
    {
        platformSettings.name = "Android";
        platformSettings.overridden = overridden;
        platformSettings.maxTextureSize = maxTextureSize;
        platformSettings.format = format;
        platformSettings.compressionQuality = (int)compressionQuality;
        platformSettings.androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings;
    }
}
/// <summary>
/// 优化 bundle 图集 窗口
/// </summary>
public class BundleAtlasOptimizeWindow : EditorWindow
{
    public UnityEngine.Object go = null;

    Dictionary<DirectoryInfo, FileSystemInfo[]> fileDir = new Dictionary<DirectoryInfo, FileSystemInfo[]>();
    List<DirectoryInfo> dirList = new List<DirectoryInfo>();
    Dictionary<DirectoryInfo, BundleAtlasInfo> dirInfoDic = new Dictionary<DirectoryInfo, BundleAtlasInfo>();
    public Dictionary<string, string> spineSkeletonData = new Dictionary<string, string>();
    private void OnEnable()
    {
        titleContent.text = "BundleAtlas优化";
        ShowUtility();
    }
    private void Awake()
    {
        LoadData();
    }

    /// <summary>
    /// 加载数据
    /// </summary>
    private void LoadData()
    {
        DirectoryInfo folder = new DirectoryInfo(Application.dataPath + "/CreatAssetBundle");
        fileDir.Clear();
        dirList.Clear();
        dirInfoDic.Clear();
        string[] bundles = AssetDatabase.GetAllAssetBundleNames();
        List<string> bundless = new List<string>();
        bundless.AddRange(bundles);
        LoadInfo(folder, bundless);
        info = null;
        dir = null;
    }

    private void LoadInfo(DirectoryInfo folder, List<string> bundless)
    {
        FileSystemInfo[] files = folder.GetFileSystemInfos();
        var iter = files.GetEnumerator();
        string name;
        while (iter.MoveNext())
        {
            info = (FileSystemInfo)iter.Current;
            if (info is DirectoryInfo)
            {
                dir = info as DirectoryInfo;
                if (bundless.Contains(dir.Name.ToLower()))
                {
                    dirList.Add(dir);
                    dirInfoDic.Add(dir, new BundleAtlasInfo());
                    fileDir.Add(dir, dir.GetFileSystemInfos());
                }
                LoadInfo(dir, bundless);
            }
            else
            {
                name = GetNameByShort(info);
                if (info.Name.EndsWith(".json"))
                {
                    if (!spineSkeletonData.ContainsKey(name))
                    {
                        spineSkeletonData[name] = name + ".png";
                    }
                }
            }
        }
    }

    private string GetNameByShort(FileSystemInfo info)
    {
        return info.Name.Split('.')[0];
    }

    FileSystemInfo info;
    DirectoryInfo dir = null;
    BundleAtlasInfo bundleInfo = null;
    FileSystemInfo[] files = null;
    string path = null;
    void OnGUI()
    {
        TitleHorizontalUI();
        GUILayout.BeginVertical();
        var iter = dirList.GetEnumerator();
        while (iter.MoveNext())
        {
            dir = iter.Current;
            bundleInfo = dirInfoDic[dir];


            bundleInfo.isValid = EditorGUILayout.BeginToggleGroup("IsValid", bundleInfo.isValid);
            GUILayout.BeginHorizontal();
            GUILayout.Label(dir.Name, EditorStyles.boldLabel, new[] { GUILayout.MinWidth(170) });

            bundleInfo.format = (TextureImporterFormat)EditorGUILayout.EnumPopup(bundleInfo.format);
            bundleInfo.compressionQuality = (TextureCompressionQuality)EditorGUILayout.EnumPopup(bundleInfo.compressionQuality);
            GUILayout.EndHorizontal();
            EditorGUILayout.EndToggleGroup();
        }
        bundleInfo = null;
        dir = null;
        iter.Dispose();
        GUILayout.EndVertical();
        if (GUILayout.Button("确定"))
        {
            if (EditorUtility.DisplayDialog("BundleAtlas优化", "是否确认修改图集设置", "确认", "取消"))
            {
                Execute();
            }
        }
    }
    void TitleHorizontalUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Bundle", EditorStyles.boldLabel);
        GUILayout.Label("AtlasFormat", EditorStyles.boldLabel);
        GUILayout.Label("AtlasQuality", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();
    }
    bool isPower(int n)
    {
        if (n < 1)
            return false;
        int i = 1;
        while (i <= n)
        {
            if (i == n)
                return true;
            i <<= 1;
        }
        return false;
    }
    byte[] getTextureByte(string texturePath)
    {
        FileStream file = new FileStream(texturePath, FileMode.Open);
        byte[] txByte = new byte[file.Length];
        file.Read(txByte, 0, txByte.Length);
        file.Close();
        return txByte;
    }

    void Execute()
    {
        Debug.Log("开始");
        var iter = dirInfoDic.GetEnumerator();
        bool isSpineTexture = false;
        FileSystemInfo fileInfo;
        while (iter.MoveNext())
        {
            dir = iter.Current.Key;
            bundleInfo = iter.Current.Value;
            if (!fileDir.ContainsKey(dir) || !bundleInfo.isValid)
            {
                continue;
            }
            files = fileDir[dir];

            for (int i = 0; i < files.Length; i++)
            {
                fileInfo = files[i];
                if (fileInfo is DirectoryInfo || !fileInfo.Name.EndsWith(".png"))
                {
                    continue;
                }
                int index = fileInfo.FullName.IndexOf("Assets");
                if (index == -1)
                    continue;
                isSpineTexture = spineSkeletonData.ContainsKey(GetNameByShort(fileInfo));
                path = fileInfo.FullName.Substring(index);
                //texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);                
                //path = AssetDatabase.GetAssetPath(texture);
                TextureImporter import = AssetImporter.GetAtPath(path) as TextureImporter;
                if (import == null)
                    continue;
                import.textureType = bundleInfo.defaultType;
                import.textureShape = TextureImporterShape.Texture2D;
                import.mipmapEnabled = bundleInfo.mipmapEnabled;
                import.ignorePngGamma = bundleInfo.ignorePngGamma;
                import.isReadable = bundleInfo.isReadable;
                import.alphaIsTransparency = isSpineTexture ? false : bundleInfo.alphaIsTransparency;
                import.sRGBTexture = bundleInfo.sRGBTexture;
                import.spritePixelsPerUnit = bundleInfo.spritePixelsPerUnit;
                import.maxTextureSize = bundleInfo.maxTextureSize;
                import.compressionQuality = (int)bundleInfo.compressionQuality;

                //NONPOWER2的贴图如果强制被拉伸成了POWER2，会产生黑边
                import.npotScale = TextureImporterNPOTScale.None;
                //透明度直接使用Texture自己的信息
                import.alphaSource = TextureImporterAlphaSource.FromInput;

                import.SetPlatformTextureSettings(bundleInfo.GetPlatformSettings());
                import.SaveAndReimport();
                //AssetDatabase.ImportAsset(path);
            }
        }
        iter.Dispose();
        files = null;
        dir = null;
        path = null;
        bundleInfo = null;
        Debug.Log("结束");
        Close();
    }
}
