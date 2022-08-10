
public class DebugControlManager
{
    private static bool _failedDebug = true;// 错误的调试开关
    private static bool _commonDebug = true;//普通打印开关


    /// <summary>
    /// 普通打印开关
    /// </summary>
    public static bool CommonDebug
    {
        get { return _commonDebug; }
        set { _commonDebug = value; }
    }


    /// <summary>
    /// 错误的调试开关
    /// </summary>
    public static bool FailedDebug
    {
        get { return _failedDebug; }
        set { _failedDebug = value; }
    }
}
