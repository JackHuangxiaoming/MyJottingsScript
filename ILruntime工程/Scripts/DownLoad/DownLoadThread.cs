using FairyGUI;
using System;
using System.IO;
using System.Net;
using UnityEngine;

public class DownLoadThread
{
    public WebClient _myWebClient;
    public bool IsDownLoading { get; set; }
    private bool _isDone;
    public bool IsDone
    {
        get { return _isDone; }
        set
        {
            _isDone = value;
            if (_isDone)
            {
                Progress = 100;
                IsDownLoading = false;
            }
        }
    }
    public bool IsError { get; private set; }
    public int Progress { get; set; }
    public long TotalBytesToReceive { get; set; }

    public DownLoadFileInfo CurDownLoadFileInfo { get; private set; }
    public int RestartDownLoadNumber { get; private set; }

    public void Init()
    {
        if (_myWebClient == null)
        {
            _myWebClient = new WebClient();
            _myWebClient.DownloadProgressChanged += myWebClient_DownloadProgressChanged;
            _myWebClient.DownloadFileCompleted += myWebClient_DownloadFileCompleted;
        }
    }


    public void StartDownLoad(DownLoadFileInfo info)
    {
        if (IsDownLoading) return;
        Init();
        CurDownLoadFileInfo = info;
        Progress = 0;
        IsDone = false;
        IsDownLoading = true;
        string serverGameResPath = URLFactory.GetServerGameResPath(info);
        string localSavePath = URLFactory.GetLocalGameResSavePath(info);
        if (File.Exists(localSavePath))
        {
            File.Delete(localSavePath);
        }
        _myWebClient.DownloadFileAsync(new Uri(serverGameResPath), localSavePath);
    }

    public void myWebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        if (Progress != e.ProgressPercentage)
        {
            Progress = e.ProgressPercentage;
            Utils.Log("下载资源进度:" + CurDownLoadFileInfo.fileName + "   progress=>" + e.ProgressPercentage);
        }
        TotalBytesToReceive = e.TotalBytesToReceive;
    }
    public void myWebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if (e != null && e.Error != null)
        {
            Utils.LogError("下载错误：" + CurDownLoadFileInfo.fileName);
            Utils.LogError(e.Error);
            WaitToRestartToDownLoad();
            return;
        }

        if (Progress != 100)
        {
            Utils.LogError("下载中断：" + CurDownLoadFileInfo.fileName);
            WaitToRestartToDownLoad();
            return;
        }

        UpdateTag();
        Utils.LogError("下载完成" + CurDownLoadFileInfo.fileName);
        IsDone = true;
    }

    public void WaitToRestartToDownLoad()
    {
        Timers.inst.Add(5, 1, RestartToDownLoad);
    }

    private void RestartToDownLoad(object obj)
    {
        IsDownLoading = false;
        RestartDownLoadNumber++;
        if (RestartDownLoadNumber > 2)
        {
            IsError = true;
            IsDone = true;
            return;
        }
        StartDownLoad(CurDownLoadFileInfo);
    }

    private void UpdateTag()
    {
        UnZipFile();
        string localTagPath = URLFactory.GetLocalGameTagPath(CurDownLoadFileInfo);
        StreamWriter streamWriter = new StreamWriter(localTagPath);
        streamWriter.WriteLine(CurDownLoadFileInfo.version);
        streamWriter.Close();

    }

    private void UnZipFile()
    {
        string fileName = CurDownLoadFileInfo.fileName;
        if (fileName.EndsWith(".zip"))
        {
            string fullPath = URLFactory.GetLocalGameResSavePath(CurDownLoadFileInfo);
            string unZipToDirectoryPath = URLFactory.GetLocalGameResSaveFolder(CurDownLoadFileInfo.GameName);
            if (!Directory.Exists(unZipToDirectoryPath))
                Directory.CreateDirectory(unZipToDirectoryPath);
            UtilsZipHelper.UnZipFile(fullPath, unZipToDirectoryPath);
            File.Delete(fullPath);
        }
    }


    public void OnDestroy()
    {
        if (_myWebClient != null)
        {
            _myWebClient.Dispose();
        }
        Timers.inst.Remove(RestartToDownLoad);
    }
}
