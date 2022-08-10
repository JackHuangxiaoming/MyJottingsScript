using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : UIForm<PauseScreen>
{
    /// <summary>
    /// 开始按钮
    /// </summary>
    public Button PlayButton;
    /// <summary>
    /// 主菜单按钮
    /// </summary>
    public Button HomeButton;
    /// <summary>
    /// 按钮
    /// </summary>
    public Button RetryButton;
    /// <summary>
    /// 控制透明度的
    /// </summary>

    /// <summary>
    /// 控制是否变化
    /// </summary>
    protected bool isChanging;
    /// <summary>
    /// 开始变化 和 变化到 的Alpha值
    /// </summary>
    protected float Alpha = 1, DesAlpha = 1;
    protected override void Awake()
    {
        base.Awake();
        PlayButton.onClick.AddListener(OnClickPlay);
        HomeButton.onClick.AddListener(OnClickHome);
        RetryButton.onClick.AddListener(OnClickRetry);
    }
    private void OnClickPlay()
    {
        GameMarager.Single.State = State.Game;
        print("Game");
    }

    private void OnClickHome()
    {
        GameMarager.Single.State = State.StartMenu;
        print("StartMenu");
    }

    private void OnClickRetry()
    {

    }
    protected override void Update()
    {
        base.Update();

        if (isChanging == true)
        {
            Alpha = Mathf.Lerp(Alpha, DesAlpha, 0.1f);
            CanvasGroup.alpha = Alpha;
            if (Mathf.Abs(Alpha - DesAlpha) < 0.001f)
            {
                Alpha = DesAlpha;
                CanvasGroup.alpha = Alpha;
                isChanging = false;
            }
        }
    }
    public override void Hied()
    {
        isChanging = true;
        DesAlpha = 0;
        isShow = false;
        Invoke("InvokeHied", 0.2f);
    }

    public override void Show()
    {
        RootPanel.SetActive(true);
        isChanging = true;
        DesAlpha = 1;
        isShow = true;
    }



    public override void InvokeHied()
    {
        RootPanel.SetActive(false);
    }
}
