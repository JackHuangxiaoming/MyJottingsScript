using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using System.Linq;
using System;
using ICSharpCode.SharpZipLib.Zip;
using UnityEngine.Networking;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    public bool IsAddAttachDebugTime = false;
    [SerializeField]
    public bool IsDownLoad = false;
    [SerializeField]
    public bool IsFairyGUIGizimos = false;
    [SerializeField]
    public bool IsOpenAssetsListApplicationVersion = false;

    public UnityEngine.AddressableAssets.AssetReference ar;
    public const string LoginViewName = "LoginView";
    //public const string LoginViewName = "LobbyMainView";
    private DownLoadCombin _downLoadCombin;
    private string VUIphone = string.Empty;         //update version iphone address
    private string VUAndroid = string.Empty;        //update version android address    

    private GComponent _lodingView;
    private GTextField _msgText;
    private Controller _msgController;
    private GProgressBar _msgProgress;

    //private Texture2D GetTexture(System.Drawing.Bitmap bmp, Texture2D texture)
    //{
    //    if (texture == null)
    //        texture = new Texture2D(bmp.Width, bmp.Height);
    //    unsafe
    //    {
    //        // lock bitmap
    //        System.Drawing.Imaging.BitmapData origdata =
    //            bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
    //        texture.LoadRawTextureData(origdata.Scan0, bmp.Width * bmp.Height);
    //        // unlock bitmap
    //        bmp.UnlockBits(origdata);
    //    }
    //    return texture;
    //}
    //private System.Drawing.Bitmap TestCreatQR_Encode(string content, int width, int height)
    //{
    //    BarcodeWriter bWriter = new BarcodeWriter()
    //    {
    //        Format = BarcodeFormat.QR_CODE,
    //        Options = new EncodingOptions()
    //        {
    //            Width = width,
    //            Height = height
    //        }
    //    };
    //    return bWriter.Write(content);
    //}
    //private Texture2D CreatQR_texture(string message)
    //{
    //    Texture2D encoded = new Texture2D(512, 512);
    //    if (message.Length > 1)
    //    {
    //        //二维码写入图片    
    //        encoded = GetTexture(TestCreatQR_Encode(message, encoded.width, encoded.height), encoded);
    //    }
    //    else
    //    {
    //        Debug.Log("生成二维码失败");
    //    }
    //    //Sprite sprite = Sprite.Create(encoded, new Rect(0, 0, encoded.width, encoded.height), Vector2.zero);
    //    return encoded;
    //}

    //定义方法生成二维码  
    public void rotateString(char[] str, int offset)
    {
        int len = str.Length;
        offset = offset > len - 1 ? offset % (len - 1) : offset;
        char[] strR = new char[len];
        for (int i = 0; i < len; i++)
        {
            strR[i] = str[offset + i > len - 1 ? (offset + i) % len : offset + i];
        }
    }

    //public class DamageMsg
    //{
    //    int objID;
    //    int source;
    //    int target;
    //    int num;
    //    bool is_critical;
    //    int effectTime;
    //    bool miss;
    //    int hp;
    //}

    public void CallBack(GameObject obj)
    {

    }
    private void Awake()
    {
        //var go = UnityEngine.AddressableAssets.Addressables.LoadAssetAsync<GameObject>("");
        //var go = UnityEngine.AddressableAssets.Addressables.InstantiateAsync("key");
        //var go = UnityEngine.AddressableAssets.Addressables.LoadAssetsAsync<GameObject>("", CallBack);

        #region Test
        //var dm = Newtonsoft.Json.JsonConvert.DeserializeObject("{\"objID\":0,\"source\":2,\"num\":123,\"isCritical\":false,\"effectTime\":1,\"miss\":true,\"hp\":15850}",typeof(DamageMsg)); ;
        //string s = "FFFFFFFF";        
        //Color32 c32 = new Color32();                
        //c32.SetColor(byte.Parse(s.Substring(0, 2)), byte.Parse(s.Substring(2, 2)), byte.Parse(s.Substring(4, 2)), byte.Parse(s.Substring(6, 2)));
        //List<Vector2> list = new List<Vector2>();
        //list.Add(new Vector2(0, 1));
        //list.Add(new Vector2(-1, 1));
        //list.Add(new Vector2(-1, 0));
        //list.Add(new Vector2(-1, -1));
        //list.Add(new Vector2(0, -1));
        //list.Add(new Vector2(1, -1));
        //list.Add(new Vector2(1, 0));
        //list.Add(new Vector2(1, 1));

        //float angle;
        //for (int i = 0; i < list.Count; i++)
        //{
        //    angle = Vector2.Angle(Vector2.right, list[i]);
        //    Debug.LogError(angle);
        //}

        //uint data = 0xff0000ff;//rgb a                
        //data = 0x0000ffff;//bgr a
        //uint d1 = (data & 0x000000ff) << 16;
        //uint d2 = (data & 0x0000FF00);
        //uint d3 = (data & 0x00FF0000) >> 16;
        //uint d4 = (data & 0xFF000000);
        //data = d1 | d2;
        //data = data | d3;
        //data = data | d4;
        //Utils.LogError("0000FFFF bgra =>:" + data.ToString("X8"));
        //var xx = new TestQRPopup();
        //return;
        #endregion
        UIConfig.defaultFont = "SIMKAI";
        UIConfig.buttonSound = new NAudioClip((AudioClip)Resources.Load("Sound/click_1", typeof(AudioClip)));
        FontManager.RegisterFont(FontManager.GetFont("SIMKAI"), "楷体");
        FontManager.RegisterFont(FontManager.GetFont("SIMKAI"), "KaiTi");
        FontManager.GetFont("SIMKAI").customBoldAndItalic = true;
        URLFactory.NewAppVersion = Application.version;

        UnityEngine.Debug.Log(" SYS ===>>" + SystemInfo.operatingSystem);//不能屏蔽不能删除 很重要 IL2CPP 使用        
        DebugControlManager.CommonDebug = true;
        DebugControlManager.FailedDebug = true;
        string str = Application.version;
        URLFactory.GameVersion = str;
        str = str.Substring(str.LastIndexOf('.') + 1);
        int versionIntValue = int.Parse(str);
        int style = versionIntValue / 100;
        int packageNum = versionIntValue % 100;
        int logoNum = packageNum % 50;

        NetManager.PackageStyle = style.ToString().PadLeft(2, '0');
        NetManager.PackageNum = packageNum.ToString().PadLeft(2, '0');
        NetManager.LogoNum = logoNum.ToString().PadLeft(2, '0');

        GameObject logoObj = GameObject.Find("logo");
        if (logoObj != null)
        {
            MeshRenderer mesh = logoObj.GetComponent<MeshRenderer>();
            mesh.material = Resources.Load<Material>(string.Format("Logo/logo_{0}", NetManager.LogoNum));
        }

        if (NetManager.IsExitAccount)
        {
            return;
        }

        //PhotonNetwork.PhotonServerSettings.Protocol = ConnectionProtocol.Tcp;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
        Application.runInBackground = true;

        InitLodingUI();

    }

    void Start()
    {
        if (NetManager.IsExitAccount)
        {
            return;
        }
        StartCoroutine(LoadAssetFromStream());
        if (IsFairyGUIGizimos)
        {
            GameObject obj = new GameObject("FairyGUIGizimos");
            obj.AddComponent<FairyGUIGizmos>();
            GameObject.DontDestroyOnLoad(obj);
        }
    }

    void Update()
    {
        if (_downLoadCombin != null)
        {
            string name = _downLoadCombin.gameResInfo.gameName;
            name = name.Length > 5 ? name.Substring(name.Length - 5) : name;
            SetLodingProgressAndMessage(name, _downLoadCombin.Progress);
        }
        else if (_msgController != null)
        {
            _msgController.selectedIndex = 0;
        }
    }

    public void GetAssetServerUrlFromWeb()
    {
        _msgText.text = "正在请求资源地址..";
        //string webPhpUrl = URLFactory.FormatWebPhp(URLFactory.WebServerPath, URLFactory.GetUrlByVersionAndDevicePHP,
        //                                    "Version", URLFactory.GameVersion, "DeviceSystem", URLFactory.DeviceSystem);
        Utils.PostHttpRequest(URLFactory.GameConfigJsonPathByServer, null, OnPhpServer, false);
    }

    private void OnPhpServer(string serverStr)
    {
        if (!string.IsNullOrEmpty(serverStr))
        {
            MyJson.JsonNode_Object jsonNode = MyJson.Parse(serverStr) as MyJson.JsonNode_Object;
            if (jsonNode == null)
                Utils.LogError(serverStr);
            else
            {
                //URLFactory.ServerGameResPath = jsonNode["ServerGameResPath"].AsString() + NetManager.PackageStyle + "/";
                //if (jsonNode.ContainsKey("AnyAmount")) URLFactory.AnyAmount = jsonNode["AnyAmount"].AsInt();
                //根据web服务器Php返回， 修正各链接地址
                URLFactory.NewAppVersion = jsonNode["version"].AsString();
                URLFactory.NewApkWebUrl = jsonNode["apkUrl"].AsString();//ApkUrl

                if (!AppVersionCompare(Application.version, URLFactory.NewAppVersion))
                {
                    StartCoroutine(StartInit());
                }
                else // 更新大版本
                {
                    string msg = "游戏版本过低，请下载最新安装包进行游戏";
                    Utils.MessagePopup(msg, UpdateIphonePackage, 1, true, "前往");
                }
            }

        }
        else
        {
            Timers.inst.Add(3, 1, OnWaitToGetWebPath);
        }
    }

    private void UpdateIphonePackage()
    {
#if UNITY_ANDROID
        //Utils.UpdateApp(VUAndroid);
        Application.OpenURL(URLFactory.NewApkWebUrl);
#elif UNITY_IPHONE
        //Application.OpenURL(VUIphone);
        Application.OpenURL(URLFactory.NewApkWebUrl);
#endif
        Application.Quit();
    }

    private int onWaitToGetWebPathCountDown = 3;
    private void OnWaitToGetWebPath(object param)
    {
        onWaitToGetWebPathCountDown -= 1;
        if (onWaitToGetWebPathCountDown < 0)
        {
            Utils.MessagePopup("当前网络有问题，请稍后重试", null, 1, true);
            return;
        }
        GetAssetServerUrlFromWeb();
    }
    private static bool unZipOk = false;
    private IEnumerator LoadAssetFromStream()
    {
        string checkTagPath = Application.persistentDataPath + "/checkHold.txt";
        if (File.Exists(checkTagPath))
        {
            StreamReader tagReader = new StreamReader(checkTagPath);
            string str = tagReader.ReadLine();
            tagReader.Dispose();
            bool isNewVersion = AppVersionCompare(Utils.AppVersion(), str);
            if (isNewVersion)
            {
                File.Delete(checkTagPath);
                string msg = "游戏版本过低，请下载最新安装包进行游戏";
                Utils.MessagePopup(msg, UpdateIphonePackage, 1, true, "前往");
            }
            else
            {
                CreatTag();
                GetAssetServerUrlFromWeb();
            }
            yield break;
        }
        yield return null;

        _msgText.text = "准备资源文件..";
        Utils.Log("准备资源文件..");

        string assetname = "/jzsh";
        string path = Application.streamingAssetsPath + assetname;
#if UNITY_EDITOR
        path = Application.streamingAssetsPath + assetname;
#elif UNITY_ANDROID
        path = "jar:file://" + Application.dataPath + "!/assets/AssetsList.txt";
#elif UNITY_IPHONE
        path = Application.streamingAssetsPath + assetname;
#endif

        unZipOk = false;
        string fullPath = Application.persistentDataPath;
        string unZipToDirectoryPath = Application.persistentDataPath;

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

#if UNITY_IPHONE || UNITY_EDITOR
        if (!Directory.Exists(path))
        {
            CreatTag();
            GetAssetServerUrlFromWeb();
            yield break;
        }
        UtilsZipHelper.CopyFileMultiSync(path, fullPath, OnUnZipFileResult);
#elif UNITY_ANDROID
        UnityWebRequest uwr = UnityWebRequest.Get(path);
        yield return uwr.SendWebRequest();//读取数据

        List<string> assetsList = new List<string>();
        if (string.IsNullOrEmpty(uwr.error))
        {
            string data = uwr.downloadHandler.text;
            uwr.Abort();
            int index = -1;
            while ((index = data.LastIndexOf("jzsh")) != -1)
            {
                assetsList.Add(data.Substring(index));
                //Utils.Log(assetsList[assetsList.Count - 1]);
                data = data.Substring(0, index);
            }
            path = "jar:file://" + Application.dataPath + "!/assets";
            UtilsZipHelper.CopyFileByWebRequest(assetsList, path, fullPath, OnUnZipFileResult);
        }
        else
        {
            Utils.Log("获取初始包资源列表文件失败：" + uwr.error);
            uwr.Abort();
            CreatTag();
            GetAssetServerUrlFromWeb();
        }
#endif        
        yield return null;

        _msgText.text = "开始加载初始化资源..不消耗流量";
        Utils.Log(_msgText.text);

        yield return null;
        while (!unZipOk)
        {
            _msgText.text = string.Format("已初始化资源...{0}/{1}", UtilsZipHelper.TestCurrentFileNumber, UtilsZipHelper.TestCurrentFileNumberTotal);
            Utils.Log(_msgText.text);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnUnZipFileResult(bool result, string sourcePath)
    {
        unZipOk = true;
        if (!string.IsNullOrEmpty(sourcePath))
        {
            //File.Delete(sourcePath);
        }
        _msgText.text = "加载初始化资源.." + (result ? "完成" : "失败");
        Utils.Log(_msgText.text);
        CreatTag();
        GetAssetServerUrlFromWeb();
    }

    private void CreatTag()
    {
        StreamWriter endWriter = new StreamWriter(Application.persistentDataPath + "/checkHold.txt");
        endWriter.Write(Application.version);
        endWriter.Flush();
        endWriter.Close();
        Utils.Log("写入标记..");

    }

    /// <summary>
    /// 版本比较
    /// </summary>
    /// <param name="vLocal">本地应用版本号</param>
    /// <param name="vNewest">最新应用版本号</param>
    /// <param name="length">比较位数(三位)</param>
    /// <returns>是否需要更新[TRUE:更新, FALSE:不更新]</returns>
    private bool AppVersionCompare(string vLocal, string vNewest, int length = 3)
    {
        string[] v1 = vLocal.Split('.');
        string[] v2 = vNewest.Split('.');
        int len = v1.Length > v2.Length ? v2.Length : v1.Length;
        len -= 1;//最后一位用于确认包风格包编号
        for (int i = 0; i < len; i++)
        {
            int a = int.Parse(v1[i]);
            int b = int.Parse(v2[i]);
            if (b > a) return true;
            else if (b < a) return false;
        }
        return false;
    }

    IEnumerator StartInit()
    {
        _msgText.text = "检测资源版本";
#if UNITY_EDITOR

#elif UNITY_ANDROID || UNITY_IPHONE
        IsDownLoad = true;
#endif
        DownLoadManager.Instance.Initialize();
        if (!IsDownLoad)
        {
            OnCodeLoaded();
            yield break;
        }
        while (!DownLoadManager.IsInited)
            yield return null;

        if (IsOpenAssetsListApplicationVersion)
        {
            if (AppVersionCompare(Application.version, URLFactory.NewAppVersion))
            {
                string msg = "游戏版本过低，请下载最新安装包进行游戏";
                Utils.MessagePopup(msg, UpdateIphonePackage, 1, true, "前往");
                yield break;
            }
        }
        _msgText.text = "更新资源......ode";
        bool b = DownLoadManager.Instance.IsNewestVersion("PlatformLobbyCode");
        if (!b)
        {
            _downLoadCombin = DownLoadManager.Instance.DownLoadGame("PlatformLobbyCode", OnCodeLoaded);
        }
        else
        {
            OnCodeLoaded();
        }
    }
    private void DelayRepeatingTimeStart()
    {
        countDown--;
        if (countDown < 0)
        {
            IsAddAttachDebugTime = false;
            CancelInvoke();
            OnCodeLoaded();
            return;
        }
        SetTipsMessage("等待附加ILruntime Debug ... CountDown:" + countDown);
    }
    private void OnCodeLoaded()
    {
        _downLoadCombin = null;
        HotScriptManager.Instance.Init();//注 初始化顺序不能乱    
        NetManager.Instance.Init();//注 初始化网络层           
        if (IsAddAttachDebugTime)
        {
            SetTipsMessage("等待附加ILruntime Debug ...");
            countDown = 15;
            InvokeRepeating("DelayRepeatingTimeStart", 0, 1);
            return;
        }
#if UNITY_EDITOR

#elif UNITY_ANDROID || UNITY_IPHONE
        IsDownLoad = true;
#endif
        if (!IsDownLoad)
        {
            OnPlatformLobbyResLoaded();
            return;
        }
        _msgText.text = "更新资源......Lobby";
        bool b = DownLoadManager.Instance.IsNewestVersion("PlatformLobby");
        if (!b)
        {
            _downLoadCombin = DownLoadManager.Instance.DownLoadGame("PlatformLobby", OnPlatformLobbyResLoaded);
        }
        else
        {
            OnPlatformLobbyResLoaded();
        }
    }
    public void OnPlatformLobbyResLoaded()
    {
        HotScriptManager.Instance.InitHotScript();//注 初始化顺序不能乱                
        _downLoadCombin = null;
        SetTipsMessage();
        Utils.Log("开始显示登录界面");
        ShowLoginView();
    }

    private int countDown = 10;
    public void ShowLoginView()
    {
        UIManager.Instance.ShowView(LoginViewName);
        DestroyLodingUI();
    }
    private void InitLodingUI()
    {
        AudioManager.Instance.GetInstanceID();
        AudioManager.Instance.BackMusicVolume = 100;
        AudioManager.Instance.EffectSoundsVolume = 100;
        AudioManager.Instance.PlayMusic("Sound/music-login-bg", true, false);

        _lodingView = AssetManager.Instance.CreatFGUIFromResouce<GComponent>(AssetManager.BasicPackage, "InitLodingView");
        if (_lodingView == null)
        {
            Utils.LogError("加载初始化界面UI失败");
            return;
        }
        _msgProgress = Utils.FindChildByName<GProgressBar>(_lodingView, "lodingProgress");
        _msgText = Utils.FindChildByName<GTextField>(_lodingView, "title");
        _msgController = _lodingView.GetController("IsShowProgress");
        _msgController.selectedIndex = 0;
        _msgProgress.value = 0;
        SetTipsMessage("正在检查资源...");
        LayerManager.NormalLayer.AddChild(_lodingView);
    }

    private void SetLodingProgressAndMessage(string v, int progress)
    {
        if (_msgController != null)
        {
            _msgController.selectedIndex = 1;
        }
        if (_msgProgress != null)
        {
            _msgProgress.value = progress;
        }
        SetTipsMessage(string.Format("更新资源......{0}  {1}%", v, progress));
    }
    private void SetTipsMessage(string message = "")
    {
        if (_msgText != null)
        {
            _msgText.text = message;
        }
    }
    void OnDestroy()
    {
        DestroyLodingUI();
    }

    private void DestroyLodingUI()
    {
        if (_lodingView != null)
        {
            _lodingView.Dispose();
        }
        _lodingView = null;
        if (_msgText != null)
        {
            _msgText.Dispose();
        }
        _msgText = null;
        if (_msgProgress != null)
        {
            _msgProgress.Dispose();
        }
        _msgProgress = null;
    }


}