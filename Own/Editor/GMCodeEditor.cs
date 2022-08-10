using CloudBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

class GMCodeEditor
{
    [MenuItem("Tools/GM命令")]
    private static void OpenAssetBundleWindow()
    {
        GameGMCodeWindow ABEditor = EditorWindow.GetWindow<GameGMCodeWindow>();
    }

    class GameGMCodeWindow : EditorWindow
    {
        private string gmString = @"{0} {1}";
        private string msg = "";
        private string code = "";
        private string param = "";

        GUIStyle titleStyle1;
        private void OnEnable()
        {
            titleContent.text = "GM命令工具";
            ShowUtility();

            titleStyle1 = new GUIStyle();
            titleStyle1.fontSize = 20;
            //titleStyle1.normal.textColor = new Color(46f / 256f, 163f / 256f, 256f / 256f, 256f / 256f);
            titleStyle1.normal.textColor = new Color(1, 0, 0, 1);
            titleStyle1.fontStyle = FontStyle.Italic;
            titleStyle1.fontSize = 13;
        }
        private void OnGUI()
        {
            TitleUI();
            ContentUI();
            ExecuteUI();
        }

        private void ContentUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("GMCode:", EditorStyles.boldLabel);
            code = GUILayout.TextField(code, GUILayout.MinWidth(200));
            //GUILayout.FlexibleSpace();	//布局之间左右对齐
            GUILayout.Label("Param:", EditorStyles.boldLabel);
            param = GUILayout.TextField(param, GUILayout.MinWidth(200));

            GUILayout.BeginHorizontal();
            GUILayout.Label("执行预览:", EditorStyles.boldLabel);
            msg = string.Format(gmString, code, param);
            GUILayout.Label(msg, titleStyle1);
            GUILayout.FlexibleSpace();	//布局之间左右对齐
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        private void ExecuteUI()
        {
            if (GUILayout.Button("开始执行"))
            {
                if (string.IsNullOrEmpty(code))
                {
                    EditorUtility.DisplayDialog("GM命令工具", "请输入GM 命令", "确认");
                }
                else
                {
                    if (!Application.isPlaying)
                    {
                        EditorUtility.DisplayDialog("GM命令工具", "请请请请请\n请请请请请请\n请请请请请请请\n运行游戏后在执行GM命令", "确认");
                    }
                    else
                    {
                        if (EditorUtility.DisplayDialog("GM命令工具", string.Format("是否执行命令：[{0}]", msg), "确认", "取消"))
                        {
                            try
                            {
                                RequestGM(msg);
                            }
                            catch (Exception e)
                            {
                                Utils.LogError("GM 云函数执行失败：" + e.Message);
                            }
                        }
                    }
                }
            }
        }

        void TitleUI()
        {
            GUILayout.BeginVertical();
            GUILayout.Label("提示：这个GM工具需要在游戏登录后使用！！！", EditorStyles.boldLabel);
            GUILayout.Label("提示：这个GM工具需要在游戏登录后使用！！！", EditorStyles.boldLabel);
            GUILayout.Label("提示：这个GM工具需要在游戏登录后使用！！！", EditorStyles.boldLabel);
            GUILayout.Label("提示：这个GM工具需要在游戏登录后使用！！！", EditorStyles.boldLabel);
            GUILayout.Label("提示：这个GM工具需要在游戏登录后使用！！！", EditorStyles.boldLabel);
            GUILayout.Label("提示：这个GM工具需要在游戏登录后使用！！！", EditorStyles.boldLabel);
            GUILayout.EndVertical();
        }
        /// <summary>
        /// GM 工具
        /// </summary>
        /// <param name="paramString">使用类 GM.Instance.GetXXXParam()</param>        
        public void RequestGM(string paramString)
        {
            var param = new Dictionary<string, object>();
            param.Add("playerUUID", UIManager.GUID);
            param.Add("code", paramString);
            Utils.StartDoCoroutine(SendMsgByCloudFunctionByEnumerator("GMCode", param, null));
        }
        private static IEnumerator SendMsgByCloudFunctionByEnumerator(string functionName, object param, Action<String> hander)
        {
            Task<FunctionResponse> res = NetManager.App.Function.CallFunctionAsync(functionName, param);
            yield return res;

            while (!res.IsCompleted)
            {
                yield return 0;
            }
            if (res.IsFaulted || res.IsCanceled)
            {
                Utils.Log("云函数" + functionName + ":执行失败");
                if (hander != null)
                {
                    hander(string.Empty);
                }
                res.Dispose();
                yield break;
            }
            FunctionResponse fr = res.Result;
            if (fr.Code == null)
            {
                if (hander != null && fr.Data != null)
                    hander(fr.Data.ToString());
                else
                    Utils.Log(functionName + "调用成功,但返回的数据 或者 回调函数是空的");
            }
            else
            {
                switch (fr.Code)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    default:
                        Utils.Log("云函数" + functionName + ":返回错误：" + fr.Message);
                        break;
                }
                if (hander != null)
                {
                    hander(string.Empty);
                }
            }
            res.Dispose();
        }
    }
}