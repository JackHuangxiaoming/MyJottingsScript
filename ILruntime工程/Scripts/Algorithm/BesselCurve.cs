using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 多阶贝塞尔曲线
/// </summary>
public class BesselCurve : IMoveCtrl
{
    private List<TwoBezier> _bezierList;
    private float _speed;
    private Vector3[] _totalPoints;

    public static IMoveCtrl GetMoveControl(Hashtable hash)
    {
        return new BesselCurve(MoveParamTool.GetPathArr(hash), MoveParamTool.GetSpeed(hash));
    }

    public static BesselCurve GetMoveControl(Vector3[] pathList, float speed)
    {
        return new BesselCurve(pathList, speed);
    }

    public BesselCurve(Vector3 startPos, Vector3 cont, Vector3 endPos, float speed = 1)
    {
        _speed = speed;
        _bezierList = new List<TwoBezier>();
        _bezierList.Add(new TwoBezier(startPos, cont, endPos));
    }

    public BesselCurve(Vector3[] pathList, float speed = 1)
    {
        _speed = speed;
        _bezierList = new List<TwoBezier>();

        for (int i = 0; i < pathList.Length - 2; i += 2)
        {
            _bezierList.Add(new TwoBezier(pathList[i], pathList[i + 1], pathList[i + 2]));
        }
    }

    public Vector3 GetPointByTime(ref bool isReachFinishline, float time)
    {
        float moveds = _speed * time;
        for (int i = 0; i < _bezierList.Count; i++)
        {
            if (moveds > _bezierList[i].Length)
            {
                moveds -= _bezierList[i].Length;
            }
            else
            {
                float timeT = moveds / _bezierList[i].Length;
                Vector3 targetPos = (_bezierList[i]).GetPointAtTime(timeT);
                return targetPos;
            }
        }
        isReachFinishline = true;
        return _bezierList[_bezierList.Count - 1].p2;
    }

    public Vector3[] GetPoints()
    {
        if (_totalPoints == null)
        {
            List<Vector3> list = new List<Vector3>();
            for (int i = 0; i < _bezierList.Count; i++)
            {
                list.AddRange(_bezierList[i].totalPoints);
            }
            _totalPoints = list.ToArray();
        }
        return _totalPoints;
    }
}
