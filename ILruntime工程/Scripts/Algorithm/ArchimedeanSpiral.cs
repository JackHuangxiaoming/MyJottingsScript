using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 阿基米得螺旋 ( 等速螺线 )
/// </summary>
public class ArchimedeanSpiral : IMoveCtrl
{

    private Vector3 _startPoint;
    private float _baseRadius;
    private float _speed;
    private float _endRadian;
    private float _totalLength;

    private static Dictionary<SpiralInvoluteParam, ArchimedeanSpiral> _spiralPool = new Dictionary<SpiralInvoluteParam, ArchimedeanSpiral>();


    public static IMoveCtrl GetMoveControl(Hashtable hash)
    {
        SpiralInvoluteParam moveParam = new SpiralInvoluteParam(hash);
        return GetMoveControl(moveParam);
    }

    private static ArchimedeanSpiral GetMoveControl(SpiralInvoluteParam moveParam)
    {
        foreach (var keyItem in _spiralPool.Keys)
        {
            if (SpiralInvoluteParam.IsEquals(keyItem, moveParam))
            {
                return _spiralPool[keyItem];
            }
        }

        ArchimedeanSpiral spiral = new ArchimedeanSpiral(moveParam);
        _spiralPool[moveParam] = spiral;
        return spiral;
    }


    private ArchimedeanSpiral(SpiralInvoluteParam moveParam)
    {
        _baseRadius = moveParam.baseRadius;
        _speed = moveParam.speed;
        _endRadian = moveParam.endRadian;
        _startPoint = moveParam.startPoint;

        Vector3 stpoint = Vector3.zero;
        float ds = Mathf.PI * 0.125f;
        _totalLength = 0;
        for (float radian = 0; radian < _endRadian; radian += ds)
        {
            Vector3 next = GetArchimedeanSpiralPoint(radian);
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
        return _startPoint + GetArchimedeanSpiralPoint(radian);
    }

    /// <summary>
    /// 阿基米得螺旋 ( 等速螺线 )
    /// </summary>
    /// <param name="radian">弧度</param>
    /// <returns></returns>
    public Vector3 GetArchimedeanSpiralPoint(float radian)
    {
        float r = _baseRadius * (1 + 1f * radian);
        float x = r * Mathf.Cos(radian);
        float y = r * Mathf.Sin(radian);
        float z = -radian;
        return new Vector3(x, y, z);
    }

}
