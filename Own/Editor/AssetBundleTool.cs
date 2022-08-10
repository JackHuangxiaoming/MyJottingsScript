using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static AssetInfo;

public class EditorTool
{
    //链接至静态工具类【AssetBundleTool.cs】
    /// <summary>
    /// 读取资源文件夹下的所有子资源
    /// </summary>
    public static void ReadAssetsInChildren(AssetInfo asset)
    {
        //不是文件夹对象，不存在子对象
        if (asset.AssetFileType != FileType.Folder)
        {
            return;
        }
        asset.ChildAssetInfo.Clear();
        //打开这个文件夹
        DirectoryInfo di = new DirectoryInfo(asset.AssetFullPath);
        //获取其中所有内容，包括文件或子文件夹
        FileSystemInfo[] fileinfo = di.GetFileSystemInfos();
        //遍历这些内容
        foreach (FileSystemInfo fi in fileinfo)
        {
            //如果该内容是文件夹
            if (fi is DirectoryInfo)
            {
                //判断是否是无效的文件夹，比如是否是Editor，StreamingAssets等，这些文件夹中的东西是无法打进AB包的
                if (IsValidFolder(fi.Name))
                {
                    //是合格的文件夹，就创建为文件夹对象，并加入到当前对象的子对象集合
                    AssetInfo ai = new AssetInfo(fi.FullName, fi.Name, false);
                    asset.ChildAssetInfo.Add(ai);

                    //然后继续深层遍历这个文件夹
                    ReadAssetsInChildren(ai);
                }

            }
            //否则该内容是文件
            else
            {
                //确保不是.meta文件
                //并且确保不是mac上的缓存文件
                if (fi.Extension != ".meta" && fi.Extension != ".DS_Store")
                {
                    //是合格的文件，就创建为资源文件对象，并加入到当前对象的子对象集合
                    AssetInfo ai = new AssetInfo(fi.FullName, fi.Name, fi.Extension);
                    asset.ChildAssetInfo.Add(ai);
                }
            }
        }
    }

    public static bool IsValidFolder(string name)
    {
        switch (name)
        {
            case "ArtAssets":
            case "App":
            case "cloudbase-dotnet-sdk":
            case "com.unity.mgobe":
            case "Editor":
            case "FairyGUI":
            case "Own":
            case "Plugins":
            case "Resources":
            case "Scripts":
            case "Spine":
            case "Spine Examples":
            case "StreamingAssets":
                return false;
            default:
                return true;
        }
    }

    public static FileType GetFileTypeByExtension(string extension)
    {
        if (string.IsNullOrEmpty(extension))
        {
            //return FileType.BinaryFile;
            return FileType.ValidFile;
        }
        switch (extension)
        {
            case ".meta":
            case ".cs":
            case ".js":
                return FileType.InValidFile;
            default:
                return FileType.ValidFile;
        }
    }

    /// <summary>
    /// 清空AB包中的资源
    /// </summary>
    public static void ClearAsset(AssetBundleBuildInfo build)
    {
        AssetDatabase.RemoveAssetBundleName(build.Name, true);
    }
    static void GetCheckedAssets(List<AssetInfo> currentAssets, AssetInfo info)
    {
        AssetInfo curInfo;
        if (info.ChildAssetInfo.Count > 0)
        {
            for (int i = 0; i < info.ChildAssetInfo.Count; i++)
            {
                curInfo = info.ChildAssetInfo[i];
                if (curInfo.AssetFileType == FileType.Folder)
                {
                    GetCheckedAssets(currentAssets, curInfo);
                    continue;
                }
                if (curInfo.IsChecked)
                {
                    currentAssets.Add(curInfo);
                }
            }
        }
    }
    /// <summary>
    /// 获取所有被选中的有效资源
    /// </summary>
    public static List<AssetInfo> GetCheckedAssets(List<AssetInfo> validAssetList)
    {
        List<AssetInfo> currentAssets = new List<AssetInfo>();
        for (int i = 0; i < validAssetList.Count; i++)
        {
            if (validAssetList[i].AssetFileType == FileType.Folder)
            {
                GetCheckedAssets(currentAssets, validAssetList[i]);
                continue;
            }
            if (validAssetList[i].IsChecked)
            {
                currentAssets.Add(validAssetList[i]);
            }
        }
        return currentAssets;
    }

    public static void Build(AssetInfo _asset = null, string _buildPath = null)
    {
        BuildAssetBundles.BuildAllAssetBundles(1, _asset, _buildPath);
    }    
}
