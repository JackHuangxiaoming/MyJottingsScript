using System;
using System.Collections.Generic;
/// <summary>
/// 存储一次对话的内容
/// </summary>
[System.Serializable]
public class Dialog : IDialogRegulation<Dialogitem>
{
    public List<Dialogitem> Dialogitems;

    public string Id = "Dialog";

    public Dialog()
    {
        Dialogitems = new List<Dialogitem>();
        Id = Id + GetHashCode();
    }
    /// <summary>
    /// 增加一段对话
    /// </summary>
    /// <returns></returns>
    public Dialogitem AddContent()
    {
        Dialogitem D = new Dialogitem();
        Dialogitems.Add(D);
        return D;
    }
    /// <summary>
    /// 删除一段对话
    /// </summary>
    /// <param name="D"></param>
    public void RemoveContent(Dialogitem D)
    {
        Dialogitems.Remove(D);
    }
}