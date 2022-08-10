using FairyGUI;
using UnityEngine;

public class LayerManager
{
    /// <summary>
    /// 顶层
    /// </summary>
    public static GComponent TopLayer { get; set; }
    /// <summary>
    /// 系统提示 弹窗层
    /// </summary>
    public static GComponent SystemPopLayer { get; set; }
    /// <summary>
    /// 加载界面层
    /// </summary>
    public static GComponent LoadingLayer { get; set; }
    /// <summary>
    /// 跑马灯层
    /// </summary>
    public static GComponent PaoMaDengLayer { get; set; }
    /// <summary>
    /// 极高层
    /// </summary>
    public static GComponent HighestLayer { get; private set; }
    /// <summary>
    /// 次高层
    /// </summary>
    public static GComponent HigherLayer { get; private set; }
    /// <summary>
    /// 略高层
    /// </summary>
    public static GComponent LittleHigherLayer { get; private set; }
    /// <summary>
    /// 普通层
    /// </summary>
    public static GComponent NormalLayer { get; private set; }
    /// <summary>
    /// 略低层
    /// </summary>
    public static GComponent LittleLowerLayer { get; private set; }
    /// <summary>
    /// 低层
    /// </summary>
    public static GComponent LowerLayer { get; private set; }
    /// <summary>
    /// 最底层 Layer = Fight
    /// </summary>
    public static GComponent LowestLayer { get; private set; }
    /// <summary>
    /// 战斗层2 Layer = Fight
    /// </summary>
    public static GComponent FightLayer { get; private set; }

    /// <summary>
    /// 战斗层1 Layer = Fight
    /// </summary>
    public static GComponent FightBackgroundLayer { get; private set; }
    /// <summary>
    /// 模拟战斗层
    /// </summary>
    public static GComponent FightLiarLayer { get; }
    public static GComponent FightBGLiarLayer { get; }
    /// <summary>
    /// 模拟战斗层 layer
    /// </summary>
    public static int LiarFightLayer_Unity = LayerMask.NameToLayer(StageCamera.FightLiarLayerName);
    /// <summary>
    /// 模拟战斗背景 layer
    /// </summary>
    public static int LiarFightBGLayer_Unity = LayerMask.NameToLayer(StageCamera.FightBGLiarLayerName);
    /// <summary>
    /// 战斗层 layer
    /// </summary>
    public static int FightLayer_Unity = LayerMask.NameToLayer(StageCamera.FightLayerName);

    /// <summary>
    /// 战斗背景 layer
    /// </summary>
    public static int FightBGLayer_Unity = LayerMask.NameToLayer(StageCamera.FightBGLayerName);


    static LayerManager()
    {
        GRoot.inst.SetContentScaleFactor(720, 1280, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
        TopLayer = new GComponent();
        SystemPopLayer = new GComponent();
        LoadingLayer = new GComponent();
        PaoMaDengLayer = new GComponent();
        HighestLayer = new GComponent();
        HigherLayer = new GComponent();
        LittleHigherLayer = new GComponent();
        NormalLayer = new GComponent();
        LittleLowerLayer = new GComponent();
        LowerLayer = new GComponent();
        LowestLayer = new GComponent();
        FightLiarLayer = new GComponent();
        FightLiarLayer.displayObject.layer = LiarFightLayer_Unity;
        FightBGLiarLayer = new GComponent();
        FightBGLiarLayer.displayObject.layer = LiarFightBGLayer_Unity;
        FightLayer = new GComponent();
        FightLayer.displayObject.layer = FightLayer_Unity;
        FightBackgroundLayer = new GComponent();
        FightBackgroundLayer.displayObject.layer = FightBGLayer_Unity;

        FightLiarLayer.gameObjectName = "FightLiarLayer";
        FightBGLiarLayer.gameObjectName = "FightBGLiarLayer";
        FightBackgroundLayer.gameObjectName = "FightBackgroundLayer";
        FightLayer.gameObjectName = "FightLayer";
        LowestLayer.gameObjectName = "LowestLayer";
        LowerLayer.gameObjectName = "LowerLayer";
        LittleLowerLayer.gameObjectName = "LittleLowerLayer";
        NormalLayer.gameObjectName = "NormalLayer";
        LittleHigherLayer.gameObjectName = "LittleHigherLayer";
        HigherLayer.gameObjectName = "HigherLayer";
        HighestLayer.gameObjectName = "HighestLayer";
        PaoMaDengLayer.gameObjectName = "PaoMaDengLayer";
        LoadingLayer.gameObjectName = "LoadingLayer";
        SystemPopLayer.gameObjectName = "SystemPopLayer";
        TopLayer.gameObjectName = "TopLayer";

        FightBGLiarLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        FightBGLiarLayer.AddRelation(GRoot.inst, RelationType.Size);
        FightLiarLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        FightLiarLayer.AddRelation(GRoot.inst, RelationType.Size);
        FightBackgroundLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        FightBackgroundLayer.AddRelation(GRoot.inst, RelationType.Size);
        FightLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        FightLayer.AddRelation(GRoot.inst, RelationType.Size);
        LowestLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        LowestLayer.AddRelation(GRoot.inst, RelationType.Size);
        LowerLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        LowerLayer.AddRelation(GRoot.inst, RelationType.Size);
        LittleLowerLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        LittleLowerLayer.AddRelation(GRoot.inst, RelationType.Size);
        NormalLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        NormalLayer.AddRelation(GRoot.inst, RelationType.Size);
        LittleHigherLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        LittleHigherLayer.AddRelation(GRoot.inst, RelationType.Size);
        HigherLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        HigherLayer.AddRelation(GRoot.inst, RelationType.Size);
        HighestLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        HighestLayer.AddRelation(GRoot.inst, RelationType.Size);
        PaoMaDengLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        PaoMaDengLayer.AddRelation(GRoot.inst, RelationType.Size);
        LoadingLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        LoadingLayer.AddRelation(GRoot.inst, RelationType.Size);
        SystemPopLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        SystemPopLayer.AddRelation(GRoot.inst, RelationType.Size);
        TopLayer.SetSize(GRoot.inst.width, GRoot.inst.height);
        TopLayer.AddRelation(GRoot.inst, RelationType.Size);

        //FightLiarLayer.fairyBatching = true;
        FightBackgroundLayer.fairyBatching = true;
        FightLayer.fairyBatching = true;
        LowestLayer.fairyBatching = true;
        LowerLayer.fairyBatching = true;
        LittleLowerLayer.fairyBatching = true;
        NormalLayer.fairyBatching = true;
        LittleHigherLayer.fairyBatching = true;
        HigherLayer.fairyBatching = true;
        HighestLayer.fairyBatching = true;
        PaoMaDengLayer.fairyBatching = true;
        LoadingLayer.fairyBatching = true;
        SystemPopLayer.fairyBatching = true;
        TopLayer.fairyBatching = true;

        GRoot.inst.AddChild(FightBackgroundLayer);
        GRoot.inst.AddChild(FightLayer);
        GRoot.inst.AddChild(FightBGLiarLayer);
        GRoot.inst.AddChild(FightLiarLayer);
        GRoot.inst.AddChild(LowestLayer);
        GRoot.inst.AddChild(LowerLayer);
        GRoot.inst.AddChild(LittleLowerLayer);
        GRoot.inst.AddChild(NormalLayer);
        GRoot.inst.AddChild(LittleHigherLayer);
        GRoot.inst.AddChild(HigherLayer);
        GRoot.inst.AddChild(HighestLayer);
        GRoot.inst.AddChild(PaoMaDengLayer);
        GRoot.inst.AddChild(LoadingLayer);
        GRoot.inst.AddChild(SystemPopLayer);
        GRoot.inst.AddChild(TopLayer);

    }


}
