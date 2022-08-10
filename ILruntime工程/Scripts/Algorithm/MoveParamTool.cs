using System;
using System.Collections;
using UnityEngine;

public class MoveParamTool
{
    /// <summary>
    /// 路线类型
    /// </summary>
    public const byte MoveType = (byte)1;
    /// <summary>
    /// 线速度
    /// </summary>
    public const byte Speed = (byte)2;
    /// <summary>
    /// 开始坐标
    /// </summary>
    public const byte StartPoint = (byte)3;
    /// <summary>
    /// 结束坐标
    /// </summary>
    public const byte EndPoint = (byte)4;
    /// <summary>
    /// 结束弧度值
    /// </summary>
    public const byte EndRadian = (byte)5;
    /// <summary>
    /// 初始半径
    /// </summary>
    public const byte Radius = (byte)6;
    /// <summary>
    /// 路径点集合
    /// </summary>
    public const byte PathArr = (byte)7;
    /// <summary>
    /// 正弦曲线的初相
    /// </summary>
    public const byte FirstPhase = (byte)8;




    public static MoveType GetMoveType(Hashtable hash)
    {
        if (hash.ContainsKey(MoveType))
        {
            return (MoveType)(byte)hash[MoveType];
        }
        else
        {
            return global::MoveType.BesselCurve;
        }

    }

    public static Vector3 GetStartPoint(Hashtable hash)
    {
        if (hash.ContainsKey(StartPoint))
        {
            return (Vector3)hash[StartPoint];
        }
        else
        {
            return Vector3.zero;
        }
    }

    public static Vector3 GetEndPoint(Hashtable hash)
    {
        if (hash.ContainsKey(EndPoint))
        {
            return (Vector3)hash[EndPoint];
        }
        else
        {
            return Vector3.zero;
        }

    }

    public static float GetSpeed(Hashtable hash)
    {
        if (hash.ContainsKey(Speed))
        {
            return Convert.ToSingle(hash[Speed]);
        }
        else
        {
            return 0;
        }
    }

    public static float GetEndRadian(Hashtable hash)
    {
        if (hash.ContainsKey(EndRadian))
        {
            return Convert.ToSingle(hash[EndRadian]) * Mathf.PI;
        }
        else
        {
            return 0;
        }
    }

    public static float GetRadius(Hashtable hash)
    {
        if (hash.ContainsKey(Radius))
        {
            return Convert.ToSingle(hash[Radius]);
        }
        else
        {
            return 0;
        }
    }


    public static Vector3[] GetPathArr(Hashtable hash)
    {
        if (hash.ContainsKey(PathArr))
        {
            return (Vector3[])hash[PathArr];
        }
        else
        {
            return new Vector3[0];
        }
    }

    public static float GetFirstPhase(Hashtable hash)
    {
        if (hash.ContainsKey(FirstPhase))
        {
            return Convert.ToSingle(hash[FirstPhase]) * Mathf.PI;
        }
        else
        {
            return 0;
        }
    }
}