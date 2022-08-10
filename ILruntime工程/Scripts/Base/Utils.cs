using FairyGUI;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;		//IOS使用
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Text.RegularExpressions;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine.Networking;
using System.Xml;
using DG.Tweening.Plugins.Options;
using Path = DG.Tweening.Plugins.Core.PathCore.Path;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public delegate void PHPStringHandler(string phpResult);
public static class BigIntegerUtils
{
    public static string ToBinaryString(this System.Numerics.BigInteger bigint)
    {
        var bytes = bigint.ToByteArray();
        var idx = bytes.Length - 1;

        // Create a StringBuilder having appropriate capacity.
        var base2 = new StringBuilder(bytes.Length * 8);

        // Convert first byte to binary.
        var binary = Convert.ToString(bytes[idx], 2);

        // Ensure leading zero exists if value is positive.
        if (binary[0] != '0' && bigint.Sign == 1)
        {
            base2.Append('0');
        }
        base2.Append(binary);
        for (idx--; idx >= 0; idx--)
        {
            base2.Append(Convert.ToString(bytes[idx], 2).PadLeft(8, '0'));
        }
        return base2.ToString();
    }
    public static System.Numerics.BigInteger BinaryStringToBigInteger(string b)
    {
        System.Numerics.BigInteger Decimalvalue = 0;
        foreach (char c in b)
        {
            if (c == '_')
                continue;
            Decimalvalue <<= 1;
            Decimalvalue += c == '1' ? 1 : 0;
        }
        return Decimalvalue;
    }
}
public static class Utils
{
    public static void PostHttpRequest(string webUrl, string data, PHPStringHandler phpHandler, bool post = true)
    {
        StartDoCoroutine(PostHttpRequestIEnumerator(webUrl, data, phpHandler, post));
    }
    private static IEnumerator PostHttpRequestIEnumerator(string url, string data, PHPStringHandler phpHandler, bool post = true)
    {
        yield return null;
        data = data == null ? string.Empty : data;
        Log("PHP 请求：" + url + data);
        UnityWebRequest uwr = null;
        if (post)
            uwr = UnityWebRequest.Post(url, data);
        else
            uwr = UnityWebRequest.Get(url + data);

        uwr.timeout = 15;
        if (!string.IsNullOrEmpty(data))
        {
            byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(data);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
            uwr.uploadHandler.contentType = "application/json";
        }
        yield return uwr.SendWebRequest();
        string str = null;
        if (!string.IsNullOrEmpty(uwr.error))
        {
            LogError(uwr.error);
        }
        else
        {
            str = uwr.downloadHandler.text;
            Utils.Log(str);
        }
        uwr.Abort();
        if (phpHandler != null)
        {
            phpHandler(str);
        }
    }

    public static Hashtable DictionaryToHashtable(Dictionary<byte, object> dict)
    {
        Hashtable hash = new Hashtable();
        foreach (var item in dict)
        {
            hash[item.Key] = item.Value;
        }
        return hash;
    }

    public static string GetMD5HashByString(string input)
    {
        // Use input string to calculate MD5 hash
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = md5.ComputeHash(inputBytes);

        // Convert the byte array to hexadecimal string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
            // To force the hex string to lower-case letters instead of
            // upper-case, use he following line instead:
            // sb.Append(hashBytes[i].ToString("x2")); 
        }
        return sb.ToString();
    }
    public static string GetMD5HashByFile(string fileName)
    {
        try
        {
            if (!File.Exists(fileName))
                throw new Exception("指定文件不存在");
            FileStream file = new FileStream(fileName, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("获取文件MD5失败:" + ex.Message);
        }
    }

    public static void Log(object msg)
    {
#if UNITY_EDITOR
        if (DebugControlManager.CommonDebug)
        {
            string str = msg.ToString();
            char[] strs = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                sb.Append(strs[i]);
                if ((i != 0 && (i % 15000) == 0) || i == str.Length - 1)
                {
                    Debug.Log(DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss:fff]\n") + sb.ToString());
                    sb.Clear();
                }
            }
        }
#else
        if (DebugControlManager.CommonDebug)
            Debug.Log(DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss:fff]\n") + msg);
#endif
    }

    public static void LogError(object msg)
    {
#if UNITY_EDITOR
        if (DebugControlManager.CommonDebug)
        {
            string str = msg.ToString();
            char[] strs = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                sb.Append(strs[i]);
                if ((i != 0 && (i % 15000) == 0) || i == str.Length - 1)
                {
                    Debug.LogError(DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss:fff]\n") + sb.ToString());
                    sb.Clear();
                }
            }
        }
#else
        if (DebugControlManager.FailedDebug)
            Debug.LogError(DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss:fff]\n") + msg);
#endif
    }

    public static object WaitFormSecond(float second)
    {
        return new WaitForSeconds(second);
    }

    public static void LogWarning(object msg)
    {
        if (DebugControlManager.CommonDebug)
            Debug.LogWarning(msg);
    }

    /// <summary>
    /// 开启一个协程
    /// </summary>
    public static Coroutine StartDoCoroutine(IEnumerator coroutine)
    {
        return CoroutineUtiliy.Instance.StartDoCoroutine(coroutine);
    }
    /// <summary>
    /// 停止一个协程
    /// </summary>
    public static void StopDoCoroutine(Coroutine coroutine)
    {
        CoroutineUtiliy.Instance.StopDoCoroutine(coroutine);
    }

    public static T FindChildByName<T>(GComponent parent, string childName) where T : GObject
    {
        if (parent == null || string.IsNullOrEmpty(childName))
        {
            return null;
        }
        GObject child = parent.GetChild(childName);
        if (child == null)
        {
            Utils.Log("=========找不到指定子对象=============名称：" + childName);
        }
        else if (child is T)
        {
            return (T)child;
        }
        return null;
    }

    public static GObject FindChildAndAddClick(GComponent parent, string childName, EventCallback0 callBackNoParam)
    {
        GObject child = FindChildByName<GObject>(parent, childName);
        if (child != null)
        {
            child.onClick.Add(callBackNoParam);
        }
        return child;
    }

    public static GObject FindChildAndAddClick(GComponent parent, string childName, EventCallback1 callBackNoParam)
    {
        GObject child = FindChildByName<GObject>(parent, childName);
        if (child != null)
        {
            child.onClick.Add(callBackNoParam);
        }
        return child;
    }

    public static List<T> FindChildsByType<T>(GComponent parent, string childName = "") where T : GObject
    {
        List<T> ls = new List<T>();
        if (parent == null)
        {
            return ls;
        }
        GComponent selfCompoent = null;
        if (!string.IsNullOrEmpty(childName))
        {
            selfCompoent = FindChildByName<GComponent>(parent, childName);
        }
        if (selfCompoent == null)
        {
            selfCompoent = parent;
        }
        GObject[] childs = selfCompoent.GetChildren();
        for (int i = 0; i < childs.Length; i++)
        {
            if (childs[i] is T)
            {
                ls.Add((T)childs[i]);
            }
        }
        return ls;
    }

    public static bool AddOnClick(GObject self, EventCallback0 callBackNoParam)
    {
        if (self != null)
        {
            self.onClick.Add(callBackNoParam);
            return true;
        }
        return false;
    }
    public static bool AddOnClick(GObject self, EventCallback1 callBackNoParam)
    {
        if (self != null)
        {
            self.onClick.Add(callBackNoParam);
            return true;
        }
        return false;
    }

    public static void SetAllChildrenRelation(GComponent comp, RelationType relationType = RelationType.Size)
    {
        if (comp == null)
        {
            return;
        }
        if (comp.numChildren > 0)
        {
            GObject[] childrenArr = comp.GetChildren();
            for (int i = 0, len = childrenArr.Length; i < len; i++)
            {
                GObject child = childrenArr[i];
                child.AddRelation(comp, relationType);
                SetAllChildrenRelation(child.asCom, relationType);
            }
        }
    }

    public static RaycastHit Raycast(Ray ray, int ds)
    {
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, ds);
        return hitInfo;
    }

    public static void ResetShader(UnityEngine.Object obj)
    {
        Type type = obj.GetType();
        if (type == typeof(Transform))
            ResetTransformShader((Transform)obj);
        else if (type == typeof(GameObject))
        {
            ResetTransformShader((obj as GameObject).transform);
        }
        else if (type == typeof(Material))
        {
            (obj as Material).shader = Shader.Find((obj as Material).shader.name);
        }
    }

    public static void ResetTransformShader(Transform trans)
    {
        Renderer[] Renderers = trans.GetComponentsInChildren<Renderer>(true);
        foreach (Renderer smr in Renderers)
        {
            if (smr.sharedMaterials == null || smr.sharedMaterials.Length == 0)
                continue;
            foreach (Material mt in smr.sharedMaterials)
            {
                if (mt == null) continue;
                mt.shader = Shader.Find(mt.shader.name);
            }
        }
    }

    public static T GetSafeComponent<T>(GameObject go, bool isCreate = true) where T : Component
    {
        T com = go.GetComponent<T>();
        if (com == null && isCreate)
            com = go.AddComponent<T>();
        return com;
    }

    public static T GetSafeComponent<T>(Transform trans, bool isCreate = true) where T : Component
    {
        T com = trans.GetComponent<T>();
        if (com == null && isCreate)
            com = trans.gameObject.AddComponent<T>();
        return com;
    }

    public static T GetComponentInChildren<T>(Transform trans) where T : Component
    {
        T com = trans.GetComponentInChildren<T>();
        return com;
    }
    public static T GetComponentInChildren<T>(GameObject go) where T : Component
    {
        T com = go.GetComponentInChildren<T>();
        return com;
    }
    public static Vector3 StringToVector3(string str)
    {
        string[] tv = str.Split(',');
        if (tv.Length >= 3)
        {
            float x = Convert.ToSingle(tv[0]);
            float y = Convert.ToSingle(tv[1]);
            float z = Convert.ToSingle(tv[2]);
            return new Vector3(x, y, z);
        }
        else
        {
            if (DebugControlManager.CommonDebug)
                Debug.LogError("StringToVector3 表填写错误！！！！————" + str);
        }

        return Vector3.zero;
    }

    public static List<Vector3> CloneListVector3(List<Vector3> list)
    {
        List<Vector3> cloneList = new List<Vector3>();
        for (int i = 0, length = list.Count; i < length; i++)
        {
            cloneList.Add(list[i]);
        }
        return cloneList;
    }


    /// <summary>
    /// 包括最小  不包括最大
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int Range(int min, int max)
    {
        if (min > max)
        {
            int temp = min;
            min = max;
            max = min;
        }
        return UnityEngine.Random.Range(min, max);
    }

    public static float Range(float min, float max)
    {
        if (min > max)
        {
            float temp = min;
            min = max;
            max = temp;
        }
        return UnityEngine.Random.Range(min, max);
    }

    /// <summary>
    /// 漂浮提示
    /// 脚本下载之后调用，   下载之前 使用弹窗提示
    /// </summary>
    /// <param name="masster"></param>
    public static void AddFlyTextTips(string masster, int type = 0, float xPos = 0, float yPos = 0)
    {
        MessageFlyTextTipsMgr.inst.AddMessageFlyInfo(masster, type, xPos, yPos);
    }

    /// <summary>    /// 跑马灯提示
    /// </summary>
    /// <param name="type">0:没有 1：优先显示该提示</param>
    public static void MessagePrompt(int type, string masster)
    {
    }

    public static void SetResolution(int width, int height, bool fullscreen, int preferredRefreshRate)
    {
        Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
    }

    public static void RandomList<T>(List<T> list)
    {
        if (list == null)
        {
            return;
        }

        T tempT;
        for (int i = 0, length = list.Count; i < length; i++)
        {
            int randomIndex = Utils.Range(i, length);
            tempT = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = tempT;
        }
    }

    /// <summary>
    /// 弹出提示
    /// </summary>
    /// <param name="title">标题 默认“提示”</param>
    /// <param name="content"> 内容 </param>
    /// <param name="callback">确定回调</param>
    /// <param name="type">类型，默认0：有确定和取消，1：只有确定</param>
    public static void MessagePopup(string content, EventCallback0 callback = null, int type = 0, bool isSystem = false, string confirmStr = "确定", string cancleStr = "取消")
    {
        MessageFlyTextTipsMgr.inst.MessagePopupShow(content, callback, type, isSystem, confirmStr, cancleStr);
    }


    public static void SetMaterialRenderingMode(Material material, RenderingMode renderingMode)
    {
        switch (renderingMode)
        {
            case RenderingMode.Opaque:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;
            case RenderingMode.Cutout:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;
            case RenderingMode.Fade:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
            case RenderingMode.Transparent:
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }

    /// <summary>
    /// TryParse
    /// Defaule Value is int default
    /// </summary>
    public static int TryParse(string str)
    {
        int value;
        if (int.TryParse(str, out value))
        {
            return value;
        }
        return 0;
    }
    /// <summary>
    /// TryParseDouble
    /// Defaule Value is double default
    /// </summary>
    public static double TryParseDouble(string str)
    {
        double value;
        if (double.TryParse(str, out value))
        {
            return value;
        }
        return 0;
    }

    public static bool IsMatch(string resStr, string regexStr)
    {
        return Regex.IsMatch(resStr, regexStr);
    }

    public static bool ValidatesName(string name)
    {
        return Utils.IsMatch(name, "^[\u4E00-\u9FA5A-Za-z0-9]{2,16}$");
    }

    /// <summary>
    /// 转换钱的函数，规则是除以100并保留两位小数，仅作显示，不参与计算，故返回string
    /// </summary>
    /// <param name="num">多少分</param>
    /// <returns></returns>
    public static string ConvertToFloat2(long num)
    {
        string signStr = num < 0 ? "-" : "";
        num = num < 0 ? -num : num;
        string str = num.ToString();
        str = str.PadLeft(3, '0');
        return signStr + str.Insert(str.Length - 2, ".");
    }

    /// <summary>
    /// 转换钱的函数，规则是除以100并保留两位小数，仅作显示，不参与计算，故返回string
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string ConvertToFloat2(float num)
    {
        num /= 100;
        return num.ToString("f2");
    }

    /// <summary>
    /// 防止用户连点
    /// </summary>
    /// <param name="target"></param>
    /// <param name="time"></param>
    public static void SetUnableTouchTime(GObject target, float time = 2)
    {
        if (target == null)
        {
            return;
        }
        target.touchable = false;
        Timers.inst.Add(time, 1, (object obj) => { SetObjTouchable(target); });
    }

    private static void SetObjTouchable(object obj)
    {
        GObject target = obj as GObject;
        if (target == null)
        {
            return;
        }
        target.touchable = true;
    }


    public static DateTime ConvertIntDateTime(long d)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
        startTime = startTime.AddSeconds(d);
        return startTime;
    }

    public static bool CheckIsMobileNum(string mobileNum)
    {
        return Utils.IsMatch(mobileNum, @"^(?=.*[1][3-9]\d{9})[0-9]{11}$");
    }

    public static bool CheckIsAuthCode(string authCode, int num)
    {
        string str = @"^\d{" + num + "}$";
        return Utils.IsMatch(authCode, str);
    }

    public static bool CheckIsPassWord(string password, int min, int max)
    {
        string str = "^(?=.*[a-zA-Z])(?=.*[0-9])[a-zA-Z0-9]{" + min + "," + max + "}$";
        return Utils.IsMatch(password, str);
    }

    public static bool CheckIsName(string name, int min, int max)
    {
        string str = "^[\u4e00-\u9fa5a-zA-Z0-9]{" + min + "," + max + "}$";
        return Utils.IsMatch(name, str);
    }

    public static bool CheckIsIDCard(string idcard, int min, int max)
    {
        string str = "^[0-9Xx]{" + min + "," + max + "}$";
        return Utils.IsMatch(idcard, str);
    }

    public static bool CheckIsPhoneNumber(string phoneNumber, int num)
    {
        string str = "^[0-9]{" + num + "}$";
        return Utils.IsMatch(phoneNumber, str);
    }

    public static void GetIdenCode(string mmobileNum)
    {
        Utils.PostHttpRequest(URLFactory.FormatWebPhpUrl(URLFactory.WebServerPath, URLFactory.RequestIdenCodePhp),
            URLFactory.FormatPhpParam(true, "phone", mmobileNum),
            null);
    }

    public static void SetPostion(Transform transform, float x, float y, float z = 0, bool isLocal = true)
    {
        if (transform == null)
        {
            return;
        }
        Vector3 postion = Vector3.zero;
        postion.Set(x, y, z);
        if (isLocal)
            transform.localPosition = postion;
        else
            transform.position = postion;
    }
    public static void SetPostionByNative(Transform transform, float x = float.MaxValue, float y = float.MaxValue, float z = float.MaxValue, bool isLocal = true)
    {
        if (transform == null)
        {
            return;
        }
        Vector3 postion = isLocal ? transform.localPosition : transform.position;
        postion.Set(x != float.MaxValue ? x : postion.x, y != float.MaxValue ? y : postion.y, z != float.MaxValue ? z : postion.z);
        if (isLocal)
            transform.localPosition = postion;
        else
            transform.position = postion;
    }
    public static void SetRotation(Transform transform, float x, float y, float z, bool isLocal = true)
    {
        if (transform == null)
        {
            return;
        }
        Quaternion quaternion = new Quaternion(x, y, z, 0);
        if (isLocal)
            transform.localRotation = quaternion;
        else
            transform.rotation = quaternion;
    }
    public static void SetRotationByNative(Transform transform, float x = float.MaxValue, float y = float.MaxValue, float z = float.MaxValue, bool isLocal = true)
    {
        if (transform == null)
        {
            return;
        }
        Vector3 quaternion = isLocal ? transform.localEulerAngles : transform.eulerAngles;
        quaternion.Set(x != float.MaxValue ? x : quaternion.x, y != float.MaxValue ? y : quaternion.y, z != float.MaxValue ? z : quaternion.z);
        if (isLocal)
        {
            transform.localEulerAngles = quaternion;
        }
        else
        {
            transform.eulerAngles = quaternion;
        }
    }
    public static void SetRotPosScale(Transform transform, float x = 0, float y = 0, float z = 0, float rx = 0, float ry = 0, float rz = 0, float rw = 0, float sx = 1, float sy = 1, float sz = 1)
    {
        if (transform == null)
        {
            return;
        }
        Vector3 postion = Vector3.zero;
        postion.Set(x, y, z);
        transform.localPosition = postion;
        Quaternion quaternion = new Quaternion();
        quaternion.Set(rx, ry, rz, rw);
        transform.localRotation = quaternion;
        postion.Set(sx, sy, sz);
        transform.localScale = postion;
    }

    #region 安卓 苹果接口
    /// <summary>
    /// 剪切板
    /// </summary>
    /// <param name="txt">剪帖内容</param>
    public static void Clipboard(string txt)
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
            Current().Call("setClipboard", txt);
#elif UNITY_IPHONE
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            setClipboard(txt);
#endif
        Utils.AddFlyTextTips("复制成功");
    }

    public static void UpdateApp(string url)
    {
#if UNITY_ANDROID
        if (RuntimePlatform.Android == Application.platform)
            Current().Call("updateApp", url, Application.persistentDataPath);
#endif
    }

    public static void WXInit()
    {
        string AppID = "wx515ba21a688c1867";
#if UNITY_ANDROID
        if (RuntimePlatform.Android == Application.platform)
            Current().Call("InitWX", AppID);
#elif UNITY_IPHONE
        if (RuntimePlatform.IPhonePlayer == Application.platform)
            InitWX(AppID);
#endif
    }

    /// <summary>
    /// 微信邀请(分享)
    /// </summary>
    /// <param name="code">分享标题</param>
    /// <param name="info">分享内容</param>
    /// <param name="type">[0 朋友圈, 1 个人或微群]</param>
    public static void WXShare(string code, string info, string url = "www.baidu.com", int type = 1)
    {//"有人@你：房号【"+code+"】", "猜拳游戏，不限底分，等你来战"
#if UNITY_ANDROID
        if (RuntimePlatform.Android == Application.platform)
            Current().Call("ShareWX", code, info, type, url);
#elif UNITY_IPHONE
        if (RuntimePlatform.IPhonePlayer == Application.platform)
            ShareWX(code, info, type, url);
#endif
    }
    /// <summary>
    /// 应用版本
    /// </summary>
    /// <returns></returns>
    public static string AppVersion()
    {
        string rAppVer = Application.version;
#if UNITY_ANDROID
        if (RuntimePlatform.Android == Application.platform)
        {
            string package = Current().Call<string>("getPackageName");
            AndroidJavaObject package_info = Current().Call<AndroidJavaObject>("getPackageManager").Call<AndroidJavaObject>("getPackageInfo", package, 0);
            //verCode = package_info.Get<int>("versionCode");
            rAppVer = package_info.Get<string>("versionName");
        }
#elif UNITY_IPHONE
        if (RuntimePlatform.IPhonePlayer == Application.platform)
           rAppVer = getAppVersion();
#endif
        return rAppVer;
    }
    /// <summary>
    /// Apple Store 内部购买
    /// </summary>
    /// <param name="item"></param>
    /// <param name="uID"></param>
    public static void applePay(string item, string uID)
    {
        Debug.Log(" ==>>内购ID:" + item);
#if UNITY_IPHONE
        if (RuntimePlatform.IPhonePlayer == Application.platform)
            payStore(item, uID);
#endif
    }
    //获取当前App的Activity  
    //讲解：new AndroidJavaClass("全类名")  ---new一个Android原生类  
    //讲解：CallStatic<返回类型>("方法名")  ---静态方法获取一个Android原生类型
    public static AndroidJavaObject Current()
    {
        if (Application.platform == RuntimePlatform.Android)
            return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        else
            return null;
    }
    //获取指定包名的Activity  
    public static AndroidJavaObject GetActivity(string package_name, string activity_name)
    {
        return new AndroidJavaClass(package_name).GetStatic<AndroidJavaObject>(activity_name);
    }

    #endregion

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern void setClipboard(string txt);                    //剪贴板
	[DllImport("__Internal")]
	private static extern string getAppVersion();
    [DllImport("__Internal")]
    private static extern float getiOSBatteryLevel();
    [DllImport("__Internal")]
    private static extern int getSignalStrength();
    [DllImport("__Internal")]
    private static extern void payStore(string item, string uID);
#endif

    #region DoTween 相关

    public static void DoKill(Component component, bool complete)
    {
        component.DOKill(complete);
    }

    public static void DoKill(Transform trans, bool complete)
    {
        trans.DOKill(complete);
    }


    public static void DOShakePosition(Camera target, float duration, Vector3 strength, int vibrato = 10, float randomness = 90, bool fadeOut = true)
    {
        target.DOShakePosition(duration, strength, vibrato, randomness, fadeOut);
    }
    public static void DOShakePosition(Transform target, float duration, Vector3 strength, int vibrato = 10, float randomness = 90, bool snapping = false, bool fadeOut = true)
    {
        target.DOShakePosition(duration, strength, vibrato, randomness, snapping, fadeOut);
    }

    public static TweenerCore<Vector3, Path, PathOptions> TweenPath(Transform trans, Vector3[] paths, float duration, PathType pathType = PathType.Linear)
    {
        if (trans == null)
        {
            return null;
        }
        return trans.DOLocalPath(paths, duration, pathType);
    }

    public static Tweener TweenAlpha(GObject target, float endValue, float duration)
    {
        return DOTween.To(() =>
                          {
                              if (target == null)
                                  return endValue;
                              else
                                  return target.alpha;
                          },
                          alpha =>
                          {
                              if (target != null && !target.isDisposed)
                                  target.alpha = alpha;
                          },
                          endValue,
                          duration);
    }

    public static Tweener TweenHeight(GObject target, float endValue, float duration)
    {
        //return DOTween.To(() => target.height, height => target.height = height, endValue, duration);
        return DOTween.To(() =>
                          {
                              if (target == null)
                                  return endValue;
                              else
                                  return target.height;
                          },
                          height =>
                          {
                              if (target != null && !target.isDisposed)
                                  target.height = height;
                          },
                          endValue,
                          duration);

    }
    public static Tweener TweenXY(DisplayObject target, Vector2 endValue, float duration)
    {
        //return DOTween.To(() => target.xy, x => target.xy = x, endValue, duration);
        return DOTween.To(() =>
        {
            if (target == null)
                return endValue;
            else
                return target.xy;
        },
                        xy =>
                        {
                            if (target != null && !target.isDisposed)
                                target.xy = xy;
                        },
                        endValue,
                        duration);

    }
    public static Tweener TweenXY(GObject target, Vector2 endValue, float duration)
    {
        //return DOTween.To(() => target.xy, x => target.xy = x, endValue, duration);
        return DOTween.To(() =>
                        {
                            if (target == null)
                                return endValue;
                            else
                                return target.xy;
                        },
                        xy =>
                        {
                            if (target != null && !target.isDisposed)
                                target.xy = xy;
                        },
                        endValue,
                        duration);

    }
    public static Tweener TweenX(GObject target, float endValue, float duration)
    {
        //return DOTween.To(() => target.x, x => target.x = x, endValue, duration);
        return DOTween.To(() =>
                    {
                        if (target == null)
                            return endValue;
                        else
                            return target.x;
                    },
                    x =>
                    {
                        if (target != null && !target.isDisposed)
                            target.x = x;
                    },
                    endValue,
                    duration);
    }
    public static Tweener TweenY(GObject target, float endValue, float duration)
    {
        //return DOTween.To(() => target.y, x => target.y = x, endValue, duration);
        return DOTween.To(() =>
                        {
                            if (target == null)
                                return endValue;
                            else
                                return target.y;
                        },
                        y =>
                        {
                            if (target != null && !target.isDisposed)
                                target.y = y;
                        },
                        endValue,
                        duration);
    }
    public static Tweener TweenScale(GObject target, Vector2 endValue, float duration)
    {
        //return DOTween.To(() => target.scale, x => target.scale = x, endValue, duration);
        return DOTween.To(() =>
                        {
                            if (target == null)
                                return endValue;
                            else
                                return target.scale;
                        },
                        scale =>
                        {
                            if (target != null && !target.isDisposed)
                                target.scale = scale;
                        },
                        endValue,
                        duration);
    }
    public static Tweener TweenScale(Transform target, Vector3 endValue, float duration)
    {
        //return DOTween.To(() => target.scale, x => target.scale = x, endValue, duration);
        return DOTween.To(() =>
        {
            if (target == null)
                return endValue;
            else
                return target.localScale;
        },
                        scale =>
                        {
                            if (target != null)
                                target.localScale = scale;
                        },
                        endValue,
                        duration);
    }
    public static Tweener TweenPosition(Transform target, Vector3 endValue, float duration)
    {
        //return DOTween.To(() => target.position, x => target.position = x, endValue, duration);
        return DOTween.To(() =>
        {
            if (target == null)
                return endValue;
            else
                return target.position;
        },
                         position =>
                         {
                             if (target != null)
                                 target.position = position;
                         },
                        endValue,
                        duration);
    }
    public static Tweener TweenLocalPosition(Transform target, Vector3 endValue, float duration)
    {
        //return DOTween.To(() => target.localPosition, x => target.localPosition = x, endValue, duration);
        return DOTween.To(() =>
        {
            if (target == null)
                return endValue;
            else
                return target.localPosition;
        },
                         position =>
                         {
                             if (target != null)
                                 target.localPosition = position;
                         },
                        endValue,
                        duration);
    }
    public static Tweener TweenScaleX(GObject target, float endValue, float duration)
    {
        //return DOTween.To(() => target.scaleX, x => target.scaleX = x, endValue, duration);
        return DOTween.To(() =>
                         {
                             if (target == null)
                                 return endValue;
                             else
                                 return target.scaleX;
                         },
                         scaleX =>
                         {
                             if (target != null && !target.isDisposed)
                                 target.scaleX = scaleX;
                         },
                         endValue,
                         duration);
    }
    public static Tweener TweenScaleY(GObject target, float endValue, float duration)
    {
        //return DOTween.To(() => target.scaleY, x => target.scaleY = x, endValue, duration);
        return DOTween.To(() =>
                        {
                            if (target == null)
                                return endValue;
                            else
                                return target.scaleY;
                        },
                        scaleY =>
                        {
                            if (target != null && !target.isDisposed)
                                target.scaleY = scaleY;
                        },
                        endValue,
                        duration);
    }
    public static Tweener TweenPosition(GObject target, Vector3 endValue, float duration)
    {
        //return DOTween.To(() => target.position, x => target.position = x, endValue, duration);
        return DOTween.To(() =>
                    {
                        if (target == null)
                        {
                            Debug.Log("空了");
                            return endValue;
                        }
                        else
                            return target.position;
                    },
                    position =>
                    {
                        if (target != null && !target.isDisposed)
                            target.position = position;
                    },
                    endValue,
                    duration);
    }
    public static Tweener TweenRotation(GObject target, float endValue, float duration)
    {
        //return DOTween.To(() => target.rotation, x => target.rotation = x, endValue, duration);
        return DOTween.To(() =>
        {
            if (target == null)
            {
                Debug.Log("空了");
                return endValue;
            }
            else
                return target.rotation;
        },
                    x =>
                    {
                        if (target != null && !target.isDisposed)
                            target.rotation = x;
                    },
                    endValue,
                    duration);
    }

    public static Tweener TweenLoader(GLoader target, float endValue, float duration)
    {
        //return DOTween.To(() => target.fillAmount, x => target.fillAmount = x, endValue, duration);
        return DOTween.To(() =>
        {
            if (target == null)
            {
                Debug.Log("空了");
                return endValue;
            }
            else
                return target.fillAmount;
        },
                    x =>
                    {
                        if (target != null && !target.isDisposed)
                            target.fillAmount = x;
                    },
                    endValue,
                    duration);
    }

    public static Tweener DoTweenTo(DOGetter<int> getter, DOSetter<int> setter, int endValue, float duration)
    {
        return DOTween.To(getter, setter, endValue, duration);
    }
    public static Tweener DoTweenTo(DOGetter<long> getter, DOSetter<long> setter, int endValue, float duration)
    {
        return DOTween.To(getter, setter, endValue, duration);
    }
    public static Tweener DoTweenTo(DOGetter<uint> getter, DOSetter<uint> setter, uint endValue, float duration)
    {
        return DOTween.To(getter, setter, endValue, duration);
    }
    public static Tweener DoTweenTo(DOGetter<ulong> getter, DOSetter<ulong> setter, ulong endValue, float duration)
    {
        return DOTween.To(getter, setter, endValue, duration);
    }
    public static Tweener DoTweenTo(DOGetter<float> getter, DOSetter<float> setter, float endValue, float duration)
    {
        return DOTween.To(getter, setter, endValue, duration);
    }

    public static void DoTweenKill(Tween tween, bool complete = false)
    {
        tween.Kill(complete);
    }

    public static Tweener ToArray(GObject target, Vector3 targetPoint, Vector3[] pathList, float[] durationsList)
    {

        return DOTween.ToArray(() =>
                               {
                                   if (target != null && !target.isDisposed)
                                       return target.position;
                                   else
                                       return targetPoint;
                               },
                               (Vector3 pos) =>
                               {
                                   if (target != null && !target.isDisposed)
                                       target.position = pos;
                               },
                               pathList,
                               durationsList);
    }


    public static Sequence Append(Sequence s, Tween t)
    {
        return s.Append(t);
    }

    public static Sequence AppendCallback(Sequence s, TweenCallback callback)
    {
        return s.AppendCallback(callback);
    }

    public static Sequence InsertCallback(Sequence s, float atposition, TweenCallback callback)
    {
        return s.InsertCallback(atposition, callback);
    }

    public static Sequence AppendInterval(Sequence s, float interval)
    {
        return s.AppendInterval(interval);
    }
    public static Sequence Insert(Sequence s, float atPosition, Tween t)
    {
        return s.Insert(atPosition, t);
    }

    public static T SetEase<T>(T t, Ease ease) where T : Tween
    {
        return t.SetEase<T>(ease);
    }

    public static T TweenPlay<T>(T t) where T : Tween
    {
        return t.Play<T>();
    }

    public static T TweenOnComplete<T>(T t, TweenCallback action) where T : Tween
    {
        return t.OnComplete<T>(action);
    }

    public static T DoTweenSetDelay<T>(T t, float delay) where T : Tween
    {
        return t.SetDelay<T>(delay);
    }


    #endregion

    /// <summary>
    /// 加载xml 
    /// </summary>
    /// <returns></returns>
    public static void LoadXMLByBundle(string xmlName, Action<XmlDocument> callBack, string bundleName, string bundleDirectory)
    {
        StartDoCoroutine(LoadXMLByBundleCoroutine(xmlName, callBack, bundleName, bundleDirectory));
    }
    private static IEnumerator LoadXMLByBundleCoroutine(string xmlName, Action<XmlDocument> callBack, string bundleName, string bundleDirectory)
    {
        yield return new WaitForEndOfFrame();
        AssetManager.LoadXMLByBundle(xmlName, callBack, bundleName, bundleDirectory);
    }
    static Dictionary<string, Action<XmlDocument>> callBackPool = new Dictionary<string, Action<XmlDocument>>();
    static void LoadXMLByBundleByThread(string xmlName, Action<XmlDocument> callBack, string bundleName, string bundleDirectory)
    {
        callBackPool[xmlName] = callBack;
        System.Xml.XmlDocument doc = AssetManager.LoadXMLByBundle(xmlName, bundleName, bundleDirectory);
        System.Threading.Tasks.Task.Run(async () =>
       {
           await Task.Delay(2);
           lock (callBackPool)
           {
               if (callBackPool.ContainsKey(xmlName))
               {
                   callBackPool[xmlName](doc);
               }
               callBackPool[xmlName] = null;
           }
       });
    }


    public static void ScreenCapture(Texture2D screenShot)
    {
        string targetPath = string.Format("{0}/Resources/UIPackage/", Application.dataPath);
        StartDoCoroutine(ScreenCapture(screenShot, targetPath + "/CaptureScreenshot.png"));
    }
    private static IEnumerator ScreenCapture(Texture2D screenShot, string filename)
    {
        //在一帧渲染之后读取屏幕信息
        yield return new WaitForEndOfFrame();
        //读取屏幕像素信息并存储为纹理数据
        screenShot.ReadPixels(new Rect(Screen.width / 2 - 80, Screen.height / 2 + 150, 200f, 200f), 0, 0);
        screenShot.Apply();
        //将纹理数据转换成png图片文件
        byte[] bytes = screenShot.EncodeToPNG();
        //写入文件,并且指定路径
        File.WriteAllBytes(filename, bytes);
    }

    /// <summary>
    /// 数据异步 XmlReader 加载方式
    /// </summary>
    public static void LoadXMLByBundleByThreadAsync(string xmlName, Action<XmlReader> callBack, string bundleName, string bundleDirectory)
    {
        AssetManager.LoadXMLByBundleAsync(xmlName, bundleName, bundleDirectory, callBack);
    }

    /// <summary>
    /// 获得移动平台安全区域的Rect
    /// </summary>
    public static int GetSafeArea()
    {
        //计算屏幕顶部异形大小
        int safeAreaOffset = (int)(Screen.height - Screen.safeArea.yMax);
        Utils.Log("GetSafeArea" + safeAreaOffset);
        return safeAreaOffset;
    }
    /// <summary>
    /// 获取屏幕比例是否小于9/17.5
    /// 9/17.5 算长屏了
    /// </summary>
    public static bool GetScreenAspectRatioLt9R16()
    {
        return (Screen.width / (float)Screen.height) <= 9 / 17.5f;
    }

    /// <summary>
    /// 修改颜色
    /// </summary>
    public static void SetColor(this Color32 color, byte r, byte g, byte b, byte a)
    {
        color.r = r;
        color.g = g;
        color.b = b;
        color.a = a;
    }

    private static string CmdPath = @"C:\Windows\System32\cmd.exe";
    public static void RunCmd(string cmd, out string output)
    {
        cmd = cmd.Trim().TrimEnd('&') + "&exit";//说明：不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
        UnityEngine.Debug.Log(cmd);
        using (Process p = new Process())
        {
            p.StartInfo.FileName = CmdPath;
            p.StartInfo.Arguments = "/c " + cmd;
            p.StartInfo.UseShellExecute = false;        //不使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;   //接受来自Unity的输入信息
            p.StartInfo.RedirectStandardOutput = true;  //从Unity控制台获取输出信息
            p.StartInfo.RedirectStandardError = true;   //从Unity控制台定向标准错误输出
            p.StartInfo.CreateNoWindow = true;          //不显示程序窗口，默默执行
            p.Start();//启动程序

            //获取cmd窗口的输出信息
            output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
        }
    }
    public static string ToChineseString(this DayOfWeek dayOfWeek, bool week = true)
    {
        switch (dayOfWeek)
        {
            case DayOfWeek.Friday:
                return week ? "周五" : "星期五";
            case DayOfWeek.Monday:
                return week ? "周一" : "星期一";
            case DayOfWeek.Saturday:
                return week ? "周六" : "星期六";
            case DayOfWeek.Sunday:
                return week ? "周日" : "星期天";
            case DayOfWeek.Thursday:
                return week ? "周四" : "星期四";
            case DayOfWeek.Tuesday:
                return week ? "周二" : "星期二";
            default:
                return week ? "周三" : "星期三";
        }
    }
}

public enum RenderingMode
{
    Opaque = 1,
    Cutout = 2,
    Fade = 3,
    Transparent = 4,
}

public enum VertexPosition : int
{
    BottomLeft = 0x00,
    BottomRight = 0x01,
    TopLeft = 0x10,
    TopRight = 0x11,
}