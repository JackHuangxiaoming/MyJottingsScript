using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using CloudBase;
using System.Threading.Tasks;

public class NetManager : MonoBehaviour
{
    /// <summary>
    /// 安装包风格标识
    /// </summary>
    public static string PackageStyle = "01";
    /// <summary>
    /// 安装包编号
    /// </summary>
    public static string PackageNum = "01";
    /// <summary>
    /// 安装包logo编号
    /// </summary>
    public static string LogoNum = "01";

    /// <summary>
    /// 子协议
    /// </summary>
    public static byte SubProtocolKey = 0;

    /// <summary>
    /// 全局唯一的 CloudBaseApp 
    /// </summary>
    private static CloudBaseApp app;
    /// <summary>
    /// 全局唯一的 CloudBaseApp 
    /// </summary>
    public static CloudBaseApp App
    {
        get
        {
            if (app == null)
            {
                app = CloudBaseApp.Init(URLFactory.CloudBaseAppDevPath, URLFactory.CloudBaseAppTimeOut);
            }
            return app;
        }
    }

    public HotClassILRuntime BasicHotNetManager { get { return HotScriptManager.BasicHotNetManager; } }

    /// <summary>
    /// 是否是自己主动退出账号
    /// </summary>
    public static bool IsExitAccount { get; set; }

    private static NetManager _instance;

    public static NetManager Instance

    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("NetManager");
                _instance = obj.AddComponent<NetManager>();
                obj.hideFlags = HideFlags.HideInHierarchy;
                GameObject.DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public void Init()
    {
        BasicHotNetManager.CallMethod("Init");
    }
    private void OnDestroy()
    {
        BasicHotNetManager.CallMethod("Destroy");
    }
}

