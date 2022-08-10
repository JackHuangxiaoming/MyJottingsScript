using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// log信息
/// </summary>
public struct LogMsg
{
    public string condition;
    public string stackTrace;
    public LogType type;
    public LogMsg(string content, string stContetn, LogType ty)
    {
        //因为GUIButton字符串长度有限制（button宽度限制）,对显示的log信息长度做截取
        if (content.Length > 100)
            condition = content.Substring(0, 100);
        else
            condition = content;
        stackTrace = content + "\n" + stContetn;
        type = ty;
    }
}
public class UIManager : MonoBehaviour
{
    #region 静态方法
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                _instance = obj.AddComponent<UIManager>();
                obj.hideFlags = HideFlags.HideInHierarchy;
                GameObject.DontDestroyOnLoad(obj);
                Input.multiTouchEnabled = true;
            }
            return _instance;
        }
    }
    #endregion
    #region GUILogWindows Data   

    private void Awake()
    {
        allMsg = new List<LogMsg>();
        allMsg.Capacity = maxMessageCount;
        logMsg = new List<LogMsg>();
        errorMsg = new List<LogMsg>();
        Application.logMessageReceivedThreaded += DrawLog;
    }
    /// <summary>
    /// 全部log信息列表
    /// </summary>
    List<LogMsg> allMsg;
    /// <summary>
    /// log列表信息
    /// </summary>
    List<LogMsg> logMsg;
    /// <summary>
    /// error列表信息
    /// </summary>
    List<LogMsg> errorMsg;
    /// <summary>
    /// 是否开始记录
    /// </summary>
    public static bool IsCacheLog = true;
    public static bool IsShowLog = false;
    private int maxMessageCount = 100;
    void DrawLog(string condition, string stackTrace, LogType type)
    {
        if (!IsCacheLog)
        {
            return;
        }
        if (allMsg.Count > maxMessageCount - 1)
        {
            allMsg.RemoveAt(0);
        }
        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
                if (errorMsg.Count > maxMessageCount - 1)
                {
                    errorMsg.RemoveAt(0);
                }
                allMsg.Add(new LogMsg(condition, stackTrace, type));
                errorMsg.Add(new LogMsg(condition, stackTrace, type));
                break;
            case LogType.Log:
                if (logMsg.Count > maxMessageCount - 1)
                {
                    logMsg.RemoveAt(0);
                }
                allMsg.Add(new LogMsg(condition, stackTrace, type));
                logMsg.Add(new LogMsg(condition, stackTrace, type));
                break;
        }
    }

    public List<LogMsg> GetLogByType(LogType type)
    {
        switch (type)
        {
            case LogType.Error:
                return errorMsg;
            case LogType.Log:
                return logMsg;
            default:
                return allMsg;
        }
    }
    public void ClearLog()
    {
        allMsg.Clear();
        logMsg.Clear();
        errorMsg.Clear();
    }
    #endregion
    private static string _guid = string.Empty;
    private static string _deviceid;
    private static string _operatingSystem;
    public static string GUID
    {
        get
        {
            if (string.IsNullOrEmpty(_guid))
            {
                _guid = PlayerPrefs.GetString("GID", _guid);
            }
            return _guid;
        }
        set
        {
            if (_guid != value)
            {
                _guid = value;
                PlayerPrefs.SetString("GID", _guid);
            }
        }
    }
    public static string DeviceID
    {
        get
        {
            if (_deviceid == null)
            {
                _deviceid = SystemInfo.deviceUniqueIdentifier;
            }
            return _deviceid;
        }
    }
    public static string OperatingSystem
    {
        get
        {
            if (_operatingSystem == null)
            {
                _operatingSystem = SystemInfo.operatingSystem;
            }
            return _operatingSystem;
        }
    }



    private HotClassILRuntime _uiManagerHotClass;
    public HotClassILRuntime UIManagerHotClass
    {
        get
        {
            if (_uiManagerHotClass == null)
            {
                _uiManagerHotClass = HotScriptManager.Instance.CreateHotClass("UIManagerCtrl", "PlatformLobbyCode");
                _uiManagerHotClass.CallMethod("Init");
            }
            return _uiManagerHotClass;
        }
    }
    private void FixedUpdate()
    {
        CheckKeyBord();
    }
    Vector2[] startPos = new Vector2[2];
    Vector2[] endPos = new Vector2[2];
    private void CheckKeyBord()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscClick();
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            HomeClick();
        }
        //if (Input.touchCount == 5)
        //{
        //    IsCacheLog = !IsCacheLog;
        //    Utils.Log("监听日志开关 :" + IsCacheLog);
        //}
        if ((Input.touchCount == 3) && IsCacheLog)
        {
            if (Input.GetTouch(0).phase != TouchPhase.Ended)
            {
                return;
            }
            IsShowLog = !IsShowLog;
            Utils.Log("日志界面开关 :" + IsShowLog);
            if (IsShowLog)
            {
                GUILogWindow.Show();
            }
            else
            {
                GUILogWindow.Hide();
            }
        }
        //if ((Input.touchCount == 2) && IsCacheLog)
        //{
        //    if (Input.touches[0].phase != TouchPhase.Ended)
        //    {
        //        return;
        //    }
        //    Utils.Log("日志缩放界面开关 :" + IsShowLog);

        //    for (int i = 0; i < 2; i++)
        //    {
        //        Touch touch = Input.touches[i];
        //        if (touch.phase == TouchPhase.Ended)
        //        {
        //            startPos[i] = touch.rawPosition;
        //            endPos[i] = touch.deltaPosition;
        //        }
        //    }
        //    if (Direction(startPos, endPos))
        //    {
        //        GUILogWindow.Show();
        //    }
        //    else
        //    {
        //        GUILogWindow.Hide();
        //    }
        //}
    }
    public bool Direction(Vector2[] startPos, Vector2[] endPos)
    {
        float leng1 = Mathf.Sqrt(Mathf.Pow((startPos[0].x - startPos[1].x), 2) + Mathf.Pow((startPos[0].y - startPos[1].y), 2));
        float leng2 = Mathf.Sqrt(Mathf.Pow((endPos[0].x - endPos[1].x), 2) + Mathf.Pow((endPos[0].y - endPos[1].y), 2));
        if (leng1 < leng2)
        {
            //放大手势
            return true;
        }
        else
        {
            //缩小手势
            return false;
        }
    }
    
    private void OnDestroy()
    {
        UIManagerHotClass.CallMethod("OnDestroy");
    }

    public void ShowView(string viewName)
    {
        UIManagerHotClass.CallMethod("ShowViewByParamCount", viewName);
    }

    public void ShowPopupPanel(string popupPanelName)
    {
        UIManagerHotClass.CallMethod("ShowPopupPanelByParamCount", popupPanelName);
    }

    /// <summary>
    /// 从游戏中返回到大厅
    /// </summary>
    public void ReturnToPlatformLobby()
    {
        UIManagerHotClass.CallMethod("ReturnToPlatformLobby");
    }

    public void CustOnConnectedToMaster(string gameID)
    {
        UIManagerHotClass.CallMethod("OnConnectedToMaster", gameID);
    }

    public void ShowLoadingUI(string LoadingUIName = "")
    {
        UIManagerHotClass.CallMethod("ShowLoadingUI", LoadingUIName);
    }
    public void StartUpdateLoadingUI(AsyncOperation ao)
    {
        UIManagerHotClass.CallMethod("StartUpdateLoadingUI", ao);
    }

    public void RemoveLoadingUI()
    {
        Utils.Log("============RemoveLoadingUI===========");
        UIManagerHotClass.CallMethod("RemoveLoadingUI");
    }

    public void DestroyCurView()
    {
        UIManagerHotClass.CallMethod("DestroyCurView");
    }

    public void ShowWaitingPopup(string content, int type = 0, float time = 5)
    {
        UIManagerHotClass.CallMethod("ShowWaitingPopup", content, type, time);
    }
    public void CloseWaitingPopup()
    {
        UIManagerHotClass.CallMethod("CloseWaitingPopup");
    }

    public void ShowNetworkMaskView(bool isNetworkMark,string content = "")
    {
        UIManagerHotClass.CallMethod("ShowNetworkMaskView", isNetworkMark, content);
    }
    public void CloseNetworkMaskView(bool isNetworkMark)
    {
        UIManagerHotClass.CallMethod("CloseNetworkMaskView");
    }
    public void EscClick()
    {
        UIManagerHotClass.CallMethod("EscClick");
    }

    public void HomeClick()
    {
        UIManagerHotClass.CallMethod("HomeClick");
    }
}
