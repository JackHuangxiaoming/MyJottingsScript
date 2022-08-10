using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class ChangeShader : EditorWindow
{
    [MenuItem("Assets/*Tool*/Change Shader")]
    public static void ShowWindow()
    {
        EditorWindow editorWindow = GetWindow(typeof(ChangeShader));
        editorWindow.autoRepaintOnSceneChange = false;
    }

    private void OnEnable()
    {
        allMat = new HashSet<Material>();
        foreach (Material item in Selection.GetFiltered<Material>(SelectionMode.DeepAssets))
        {
            allMat.Add(item);
        }
        currentShader = new List<Shader>(shaderCount);
        changeShader = new List<Shader>(shaderCount);
        for (int i = 0; i < shaderCount; i++)
        {
            currentShader.Add(null);
            changeShader.Add(null);
        }
    }
    public int shaderCount = 5;
    //当前 shader
    public List<Shader> currentShader;
    //⽬标 shader
    public List<Shader> changeShader;
    public HashSet<Material> allMat;
    public HashSet<Material> mat = new HashSet<Material>();
    private void OnGUI()
    {
        for (int i = 0; i < shaderCount; i++)
        {
            currentShader[i] = EditorGUILayout.ObjectField("查找 shader", currentShader[i], typeof(Shader), false) as Shader;
            changeShader[i] = EditorGUILayout.ObjectField("替换 shader", changeShader[i], typeof(Shader), false) as Shader;
        }
        if (GUILayout.Button("Change"))
        {
            if (allMat.Count == 0)
            {
                EditorUtility.DisplayDialog("警告", "选中资源无可替换材质", "知道了");
                return;
            }
            if (EditorUtility.DisplayDialog("警告", $"是否开始替换【{AssetDatabase.GetAssetPath(allMat.First())}】区域的 Shader?", "是"))
            {
                Change();
            }
        }
        if (mat.Count > 0)
        {
            //界⾯中显⽰被替换的材质球
            EditorGUILayout.LabelField("替换掉的材质：");
            foreach (var material in mat)
            {
                if (material != null)
                    EditorGUILayout.ObjectField(material, typeof(Material), false);
            }
            if (GUILayout.Button("Clear"))
                mat.Clear();
        }
    }

    public void Change()
    {
        mat.Clear();//加载的路径需要加后缀名
        if (currentShader == null || changeShader == null)
        {
            return;
        }
        return;
        var allfiles = Directory.GetFiles("Assets/", "*.mat", SearchOption.AllDirectories);
        var length = allfiles.Length;
        var count = 0;
        for (var i = 0; i < length; i++)
        {
            var file = allfiles[i];
            //显⽰替换进度

            if (EditorUtility.DisplayCancelableProgressBar("Changing", file, i / (float)length))
            {
                break;
            }
            //
            Material tempMat = AssetDatabase.LoadAssetAtPath<Material>(file);
            if (currentShader.Contains(tempMat.shader))
            {
                tempMat.shader = changeShader[currentShader.IndexOf(tempMat.shader)];
                mat.Add(tempMat);
                // Debug.Log($"Change {tempMat.name} from {currentShader.name} to {changeShader.name}");
                count++;
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
        EditorUtility.DisplayDialog("⼀共修改:" + count + "个材质", "就绪", "OK");
    }
}