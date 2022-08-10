using System;
using System.Collections.Generic;

public class DialogLibrary : Singleton<DialogLibrary>, IDialogRegulation<Dialog>
{

    public List<Dialog> Dialogs;
    /// <summary>
    /// 增加一段对话
    /// </summary>
    /// <returns></returns>
    public Dialog AddContent()
    {
        Dialog D = new Dialog();
        Dialogs.Add(D);
        return D;
    }
    /// <summary>
    /// 删除一段对话
    /// </summary>
    public void RemoveContent(Dialog D)
    {
        Dialogs.Remove(D);
    }

    private void Awake()
    {
        if (Dialogs == null) Dialogs = new List<Dialog>();
    }
    /// <summary>
    /// 查找指定Id的Dialog
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Dialog GetDialogById(string id)
    {
        //查找某个集合内的东西 使用XXX表达式 d为集合内的泛指 这里是d的Id==id 就返回d
        Dialog dialog = Dialogs.Find(d => d.Id == id);
        if (dialog == null) print(string.Format("要查找Id为{0}的Dialog不存在", id));
        return dialog;
    }
    /// <summary>
    /// 返回所有对话内容的ID
    /// </summary>
    /// <returns></returns>
    public string[] GetDialogAllId()
    {
        string[] Dialogid = new string[Dialogs.Count];
        for (int i = 0; i < Dialogs.Count; i++)
        {
            Dialogid[i] = Dialogs[i].Id;
        }
        return Dialogid;
    }
}
