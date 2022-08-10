using System;
using FairyGUI;
using UnityEngine;
public class MessageFlyTextTipsMgr : MonoBehaviour
{
    #region 静态方法 FlyTextTips
    private static MessageFlyTextTipsMgr _inst;
    public static MessageFlyTextTipsMgr inst
    {
        get
        {
            if (_inst == null)
            {
                GameObject obj = new GameObject();
                _inst = obj.AddComponent<MessageFlyTextTipsMgr>();
                obj.hideFlags = HideFlags.HideInHierarchy;
                GameObject.DontDestroyOnLoad(obj);
            }
            return _inst;
        }
    }
    #endregion

    private HotClassILRuntime _hotClass;
    public HotClassILRuntime NormalTipsHotClass
    {
        get
        {
            if (_hotClass == null)
                _hotClass = HotScriptManager.Instance.CreateHotClass("HotMessageFlyTextTipsMgr");
            return _hotClass;
        }
    }

    public void AddMessageFlyInfo(string masster, int type = 0, float xPos = 0, float yPos = 0)
    {
        NormalTipsHotClass.CallMethod("AddMessageFlyInfo", masster, type, xPos, yPos);
    }

    internal void MessagePopupShow(string content, EventCallback0 callback, int type, bool isSystem, string confirmStr = "确定", string cancleStr = "取消")
    {
        MessagePopup popup = MessagePopup.Instance;
        popup.SetContent(content, callback, type, confirmStr, cancleStr);
        if (isSystem)
        {
            popup.Show(LayerManager.SystemPopLayer);
        }
        else
        {
            popup.Show(LayerManager.HighestLayer);
        }
    }
}
