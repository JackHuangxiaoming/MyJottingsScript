using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

class AssetCheckEditor
{
    [MenuItem("Tools/Assets/BundleAtlasOptimize")]
    public static void BundleAtlasOptimize()
    {
        BundleAtlasOptimizeWindow window = EditorWindow.GetWindow<BundleAtlasOptimizeWindow>();
    }
    [MenuItem("Tools/AssetBundle Editor %#O")]
    private static void OpenAssetBundleWindow()
    {
        AssetBundleWindow ABEditor = EditorWindow.GetWindow<AssetBundleWindow>();
    }
    [MenuItem("Tools/CreateAssetList")]
    private static void CreateAssetList()
    {
        AssetInfo _assets = new AssetInfo(Path.Combine(Application.dataPath, "StreamingAssets/jzsh"), "Assets", true);
        //从根对象开始，读取所有文件创建子对象
        EditorTool.ReadAssetsInChildren(_assets);

        List<string> assetsList = new List<string>();
        CheckAssetsList(_assets, assetsList);

        string assetsListPath = Path.Combine(Application.dataPath, "StreamingAssets/AssetsList.txt");
        if (File.Exists(assetsListPath))
            File.Delete(assetsListPath);
        try
        {
            using (StreamWriter stream = File.CreateText(assetsListPath))
            {
                foreach (string item in assetsList)
                {
                    stream.WriteLine(item);
                }
            }
        }
        catch (Exception)
        {
            Utils.LogError("创建资源文件失败");
        }
        Utils.LogError("创建资源文件列表成功");
    }

    private static void CheckAssetsList(AssetInfo _assets, List<string> assetsList)
    {
        AssetInfo info;
        int index;
        for (int i = 0; i < _assets.ChildAssetInfo.Count; i++)
        {
            info = _assets.ChildAssetInfo[i];
            if (info.AssetFileType == AssetInfo.FileType.Folder)
            {
                CheckAssetsList(info, assetsList);
            }
            else if (info.AssetFileType == AssetInfo.FileType.ValidFile)
            {
                index = info.AssetFullPath.LastIndexOf("jzsh\\");
                assetsList.Add(info.AssetFullPath.Substring(index));
            }
        }
    }

    [MenuItem("Tools/Assets/EffectsShaderReplace")]
    private static void EffectsShaderOptimize()
    {
        //AssetInfo _assets = new AssetInfo(Application.dataPath, "GTX", "prefab");
        //以Assets目录创建根对象
        AssetInfo _assets = new AssetInfo(Application.dataPath + "/CreatAssetBundle/ParticleEffects/TX", "Assets", true);
        //从根对象开始，读取所有文件创建子对象
        EditorTool.ReadAssetsInChildren(_assets);

        if (EditorUtility.DisplayDialog("警告", "是否替换 TX Shader?", "是"))
        {
            HashSet<string> shaderNames = new HashSet<string>();
            ReplaceEffectShader(_assets, shaderNames);
            foreach (var name in shaderNames)
            {
                Debug.Log("全部特效使用中的Shader: " + name);
            }
        }
    }
    private static void ReplaceEffectShader(AssetInfo _assets, HashSet<string> shaderNames)
    {
        AssetInfo info;
        GameObject obj;
        HashSet<Material> maters = new HashSet<Material>();
        string shaderName;
        Dictionary<string, string> shaderReplaceNames = new Dictionary<string, string>()
        {
            {"Legacy Shaders/Particles/Alpha Blended","Mobile/Particles/MyAlphaBlended" },
            {"Legacy Shaders/Particles/Additive","Mobile/Particles/MyAdditive" },
            {"Mobile/Particles/Additive","Mobile/Particles/MyAdditive" },
            {"Mobile/Particles/Alpha Blended","Mobile/Particles/MyAlphaBlended" },
        };
        for (int i = 0; i < _assets.ChildAssetInfo.Count; i++)
        {
            info = _assets.ChildAssetInfo[i];
            if (info.AssetFileType == AssetInfo.FileType.Folder)
            {
                ReplaceEffectShader(info, shaderNames);
            }
            else if (info.AssetFileType == AssetInfo.FileType.ValidFile && info.Extension == ".prefab" /*&& info.AssetName == "ZC_fg.prefab"*/)
            {
                obj = AssetDatabase.LoadAssetAtPath<GameObject>(info.AssetPath);
                Renderer[] Rs = obj.GetComponentsInChildren<Renderer>();
                for (int r = 0; r < Rs.Length; r++)
                {
                    foreach (Material mater in Rs[r].sharedMaterials)
                    {
                        if (maters.Contains(mater))
                        {
                            continue;
                        }
                        maters.Add(mater);
                        shaderName = mater.shader.name;
                        if (shaderName.Contains("Standard"))
                        {
                            Debug.LogError("发现使用Standard 相关的Shader:" + obj.name + "[" + Rs[r].name);
                            continue;
                        }
                        if (shaderReplaceNames.ContainsKey(shaderName))
                        {
                            mater.shader = Shader.Find(shaderReplaceNames[shaderName]);
                        }
                        shaderNames.Add(shaderName);
                    }
                    //sortingOrder = Rs[r].sortingOrder / 100f;
                    //Rs[r].sortingOrder = 0;
                    //pos = Rs[r].transform.localPosition;
                    //pos.Set(pos.x, pos.y, sortingOrder);
                    //Rs[r].transform.localPosition = pos;

                }
                if (obj != null)
                {
                    //PrefabUtility.SavePrefabAsset(obj);
                    EditorUtility.SetDirty(obj);
                }
            }
        }
        AssetDatabase.Refresh();
        info = null;
        obj = null;
        maters.Clear();
        maters = null;
    }

    [MenuItem("Tools/Assets/EffectsTextureReplace")]
    private static void EffectsTextureReplaceAtlas()
    {
        if (EditorUtility.DisplayDialog("警告", "是否替换 TX Texeure To Atlas?", "是"))
        {
            //以Assets目录创建根对象
            AssetInfo _assets = new AssetInfo(Application.dataPath + "/CreatAssetBundle/ParticleEffects/TX", "Assets", true);
            //从根对象开始，读取所有文件创建子对象
            EditorTool.ReadAssetsInChildren(_assets);

            string assetPath = "Assets/CreatAssetBundle/ParticleEffects/GFXAtlas.png";
            object gfxTexture = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture));
            if (gfxTexture == null || !(gfxTexture is Texture))
            {
                //尝试创建图集
                CreatGFXTexture(_assets, true);
            }
            Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
            object[] sprites = AssetDatabase.LoadAllAssetsAtPath(assetPath);
            object sprite;
            Texture texture = null;
            for (int i = 0; i < sprites.Length; i++)
            {
                sprite = sprites[i];
                if (sprite is Sprite)
                {
                    spriteDic[(sprite as Sprite).name] = sprite as Sprite;
                }
                else if (sprite is Texture)
                {
                    texture = sprite as Texture;
                }
            }
            DontReplaceMaters.Clear();
            ReplaceEffectTexture(_assets, spriteDic, new Vector2(texture == null ? 2048 : texture.width, texture == null ? 2048 : texture.height));

            spriteDic = null;
            sprites = null;
            sprite = null;
            texture = null;
        }
    }
    static HashSet<Material> DontReplaceMaters = new HashSet<Material>();
    private static void ReplaceEffectTexture(AssetInfo _assets, Dictionary<string, Sprite> spriteDic, Vector2 wh)
    {
        AssetInfo info;
        GameObject obj;
        ParticleSystemRenderer[] Psr;
        Renderer[] Rs;
        HashSet<Material> maters = new HashSet<Material>();
        string name = "";
        Sprite sprite;
        Rect rect;
        Vector2 offset;
        for (int i = 0; i < _assets.ChildAssetInfo.Count; i++)
        {
            info = _assets.ChildAssetInfo[i];
            if (info.AssetFileType == AssetInfo.FileType.Folder)
            {
                ReplaceEffectTexture(info, spriteDic, wh);
            }
            else if (info.AssetFileType == AssetInfo.FileType.ValidFile && info.Extension == ".prefab"/* && info.AssetName == "Fx_bg_01.prefab"*/)
            {
                obj = AssetDatabase.LoadAssetAtPath<GameObject>(info.AssetPath);
                Animator[] animators = obj.GetComponentsInChildren<Animator>();
                HashSet<string> nodePaths = new HashSet<string>();
                foreach (var animator in animators)
                {
                    foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
                    {
                        IEnumerable<string> curves = AnimationUtility.GetCurveBindings(clip).Select(z =>
                        {
                            string[] paths = z.path.Split('/');
                            return paths[paths.Length - 1];
                        });
                        foreach (string path in curves)
                        {
                            nodePaths.Add(path);
                        }
                    }
                }
                Psr = obj.GetComponentsInChildren<ParticleSystemRenderer>();
                for (int p = 0; p < Psr.Length; p++)
                {
                    //if (Psr[p].renderMode == ParticleSystemRenderMode.Mesh)
                    //    continue;
                    Rs = Psr[p].GetComponentsInChildren<Renderer>();
                    bool dontReplace = nodePaths.Contains(Psr[p].gameObject.name);

                    for (int r = 0; r < Rs.Length; r++)
                    {
                        foreach (Material mater in Rs[r].sharedMaterials)
                        {
                            if (dontReplace)
                            {
                                DontReplaceMaters.Add(mater);
                                continue;
                            }
                            if (mater == null || DontReplaceMaters.Contains(mater)  /*|| mater.mainTextureScale != Vector2.one || mater.mainTextureOffset != Vector2.zero*/)
                            {
                                continue;
                            }
                            DontReplaceMaters.Add(mater);
                            maters.Add(mater);
                            if (mater.mainTexture != null)
                            {
                                name = mater.mainTexture.name;
                            }
                            if (spriteDic.ContainsKey(name))
                            {
                                offset = mater.mainTextureOffset;
                                sprite = spriteDic[name];
                                rect = sprite.rect;
                                mater.mainTexture = sprite.texture;
                                mater.mainTextureOffset = new Vector2(rect.x / wh.x, rect.y / wh.y);
                                mater.mainTextureScale = new Vector2((rect.width) / wh.x, (rect.height) / wh.y);
                            }
                        }
                    }
                    if (obj != null)
                    {
                        //PrefabUtility.SavePrefabAsset(obj);
                        EditorUtility.SetDirty(obj);
                    }
                }
            }
        }
        AssetDatabase.Refresh();
        info = null;
        obj = null;
        Rs = null;
        Psr = null;
        sprite = null;

        var iter = maters.GetEnumerator();
        while (iter.MoveNext())
        {
            Debug.Log("替换特效材质球的 Maintexture :" + iter.Current.name);
        }
        Debug.LogError(_assets.AssetName + "文件夹替换特效成功 一共替换材质球数量：" + maters.Count);
        iter.Dispose();
        maters = null;
    }
    static HashSet<Material> maters = new HashSet<Material>();

    static public void CreatGFXTexture(AssetInfo _assets, bool isCreat, List<Texture2D> textures = null)
    {
        AssetInfo info;
        GameObject obj;
        if (isCreat)
            maters.Clear();
        if (textures == null)
            textures = new List<Texture2D>();
        for (int i = 0; i < _assets.ChildAssetInfo.Count; i++)
        {
            info = _assets.ChildAssetInfo[i];
            if (info.AssetFileType == AssetInfo.FileType.Folder)
            {
                CreatGFXTexture(info, false, textures);
            }
            else if (info.AssetFileType == AssetInfo.FileType.ValidFile && info.Extension == ".prefab"/* && info.AssetName == "Test.prefab"*/)
            {
                obj = AssetDatabase.LoadAssetAtPath<GameObject>(info.AssetPath);
                Renderer[] Rs = obj.GetComponentsInChildren<Renderer>();
                for (int r = 0; r < Rs.Length; r++)
                {
                    foreach (Material mater in Rs[r].sharedMaterials)
                    {
                        if (maters.Contains(mater))
                        {
                            continue;
                        }
                        maters.Add(mater);
                        if (mater.mainTexture != null)
                            textures.Add((Texture2D)mater.mainTexture);
                    }
                }
            }
        }
        if (isCreat && textures.Count > 0)
        {
            //得到图片的设置信息
            TextureImporterSettings[] originalSets = AtlasTool.GatherSettings(textures.ToArray());
            //最终打成的图集路径，包括名字
            string assetPath = "Assets/CreatAssetBundle/ParticleEffects/GFXAtlas.png";
            string outputPath = Application.dataPath + "/../" + assetPath;
            //主要的打图集代码
            AtlasTool.PackAndOutputSprites(textures.ToArray(), assetPath, outputPath);
            maters.Clear();
        }
    }

    [MenuItem("Tools/TEST11111111")]
    private static void EffectsOptimize111111111111111()
    {
        string[] bundles = AssetDatabase.GetAllAssetBundleNames();

        return;
        List<int> mall = new List<int>();
        mall.Add(99999);
        mall.Add(90009);
        mall.Add(89999);
        mall.Add(89997);
        mall.Add(89989);
        mall.Add(89899);
        mall.Add(55555);
        mall.Add(19999);
        mall.Add(10000);
        mall.Add(4523);
        mall.Add(6344);
        mall.Add(325);
        mall.Add(123);

        for (int i = 0; i < mall.Count; i++)
        {
            Utils.LogError(mall[i] + "=>>>>>>" + SortingOrderToZaxis(mall[i]));
        }
    }
    private static float SortingOrderToZaxis(float sortingOrder)
    {
        if (sortingOrder / 10000 >= 1)
        {
            return ((int)sortingOrder / 10000) * 40 + 1200 + SortingOrderToZaxis(sortingOrder % 10000);
        }
        else if (sortingOrder / 1000 >= 1)
        {
            return ((int)sortingOrder / 1000) * 30 + 410 + SortingOrderToZaxis(sortingOrder % 1000);
        }
        else if (sortingOrder / 100 >= 1)
        {
            return ((int)sortingOrder / 100) * 20 + 100 + SortingOrderToZaxis(sortingOrder % 100);
        }
        else if (sortingOrder / 10 >= 1)
        {
            return ((int)sortingOrder / 10) * 10 + SortingOrderToZaxis(sortingOrder % 10);
        }
        return sortingOrder;
    }
}
