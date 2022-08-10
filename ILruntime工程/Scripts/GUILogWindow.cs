using System;
using System.Collections.Generic;
using UnityEngine;
public class GUILogWindow : MonoBehaviour
{
    private static GUILogWindow _instacne;
    public static void Show()
    {
        if (_instacne == null)
        {
            GameObject obj = new GameObject("GUILogWindow");
            _instacne = obj.AddComponent<GUILogWindow>();
            DontDestroyOnLoad(obj);
        }
        else
        {
            _instacne.gameObject.SetActive(true);
        }
    }
    public static void Hide()
    {
        if (_instacne != null)
        {
            _instacne.gameObject.SetActive(false);
        }
    }
    public void OnDestroy()
    {
        ClearLogMsgList();
        if (_instacne != null)
        {
            uiManager = null;
            _instacne = null;
        }
    }

    private UIManager uiManager;
    private LogType type = LogType.Exception;
    private List<LogMsg> logShowMsg;

    #region 窗体区域
    //窗体区域坐标
    static Vector2 windowPos = new Vector2(0, 0);
    //窗体区域大小
    static Vector2 windowSize = new Vector2(Screen.width, Screen.height);
    //窗体
    Rect WindowBackgroundRect = new Rect(windowPos.x, windowPos.y, windowSize.x, windowSize.y);
    string WindowBackgroundName = "控制台";
    #endregion

    #region 按钮区域
    //按钮区域坐标
    static Vector2 btnPos = new Vector2(10, 0);
    //按钮区域大小
    static Vector2 btnSize = new Vector2(170, 50);
    //按钮排列坐标偏差值
    static Vector2 btnPosValue = new Vector2(180, 0);
    //按钮清理Log
    Rect clearBtnRect = new Rect(btnPos.x, btnPos.y, btnSize.x, btnSize.y);
    string clearBtnName = "清理Log";
    //按钮全部Log
    Rect allLogBtnRect = new Rect(btnPos.x + btnPosValue.x, btnPos.y + btnPosValue.y, btnSize.x, btnSize.y);
    string allLogBtnName = "全部Log";
    //按钮打印Log
    Rect logBtnRect = new Rect(btnPos.x + (btnPosValue.x * 2), btnPos.y + (btnPosValue.y * 2), btnSize.x, btnSize.y);
    string logBtnName = "打印Log";
    //按钮报错Log
    Rect logErrBtnRect = new Rect(btnPos.x + (btnPosValue.x * 3), btnPos.y + (btnPosValue.y * 3), btnSize.x, btnSize.y);
    string logErrBtnName = "报错Log";
    #endregion

    #region 日志列表区域
    //日志按钮坐标
    static Vector2 logListBtnPos = new Vector2(0, 50);
    //日志按钮大小
    static Vector2 logListBtnSize = new Vector2(windowSize.x - 60, 45);

    //日志
    private Vector2 logScrollPosition;
    //日志列表区域坐标
    static Vector2 logListPos = new Vector2(10, 60);
    //日志列表区域大小
    static Vector2 logListSize = new Vector2(windowSize.x - 13, Screen.height * 0.5f);
    //日志列表
    Rect logListRect = new Rect(logListPos.x, logListPos.y, logListSize.x, logListSize.y);

    //日志列表显示坐标
    static Vector2 logListViewPos = new Vector2(0, 0);
    //日志列表显示大小
    static Vector2 logListViewSize = new Vector2(windowSize.x - 30, 40);
    //日志列表显示
    Rect logListViewRect = new Rect(logListViewPos.x, logListViewPos.y, logListViewSize.x, logListViewSize.y);
    #endregion

    #region 详细日志区域
    //详细日志
    private Vector2 logContentScrollPosition;
    //详细日志区域坐标
    static Vector2 logContentPos = new Vector2(10, logListPos.y + logListSize.y + 20);
    //详细日志区域大小
    static Vector2 logContentSize = new Vector2(windowSize.x - 43, windowSize.y - windowPos.y - logContentPos.y - 30);
    //详细日志
    Rect logContentRect = new Rect(logContentPos.x, logContentPos.y, logContentSize.x, logContentSize.y);

    //详细日志显示坐标
    static Vector2 logContentViewPos = new Vector2(0, 0);
    //详细日志显示大小
    static Vector2 logContentViewSize = new Vector2(logContentSize.x, logContentSize.y);
    //详细日志显示
    Rect logContentViewRect = new Rect(logContentViewPos.x, logContentViewPos.y, logContentViewSize.x, logContentViewSize.y);

    //文本坐标
    static Vector2 logTextPos = new Vector2(0, 0);
    //文本大小
    static Vector2 logTextSize = new Vector2(logContentViewSize.x - 30, logContentViewSize.y - 30);
    //文本
    Rect logTextRect = new Rect(logTextPos.x, logTextPos.y, logTextSize.x, logTextSize.y);
    //日志详细内容
    string logText = "";
    #endregion

    private void Start()
    {
        uiManager = UIManager.Instance;
    }
    GUIStyle buttonStyle;
    GUIStyle buttonStyle1;
    GUIStyle textAreaStyle;
    GUIStyle barStyle;
    GUIStyle barStyle1;
    void OnGUI()
    {
        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 40;
        buttonStyle.fixedHeight = 43;
        buttonStyle1 = new GUIStyle("button");
        buttonStyle1.fontSize = 40;
        buttonStyle1.fixedHeight = 50;
        textAreaStyle = new GUIStyle("textarea");
        textAreaStyle.fontSize = 40;
        barStyle = new GUIStyle("HorizontalScrollbar");
        barStyle.fixedHeight = 35;
        barStyle.stretchWidth = true;
        barStyle1 = new GUIStyle("VerticalScrollbar");
        barStyle1.fixedWidth = 35;
        barStyle1.stretchHeight = true;
        DrawConsole();
    }


    /// <summary>
    /// 绘制控制台
    /// </summary>
    void DrawConsole()
    {
        GUI.Window(0, WindowBackgroundRect, WindowFunction, WindowBackgroundName);
    }
    /// <summary>
    /// 窗体
    /// </summary>
    /// <param name="id"></param>
    void WindowFunction(int id)
    {
        //显示全部控制台输出
        logShowMsg = uiManager.GetLogByType(type);
        if (GUI.Button(clearBtnRect, clearBtnName, buttonStyle1))
        {
            //清理控制台输出
            ClearLogMsgList();
        }
        if (GUI.Button(allLogBtnRect, allLogBtnName, buttonStyle1))
        {
            //显示全部控制台输出
            type = LogType.Exception;
        }
        if (GUI.Button(logBtnRect, logBtnName, buttonStyle1))
        {
            //只显示控制台输出打印
            type = LogType.Log;
        }
        if (GUI.Button(logErrBtnRect, logErrBtnName, buttonStyle1))
        {
            //只显示控制台输出报错
            type = LogType.Error;
        }
        //定义窗体可以活动的范围
        logListViewRect.height = logShowMsg.Count * logListBtnSize.y + logListViewSize.y;
        logScrollPosition = GUI.BeginScrollView(logListRect, logScrollPosition, logListViewRect, false, false, barStyle, barStyle1);
        for (int i = logShowMsg.Count - 1; i >= 0; i--)
        {
            GUI.color = LogCloor(logShowMsg[i].type);
            if (GUI.Button(new Rect(logListBtnPos.x, i * logListBtnPos.y, logListBtnSize.x, logListBtnSize.y), logShowMsg[i].condition, buttonStyle))
            {
                logText = logShowMsg[i].stackTrace;
            }
            GUI.color = LogCloor(LogType.Log);
        }
        GUI.EndScrollView();
        logContentScrollPosition = GUI.BeginScrollView(logContentRect, logContentScrollPosition, logContentViewRect, false, false, barStyle, barStyle1);
        GUI.TextField(logTextRect, logText, textAreaStyle);
        GUI.EndScrollView();
    }

    /// <summary>
    /// 清理log列表
    /// </summary>
    void ClearLogMsgList()
    {
        uiManager.ClearLog();
        logText = "";
    }
    /// <summary>
    /// 返回log颜色
    /// </summary>
    /// <param name="logType">日志类型</param>
    /// <returns></returns>
    Color LogCloor(LogType logType)
    {
        switch (logType)
        {
            case LogType.Error:
            case LogType.Exception:
                return Color.red;
            case LogType.Assert:
                return Color.white;
            case LogType.Warning:
                return Color.white;
            case LogType.Log:
                return Color.white;
            default:
                return Color.white;
        }
    }

}