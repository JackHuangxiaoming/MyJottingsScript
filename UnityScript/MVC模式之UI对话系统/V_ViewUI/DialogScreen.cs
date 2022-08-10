using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class DialogScreen : UIForm<DialogScreen>
{
    /// <summary>
    /// 头像
    /// </summary>
    public Image Icon;
    /// <summary>
    /// 名字
    /// </summary>
    public Text Name;
    /// <summary>
    /// 消息内容
    /// </summary>
    public Text ContentText;
    /// <summary>
    /// 消息内容片断的索引
    /// </summary>
    private int Index;
    /// <summary>
    /// 存储对话的内容
    /// </summary>
    private Dialog dialog;
    /// <summary>
    /// 消息显示的sb
    /// </summary>
    private StringBuilder Sb = new StringBuilder();
    /// <summary>
    /// 声音播放器
    /// </summary>
    private AudioSource Audio;
    protected override void Awake()
    {
        base.Awake();
        Audio = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();

        if (isShow == false) return;

        if (InputMarager.Use)
        {
            Index++;
            if (Index >= dialog.Dialogitems.Count)
            {
                CancelInvoke("FillByOneBy");
                GameMarager.Single.State = State.Game;
            }
            else
            {
                Fill();
            }
        }
    }

    /// <summary>
    /// 设置对话的内容
    /// </summary>
    /// <param name="D"></param>
    public void SetDialog(Dialog D)
    {
        if (D == null || D.Dialogitems.Count == 0)
        {
            GameMarager.Single.State = State.Game;
            return;
        }

        dialog = D;
        Index = 0;
        Fill();
    }
    /// <summary>
    /// 填充一段聊天内容
    /// </summary>
    private void Fill()
    {
        Icon.sprite = dialog.Dialogitems[Index].Icon;
        Name.text = dialog.Dialogitems[Index].Name;
        //ContentText.text = dialog.Dialogitems[Index].Text;

        ContentText.text = "";
        Sb = new StringBuilder();
        //打字机效果
        CancelInvoke("FillByOneBy");
        InvokeRepeating("FillByOneBy", 0, 0.1f);
    }
    /// <summary>
    /// 打字机效果
    /// </summary>
    void FillByOneBy()
    {
        string Content = dialog.Dialogitems[Index].Text;

        int ContentIndex = ContentText.text.Length;
        //当内容长度小于将要显示的长度 显示
        if (ContentIndex < Content.Length)
        {
            Sb.Append(Content[ContentIndex]);
            ContentText.text = Sb.ToString();
            Audio.Stop();
            Audio.Play();
        }
    }
}
