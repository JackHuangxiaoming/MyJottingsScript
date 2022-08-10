using DG.Tweening;
using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MessagePopup
{
    private static MessagePopup _inst;
    public static MessagePopup Instance
    {
        get
        {
            if (_inst == null)
            {
                _inst = new MessagePopup();
            }
            return _inst;
        }
    }
    public bool IsDestroyed { get; set; }
    public GComponent SelfGComponent { get; set; }
    private GTextField txtContent;
    private Controller typeController;
    private Controller confirmController;
    private EventCallback0 ecb0;
    private float minHight;

    private GGraph _backGraph;
    private Tweener _curTween;

    private GButton confirmBtn;
    private GButton cancleBtn;

    MessagePopup()
    {
        SelfGComponent = GetSelfComponent();
        FindNode();
        InitParam();
        AddEventListener();
    }

    public void FindNode()
    {
        txtContent = Utils.FindChildByName<GTextField>(SelfGComponent, "tipscontent");
        txtContent.SetPivot(0.5f, 0.5f);
        txtContent.autoSize = AutoSizeType.None;
        typeController = SelfGComponent.GetController("type");
        confirmController = SelfGComponent.GetController("confirmXY");
    }
    public void InitParam()
    {
        txtContent.UBBEnabled = true;
    }
    public void AddEventListener()
    {
        confirmBtn = (GButton)Utils.FindChildAndAddClick(SelfGComponent, "btnconfirm", OnConfirmClick);
        cancleBtn = (GButton)Utils.FindChildAndAddClick(SelfGComponent, "btncancel", Close);
    }

    /// <summary>
    /// 设置弹窗信息
    /// </summary>
    /// <param name="str"></param>
    /// <param name="ecb0"></param>
    /// <param name="type"></param>
    public void SetContent(string str, EventCallback0 ecb0 = null, int type = 0, string confirmStr = "确定", string cancleStr = "取消")
    {
        txtContent.text = str;

        txtContent.align = txtContent.singleLine ? AlignType.Center : AlignType.Left;
        confirmBtn.title = confirmStr;
        cancleBtn.title = cancleStr;
        if (typeController != null)
        {
            typeController.SetSelectedIndex(type);
        }
        if (confirmController != null)
        {
            confirmController.SetSelectedIndex(type);
        }
        this.ecb0 = ecb0;
    }
    /// <summary>
    /// 确定按钮
    /// </summary>
    private void OnConfirmClick()
    {
        ecb0?.Invoke();
        Close();
    }
    private void Close()
    {
        ecb0 = null;
        if (_curTween != null)
        {
            _curTween.Kill();
        }
        if (SelfGComponent != null)
        {
            SelfGComponent.RemoveFromParent();
        }
        if (_backGraph != null)
        {
            _backGraph.RemoveFromParent();
        }
    }
    public void Show(GComponent parent)
    {
        if (parent != null)
        {
            parent.AddChild(SelfGComponent);
        }
        if (_backGraph == null)
        {
            _backGraph = new GGraph();
            _backGraph.name = SelfGComponent.name + "_back";
            _backGraph.DrawRect(GRoot.inst.width, GRoot.inst.height, 0, UnityEngine.Color.gray, new UnityEngine.Color(0, 0, 0, 0.7f));
            _backGraph.AddRelation(GRoot.inst, RelationType.Size);
            _backGraph.onClick.Add(Close);
        }

        parent.AddChild(_backGraph);
        parent.AddChild(SelfGComponent);
        SelfGComponent.SetPivot(0.5f, 0.5f, true);
        SelfGComponent.SetScale(0.2f, 0.2f);
        SelfGComponent.SetXY(GRoot.inst.width * 0.5f, GRoot.inst.height * 0.5f);
        SelfGComponent.AddRelation(GRoot.inst, RelationType.Center_Center);
        SelfGComponent.AddRelation(GRoot.inst, RelationType.Middle_Middle);
        _curTween = Utils.TweenScale(SelfGComponent, Vector2.one, 0.2f);
    }
    public GComponent GetSelfComponent()
    {
        return AssetManager.Instance.CreateFGUIFromResName<GComponent>(AssetManager.PlatformLobby, AssetManager.BasicPackage, "normal_tips");
    }
}