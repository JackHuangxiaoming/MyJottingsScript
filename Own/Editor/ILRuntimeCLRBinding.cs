#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Adaptors;
using FairyGUI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
//using com.unity.cloudbase;
using CloudBase;
using System.Threading;

[System.Reflection.Obfuscation(Exclude = true)]
public class ILRuntimeCLRBinding
{
    public static string filename = "PlatformLobbyCode";

    //自己根据热更里面使用的类型进行自主添加的绑定
    //[MenuItem("ILRuntime/Generate CLR Binding Code by Analysis")]
    public static void GenerateCLRBindingByAnalysis()
    {
        //用新的分析热更dll调用引用来生成绑定代码
        ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
        string dllPath = Path.Combine(URLFactory.DownLoadHotFilePath, "PlatformLobbyCode/" + filename + ".dll");
        using (System.IO.FileStream fs = new System.IO.FileStream(dllPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
        {
            domain.LoadAssembly(fs);

            //Crossbind Adapter is needed to generate the correct binding code
            //HotScriptManager.Instance.UsedOnlyForAOTCodeGeneration(domain);
            HotScriptManager.Instance.RegistDelegate(domain);
            HotScriptManager.Instance.RegistCLR(domain);
            ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, "Assets/Scripts/Generated/CoreGenerated");
        }

        GenerateCLRBindingByCode();
        AssetDatabase.Refresh();
    }
    //[MenuItem("ILRuntime/Generate Cross Binding Adapter")]
    static void GenerateCrossbindAdapter()
    {
        //由于跨域继承特殊性太多，自动生成无法实现完全无副作用生成，所以这里提供的代码自动生成主要是给大家生成个初始模版，简化大家的工作
        //大多数情况直接使用自动生成的模版即可，如果遇到问题可以手动去修改生成后的文件，因此这里需要大家自行处理是否覆盖的问题

        //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("Assets/Plugins/ILRuntime/Runtime/Adaptors/CoroutineAdapter.cs"))
        //{
        //    sw.WriteLine(ILRuntime.Runtime.Enviorment.CrossBindingCodeGenerator.GenerateCrossBindingAdapterCode(typeof(CoroutineAdapter), "ILRuntime.Runtime.Adaptors"));
        //}
        //AssetDatabase.Refresh();
    }

    //[MenuItem("ILRuntime/Generate CLR Binding Type Code By Subjoin")]
    static void GenerateCLRBindingByCode()
    {
        //CLR Binding 绑定
        List<Type> types = new List<Type>();
        types.Add(typeof(DownLoadCombin));
        types.Add(typeof(GComboBox));
        types.Add(typeof(GearAnimation));
        types.Add(typeof(GearBase));
        types.Add(typeof(GearColor));
        types.Add(typeof(GGroup));
        //types.Add(typeof(GLoader3D));
        types.Add(typeof(GMovieClip));
        //types.Add(typeof(GoWrapper));
        types.Add(typeof(GRichTextField));
        types.Add(typeof(GSlider));
        types.Add(typeof(GTween));
        types.Add(typeof(GTweener));
        //types.Add(typeof(NTexture));
        types.Add(typeof(Stage));
        types.Add(typeof(UIPanel));
        types.Add(typeof(WaitForSeconds));
        types.Add(typeof(Shader));
        types.Add(typeof(RectTransform));
        types.Add(typeof(Quaternion));
        //types.Add(typeof(ParticleSystem));
        types.Add(typeof(LogType));
        //types.Add(typeof(Color32));
        types.Add(typeof(short));
        types.Add(typeof(IComparable));
        types.Add(typeof(IComparer));
        types.Add(typeof(EventArgs));
        types.Add(typeof(IEnumerator<object>));
        types.Add(typeof(List<long>));
        types.Add(typeof(List<object>));
        //types.Add(typeof(List<float>));
        //types.Add(typeof(List<Transform>));
        //types.Add(typeof(List<Vector2>));
        //types.Add(typeof(List<Vector3>));
        //==================================
        //types.Add(typeof(int));        
        //types.Add(typeof(long));
        //types.Add(typeof(object));
        //types.Add(typeof(string));
        //types.Add(typeof(Array));
        //types.Add(typeof(Vector2));
        //types.Add(typeof(Vector3));
        //types.Add(typeof(Quaternion));
        //types.Add(typeof(GameObject));
        //types.Add(typeof(UnityEngine.Object));
        //types.Add(typeof(Transform));
        //types.Add(typeof(Time));
        //types.Add(typeof(Debug));
        //types.Add(typeof(bool));   
        //types.Add(typeof(Hashtable));
        //types.Add(typeof(List<Hashtable>));
        //types.Add(typeof(List<int>));
        //types.Add(typeof(Application));
        //types.Add(typeof(AsyncOperation));        
        //types.Add(typeof(Color));        
        //types.Add(typeof(IEnumerator));
        //types.Add(typeof(IEnumerator<object>));
        //types.Add(typeof(IDisposable));        
        //types.Add(typeof(WaitForSeconds));
        //types.Add(typeof(Shader));
        //types.Add(typeof(Enum));
        //types.Add(typeof(EventContext));
        //types.Add(typeof(GButton));
        //types.Add(typeof(GComponent));
        //types.Add(typeof(GGraph));
        //types.Add(typeof(GImage));
        //types.Add(typeof(GLabel));
        //types.Add(typeof(GList));
        //types.Add(typeof(GLoader));
        //types.Add(typeof(GObject));
        //types.Add(typeof(GProgressBar));
        //types.Add(typeof(GRoot));
        //types.Add(typeof(GScrollBar));
        //types.Add(typeof(GTextField));
        //types.Add(typeof(GTextInput));
        //types.Add(typeof(ScrollPane));
        //types.Add(typeof(Transition));
        //types.Add(typeof(RectTransform));
        //types.Add(typeof(ParticleSystem));
        //types.Add(typeof(LogType));
        //types.Add(typeof(Color32));
        //types.Add(typeof(short));
        //types.Add(typeof(EventArgs));
        //types.Add(typeof(Timers));
        //types.Add(typeof(Utils));
        //types.Add(typeof(CloudBaseApp));
        //types.Add(typeof(FunctionResponse));
        //types.Add(typeof(RigidbodyAndBoxCollider));
        //types.Add(typeof(RigidbodyAndBoxColliderByMap));

        //所有DLL内的类型的真实C#类型都是ILTypeInstance
        types.Add(typeof(ILRuntime.Runtime.Intepreter.ILTypeInstance));

        ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(types, "Assets/Scripts/Generated/TypeGenerated");
        AssetDatabase.Refresh();
    }
}
#endif
