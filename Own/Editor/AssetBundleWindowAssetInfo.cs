using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class AssetInfo
{
    /// <summary>
    /// 资源文件类型
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// 有效的文件资源
        /// </summary>
        ValidFile,
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder,
        /// <summary>
        /// 无效的文件资源
        /// </summary>
        InValidFile,
        /// <summary>
        /// 二进制文件(没有后缀的文件)
        /// </summary>
        BinaryFile
    }
    /// <summary>
    /// 资源全路径
    /// </summary>
    public string AssetFullPath
    {
        get;
        private set;
    }
    /// <summary>
    /// 资源路径
    /// </summary>
    public string AssetPath
    {
        get;
        private set;
    }
    /// <summary>
    /// 资源名称
    /// </summary>
    public string AssetName
    {
        get;
        private set;
    }
    public string Extension
    {
        get;
        private set;
    }
    /// <summary>
    /// 资源的GUID（文件夹无效）
    /// </summary>
    public string GUID
    {
        get;
        private set;
    }
    /// <summary>
    /// 资源文件类型
    /// </summary>
    public FileType AssetFileType
    {
        get;
        private set;
    }
    /// <summary>
    /// 资源类型（文件夹无效）
    /// </summary>
    public Type AssetType
    {
        get;
        private set;
    }
    /// <summary>
    /// 资源是否勾选
    /// </summary>
    public bool IsChecked
    {
        get;
        set;
    }
    /// <summary>
    /// 文件夹是否展开（资源无效）
    /// </summary>
    public bool IsExpanding
    {
        get;
        set;
    }
    /// <summary>
    /// 所属AB包（文件夹无效）
    /// </summary>
    public string Bundled
    {
        get;
        set;
    }
    /// <summary>
    /// 文件夹的子资源（资源无效）
    /// </summary>
    public List<AssetInfo> ChildAssetInfo
    {
        get;
        set;
    }

    /// <summary>
    /// 文件夹类型资源
    /// </summary>
    public AssetInfo(string fullPath, string name, bool isExpanding)
    {
        AssetFullPath = fullPath;
        AssetPath = "Assets" + fullPath.Replace(Application.dataPath.Replace("/", "\\"), "");
        AssetName = name;
        GUID = "";
        AssetFileType = FileType.Folder;
        AssetType = null;
        IsChecked = false;
        IsExpanding = isExpanding;
        Bundled = "";
        Extension = "";
        ChildAssetInfo = new List<AssetInfo>();
    }

    /// <summary>
    /// 文件类型资源
    /// </summary>
    public AssetInfo(string fullPath, string name, string extension)
    {
        AssetFullPath = fullPath;
        
#if UNITY_EDITOR_OSX
        //之前的代码反正在MAC上是有问题的，不确定Windows上是否正常，所以我这里只改动Mac的。
        AssetPath = "Assets/" + fullPath.Replace(Application.dataPath, "");
#else
        AssetPath = "Assets" + fullPath.Replace(Application.dataPath.Replace("/", "\\"), "");
#endif
        AssetName = name;
        Extension = extension;
        GUID = AssetDatabase.AssetPathToGUID(AssetPath);
        AssetFileType = EditorTool.GetFileTypeByExtension(extension);
        AssetType = AssetDatabase.GetMainAssetTypeAtPath(AssetPath);
        IsChecked = false;
        IsExpanding = false;
        Bundled = AssetDatabase.GetImplicitAssetBundleName(AssetPath);
        ChildAssetInfo = null;
        AssetBundleInfo.TryAddBundleInfo(this);
    }

    public List<AssetInfo> GetCheckedAssets()
    {
        return EditorTool.GetCheckedAssets(ChildAssetInfo);
    }
}

public class AssetBundleBuildInfo
{
    /// <summary>
    /// AB包的名称
    /// </summary>
    public string Name
    {
        get;
        set;
    }
    /// <summary>
    /// AB包中的所有资源
    /// </summary>
    public List<AssetInfo> Assets
    {
        get;
        set;
    }

    public AssetBundleBuildInfo(string name)
    {
        Name = name;
        Assets = new List<AssetInfo>();

    }

    public void RenameAssetBundle(string renameValue)
    {
        Name = renameValue;
        for (int i = 0; i < Assets.Count; i++)
        {
            AssetImporter import = AssetImporter.GetAtPath(Assets[i].AssetPath);
            import.assetBundleName = Name;
        }
    }
    public void RemoveAsset(AssetInfo assetInfo)
    {
        assetInfo.Bundled = "";
        if (Assets.Contains(assetInfo))
        {
            Assets.Remove(assetInfo);
            AssetImporter import = AssetImporter.GetAtPath(assetInfo.AssetPath);
            import.assetBundleName = "";
        }
    }

    public void ClearAsset()
    {
        if (Assets != null)
        {
            EditorTool.ClearAsset(this);
            Assets.Clear();
        }
    }

    public void AddAsset(AssetInfo assetInfo)
    {
        if (assetInfo != null && Assets != null)
        {
            assetInfo.IsChecked = false;
            if (!Assets.Contains(assetInfo))
            {
                Assets.Add(assetInfo);
                assetInfo.Bundled = Name;
                AssetImporter import = AssetImporter.GetAtPath(assetInfo.AssetPath);
                import.assetBundleName = Name;
            }
        }
    }
}
public class AssetBundleInfo
{
    private static AssetBundleInfo _instance;
    /// <summary>
    /// 当前的所有AB包
    /// </summary>
    public List<AssetBundleBuildInfo> AssetBundles
    {
        get;
        set;
    }

    public AssetBundleInfo()
    {
        _instance = this;
        AssetBundles = new List<AssetBundleBuildInfo>();
    }

    public bool IsExistName(string renameValue)
    {
        foreach (var item in AssetBundles)
        {
            if (item.Name == renameValue)
            {
                return true;
            }
        }
        return false;
    }
    public AssetBundleBuildInfo TryGetByName(string name)
    {
        foreach (var item in AssetBundles)
        {
            if (item.Name == name)
            {
                return item;
            }
        }
        return null;
    }

    public void DeleteAssetBundle(int currentAB)
    {
        if (currentAB < AssetBundles.Count)
        {
            AssetBundles[currentAB].ClearAsset();
            AssetBundles.RemoveAt(currentAB);
        }
    }

    public static void TryAddBundleInfo(AssetInfo assetInfo)
    {
        if (_instance != null)
        {
            if (assetInfo != null && !string.IsNullOrEmpty(assetInfo.Bundled))
            {
                AssetBundleBuildInfo info = _instance.TryGetByName(assetInfo.Bundled);
                if (info == null)
                {
                    info = new AssetBundleBuildInfo(assetInfo.Bundled);
                    _instance.AssetBundles.Add(info);
                }
                info.AddAsset(assetInfo);
            }
        }
    }
}
