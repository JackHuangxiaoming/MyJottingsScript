using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 斐波拉契螺旋线 （黄金螺旋）
/// </summary>
public class FibonacciSpiral : IMoveCtrl
{
    private Vector3 _startPoint;
    private float _baseRadius;
    private float _speed;
    private float _endRadian;
    private float _totalLength;

    private static Dictionary<SpiralInvoluteParam, FibonacciSpiral> _spiralPool = new Dictionary<SpiralInvoluteParam, FibonacciSpiral>();

    public static IMoveCtrl GetMoveControl(Hashtable hash)
    {
        SpiralInvoluteParam moveParam = new SpiralInvoluteParam(hash);
        return GetMoveControl(moveParam);
    }

    private static FibonacciSpiral GetMoveControl(SpiralInvoluteParam moveParam)
    {
        foreach (var keyItem in _spiralPool.Keys)
        {
            if (SpiralInvoluteParam.IsEquals(keyItem, moveParam))
            {
                return _spiralPool[keyItem];
            }
        }
        FibonacciSpiral spiral = new FibonacciSpiral(moveParam);
        _spiralPool[moveParam] = spiral;
        return spiral;
    }

    private FibonacciSpiral(SpiralInvoluteParam param)
    {
        _baseRadius = param.baseRadius;
        _speed = param.speed;
        _endRadian = param.endRadian;
        _startPoint = param.startPoint;
        float ds = Mathf.PI * 0.125f;
        Vector3 stpoint = Vector3.zero;
        _totalLength = 0;
        for (float radian = 0; radian < _endRadian; radian += ds)
        {
            Vector3 next = GetFibonacciPoint(radian);
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
        return _startPoint + GetFibonacciPoint(radian);
    }

    /// <summary>
    /// 斐波拉契螺旋线 （黄金螺旋）
    /// </summary>
    /// <param name="radian">弧度</param>
    /// <returns></returns>
    public Vector3 GetFibonacciPoint(float radian)
    {
        float r = _baseRadius * Mathf.Exp(0.3063489f * radian);
        float x = r * Mathf.Cos(radian);
        float y = r * Mathf.Sin(radian);
        float z = -radian;
        return new Vector3(x, y, z);
    }


}
