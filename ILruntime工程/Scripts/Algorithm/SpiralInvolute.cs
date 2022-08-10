using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 螺旋线参数
/// </summary>
public class SpiralInvoluteParam
{
    public float baseRadius = 0.5f;
    public float speed = 1;
    public float endRadian = 3.5f * Mathf.PI;
    public Vector3 startPoint = new Vector3(0, 0f, 0);
    public float firstPhase = 0;

    public SpiralInvoluteParam(Hashtable hash)
    {
        baseRadius = MoveParamTool.GetRadius(hash);
        speed = MoveParamTool.GetSpeed(hash);
        endRadian = MoveParamTool.GetEndRadian(hash);
        startPoint = MoveParamTool.GetStartPoint(hash);
        firstPhase = MoveParamTool.GetFirstPhase(hash);
    }

    public static bool IsEquals(SpiralInvoluteParam res, SpiralInvoluteParam target)
    {
        if (res == null && target == null)
        {
            return true;
        }
        if (res != null && target != null)
        {
            return res.baseRadius == target.baseRadius && res.speed == target.speed
                   && res.endRadian == target.endRadian && res.startPoint == target.startPoint
                    && res.firstPhase == target.firstPhase;
        }
        else
        {
            return false;
        }
    }

}
/// <summary>
/// 螺旋渐开线 (对数螺旋)
/// </summary>
public class SpiralInvolute : IMoveCtrl
{
    private Vector3 _startPoint;
    private float _baseRadius;
    private float _speed;
    private float _endRadian;
    private float _totalLength;

    private static Dictionary<SpiralInvoluteParam, SpiralInvolute> _spiralPool = new Dictionary<SpiralInvoluteParam, SpiralInvolute>();

    public static IMoveCtrl GetMoveControl(Hashtable hash)
    {
        SpiralInvoluteParam moveParam = new SpiralInvoluteParam(hash);
        return GetMoveControl(moveParam);
    }

    private static SpiralInvolute GetMoveControl(SpiralInvoluteParam moveParam)
    {
        foreach (var keyItem in _spiralPool.Keys)
        {
            if (SpiralInvoluteParam.IsEquals(keyItem, moveParam))
            {
                return _spiralPool[keyItem];
            }
        }
        SpiralInvolute spiral = new SpiralInvolute(moveParam);
        _spiralPool[moveParam] = spiral;
        return spiral;
    }


    private SpiralInvolute(SpiralInvoluteParam param)
    {
        _baseRadius = param.baseRadius;
        _speed = param.speed;
        _endRadian = param.endRadian;
        float ds = Mathf.PI * 0.125f;
        Vector3 stpoint = Vector3.zero;
        _totalLength = 0;
        for (float radian = 0; radian < _endRadian; radian += ds)
        {
            Vector3 next = GetSpiralInvolutePoint(radian);
            if (radian != 0)
            {
                _totalLength += Vector3.Distance(stpoint, next);
            }
            stpoint = next;
        }
    }

    public Vector3 GetPointByTime(ref bool isReachFinishline, float time)
    {
        float ds = time * _speed;
        float percent = ds / _totalLength;
        percent = percent > 1 ? 1 : percent;
        isReachFinishline = percent >= 1;
        float radian = _endRadian * percent;
        return _startPoint + GetSpiralInvolutePoint(radian);
    }

    /// <summary>
    /// 螺旋渐开线 (对数螺旋)
    /// </summary>
    /// <param name="radian">弧度</param>
    /// <returns></returns>
    public Vector3 GetSpiralInvolutePoint(float radian)
    {
        float r = _baseRadius * Mathf.Exp(0.2063489f * radian);
        float x = r * Mathf.Cos(radian);
        float y = r * Mathf.Sin(radian);
        float z = -radian;
        return new Vector3(x, y, z);
    }

}
