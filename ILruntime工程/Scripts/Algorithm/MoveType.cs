public enum MoveType : byte
{
    /// <summary>
    /// 贝塞尔曲线 
    /// </summary>
    BesselCurve = 0,
    /// <summary>
    /// 斐波拉契螺旋线 （黄金螺旋）
    /// </summary>
    FibonacciSpiral = 1,
    /// <summary>
    /// 放射形
    /// </summary>
    Actiniform = 2,
    /// <summary>
    /// 固定形状整体直线移动，如：圆形
    /// </summary>
    WholeLine = 3,
    /// <summary>
    /// 环形移动
    /// </summary>
    RoundMove = 6,
    /// <summary>
    /// 八字形移动
    /// </summary>
    EightMove = 7,
    /// <summary>
    /// 对角线移动
    /// </summary>
    DiagonalMove = 8,
    /// <summary>
    /// 阿基米得螺旋 ( 等速螺线 )
    /// </summary>
    ArchimedeanSpiral = 9,
    /// <summary>
    /// 正弦曲线
    /// </summary>
    SineSpiral = 10,
    /// <summary>
    /// 圆锥曲线
    /// </summary>
    ConicCurve = 11,
}


