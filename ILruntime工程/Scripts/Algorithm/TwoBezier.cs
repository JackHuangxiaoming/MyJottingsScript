using System.Collections.Generic;
using UnityEngine;

public class TwoBezier
{
    public Vector3[] totalPoints;
    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public float ti = 0f;
    private Vector3 b0 = Vector3.zero;
    private Vector3 b1 = Vector3.zero;
    private Vector3 b2 = Vector3.zero;
    /// <summary>
    /// 贝塞尔曲线近似长度
    /// </summary>
    public float Length { get; private set; }

    public TwoBezier(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        this.p0 = v0;
        this.p1 = v1;
        this.p2 = v2;
        Length = GetBezierLength();
    }

    public Vector3 GetPointAtTime(float t)
    {
        float a = 1 - t;
        return a * a * p0 + 2 * t * a * p1 + t * t * p2;
    }

    private float GetBezierLength()
    {
        Vector3 point = p0;
        float totalDs = 0;
        List<Vector3> points = new List<Vector3>();
        for (float i = 0; i <= 1; i += 0.025f)
        {
            Vector3 next = GetPointAtTime(i);
            points.Add(next);
            totalDs += Vector3.Distance(point, next);
            point = next;
        }
        totalPoints = points.ToArray();
        return totalDs;
    }

}