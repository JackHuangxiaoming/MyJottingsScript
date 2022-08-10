using System.Collections.Generic;
using System.IO;
using UnityEngine;


public delegate void DownLoadProgressHandler(int progress);
public delegate void DownLoadCompletedHandler();


public class DownLoadCombin : MonoBehaviour
{


    public GameResFileInfo gameResInfo;

    public DownLoadProgressHandler progressHandler;
    public DownLoadCompletedHandler completedHandler;
    public DownLoadThread downLoadThread = new DownLoadThread();

    public List<DownLoadThread> downLoadThreadList = new List<DownLoadThread>();
    public bool IsLoading { get; private set; }
    public bool IsDone { get; private set; }
    public bool IsError { get; private set; }
    public int Progress { get; private set; }
    /// <summary>
    /// 总字节数  单位KB
    /// </summary>
    public float TotalBytesKB { get; private set; }

    public static DownLoadCombin CreatToStartDownLoad(GameResFileInfo info, DownLoadCompletedHandler handler = null)
    {
        GameObject obj = new GameObject("downLoad_");
        obj.name = "downLoad_" + obj.GetInstanceID();
        obj.hideFlags = HideFlags.HideInHierarchy;
        GameObject.DontDestroyOnLoad(obj);
        DownLoadCombin dlm = obj.AddComponent<DownLoadCombin>();
        dlm.completedHandler = handler;
        dlm.StartDownLoad(info);
        return dlm;
    }


    private void StartDownLoad(GameResFileInfo resInfo)
    {
        if (IsLoading) return;
        IsLoading = true;
        IsDone = false;
        Progress = 0;
        gameResInfo = resInfo;

        string localfolderPath = URLFactory.GetLocalGameResSaveFolder(resInfo.gameName);
        string tagfolderPath = URLFactory.GetLocalGameTagFolder(resInfo.gameName);

        if (!Directory.Exists(localfolderPath))
        {
            Directory.CreateDirectory(localfolderPath);
        }
        if (!Directory.Exists(tagfolderPath))
        {
            Directory.CreateDirectory(tagfolderPath);
        }

        TotalBytesKB = 0;
        foreach (DownLoadFileInfo info in resInfo.infoList)
        {
            if (DownLoadManager.CheckOnceFileInfo(info) != GameVersionState.NewestVersion)
            {
                DownLoadThread downLoadThread = new DownLoadThread();
                downLoadThreadList.Add(downLoadThread);
                downLoadThread.StartDownLoad(info);
                TotalBytesKB += info.size;
            }
        }


    }


    private void Update()
    {
        if (IsDone) return;
        long loadedBytes = 0;
        IsDone = true;
        IsError = false;
        IsLoading = false;
        foreach (DownLoadThread loadThread in downLoadThreadList)
        {
            loadedBytes += loadThread.TotalBytesToReceive * loadThread.Progress;

            if (loadThread.IsDownLoading)
            {
                IsLoading = true;
            }
            if (!loadThread.IsDone)
            {
                IsDone = false;
            }
            if (loadThread.IsError)
            {
                IsError = true;
            }
        }

        if (TotalBytesKB != 0)
        {
            long loadedBytesKB = loadedBytes / 1024;
            Progress = (int)(loadedBytesKB / TotalBytesKB);
            Progress = Progress > 100 ? 100 : Progress;
        }

        if (IsDone)
        {
            Progress = 100;
        }

        //Utils.Log("=====>" + Progress);
        if (progressHandler != null)
        {
            progressHandler(Progress);
        }

        if (IsDone)
        {
            if (IsError)
            {
                DownLoadManager.Instance.DestroyDownloadCombin(gameResInfo.gameName);
                return;
            }
            DownLoadManager.Instance.UpdateGameVersion(gameResInfo.gameName);
            if (completedHandler != null)
                completedHandler();
        }
    }

    void OnDestroy()
    {
        foreach (var item in downLoadThreadList)
        {
            item.OnDestroy();
        }
    }

    public void Destroy()
    {
        gameResInfo = null;
        progressHandler = null;
        completedHandler = null;
        Destroy(this.gameObject);
    }
}
