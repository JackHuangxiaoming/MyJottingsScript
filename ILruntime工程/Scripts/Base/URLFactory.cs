using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class URLFactory
{
#if UNITY_ANDROID
    public static string DeviceSystem = "android";
#elif UNITY_IPHONE
    public static string DeviceSystem = "iphone";
#else
    public static string DeviceSystem = "android";
#endif
    public static string GameVersion = "1.0.0";
    public static string GameAuditState = "debug";
    public static string ResDirectoryName = "files";
    public static string TagDirectoryName = "tag_files";
    public static string DownLoadHotFilePath = Path.Combine(Application.persistentDataPath, ResDirectoryName);  // 资源文件路径
    public static string DownLoadTagPath = Path.Combine(Application.persistentDataPath, TagDirectoryName);  // 资源标识文件路径

    /// <summary>
    /// 云函数环境ID
    /// </summary>
    //public static string CloudBaseAppDevPath = "xiuxian-0gx590ei3ae54083"; //V1版本
    //public static string CloudBaseAppDevPath = "jiuzhou0817-3geqmadtd1b718ac";//V2 送审版本
    //public static string CloudBaseAppDevPath = "jiuzhou1013-4g0tz7ghb6707edd";//开发版本(暂时不用)
    public static string CloudBaseAppDevPath = "jiuzhou1117-5gdlrvmp60a60aff";//开发版本V3(目前在用的开发版本环境ID)
    //public static string CloudBaseAppDevPath = "jiuzhou0104-7gw5i03a0e309299";//开发版本V4(暂时不用)
    //public static string CloudBaseAppDevPath = "jiuzhou0303-6gvv2bvhb68ccbfd";//展示版本
    //public static string CloudBaseAppDevPath = "jiuzhou0708-6gs0fdxr79c1b3b1";//(2022年7月8号最新展示环境ID)

    /// <summary>
    /// mgobeSDK的游戏ID,游戏密钥，游戏域名
    /// </summary>
    public static string MgobeSDKGameId = "obg-8nvbwttl";//游戏ID
    public static string MgobeSDKSecretKey = "c2342dbf4e13aa1b03f41c796e4bb9095985ee53";//游戏密钥
    public static string ServerUrl = "8nvbwttl.wxlagame.com";//游戏域名

    /// <summary>
    /// 游戏版本控制
    /// </summary>
    public static string NewAppVersion = string.Empty;    //new app versionCode
    /// <summary>
    /// 游戏最新APK Url 现在是放的官网
    /// </summary>
    public static string NewApkWebUrl = string.Empty;     //new app web url

    /// <summary>
    /// 云函数超时时间
    /// </summary>
    public static int CloudBaseAppTimeOut = 15000;
    /// <summary>
    /// 主web Php服务器地址
    /// </summary>    
    public static string WebServerPath = "https://" + CloudBaseAppDevPath + "-1252618189.ap-shanghai.app.tcloudbase.com/";

    //===========================================================================================================================

    /// <summary>
    /// 服务器资源文件路径
    /// </summary>
    public static string ServerGameResPath = "https://game-resource-1305024763.cos.ap-chongqing.myqcloud.com/Resources/";
    /// <summary>
    /// 服务器资源安卓文件
    /// </summary>
    public static string ServerGameResAndroidPath = ServerGameResPath + "Develop/Platform/android";
    /// <summary>
    /// 游戏配置json 路径
    /// </summary>
    public static string GameConfigJsonPathByServer = "https://game-resource-1305024763.cos.ap-chongqing.myqcloud.com/Resources/Develop/Tmp/config.json";
    /// <summary>
    /// 获取验证码
    /// </summary>
    public static string RequestIdenCodePhp = "SendSMS";
    /// <summary>
    /// 云函数Ticket
    /// </summary>
    public static string RequestCloudBaseTicketPhp = "Ticket";
    /// <summary>
    /// 云函数长链流程(socketio)
    /// </summary>
    public static string RequestSocketIOTokenPhp = "NakamaTicket";


    #region 下面是没使用的
    /// <summary>
    /// 登录PHP
    /// </summary>
    public static string LoginOrRegisterPHP = "LoginOrRegisterPHP.php";
    /// <summary>
    /// 开始游戏PHP
    /// </summary>
    public static string LoginInServerPHP = "LoginInServerPHP.php";
    /// <summary>
    /// 主web服务器php 分配各个请求
    /// </summary>
    public static string GetUrlByVersionAndDevicePHP = "GetUrlByVersionAndDevice.php";
    /// <summary>
    /// 注册
    /// </summary>
    public static string RegisterPHP = "CreateUser.php";
    /// <summary>
    /// 修改密码
    /// </summary>
    public static string ModifyPasswordPHP = "ChangePass.php";
    /// <summary>
    /// 公告板信息
    /// </summary>
    public static string AnnouncementPhp = "Announcement.php";
    /// <summary>
    /// 客服PHP
    /// </summary>
    public static string KeFuPhp = "kefu.php";

    public static string LoginPhpPath = "http://shayukk.com:88/Login.php";//php登录服务器地址
    public static string PhotonLoginServer = "shayukk.com";//游戏登录服务器地址
    public static int PhotonLoginServerPort = 4532;//游戏登录服务器 端口
    public static string PhotonChatServer = "shayukk.com:4533";//游戏聊天服务器地址
    public static string UpAndDownLoadImagUrl = "http://shayukk.com:88/img/pic/";//图片服务器地址
    public static string LandingPageUrl = "www.baidu.com";
    #endregion

    /// <summary>
    /// 是否开放任意金额,[非0为任意金额]
    /// </summary>
    public static int AnyAmount = 0;

    public static string FormatWebPhp(string phpServerPath, string phpName, params object[] paramsArr)
    {
        string phpFullPath = phpServerPath.EndsWith("/") ? phpServerPath + phpName : phpServerPath + "/" + phpName;
        string paramStr = FormatPhpParams(paramsArr);
        return phpFullPath + paramStr;
    }
    public static string FormatWebPhpUrl(string phpServerPath, string phpName)
    {
        return phpServerPath.EndsWith("/") ? phpServerPath + phpName : phpServerPath + "/" + phpName;
    }
    public static string FormatWebPhpByFullPath(string phpFullPath, params object[] paramsArr)
    {
        string paramStr = FormatPhpParams(paramsArr);
        return phpFullPath + paramStr;
    }
    public static string FormatPhpParam(bool isPost, params object[] paramsArr)
    {
        if (isPost)
        {
            return FormatPhpParamsByPost(paramsArr);
        }
        return FormatPhpParams(paramsArr);
    }

    private static string FormatPhpParamsByPost(object[] paramsArr)
    {
        if (paramsArr != null && paramsArr.Length > 1)
        {
            var param = new Dictionary<string, string>();
            param.Add("device_id", UIManager.DeviceID);
            for (int i = 0, len = paramsArr.Length; i < len; i += 2)
            {
                param.Add(paramsArr[i].ToString(), paramsArr[i + 1].ToString());
            }
            return MyJson.SerializeDictionaryToJsonString<string, string>(param);
        }
        return "";
    }

    public static string FormatPhpParams(object[] paramsArr)
    {
        string paramStr = "?";
        if (paramsArr == null || paramsArr.Length < 2)
            paramStr = "";
        else
        {
            for (int i = 0, len = paramsArr.Length; i < len; i += 2)
            {
                paramStr += string.Format("{0}={1}{2}", paramsArr[i], paramsArr[i + 1], "&");
            }
            if (paramStr.EndsWith("&"))
                paramStr = paramStr.Remove(paramStr.Length - 1);
        }
        return paramStr;
    }


    public static string FormatNameSpace(string nameSpace, string className)
    {
        return string.Format("{0}.{1}", nameSpace, className);
    }


    public static string GetServerGameResPath(DownLoadFileInfo info)
    {
        string path = Path.Combine(ServerGameResAndroidPath, ResDirectoryName, info.GameName, info.fileName);
        path = path.Replace("\\", "/");
        return path;
    }

    /// <summary>
    /// 获取本地资源文件的文件夹全路径
    /// </summary>
    /// <param name="gameName"></param>
    /// <returns></returns>
    public static string GetLocalGameResSaveFolder(string gameName)
    {
        return Path.Combine(DownLoadHotFilePath, gameName);
    }

    /// <summary>
    /// 获取本地资源文件全路径
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static string GetLocalGameResSavePath(DownLoadFileInfo info)
    {
        string folderPath = GetLocalGameResSaveFolder(info.GameName);
        return Path.Combine(folderPath, info.fileName);
    }

    /// <summary>
    /// 获取本地标记文件全路径
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static string GetLocalGameTagPath(DownLoadFileInfo info)
    {
        string folderPath = GetLocalGameTagFolder(info.GameName);
        return Path.Combine(folderPath, info.fileName + ".txt");
    }

    /// <summary>
    /// 获取本地标记文件的文件夹全路径
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>

    public static string GetLocalGameTagFolder(string gameName)
    {
        return Path.Combine(DownLoadTagPath, gameName);
    }

    public static string MoneyName
    {
        get
        {
            return "占位符号";
        }
    }
}
