using System;
using UnityEngine;
/// <summary>
/// UI的父类
/// </summary>
public class UIForm<T> : Singleton<T> where T : MonoBehaviour
{
    /// <summary>
    /// 根节点
    /// </summary>
    public GameObject RootPlane;
    /// <summary>
    /// 自己的状态
    /// </summary>
    public GameState[] MyState;
    /// <summary>
    /// 是否显示UI
    /// </summary>
    public bool isShow { get; protected set; }
    /// <summary>
    /// 幕布
    /// </summary>
    protected CanvasGroup _CanvasGroup;

    protected virtual void Awake()
    {
        GameMarager.Single.OnStateChang += Single_OnStateChang;

        _CanvasGroup = RootPlane.AddComponent<CanvasGroup>();
    }

    /// <summary>
    /// 响应状态改变事件
    /// </summary>
    /// <param name="state"></param>
    protected void Single_OnStateChang(GameState state)
    {
        foreach (var i in MyState)
        {
            if (i == state)
            {
                Show();
                return;
            }
        }
        Hide();
    }

    /// <summary>
    /// 隐藏自己
    /// </summary>
    public virtual void Hide()
    {
        RootPlane.SetActive(false);
        isShow = RootPlane.activeSelf;
    }
    /// <summary>
    /// 显示自己
    /// </summary>
    public virtual void Show()
    {
        RootPlane.SetActive(true);
        isShow = RootPlane.activeSelf;
    }
    /// <summary>
    /// 自动隐藏自己
    /// </summary>
    public virtual void InvokeHide()
    {
        RootPlane.SetActive(false);
        isShow = RootPlane.activeSelf;
    }
    /// <summary>
    /// 设置显示或者隐藏
    /// </summary>
    public void SetState()
    {
        if (isShow) Show();
        else Hide();
    }
}

