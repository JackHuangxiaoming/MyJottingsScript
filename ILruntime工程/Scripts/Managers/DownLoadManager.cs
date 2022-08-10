using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using System.Net;
using FairyGUI;

/// <summary>
/// 资源下载管理器。
/// 获取更新。
/// </summary>
public class DownLoadManager : MonoBehaviour
{

    private static DownLoadManager mInstance;
    public static DownLoadManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject obj = new GameObject("DownLoadManager");
                mInstance = obj.AddComponent<DownLoadManager>();
                GameObject.DontDestroyOnLoad(obj);
            }
            return mInstance;
        }
    }

    private Dictionary<string, GameResFileInfo> _gameResfileInfoDict = new Dictionary<string, GameResFileInfo>();
    private Dictionary<string, GameVersionState> _gameVersionStateDict = new Dictionary<string, GameVersionState>();
    private Dictionary<string, DownLoadCombin> _startLoadGameDict = new Dictionary<string, DownLoadCombin>();
    private WebClient _assetBundleListWebClient;

    /// <summary>
    /// 是否初始化完成 
    /// </summary>
    public static bool IsInited { get; set; }

    private bool _isDownLoadAssetListed = false;

    /// <summary>
    /// 初始化。
    /// </summary>
    public void Initialize()
    {
        _assetBundleListWebClient = new WebClient();
        _assetBundleListWebClient.DownloadProgressChanged += myWebClient_DownloadProgressChanged;
        _assetBundleListWebClient.DownloadFileCompleted += loadVersionWebClient_DownloadFileCompleted;
        RestartToDownLoad(null);
    }

    private void myWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        if (e.ProgressPercentage == 100)
        {
            _isDownLoadAssetListed = true;
        }
    }

    void loadVersionWebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (!_isDownLoadAssetListed)
        {
            WaitToRestartToDownLoad();
        }
        else
        {
            _assetBundleListWebClient.Dispose();
            InitGameResFileInfo();
        }
    }

    public void WaitToRestartToDownLoad()
    {
        Timers.inst.Add(5, 1, RestartToDownLoad);
    }

    private void RestartToDownLoad(object param)
    {
        Utils.Log("下载AssetList.txt");
        string path = Path.Combine(URLFactory.DownLoadHotFilePath, "AssetList.txt");
        string serverResPath = Path.Combine(URLFactory.ServerGameResAndroidPath, "AssetList.txt");

        if (!Directory.Exists(URLFactory.DownLoadHotFilePath))
            Directory.CreateDirectory(URLFactory.DownLoadHotFilePath);
        if (!Directory.Exists(URLFactory.DownLoadTagPath))
            Directory.CreateDirectory(URLFactory.DownLoadTagPath);
        if (File.Exists(path))
            File.Delete(path);
        _assetBundleListWebClient.DownloadFileAsync(new Uri(serverResPath), path);
    }

    private void InitGameResFileInfo()
    {
        StreamReader streamReader = new StreamReader(Path.Combine(URLFactory.DownLoadHotFilePath, "AssetList.txt"));
        string str = null;
        while (!streamReader.EndOfStream)
        {
            str = streamReader.ReadLine();
            if (str.Contains("//"))
            {
                continue;
            }
            if (str.Contains("ApplicationVersion"))
            {
                URLFactory.NewAppVersion = str.Split('~')[0];
                continue;
            }
            else if (str.Contains("ApkUrl"))
            {
                URLFactory.NewAppVersion = str.Split('~')[0];
                continue;
            }
            DownLoadFileInfo info = new DownLoadFileInfo(str);
            if (info.IsInvalid) continue;
            if (!_gameResfileInfoDict.ContainsKey(info.GameName))
            {
                _gameResfileInfoDict[info.GameName] = new GameResFileInfo(info.GameName);
            }
            _gameResfileInfoDict[info.GameName].AddFileInfo(info);
        }
        streamReader.Close();

        foreach (string gameName in _gameResfileInfoDict.Keys)
        {
            GameVersionState state = GetGameResVersionState(gameName);
            _gameVersionStateDict[gameName] = state;
        }
        IsInited = true;
    }


    /// <summary>
    /// 比较版本号确定是否需要下载。
    /// </summary>
    /// <param name="gameName"></param>
    /// <returns></returns>
    public GameVersionState GetGameResVersionState(string gameName)
    {
        if (!_gameResfileInfoDict.ContainsKey(gameName))
        {
            return GameVersionState.NewestVersion;
        }
        if (_gameVersionStateDict.ContainsKey(gameName))
        {
            return _gameVersionStateDict[gameName];
        }
        GameResFileInfo gameResFileInfo = _gameResfileInfoDict[gameName];
        GameVersionState totalState = GameVersionState.NewestVersion;
        foreach (DownLoadFileInfo info in gameResFileInfo.infoList)
        {
            GameVersionState state = CheckOnceFileInfo(info);
            if (state == GameVersionState.UnLoad)
            {
                return GameVersionState.UnLoad;
            }
            else if (state == GameVersionState.OldVersion)
            {
                totalState = state;
            }
        }
        return totalState;
    }

    public static GameVersionState CheckOnceFileInfo(DownLoadFileInfo info)
    {
        int gameServerVersion = info.version;
        string localTagPath = URLFactory.GetLocalGameTagPath(info);
        if (File.Exists(localTagPath))
        {
            StreamReader streamReader = new StreamReader(localTagPath);
            string versionStr = streamReader.ReadToEnd();
            streamReader.Close();
            int versioni;
            if (int.TryParse(versionStr, out versioni))
            {
                if (versioni >= gameServerVersion)
                    return GameVersionState.NewestVersion;
                return GameVersionState.OldVersion;
            }
        }
        return GameVersionState.UnLoad;
    }


    /// <summary>
    /// 文件夹是否是最新版本
    /// </summary>
    /// <param name="gameName">文件夹</param>
    /// <returns></returns>
    public bool IsNewestVersion(string gameName)
    {
        if (gameName.EndsWith(".zip"))
        {
            int index = gameName.LastIndexOf(".zip");
            gameName = gameName.Remove(index);
        }
        GameVersionState state = GetGameResVersionState(gameName);
        return state == GameVersionState.NewestVersion;
    }

    /// <summary>
    /// 开始下载对应资源
    /// </summary>
    /// <param name="gameName">文件夹</param>
    /// <param name="handler"></param>
    public DownLoadCombin DownLoadGame(string gameName, DownLoadCompletedHandler handler = null)
    {
        if (_startLoadGameDict.ContainsKey(gameName) && _startLoadGameDict[gameName].IsLoading)
        {
            return _startLoadGameDict[gameName];
        }
        DownLoadCombin dlc = DownLoadCombin.CreatToStartDownLoad(_gameResfileInfoDict[gameName], handler);
        _startLoadGameDict[gameName] = dlc;
        return dlc;
    }

    public void DestroyDownloadCombin(string gameName)
    {
        if (_startLoadGameDict.ContainsKey(gameName))
        {
            _startLoadGameDict[gameName].Destroy();
            _startLoadGameDict.Remove(gameName);
        }

    }
    private void DecompressAsset()
    {
    }

    public DownLoadCombin GetDownLoadCombin(string gameName)
    {
        if (_startLoadGameDict.ContainsKey(gameName))
        {
            return _startLoadGameDict[gameName];
        }
        return null;
    }

    public void UpdateGameVersion(string gameName)
    {
        _gameVersionStateDict[gameName] = GameVersionState.NewestVersion;
    }


}

public class DownLoadFileInfo
{
    public string GameName;
    public string fileName;
    /// <summary>
    /// 单位  KB
    /// </summary>
    public float size;
    public int version;
    /// <summary>
    /// 是否损坏  ture  损坏    false  有效
    /// </summary>
    public bool IsInvalid = true;

    public DownLoadFileInfo(string str)
    {
        string[] arr = str.Split('~');
        if (arr.Length >= 4 && float.TryParse(arr[2], out size) && int.TryParse(arr[3], out version))
        {
            GameName = arr[0];
            fileName = arr[1];
            IsInvalid = false;
        }
    }
}

public class GameResFileInfo
{
    public string gameName;
    public List<DownLoadFileInfo> infoList;

    public GameResFileInfo(string name)
    {
        gameName = name;
        infoList = new List<DownLoadFileInfo>();
    }

    public void AddFileInfo(DownLoadFileInfo info)
    {
        infoList.Add(info);
    }

}

public enum GameVersionState
{
    /// <summary>
    /// 还未下载
    /// </summary>
    UnLoad = 1,
    /// <summary>
    /// 需要更新版本
    /// </summary>
    OldVersion = 2,
    /// <summary>
    /// 最新的版本
    /// </summary>
    NewestVersion = 3,
}
