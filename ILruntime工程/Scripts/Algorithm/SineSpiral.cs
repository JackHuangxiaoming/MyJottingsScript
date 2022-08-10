using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 正弦曲线
/// </summary>
public class SineSpiral : IMoveCtrl
{
    private Vector3 _startPoint;
    private float _baseRadius;
    private float _speed;
    private float _endRadian;
    private float _totalLength;
    private float _firstPhase;

    private static Dictionary<SpiralInvoluteParam, SineSpiral> _spiralPool = new Dictionary<SpiralInvoluteParam, SineSpiral>();

    public static IMoveCtrl GetMoveControl(Hashtable hash)
    {
        SpiralInvoluteParam moveParam = new SpiralInvoluteParam(hash);
        return GetMoveControl(moveParam);
    }

    private static SineSpiral GetMoveControl(SpiralInvoluteParam moveParam)
    {
        foreach (var keyItem in _spiralPool.Keys)
        {
            if (SpiralInvoluteParam.IsEquals(keyItem, moveParam))
            {
                return _spiralPool[keyItem];
            }
        }

        SineSpiral spiral = new SineSpiral(moveParam);
        _spiralPool[moveParam] = spiral;
        return spiral;
    }

    private SineSpiral(SpiralInvoluteParam param)
    {
        _startPoint = param.startPoint;
        _baseRadius = param.baseRadius;
        _speed = param.speed;
        _endRadian = param.endRadian;
        _firstPhase = param.firstPhase;
        _totalLength = param.endRadian * _baseRadius;
    }

    public Vector3 GetPointByTime(ref bool isReachFinishline, float time)
    {
        float ds = time * _speed;
        float percent = ds / _totalLength;
        percent = percent > 1 ? 1 : percent;
        isReachFinishline = percent >= 1;
        float radian = _endRadian * percent;
        return _startPoint + GetSineSpiralPoint(radian);
    }

    /// <summary>
    /// 正弦曲线
    /// </summary>
    /// <param name="radian">弧度</param>
    public Vector3 GetSineSpiralPoint(float radian)
    {
        float x = _baseRadius * radian;
        float y = _baseRadius * Mathf.Sin(radian + _firstPhase);
        float z = _baseRadius * 2 * Mathf.Cos(radian + _firstPhase);

        return new Vector3(x, y, z);
    }


}
