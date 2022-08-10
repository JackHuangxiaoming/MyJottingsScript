using System.IO;
using System;
using FairyGUI;
using UnityEngine.Events;
using UnityEngine;
using ILRuntime.Runtime.Stack;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.CLR.Utils;
using ILRuntime.Runtime.Adaptors;
using ILRuntime.CLR.Method;
using System.Collections.Generic;
using ILRuntime.CLR.TypeSystem;
using DG.Tweening.Core;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml;
using CloudBase;
using LitJson;
using ILRuntime.Runtime.Enviorment;
using System.Reflection;
using Newtonsoft.Json;
using ILRuntime.Runtime.Generated;
using SocketIOClient;
using System.Text.Json;

public class HotScriptManager
{

    private static HotScriptManager _instance;
    public static HotScriptManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new HotScriptManager();
            return _instance;
        }
    }

    public ILRuntime.Runtime.Enviorment.AppDomain appDomain { get; private set; }

    private bool _isInit = false;



    private static HotClassILRuntime _basicHotNetManager;
    public static HotClassILRuntime BasicHotNetManager
    {
        get
        {
            if (_basicHotNetManager == null)
                _basicHotNetManager = Instance.CreateHotClass("HotNetManager", "PlatformLobbyCode");
            return _basicHotNetManager;
        }
    }

    private static HotClassILRuntime _testMain;

    public static HotClassILRuntime TestMain
    {
        get
        {
            if (_testMain == null)
                _testMain = Instance.CreateHotClass("TestMain");
            return _testMain;
        }
    }
    public void TestUnityCallILRuntime()
    {
        TestMain.CallMethod("TestUnityCallILRuntime");
        TestMain.CallMethod("TestUnityCallILRuntime", 666);
        TestMain.CallMethodByType("TestUnityCallILRuntime", "张飞", typeof(string), 250, typeof(int));
        TestMain.CallGenericMethod("TestUnityCallILRuntime", new Type[] { typeof(string) }, "李白", 99);
    }

    private HotScriptManager()
    {
        Init();
    }
    public void Init(string filename = "PlatformLobbyCode")
    {
        if (_isInit)
            return;

        //创建ILRuntime环境        
        appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        appDomain.DebugService.StartDebugService(56000);
#if UNITY_EDITOR

        //appDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        _isInit = true;
        string dllPath = Path.Combine(URLFactory.DownLoadHotFilePath, "PlatformLobbyCode/" + filename + ".dll");
        string pdbPath = Path.Combine(URLFactory.DownLoadHotFilePath, "PlatformLobbyCode/" + filename + ".pdb");
        var dllFile = System.IO.File.ReadAllBytes(dllPath);
        var pdbFile = System.IO.File.ReadAllBytes(pdbPath);
#if !UNITY_EDITOR || UNITY_UNENCYPTDLL
        AssetManager.Encypt(ref dllFile);
        AssetManager.Encypt(ref pdbFile);
#endif
        System.IO.MemoryStream msDll = new System.IO.MemoryStream(dllFile);
        System.IO.MemoryStream msPdb = new System.IO.MemoryStream(pdbFile);
        try
        {
            appDomain.LoadAssembly(msDll, msPdb, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch (System.Exception e)
        {
            Utils.LogError("脚本初始化失败 加载PlatformLobbyCode库 出现错误：" + e.ToString());
            throw;
        }

        UsedOnlyForAOTCodeGeneration(appDomain);
        RegistDelegate(appDomain);
        RegistCLR(appDomain);

        Utils.Log("脚本初始化完成");
    }
    /// <summary>
    /// 初始化 热更新中的基础类
    /// </summary>
    public void InitHotScript()
    {
        if (appDomain != null)
        {
            appDomain.Invoke("PlatformLobbyCode.Main", "Start", null, null);
        }

        UIManager.Instance.UIManagerHotClass.GetHashCode();
        MessageFlyTextTipsMgr.inst.NormalTipsHotClass.GetHashCode();
    }


    public void RegistCLR(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        //注册适配器        
        //注册协程适配器
        appDomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        //注册Mono适配器
        appDomain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
        //注册Async适配器
        appDomain.RegisterCrossBindingAdaptor(new IAsyncStateMachineAdaptors());
        //注册IComparer适配器
        appDomain.RegisterCrossBindingAdaptor(new IComparerAdaptor());
        //注册IEquatable适配器
        appDomain.RegisterCrossBindingAdaptor(new IEquatableAdaptor());
        //注册Protobuf适配器
        appDomain.RegisterCrossBindingAdaptor(new Google.Protobuf.ProtobufIMessageAdaptor());

        //注册适配器
        //appDomain.RegisterCrossBindingAdaptor(new BinaryFormatterAdaptors());
        //appDomain.RegisterCrossBindingAdaptor(new SerializationBinderAdaptor());
        //appDomain.RegisterCrossBindingAdaptor(new ISerializableAdapter());



        //==================================================================================================================
        RegisterCLRDebugLog(appDomain);
        RegisterCLRDebugLogError(appDomain);
        RegisterCLRDebugLogWarning(appDomain);
        RegisterCLRAddCompontent(appDomain);
        RegisterCLRGetCompontent(appDomain);
        //RegisterCLRBinaryFormatter(appDomain);
        Newtonsoft_Json_Redirections.Register(appDomain);
        JsonMapper.RegisterILRuntimeCLRRedirection(appDomain);
        RegisterCLRScoketIOGetValue(appDomain);

        //绑定注册 (最后执行)
        ILRuntime.Runtime.Generated.CLRBindings.Initialize(appDomain);
    }


    public void RegistDelegate(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {

        //没有返回值委托
        appDomain.DelegateManager.RegisterMethodDelegate<string>();
        appDomain.DelegateManager.RegisterMethodDelegate<GObject>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Object>();
        appDomain.DelegateManager.RegisterMethodDelegate<FairyGUI.EventContext>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Runtime.CompilerServices.IAsyncStateMachine>();
        appDomain.DelegateManager.RegisterMethodDelegate<XmlDocument>();
        appDomain.DelegateManager.RegisterMethodDelegate<object>();
        appDomain.DelegateManager.RegisterMethodDelegate<List<object>>();
        appDomain.DelegateManager.RegisterMethodDelegate<Dictionary<object, object>>();
        appDomain.DelegateManager.RegisterMethodDelegate<Hashtable>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Int32, FairyGUI.GObject>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Int32, System.String>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.String, Shader>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Object, System.EventArgs>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Int32>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Int64>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.UInt32>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.UInt64>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Single>();
        appDomain.DelegateManager.RegisterMethodDelegate<ILTypeInstance>();
        appDomain.DelegateManager.RegisterMethodDelegate<ILTypeInstance, ILTypeInstance>();
        appDomain.DelegateManager.RegisterMethodDelegate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>();
        appDomain.DelegateManager.RegisterMethodDelegate<IComparerAdaptor.Adaptor, IComparerAdaptor.Adaptor>();
        appDomain.DelegateManager.RegisterMethodDelegate<LitJson.JsonData>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Int32, System.String, JsonData>();
        appDomain.DelegateManager.RegisterMethodDelegate<TencentIMSDKMessage>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.String, System.Collections.Generic.List<global::TencentIMSDKMessage>>();
        appDomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Collision>();
        appDomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Collider>();
        appDomain.DelegateManager.RegisterMethodDelegate<UnityEngine.GameObject>();
        appDomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Transform>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Xml.XmlReader>();

        appDomain.DelegateManager.RegisterMethodDelegate<IAsyncStateMachineAdaptors.Adaptor>();
        appDomain.DelegateManager.RegisterMethodDelegate<CoroutineAdapter.Adaptor>();
        appDomain.DelegateManager.RegisterMethodDelegate<IEquatableAdaptor.Adaptor>();
        appDomain.DelegateManager.RegisterMethodDelegate<ISerializableAdapter.Adaptor>();
        appDomain.DelegateManager.RegisterMethodDelegate<MonoBehaviourAdapter.Adaptor>();
        appDomain.DelegateManager.RegisterMethodDelegate<SerializationBinderAdaptor.Adaptor>();
        appDomain.DelegateManager.RegisterMethodDelegate<CrossBindingAdaptorType>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Int32, System.String, com.tencent.imsdk.LitJson.JsonData>();

        appDomain.DelegateManager.RegisterMethodDelegate<object, string>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Object, System.Int32>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Object, object>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Object, Exception>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.Object, TimeSpan>();
        appDomain.DelegateManager.RegisterMethodDelegate<SocketIOResponse>();
        appDomain.DelegateManager.RegisterMethodDelegate<System.String, SocketIOClient.SocketIOResponse>();
        appDomain.DelegateManager.RegisterMethodDelegate<EventHandler>();
        appDomain.DelegateManager.RegisterMethodDelegate<EventHandler<object>>();
        appDomain.DelegateManager.RegisterMethodDelegate<EventHandler<string>>();
        appDomain.DelegateManager.RegisterMethodDelegate<EventHandler<int>>();
        appDomain.DelegateManager.RegisterMethodDelegate<EventHandler<Exception>>();
        appDomain.DelegateManager.RegisterMethodDelegate<EventHandler<TimeSpan>>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnAnyHandler>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnOpenedHandler>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnAck>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnBinaryAck>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnBinaryReceived>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnDisconnected>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnError>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnEventReceived>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnOpened>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnPing>();
        appDomain.DelegateManager.RegisterMethodDelegate<OnPong>();




        //==================================================================================================================
        //有返回值委托
        appDomain.DelegateManager.RegisterFunctionDelegate<int, string>();
        appDomain.DelegateManager.RegisterFunctionDelegate<FairyGUI.GComponent>();
        appDomain.DelegateManager.RegisterFunctionDelegate<FairyGUI.GLoader>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Int32>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Int64>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.UInt32>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.UInt64>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Single>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Object>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Object, System.Object>();

        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, ILTypeInstance, System.Int32>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.String, System.Boolean>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Int32, System.Boolean>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Int64, System.Boolean>();
        appDomain.DelegateManager.RegisterFunctionDelegate<System.Boolean, System.Boolean>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, IComparerAdaptor.Adaptor, System.Int32>();

        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, System.Boolean>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, System.Object>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, System.String>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, System.Int32>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, System.Boolean>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, IComparerAdaptor.Adaptor>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, List<IComparerAdaptor.Adaptor>>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, IComparerAdaptor.Adaptor[]>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, ILTypeInstance>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, List<ILTypeInstance>>();
        appDomain.DelegateManager.RegisterFunctionDelegate<IComparerAdaptor.Adaptor, ILTypeInstance[]>();

        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, System.Boolean>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, System.Object>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, System.String>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, System.Int32>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, System.Boolean>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, ILTypeInstance>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, List<ILTypeInstance>>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, ILTypeInstance[]>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, IComparerAdaptor.Adaptor>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, List<IComparerAdaptor.Adaptor>>();
        appDomain.DelegateManager.RegisterFunctionDelegate<ILTypeInstance, IComparerAdaptor.Adaptor[]>();

        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, MyJson.IJsonNode>();
        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, int>();
        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, float>();
        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, bool>();
        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, System.Numerics.Vector2>();
        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, double>();
        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, string>();
        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, IList<MyJson.IJsonNode>>();
        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, IDictionary<string, MyJson.IJsonNode>>();
        appDomain.DelegateManager.RegisterFunctionDelegate<global::MyJson.IJsonNode, MyJson.JsonNode_Object>();
        appDomain.DelegateManager.RegisterFunctionDelegate<Google.Protobuf.ProtobufIMessageAdaptor.Adaptor>();


        //==================================================================================================================
        //自定义委托转换       
        appDomain.DelegateManager.RegisterDelegateConvertor<DownLoadCompletedHandler>((act) =>
        {
            return new DownLoadCompletedHandler(() =>
            {
                ((Action)act)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DownLoadProgressHandler>((act) =>
        {
            return new DownLoadProgressHandler((a) =>
            {
                ((Action<Int32>)act)(a);
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<EventCallback0>((action) =>
        {
            //转换器的目的是把Action或者Func转换成正确的类型，这里则是把Action<int>转换成EventCallback0
            return new EventCallback0(() =>
            {
                //调用委托实例
                ((System.Action)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<EventCallback1>((action) =>
        {
            return new EventCallback1((a) =>
            {
                ((System.Action<FairyGUI.EventContext>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<PHPStringHandler>((action) =>
        {
            return new PHPStringHandler((a) =>
            {
                ((System.Action<System.String>)action)(a);
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<UnityAction<System.Single>>((action) =>
        {
            return new UnityAction<System.Single>((a) =>
            {
                ((System.Action<System.Single>)action)(a);
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<ListItemRenderer>((action) =>
        {
            return new ListItemRenderer((a, b) =>
            {
                ((System.Action<System.Int32, FairyGUI.GObject>)action)(a, b);
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<ListItemProvider>((action) =>
        {
            return new ListItemProvider((a) =>
            {
                return ((System.Func<System.Int32, System.String>)action)(a);
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<FairyGUI.ShaderConfig.GetFunction>((action) =>
        {
            return new FairyGUI.ShaderConfig.GetFunction((a) =>
            {
                return ((System.Func<System.String, UnityEngine.Shader>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<FairyGUI.GObjectPool.InitCallbackDelegate>((action) =>
        {
            return new FairyGUI.GObjectPool.InitCallbackDelegate((a) =>
            {
                ((System.Action<FairyGUI.GObject>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<UILoadCallback>((action) =>
        {
            return new UILoadCallback(() =>
            {
                ((System.Action)action)();
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<PlayCompleteCallback>((action) =>
        {
            return new PlayCompleteCallback(() =>
            {
                ((System.Action)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<TransitionHook>((action) =>
        {
            return new TransitionHook(() =>
            {
                ((System.Action)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<FairyGUI.UIObjectFactory.GComponentCreator>((action) =>
        {
            return new FairyGUI.UIObjectFactory.GComponentCreator(() =>
            {
                return ((System.Func<FairyGUI.GComponent>)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<FairyGUI.UIObjectFactory.GLoaderCreator>((action) =>
        {
            return new FairyGUI.UIObjectFactory.GLoaderCreator(() =>
            {
                return ((System.Func<FairyGUI.GLoader>)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<FairyGUI.UIPackage.CreateObjectCallback>((action) =>
        {
            return new FairyGUI.UIPackage.CreateObjectCallback((a) =>
            {
                ((System.Action<FairyGUI.GObject>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<TimerCallback>((action) =>
        {
            return new TimerCallback((a) =>
            {
                ((System.Action<System.Object>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DOGetter<System.Int32>>((action) =>
        {
            return new DOGetter<System.Int32>(() =>
            {
                return ((System.Func<System.Int32>)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DOGetter<System.Int64>>((action) =>
        {
            return new DOGetter<System.Int64>(() =>
            {
                return ((System.Func<System.Int64>)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DOGetter<System.UInt32>>((action) =>
        {
            return new DOGetter<System.UInt32>(() =>
            {
                return ((System.Func<System.UInt32>)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DOGetter<System.UInt64>>((action) =>
        {
            return new DOGetter<System.UInt64>(() =>
            {
                return ((System.Func<System.UInt64>)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DOGetter<System.Single>>((action) =>
        {
            return new DOGetter<System.Single>(() =>
            {
                return ((System.Func<System.Single>)action)();
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<DOSetter<System.Int32>>((action) =>
        {
            return new DOSetter<System.Int32>((a) =>
            {
                ((System.Action<System.Int32>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DOSetter<System.Int64>>((action) =>
        {
            return new DOSetter<System.Int64>((a) =>
            {
                ((System.Action<System.Int64>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DOSetter<System.UInt32>>((action) =>
        {
            return new DOSetter<System.UInt32>((a) =>
            {
                ((System.Action<System.UInt32>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DOSetter<System.UInt64>>((action) =>
        {
            return new DOSetter<System.UInt64>((a) =>
            {
                ((System.Action<System.UInt64>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DOSetter<System.Single>>((action) =>
        {
            return new DOSetter<System.Single>((a) =>
            {
                ((System.Action<System.Single>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<EventHandler>((action) =>
        {
            return new EventHandler((a, b) =>
            {
                ((System.Action<System.Object, System.EventArgs>)action)(a, b);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<vp_Timer.Callback>((action) =>
        {
            return new vp_Timer.Callback(() =>
            {
                ((System.Action)action)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<vp_Timer.ArgCallback>((action) =>
        {
            return new vp_Timer.ArgCallback((a) =>
            {
                ((System.Action<System.Object>)action)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.TweenCallback>((act) =>
        {
            return new DG.Tweening.TweenCallback(() =>
            {
                ((Action)act)();
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
        {
            return new System.Comparison<ILRuntime.Runtime.Intepreter.ILTypeInstance>((x, y) =>
            {
                return ((Func<ILTypeInstance, ILTypeInstance, System.Int32>)act)(x, y);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>((act) =>
        {
            return new System.Comparison<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>((x, y) =>
            {
                return ((Func<IComparerAdaptor.Adaptor, IComparerAdaptor.Adaptor, System.Int32>)act)(x, y);
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
        {
            return new System.Predicate<ILRuntime.Runtime.Intepreter.ILTypeInstance>((x) =>
            {
                return ((Func<ILTypeInstance, System.Boolean>)act)(x);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<System.String>>((act) =>
        {
            return new System.Predicate<System.String>((obj) =>
            {
                return ((Func<System.String, System.Boolean>)act)(obj);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<System.Boolean>>((act) =>
        {
            return new System.Predicate<System.Boolean>((obj) =>
            {
                return ((Func<System.Boolean, System.Boolean>)act)(obj);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<System.Int32>>((act) =>
        {
            return new System.Predicate<System.Int32>((obj) =>
            {
                return ((Func<System.Int32, System.Boolean>)act)(obj);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<System.Int64>>((act) =>
        {
            return new System.Predicate<System.Int64>((obj) =>
            {
                return ((Func<System.Int64, System.Boolean>)act)(obj);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>>((act) =>
        {
            return new System.Predicate<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor>((obj) =>
            {
                return ((Func<ILRuntime.Runtime.Adaptors.IComparerAdaptor.Adaptor, System.Boolean>)act)(obj);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<com.tencent.imsdk.unity.CallBackData>((act) =>
        {
            return new com.tencent.imsdk.unity.CallBackData((obj) =>
            {
                ((Action<LitJson.JsonData>)act)(obj);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<UnityIMCallback>((act) =>
        {
            return new UnityIMCallback((a, b, c) =>
            {
                ((System.Action<System.Int32, System.String, com.tencent.imsdk.LitJson.JsonData>)act)(a, b, c);
            });
        });

        appDomain.DelegateManager.RegisterDelegateConvertor<EventHandler<object>>((act) =>
        {
            return new EventHandler<object>((a, b) =>
            {
                ((Action<object, object>)act)(a, b);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<EventHandler<string>>((act) =>
        {
            return new EventHandler<string>((a, b) =>
            {
                ((Action<object, string>)act)(a, b);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<EventHandler<int>>((act) =>
        {
            return new EventHandler<int>((a, b) =>
            {
                ((Action<object, int>)act)(a, b);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<EventHandler<Exception>>((act) =>
        {
            return new EventHandler<Exception>((a, b) =>
            {
                ((Action<object, Exception>)act)(a, b);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<EventHandler<TimeSpan>>((act) =>
        {
            return new EventHandler<TimeSpan>((a, b) =>
            {
                ((Action<object, TimeSpan>)act)(a, b);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnAnyHandler>((act) =>
        {
            return new OnAnyHandler((a, b) =>
            {
                ((Action<string, SocketIOResponse>)act)(a, b);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnOpenedHandler>((act) =>
        {
            return new OnOpenedHandler((a, b, c) =>
            {
                ((Action<string, int, int>)act)(a, b, c);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnAck>((act) =>
        {
            return new OnAck((a, b) =>
            {
                ((Action<int, List<JsonElement>>)act)(a, b);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnBinaryAck>((act) =>
        {
            return new OnBinaryAck((a, b, c) =>
            {
                ((Action<int, int, List<JsonElement>>)act)(a, b, c);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnBinaryReceived>((act) =>
        {
            return new OnBinaryReceived((a, b, c, d) =>
            {
                ((Action<int, int, string, List<JsonElement>>)act)(a, b, c, d);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnDisconnected>((act) =>
        {
            return new OnDisconnected(() =>
            {
                ((Action)act)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnError>((act) =>
        {
            return new OnError((a) =>
            {
                ((Action<string>)act)(a);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnEventReceived>((act) =>
        {
            return new OnEventReceived((a, b, c) =>
            {
                ((Action<int, string, List<JsonElement>>)act)(a, b, c);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnOpened>((act) =>
        {
            return new OnOpened((a, b, c) =>
            {
                ((Action<string, int, int>)act)(a, b, c);
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnPing>((act) =>
        {
            return new OnPing(() =>
            {
                ((Action)act)();
            });
        });
        appDomain.DelegateManager.RegisterDelegateConvertor<OnPong>((act) =>
        {
            return new OnPong(() =>
            {
                ((Action)act)();
            });
        });


    }

    public static void UsedOnlyForAOTCodeGeneration(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        //泛型方法调用一下即可，这个方法无需被调用，只是用来告诉IL2CPP我们需要这个方法 IOS会出现很多类不能识别问题 所以在工程中调用一下   
        List<Type> types = new List<Type>();
        types.Add(typeof(float));
        types.Add(typeof(HashSet<int>));
        types.Add(typeof(HashSet<float>));
        types.Add(typeof(HashSet<long>));
        types.Add(typeof(HashSet<string>));
        types.Add(typeof(HashSet<bool>));
        types.Add(typeof(HashSet<object>));
        types.Add(typeof(HashSet<Transform>));
        types.Add(typeof(HashSet<Quaternion>));
        types.Add(typeof(HashSet<Vector2>));
        types.Add(typeof(HashSet<Vector3>));

        types.Add(typeof(HashSet<Vector2>));
        types.Add(typeof(DictionaryEntry));
        types.Add(typeof(Dictionary<Transform, Quaternion>));
        types.Add(typeof(Dictionary<int, bool>));
        types.Add(typeof(Dictionary<int, long>));
        types.Add(typeof(Dictionary<int, Vector2>));
        types.Add(typeof(Dictionary<int, Vector3>));
        types.Add(typeof(Dictionary<byte, object>));
        types.Add(typeof(System.Runtime.CompilerServices.IAsyncStateMachine));
        types.Add(typeof(CloudBaseApp));
        types.Add(typeof(FunctionResponse));
        types.Add(typeof(AuthState));

        types.Add(typeof(Queue<char>));
        types.Add(typeof(Queue<byte>));
        types.Add(typeof(Queue<short>));
        types.Add(typeof(Queue<float>));
        types.Add(typeof(Queue<int>));
        types.Add(typeof(Queue<double>));
        types.Add(typeof(Queue<long>));
        types.Add(typeof(Queue<decimal>));

        types.Add(typeof(Stack<char>));
        types.Add(typeof(Stack<byte>));
        types.Add(typeof(Stack<short>));
        types.Add(typeof(Stack<float>));
        types.Add(typeof(Stack<int>));
        types.Add(typeof(Stack<double>));
        types.Add(typeof(Stack<long>));
        types.Add(typeof(Stack<decimal>));

        //types.Add(typeof(RepeatedField<float>));
        //types.Add(typeof(RepeatedField<int>));
        //types.Add(typeof(RepeatedField<long>));
        //types.Add(typeof(RepeatedField<UInt16>));
        //types.Add(typeof(RepeatedField<UInt32>));
        //types.Add(typeof(RepeatedField<UInt64>));
        //types.Add(typeof(RepeatedField<bool>));
        //types.Add(typeof(RepeatedField<string>));
        //types.Add(typeof(RepeatedField<ByteString>));
        //types.Add(typeof(RepeatedField<object>));
        //types.Add(typeof(RepeatedField<ILType>));
        //types.Add(typeof(RepeatedField<ILTypeInstance>));



        XmlSerializer xmlSerializer = null;
        BinaryFormatter b1 = new BinaryFormatter();
        FileStream b2 = null;
        FileMode f = FileMode.Create;
        types.Clear();
    }


    public HotClassILRuntime CreateHotClass(string className, string nameSpace = "PlatformLobbyCode")
    {
        string classFullName = URLFactory.FormatNameSpace(nameSpace, className);
        return new HotClassILRuntime(classFullName);
    }

    #region CLR Redirection 重定向



    unsafe void RegisterCLRDebugLog(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        var mi = typeof(Debug).GetMethod("Log", new System.Type[] { typeof(object) });
        var my_mi = typeof(Utils).GetMethod("Log", new System.Type[] { typeof(object) });
        appDomain.RegisterCLRMethodRedirection(mi, Log);
        appDomain.RegisterCLRMethodRedirection(my_mi, Log);
    }

    unsafe static StackObject* Log(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    {
        //ILRuntime的调用约定为被调用者清理堆栈，因此执行这个函数后需要将参数从堆栈清理干净，并把返回值放在栈顶，具体请看ILRuntime实现原理文档
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
        StackObject* ptr_of_this_method;
        //这个是最后方法返回后esp栈指针的值，应该返回清理完参数并指向返回值，这里是只需要返回清理完参数的值即可
        StackObject* __ret = ILIntepreter.Minus(__esp, 1);
        //取Log方法的参数，如果有两个参数的话，第一个参数是esp - 2,第二个参数是esp -1, 因为Mono的bug，直接-2值会错误，所以要调用ILIntepreter.Minus
        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);

        //这里是将栈指针上的值转换成object，如果是基础类型可直接通过ptr->Value和ptr->ValueLow访问到值，具体请看ILRuntime实现原理文档
        object message = typeof(object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
        //所有非基础类型都得调用Free来释放托管堆栈
        __intp.Free(ptr_of_this_method);

        //在真实调用Debug.Log前，我们先获取DLL内的堆栈
        var stacktrace = __domain.DebugService.GetStackTrace(__intp);

        //我们在输出信息后面加上DLL堆栈
        Utils.Log(message + "\n" + stacktrace);

        return __ret;
    }

    unsafe void RegisterCLRDebugLogError(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        var mi = typeof(Debug).GetMethod("LogError", new System.Type[] { typeof(object) });
        var my_mi = typeof(Utils).GetMethod("LogError", new System.Type[] { typeof(object) });
        appDomain.RegisterCLRMethodRedirection(mi, LogError);
        appDomain.RegisterCLRMethodRedirection(my_mi, LogError);
    }

    unsafe static StackObject* LogError(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
        StackObject* ptr_of_this_method;

        StackObject* __ret = ILIntepreter.Minus(__esp, 1);

        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);

        object message = typeof(object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));

        __intp.Free(ptr_of_this_method);

        var stacktrace = __domain.DebugService.GetStackTrace(__intp);

        Utils.LogError(message + "\n" + stacktrace);
        return __ret;
    }

    unsafe void RegisterCLRDebugLogWarning(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        var mi = typeof(Debug).GetMethod("LogWarning", new System.Type[] { typeof(object) });
        var my_mi = typeof(Utils).GetMethod("LogWarning", new System.Type[] { typeof(object) });
        appDomain.RegisterCLRMethodRedirection(mi, LogWarning);
        appDomain.RegisterCLRMethodRedirection(my_mi, LogWarning);
    }

    unsafe static StackObject* LogWarning(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
        StackObject* ptr_of_this_method;
        StackObject* __ret = ILIntepreter.Minus(__esp, 1);
        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
        object message = typeof(object).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
        __intp.Free(ptr_of_this_method);
        var stacktrace = __domain.DebugService.GetStackTrace(__intp);
        Utils.LogWarning(message + "\n" + stacktrace);
        return __ret;
    }

    unsafe void RegisterCLRGetCompontent(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        var arr = typeof(GameObject).GetMethods();
        foreach (var i in arr)
        {
            if (i.Name == "GetCompontent" && i.GetGenericArguments().Length == 1)
            {
                appDomain.RegisterCLRMethodRedirection(i, GetCompontent);
            }
        }
    }

    private unsafe StackObject* GetCompontent(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack,
        CLRMethod __method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

        var ptr = __esp - 1;
        GameObject instance = StackObject.ToObject(ptr, __domain, __mStack) as GameObject;
        if (instance == null)
            throw new System.NullReferenceException();

        __intp.Free(ptr);

        var genericArgument = __method.GenericArguments;
        if (genericArgument != null && genericArgument.Length == 1)
        {
            var type = genericArgument[0];
            object res = null;
            if (type is CLRType)
            {
                res = instance.GetComponent(type.TypeForCLR);
            }
            else
            {
                var clrInstances = instance.GetComponents<MonoBehaviourAdapter.Adaptor>();
                foreach (var clrInstance in clrInstances)
                {
                    if (clrInstance.ILInstance != null)
                    {
                        if (clrInstance.ILInstance.Type == type)
                        {
                            res = clrInstance.ILInstance;
                            break;
                        }
                    }
                }
            }

            return ILIntepreter.PushObject(ptr, __mStack, res);
        }

        return __esp;
    }

    unsafe void RegisterCLRAddCompontent(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        var arr = typeof(GameObject).GetMethods();
        foreach (var i in arr)
        {
            if (i.Name == "AddComponent" && i.GetGenericArguments().Length == 1)
            {
                appDomain.RegisterCLRMethodRedirection(i, AddCompontent);
            }
        }
    }

    private unsafe StackObject* AddCompontent(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack,
        CLRMethod __method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

        var ptr = __esp - 1;
        GameObject instance = StackObject.ToObject(ptr, __domain, __mStack) as GameObject;
        if (instance == null)
        {
            throw new System.NullReferenceException();
        }

        __intp.Free(ptr);

        var genericArgument = __method.GenericArguments;
        if (genericArgument != null && genericArgument.Length == 1)
        {
            var type = genericArgument[0];
            object res;
            if (type is CLRType) //CLRType表示这个类型是Unity工程里的类型   //ILType表示是热更dll里面的类型
            {
                //Unity主工程的类，不需要做处理
                res = instance.AddComponent(type.TypeForCLR);
            }
            else
            {
                //创建出来MonoTest
                var ilInstance = new ILTypeInstance(type as ILType, false);
                var clrInstance = instance.AddComponent<MonoBehaviourAdapter.Adaptor>();
                clrInstance.ILInstance = ilInstance;
                clrInstance.AppDomain = __domain;
                //这个实例默认创建的CLRInstance不是通过AddCompontent出来的有效实例，所以要替换
                ilInstance.CLRInstance = clrInstance;

                res = clrInstance.ILInstance;

                //补掉Awake
                clrInstance.Awake();
                clrInstance.OnEnable();
            }

            return ILIntepreter.PushObject(ptr, __mStack, res);
        }

        return __esp;
    }

    unsafe void RegisterCLRBinaryFormatter(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        var method = typeof(BinaryFormatter).GetMethod("Deserialize", new System.Type[] { typeof(Stream) });
        appDomain.RegisterCLRMethodRedirection(method, Deserialize);
    }

    private unsafe StackObject* Deserialize(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack,
       CLRMethod __method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

        StackObject* ptr_of_this_method;
        StackObject* __ret = ILIntepreter.Minus(__esp, 2);


        ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
        BinaryFormatter instance_of_this_method = (BinaryFormatter)typeof(BinaryFormatter).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
        if (instance_of_this_method == null)
        {
            throw new System.NullReferenceException();
        }
        __intp.Free(ptr_of_this_method);

        ptr_of_this_method = ILIntepreter.Minus(__esp, 1); ;
        Stream stream = StackObject.ToObject(ptr_of_this_method, __domain, __mStack) as Stream;
        if (stream == null)
        {
            throw new System.NullReferenceException();
        }
        __intp.Free(ptr_of_this_method);
        object obj = instance_of_this_method.Deserialize(stream);
        return ILIntepreter.PushObject(__ret, __mStack, obj);
    }
    unsafe void RegisterCLRD_Deserialize(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        Type[] args = new Type[] { typeof(string), typeof(Type) };
        BindingFlags flag = BindingFlags.Static | BindingFlags.Public;

        Type jcType = typeof(JsonConvert);
        var c_method = jcType.GetMethod("DeserializeObject", flag, null, args, null);
        appDomain.RegisterCLRMethodRedirection(c_method, D_Deserialize_2);
    }

    private unsafe StackObject* D_Deserialize_1(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack,
       CLRMethod __method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

        StackObject* ptr_of_this_method;
        StackObject* __ret = ILIntepreter.Minus(__esp, 2);

        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
        string str = StackObject.ToObject(ptr_of_this_method, __domain, __mStack) as string;
        if (str == null)
        {
            throw new System.NullReferenceException();
        }
        __intp.Free(ptr_of_this_method);
        object obj = JsonConvert.DeserializeObject(str);
        return ILIntepreter.PushObject(__ret, __mStack, obj);
    }
    private unsafe StackObject* D_Deserialize_2(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack,
       CLRMethod __method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;

        StackObject* ptr_of_this_method;
        StackObject* __ret = ILIntepreter.Minus(__esp, 3);

        ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
        string str = StackObject.ToObject(ptr_of_this_method, __domain, __mStack) as string;
        if (str == null)
        {
            throw new System.NullReferenceException();
        }
        __intp.Free(ptr_of_this_method);

        ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
        var t_param = StackObject.ToObject(ptr_of_this_method, __domain, __mStack);
        if (t_param == null)
        {
            throw new System.NullReferenceException();
        }

        __intp.Free(ptr_of_this_method);
        object obj = null;
        Type t = t_param.GetType();
        if (t.FullName.Equals("ILRuntime.Reflection.ILRuntimeType"))
            obj = JsonConvert.DeserializeObject(str, t);
        else
            obj = JsonConvert.DeserializeObject(str, t);
        return ILIntepreter.PushObject(__ret, __mStack, obj);
    }


    unsafe void RegisterCLRScoketIOGetValue(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        foreach (var i in typeof(SocketIOResponse).GetMethods())
        {
            if (i.Name == "GetValue" && i.IsGenericMethodDefinition)
            {
                var param = i.GetParameters();
                if (param[0].ParameterType == typeof(int))
                {
                    appDomain.RegisterCLRMethodRedirection(i, ScoketIO_SocketIOResponse_GetValue);
                }
            }
        }
    }

    unsafe StackObject* ScoketIO_SocketIOResponse_GetValue(ILIntepreter intp, StackObject* esp, IList<object> mStack, CLRMethod method, bool isNewObj)
    {
        ILRuntime.Runtime.Enviorment.AppDomain __domain = intp.AppDomain;
        StackObject* ptr_of_this_method;
        StackObject* __ret = ILIntepreter.Minus(esp, 2);

        ptr_of_this_method = ILIntepreter.Minus(esp, 1);
        System.Int32 index = (System.Int32)typeof(System.Int32).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, mStack));
        intp.Free(ptr_of_this_method);

        ptr_of_this_method = ILIntepreter.Minus(esp, 2);
        SocketIOResponse instance_of_this_method = (SocketIOResponse)typeof(SocketIOResponse).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, mStack));
        intp.Free(ptr_of_this_method);

        var type = method.GenericArguments[0].ReflectionType;
        try
        {
            object result_of_this_method;
            if (type.Equals(typeof(double)))
                result_of_this_method = instance_of_this_method.GetValue<double>(index);
            else if (type.Equals(typeof(float)))
                result_of_this_method = instance_of_this_method.GetValue<float>(index);
            else if (type.Equals(typeof(long)))
                result_of_this_method = instance_of_this_method.GetValue<long>(index);
            else if (type.Equals(typeof(int)))
                result_of_this_method = instance_of_this_method.GetValue<int>(index);
            else if (type.Equals(typeof(string)))
                result_of_this_method = instance_of_this_method.GetValue<string>(index);
            else if (type.Equals(typeof(bool)))
                result_of_this_method = instance_of_this_method.GetValue<bool>(index);
            else if (type.Equals(typeof(byte)))
                result_of_this_method = instance_of_this_method.GetValue<byte>(index);
            else if (type.Equals(typeof(char)))
                result_of_this_method = instance_of_this_method.GetValue<char>(index);
            else if (type.Equals(typeof(decimal)))
                result_of_this_method = instance_of_this_method.GetValue<decimal>(index);
            else
            {
                result_of_this_method = JsonMapper.ReadValue(type, new LitJson.JsonReader(instance_of_this_method.GetValue(index).GetRawText()));
            }
            return ILIntepreter.PushObject(__ret, mStack, result_of_this_method);
        }
        catch (Exception e)
        {
            var stacktrace = __domain.DebugService.GetStackTrace(intp);
            Utils.LogError("ScoketIO_SocketIOResponse_GetValue 错误：" + e.ToString() + "\n" + stacktrace);
            return null;
        }
    }

    #endregion

}
