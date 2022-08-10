using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogObject : MonoBehaviour, IInteraction
{
    /// <summary>
    /// 对话物体的名字
    /// </summary>
    public string ObjectName;
    /// <summary>
    /// 对话物品ID
    /// </summary>
    public string ObjectID;

    public string tips;

    public bool IsActive
    {
        get
        {
            return true;
        }
    }

    string IInteraction.Name
    {
        get
        {
            return ObjectName;
        }
    }



    string IInteraction.Tips
    {
        get
        {
            return tips;
        }
    }

    IInteractionAction IInteraction.Action
    {
        get
        {
            return IInteractionAction.Read;
        }
    }

    void IInteraction.Use()
    {
        Dialog dialog = DialogLibrary.Single.GetDialogById(ObjectID);
        if (dialog != null)
        {
            GameMarager.Single.State = State.Dialogue;
            DialogScreen.Single.SetDialog(dialog);
        }
    }
}

