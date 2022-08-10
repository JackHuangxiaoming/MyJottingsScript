using System;
/// <summary>
/// 对对话内容的增加删除
/// 泛型T 是将要增加的内容的类
/// 提供add remove方法
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDialogRegulation<T>
{
    T AddContent();

    void RemoveContent(T D);
}