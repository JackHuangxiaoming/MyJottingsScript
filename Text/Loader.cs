using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : Singleton<Loader> {

    public List<AssetBundle> loadedBundle = new List<AssetBundle>();
    public List<Sprite> loadedSprte = new List<Sprite>();

    public AssetBundle currentCADBounld;
	public AssetBundle currentAlumbBounld;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        print("Loader Clear!");
        loadedSprte.Clear();
        //loadedBundle.Clear();
        //AssetBundle.UnloadAllAssetBundles(true);

        if (SceneManager.GetActiveScene().name == "FuXing")
        {

        }
        else {
            if (currentCADBounld)
            {
                currentCADBounld.Unload(true);
            }
        }

		if (currentAlumbBounld) {
			currentAlumbBounld.Unload (true);
		}
    }

    /// <summary>
    /// 载入画册资源 并且释放assetbound
    /// </summary>
    /// <returns></returns>
    public Sprite[] LoadAlumbSprite()
    {
        string boundldName = GameManager.Instance.CurrentProject.albumAssetName;
        //AssetBundle bounld = null;
        //载入bounld
        for (int i = 0;i<loadedBundle.Count;i++)
        {
            if (loadedBundle[i].name == boundldName)
            {
				currentAlumbBounld = loadedBundle[i];

                print("bounld has loaded before!");
                break;
            }
        }

		if (currentAlumbBounld == null)
        {
			currentAlumbBounld = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, boundldName));
			print("load bounld file,bounld name:" + currentAlumbBounld.name);
        }

        
        string[] albumName = GameManager.Instance.CurrentProject.albumName;
        int count = albumName.Length;
        Sprite[] s = new Sprite[count];
        
        for (int i = 0; i < count; i++)
        {
			s[i] = currentAlumbBounld.LoadAsset<Sprite>(albumName[i]);
            if (s[i]== null)
            {
                Debug.LogError("Can't Load:"+albumName[i]);
            }
            print("Load："+s[i]);
        }

        //卸载Bounld
        //bounld.Unload(false);
        return s;
    }

    /// <summary>
    /// 载入画册资源 并且释放assetbound
    /// </summary>
    /// <returns></returns>
    public Sprite[] LoadCadSprite()
    {
        string boundldName = GameManager.Instance.CurrentProject.cadAssetName;
        //载入bounld
        for (int i = 0; i < loadedBundle.Count; i++)
        {
            if (loadedBundle[i].name == boundldName)
            {
                currentCADBounld = loadedBundle[i];

                print("bounld has loaded before!");
                break;
            }
        }

        if (currentCADBounld == null)
        {
            currentCADBounld = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, boundldName));
            print("load bounld file,bounld name:" + currentCADBounld.name);
        }


        string[] albumName = GameManager.Instance.CurrentDanYuan.floorCADName;
        int count = albumName.Length;
        Sprite[] s = new Sprite[count];

        for (int i = 0; i < count; i++)
        {
            s[i] = currentCADBounld.LoadAsset<Sprite>(albumName[i]);
            if (s[i] == null)
            {
                Debug.LogError("Can't Load:" + albumName[i]);
            }
            //print("Load：" + s[i]);
        }

        //卸载Bounld
        //bounld.Unload(false);
        return s;
    }


    public Sprite LoadSprite(string bounldName, string spriteName)
    {
        foreach (Sprite s in loadedSprte)
        {
            if (s.name == spriteName)
            {
                print("sprite has loaded before!");
                return s;
            }
        }



        bool isLoaded = false;
        AssetBundle mybounld =null;
        foreach (AssetBundle ab in loadedBundle)
        {
            if (ab.name == bounldName)
            {
                isLoaded = true;
                mybounld = ab;
                print("bounld has loaded before!");
                break;
            }
        }
        if (!isLoaded)
        {
            mybounld = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bounldName));
            print("load bounld file,bounld name:"+mybounld.name);
            loadedBundle.Add(mybounld);
        }

        Sprite mysprite = mybounld.LoadAsset<Sprite>(spriteName);
        loadedSprte.Add(mysprite);
        return mysprite;
    }
}
