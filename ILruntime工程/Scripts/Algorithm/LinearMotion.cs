using System.Collections;
using UnityEngine;

/// <summary>
/// 直线运动
/// </summary>
public class LinearMotion : IMoveCtrl
{
    private Vector3 _startPoint;
    private Vector3 _speed;
    private Vector3 _endPoint;
    private float _totalLength;

    public static LinearMotion GetMoveControl(Hashtable hash)
    {
        return new LinearMotion(MoveParamTool.GetStartPoint(hash),
                                MoveParamTool.GetEndPoint(hash),
                                MoveParamTool.GetSpeed(hash));
    }

    public static LinearMotion GetMoveControl(Vector3 startPoint, Vector3 endPoint, float speed)
    {
        return new LinearMotion(startPoint, endPoint, speed);
    }

    public LinearMotion(Vector3 startPoint, Vector3 endPoint, float speed)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
        Vector3 dir = endPoint - startPoint;
        _totalLength = dir.magnitude;
        _speed = speed * Vector3.Normalize(dir);
    }


    public Vector3 GetPointByTime(ref bool isReachFinishline, float time)
    {
        Vector3 ds = time * _speed;
        isReachFinishline = ds.magnitude >= _totalLength;
        return _startPoint + ds;
    }

}
