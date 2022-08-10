using System;
using UnityEngine;
public class UIForm<T> : Singleton<T> where T : MonoBehaviour
{
    /// <summary>
    /// 根节点
    /// </summary>
    public GameObject RootPanel;

    public State MyState;
    /// <summary>
    /// 是否展示
    /// </summary>
    public bool isShow { get; protected set; }

    protected CanvasGroup CanvasGroup;
    ///// <summary>
    ///// 控制是否变化
    ///// </summary>
    //protected bool isChanging;
    ///// <summary>
    ///// 开始变化 和 变化到 的Alpha值
    ///// </summary>
    //protected float Alpha = 1, DesAlpha = 1;
    protected virtual void Awake()
    {
        GameMarager.Single.OnStateChang += Single_OnStateChang;

        RootPanel.AddComponent(typeof(CanvasGroup));
        CanvasGroup = RootPanel.GetComponent<CanvasGroup>();
    }
    void Single_OnStateChang(State state)
    {
        if (MyState == state) Show();
        else Hied();
    }
    public virtual void Show()
    {
        RootPanel.SetActive(true);
        isShow = true;
        //RootPanel.SetActive(true);
        //isChanging = true;
        //DesAlpha = 1;
        //isShow = true;
    }
    public virtual void Hied()
    {
        RootPanel.SetActive(false);
        isShow = false;
        //isChanging = true;
        //DesAlpha = 0;
        //isShow = false;
        //Invoke("InvokeHied", 0.2f);
    }
    public virtual void InvokeHied()
    {
        RootPanel.SetActive(false);
        isShow = false;
    }


    /// <summary>
    /// 设置调用状态
    /// </summary>
    public void SetState()
    {
        switch (isShow)
        {
            case true:
                Show();
                break;
            case false:
                Hied();
                break;
        }
    }

    protected virtual void Update()
    {
        //if (isChanging == true)
        //{
        //    Alpha = Mathf.Lerp(Alpha, DesAlpha, 0.1f);
        //    CanvasGroup.alpha = Alpha;
        //    if (Mathf.Abs(Alpha - DesAlpha) < 0.001f)
        //    {
        //        Alpha = DesAlpha;
        //        CanvasGroup.alpha = Alpha;
        //        isChanging = false;
        //    }
        //}
    }
}

