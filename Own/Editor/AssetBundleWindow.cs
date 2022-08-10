using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEditor;
using UnityEngine;
using static AssetInfo;

public class AssetBundleWindow : EditorWindow
{
    //标记，用于标记当前选中的AB包索引
    private int _currentAB = -1;
    private int _currentABAsset = -1;

    //是否隐藏无效资源
    private bool _hideInvalidAsset = false;
    //是否隐藏已绑定资源
    private bool _hideBundleAsset = false;
    //打包路径
    private string _buildPath;
    //打包平台
    private BuildTarget _buildTarget = BuildTarget.Android;


    //区域视图的范围
    private Rect _ABViewRect;
    //区域视图滚动的范围
    private Rect _ABScrollRect;
    //区域视图滚动的位置
    private Vector2 _ABScroll;
    //区域高度标记，这里不用管它，是后续用来控制视图滚动量的
    private int _ABViewHeight = 0;


    //区域视图的范围
    private Rect _currentABViewRect;
    //区域视图滚动的范围
    private Rect _currentABScrollRect;
    //区域视图滚动的位置
    private Vector2 _currentABScroll;
    //区域高度标记，这里不用管它，是后续用来控制视图滚动量的
    private int _currentABViewHeight = 0;


    //区域视图的范围
    private Rect _assetViewRect;
    //区域视图滚动的范围
    private Rect _assetScrollRect;
    //区域视图滚动的位置
    private Vector2 _assetScroll;
    //区域高度标记，这里不用管它，是后续用来控制视图滚动量的
    private int _assetViewHeight = 0;

    AssetInfo _asset = null;
    private AssetBundleInfo _assetBundle = new AssetBundleInfo();

    private bool _isRename = false;
    private string _renameValue = "";

    private void OnEnable()
    {
        _buildPath = EditorPrefs.GetString("buildAssetBundlePaht");
        if (string.IsNullOrEmpty(_buildPath))
        {
            _buildPath = Path.Combine(Application.persistentDataPath, "AllPackage");
        }
        titleContent.text = "AssetBundles";
        Init();
        Show();
    }
    private void OnDestroy()
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();
    }
    private void Init()
    {
        string stylePrePath = string.Format("{0}/CreatAssetBundle/", Application.dataPath);
        BuildAssetBundles.Pack(stylePrePath + "PlatformLobby/DataTable", "DataTable");

        //以Assets目录创建根对象
        _asset = new AssetInfo(Application.dataPath + "/CreatAssetBundle", "CreatAssetBundle", true);
        //从根对象开始，读取所有文件创建子对象
        EditorTool.ReadAssetsInChildren(_asset);

        Resources.UnloadUnusedAssets();
    }
    private GUIStyle _preButton;
    private GUIStyle _preDropDown;
    private GUIStyle _LRSelect;
    private GUIStyle _prefabLabel;
    private GUIStyle _miniButtonLeft;
    private GUIStyle _miniButtonRight;
    private GUIStyle _box;
    private GUIStyle _oLMinus;

    public void SelectDirctory()
    {
        string path = UnityEditor.EditorUtility.OpenFolderPanel("Select Bundle Save Path", UnityEngine.Application.persistentDataPath, "");

        if (!string.IsNullOrEmpty(path))
        {
            _buildPath = path;
            EditorPrefs.SetString("buildAssetBundlePaht", _buildPath);
        }
    }
    public static void OpenDirectory(string path)
    {
        if (string.IsNullOrEmpty(path)) return;

        path = path.Replace("/", "\\");
        if (!Directory.Exists(path))
        {
            UnityEngine.Debug.LogError("No Directory: " + path);
            return;
        }
        System.Diagnostics.Process process = System.Diagnostics.Process.Start("explorer.exe", path);
    }

    public static void OpenCMDDirectory(string path)
    {
        //OpenDirectory(path);
        // 新开线程防止锁死
        Thread newThread = new Thread(new ParameterizedThreadStart(CmdOpenDirectory));
        newThread.Start(path);
    }

    private static void CmdOpenDirectory(object obj)
    {
        Process p = new Process();
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.Arguments = "/c start " + obj.ToString();
        UnityEngine.Debug.Log(p.StartInfo.Arguments);
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.CreateNoWindow = true;
        p.Start();

        p.WaitForExit();
        p.Close();
    }

    private void OnGUI()
    {
        _preButton = new GUIStyle("PreButton");
        _prefabLabel = new GUIStyle("PreLabel");
        _preDropDown = new GUIStyle("PreDropDown");
        _LRSelect = new GUIStyle("LODSliderRangeSelected");
        _miniButtonLeft = new GUIStyle("miniButtonLeft");
        _miniButtonRight = new GUIStyle("miniButtonRight");
        _box = new GUIStyle("Box");
        _oLMinus = new GUIStyle("OL Minus");
        TitleGUI();
        AssetBundlesGUI();
        CurrentAssetBundlesGUI();
        AssetsGUI();
    }

    private void AssetsGUI()
    {
        //区域的视图范围：左上角位置固定，宽度为窗口宽度减去左边的区域宽度以及一些空隙（255），高度为窗口高度减去上方两层标题栏以及一些空隙（50）
        _assetViewRect = new Rect(250, 45, (int)position.width - 255, (int)position.height - 50);
        _assetScrollRect = new Rect(250, 45, (int)position.width - 255, _assetViewHeight);


        _assetScroll = GUI.BeginScrollView(_assetViewRect, _assetScroll, _assetScrollRect);
        GUI.BeginGroup(_assetScrollRect, _box);
        _assetViewHeight = 0;
        AssetGUI(_asset, 0);

        if (_assetViewHeight < _assetViewRect.height)
        {
            _assetViewHeight = (int)_assetViewRect.height;
        }

        GUI.EndGroup();

        GUI.EndScrollView();
    }

    private void AssetGUI(AssetInfo asset, int indentation)
    {
        if (_hideBundleAsset && !string.IsNullOrEmpty(asset.Bundled))
        {
            if (asset.AssetFileType != FileType.Folder)
            {
                return;
            }
        }
        if (_hideInvalidAsset)
        {

        }
        //开启一行
        GUILayout.BeginHorizontal();
        //以空格缩进
        GUILayout.Space(indentation * 20 + 5);
        //这个资源是文件夹
        if (asset.AssetFileType == FileType.Folder)
        {
            //画一个勾选框
            if (GUILayout.Toggle(asset.IsChecked, "", GUILayout.Width(20)) != asset.IsChecked)
            {
            }
            //获取系统中的文件夹图标
            GUIContent content = EditorGUIUtility.IconContent("Folder Icon");
            content.text = asset.AssetName;
            //创建一个上下文菜单按钮
            asset.IsExpanding = EditorGUILayout.Foldout(asset.IsExpanding, content);
        }
        //否则是文件
        else
        {
            //判断是否禁用
            GUI.enabled = !(asset.AssetFileType == FileType.InValidFile || asset.Bundled != "");
            //画一个勾选框
            if (GUILayout.Toggle(asset.IsChecked, "", GUILayout.Width(20)) != asset.IsChecked)
            {
                asset.IsChecked = !asset.IsChecked;
            }
            //缩进单位10的长度，为了抵消文件夹前面的上下文菜单按钮
            GUILayout.Space(10);
            //根据对象的类型获取他的图标样式
            GUIContent content = EditorGUIUtility.ObjectContent(null, asset.AssetType);
            content.text = asset.AssetName;
            //展示这个对象，以Label控件
            GUILayout.Label(content, GUILayout.Height(20));
            //如果此对象绑定有AB包，就显示这个AB包的名称
            if (asset.Bundled != "")
            {
                //获取系统中的文件夹图标
                GUIContent preC = EditorGUIUtility.IconContent("Prefab Icon");
                preC.text = "[" + asset.Bundled + "]";
                if (_currentAB != -1)
                {
                    GUI.enabled = _assetBundle.AssetBundles[_currentAB].Name == asset.Bundled;
                }
                GUILayout.Label(preC, _prefabLabel);
            }
            GUI.enabled = true;
        }
        //每一行的高度20，让高度累加
        _assetViewHeight += 22;
        GUILayout.FlexibleSpace();
        //结束一行
        GUILayout.EndHorizontal();

        //如果当前文件夹是展开的，换一行进行深层遍历其子对象，且缩进等级加1
        if (asset.IsExpanding)
        {
            for (int i = 0; i < asset.ChildAssetInfo.Count; i++)
            {
                AssetGUI(asset.ChildAssetInfo[i], indentation + 1);
            }
        }
    }

    private void CurrentAssetBundlesGUI()
    {
        _currentABViewRect = new Rect(5, (int)position.height / 2 + 10, 240, (int)position.height / 2 - 15);
        _currentABScrollRect = new Rect(5, (int)position.height / 2 + 10, 240, _currentABViewHeight);
        _currentABScroll = GUI.BeginScrollView(_currentABViewRect, _currentABScroll, _currentABScrollRect);
        GUI.BeginGroup(_currentABScrollRect, _box);

        _currentABViewHeight = 5;
        //这里是我们加入的内容，判断条件：只有当前选中任意AB包对象后，此区域的内容才生效（_currentAB != -1）
        if (_currentAB != -1)
        {
            //遍历build.Assets（当前选中的AB包对象的资源列表）
            AssetBundleBuildInfo build = _assetBundle.AssetBundles[_currentAB];
            for (int i = 0; i < build.Assets.Count; i++)
            {
                //同理，遍历到当前选中的资源对象，在原地画蓝色高亮背景框
                if (_currentABAsset == i)
                {
                    GUI.Box(new Rect(0, _currentABViewHeight, 240, 15), "", _LRSelect);
                }
                //原地绘制Button控件，当点击时，此资源对象被选中
                GUIContent content = EditorGUIUtility.ObjectContent(null, build.Assets[i].AssetType);
                content.text = build.Assets[i].AssetName;
                if (GUI.Button(new Rect(5, _currentABViewHeight, 205, 15), content, _prefabLabel))
                {
                    _currentABAsset = i;
                }
                //在Button控件右方绘制减号Button控件，当点击时，删除此资源对象在当前选中的AB包中
                if (GUI.Button(new Rect(215, _currentABViewHeight, 20, 15), "", _oLMinus))
                {
                    build.RemoveAsset(build.Assets[i]);
                    _currentABAsset = -1;
                }
                _currentABViewHeight += 20;
            }
        }

        _currentABViewHeight += 5;
        if (_currentABViewHeight < _currentABViewRect.height)
        {
            _currentABViewHeight = (int)_currentABViewRect.height;
        }

        GUI.EndGroup();
        GUI.EndScrollView();
    }

    private void AssetBundlesGUI()
    {
        _ABViewRect = new Rect(5, 25, 240, (int)position.height / 2 - 20);
        _ABScrollRect = new Rect(5, 25, 240, _ABViewHeight);
        _ABScroll = GUI.BeginScrollView(_ABViewRect, _ABScroll, _ABScrollRect);
        GUI.BeginGroup(_ABScrollRect, _box);

        _ABViewHeight = 5;

        //整个for循环是我们新加的内容，遍历_assetBundle.AssetBundles将每个AB包创建为一个按钮
        for (int i = 0; i < _assetBundle.AssetBundles.Count; i++)
        {
            //判断当前AB包对象中是否存有资源（也即是判断是否为空AB包）
            string icon = _assetBundle.AssetBundles[i].Assets.Count > 0 ? "Prefab Icon" : "GameObject Icon";
            //遍历到当前选中的AB包对象
            if (_currentAB == i)
            {
                //在对象位置绘制蓝色背景框（也即是被选中的高亮效果，_LRSelect样式有这种效果）
                GUI.Box(new Rect(0, _ABViewHeight, 240, 17), "", _LRSelect);
                //是否正在进行重命名操作
                if (_isRename)
                {
                    //重命名操作时，在原地绘制编辑框TextField，并消去AB包名字（content.text = ""）
                    GUIContent content = EditorGUIUtility.IconContent(icon);
                    content.text = "";

                    GUI.Label(new Rect(5, _ABViewHeight, 230, 17), content, _prefabLabel);
                    GUI.SetNextControlName("_renameTextField");
                    _renameValue = GUI.TextField(new Rect(40, _ABViewHeight, 140, 17), _renameValue);
                    GUI.FocusControl("_renameTextField");
                    //重命名OK
                    if (GUI.Button(new Rect(180, _ABViewHeight, 30, 17), "OK", _miniButtonLeft))
                    {
                        if (_renameValue != "")
                        {
                            if (!_assetBundle.IsExistName(_renameValue))
                            {
                                _assetBundle.AssetBundles[_currentAB].RenameAssetBundle(_renameValue);
                                _renameValue = "";
                                _isRename = false;
                            }
                            else
                            {
                                UnityEngine.Debug.LogError("Already existed name:" + _renameValue);
                            }
                        }
                    }
                    //重命名NO
                    if (GUI.Button(new Rect(210, _ABViewHeight, 30, 17), "NO", _miniButtonRight))
                    {
                        _isRename = false;
                    }
                }
                //未进行重命名操作，在原地绘制为不可编辑的Label控件
                else
                {
                    GUIContent content = EditorGUIUtility.IconContent(icon);
                    content.text = _assetBundle.AssetBundles[i].Name;
                    GUI.Label(new Rect(5, _ABViewHeight, 230, 15), content, _prefabLabel);
                }
            }
            //遍历到非选中的AB包对象
            else
            {
                //在原地绘制Button控件，当被点击时此AB包对象被选中
                GUIContent content = EditorGUIUtility.IconContent(icon);
                content.text = _assetBundle.AssetBundles[i].Name;
                if (GUI.Button(new Rect(5, _ABViewHeight, 230, 15), content, _prefabLabel))
                {
                    _currentAB = i;
                    _currentABAsset = -1;
                    _isRename = false;
                }
            }
            _ABViewHeight += 20;
        }

        _ABViewHeight += 5;
        if (_ABViewHeight < _ABViewRect.height)
        {
            _ABViewHeight = (int)_ABViewRect.height;
        }

        GUI.EndGroup();
        GUI.EndScrollView();
    }

    private void TitleGUI()
    {
        if (GUI.Button(new Rect(5, 5, 60, 15), "Create", _preButton))
        {
            //新建一个AB包对象，并将之加入到AB包配置信息对象中
            AssetBundleBuildInfo build = new AssetBundleBuildInfo("ab" + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
            _assetBundle.AssetBundles.Add(build);
        }
        //当前未选中任一AB包的话，禁用之后的所有UI控件
        GUI.enabled = _currentAB == -1 ? false : true;
        if (GUI.Button(new Rect(65, 5, 60, 15), "Rename", _preButton))
        {
            _isRename = true;
        }
        if (GUI.Button(new Rect(125, 5, 60, 15), "Clear", _preButton))
        {
            if (EditorUtility.DisplayDialog("Prompt", "Clear " + _assetBundle.AssetBundles[_currentAB].Name + " ？", "Yes", "No"))
            {
                _assetBundle.AssetBundles[_currentAB].ClearAsset();
            }
        }
        if (GUI.Button(new Rect(185, 5, 60, 15), "Delete", _preButton))
        {
            if (EditorUtility.DisplayDialog("Prompt", "Delete " + _assetBundle.AssetBundles[_currentAB].Name + "？This will clear all assets！", "Yes", "No"))
            {
                _assetBundle.DeleteAssetBundle(_currentAB);
                _currentAB = -1;
            }
        }
        if (GUI.Button(new Rect(250, 5, 100, 15), "Add Assets", _preButton))
        {
            List<AssetInfo> assets = _asset.GetCheckedAssets();
            for (int i = 0; i < assets.Count; i++)
            {
                _assetBundle.AssetBundles[_currentAB].AddAsset(assets[i]);
            }
        }
        //取消UI控件的禁用
        GUI.enabled = true;

        _hideInvalidAsset = GUI.Toggle(new Rect(360, 5, 100, 15), _hideInvalidAsset, "Hide Invalid");
        _hideBundleAsset = GUI.Toggle(new Rect(460, 5, 100, 15), _hideBundleAsset, "Hide Bundled");

        if (GUI.Button(new Rect(250, 25, 60, 15), "Open", _preButton))
        {
            OpenCMDDirectory(string.IsNullOrEmpty(_buildPath) ? "C:\\" : _buildPath);
        }
        if (GUI.Button(new Rect(310, 25, 60, 15), "Browse", _preButton))
        {
            SelectDirctory();
        }

        GUI.Label(new Rect(370, 25, 70, 15), "Build Path:");
        _buildPath = GUI.TextField(new Rect(440, 25, 300, 15), _buildPath);

        BuildTarget buildTarget = (BuildTarget)EditorGUI.EnumPopup(new Rect((int)position.width - 205, 5, 150, 15), _buildTarget, _preDropDown);
        if (buildTarget != _buildTarget)
        {
            _buildTarget = buildTarget;
            EditorPrefs.SetInt("BuildTarget", (int)_buildTarget);
        }

        if (GUI.Button(new Rect((int)position.width - 55, 5, 50, 15), "Build", _preButton))
        {
            EditorTool.Build(_asset, _buildPath);
            Close();
        }
    }
}
