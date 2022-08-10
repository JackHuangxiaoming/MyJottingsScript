using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScreen : UIForm<HUDScreen>
{
    /// <summary>
    /// 提示文字UI
    /// </summary>
    public Text TispText;

    protected override void Awake()
    {
        base.Awake();
        PlayerColtroller.Single.OnInteraction += Single_OnInteraction;
    }

    /// <summary>
    /// 交互时事件
    /// </summary>
    /// <param name="obj"></param>
    private void Single_OnInteraction(IInteraction obj)
    {
        if (obj == null)
        {
            HiedTispText();
        }
        else
        {
            ShowTipsText(obj.Tips);
        }
    }
    /// <summary>
    /// 展示提示文字
    /// </summary>
    /// <param name="v"></param>
    void ShowTipsText(string v)
    {
        TispText.gameObject.SetActive(true);
        TispText.text = v;
    }
    /// <summary>
    /// 隐藏提示文字
    /// </summary>
    void HiedTispText()
    {
        TispText.gameObject.SetActive(false);
    }
}
