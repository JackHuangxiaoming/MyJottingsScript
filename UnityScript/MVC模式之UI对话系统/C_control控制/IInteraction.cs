public enum IInteractionAction
{
    /// <summary>
    /// 使用
    /// </summary>
    Use,
    /// <summary>
    /// 读
    /// </summary>
    Read,
    /// <summary>
    /// 取得
    /// </summary>
    Tack
}
/// <summary>
/// 交互的 接口
/// </summary>
public interface IInteraction
{
    string Name { get; }
    bool IsActive { get; }
    string Tips { get; }
    IInteractionAction Action { get; }
    void Use();
}