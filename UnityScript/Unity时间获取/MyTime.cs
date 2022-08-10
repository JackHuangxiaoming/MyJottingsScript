using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 时间枚举 数字时间
/// </summary>
public enum NumTime
{
    年,
    月,
    日,
    小时,
    分,
    秒,
    毫秒,
    时分,
    现在
}
/// <summary>
/// 时间枚举 汉字时间
/// </summary>
public enum ChineseTime
{
    现在,
    年月,
    月日,
    今天,
    明天,
    昨天
}
/// <summary>
/// 时间类
/// 参数NumTime/ChineseTime 
/// 拿到指定格式时间
/// </summary>
public static class MyTime
{
    public static string Get(NumTime tyep)
    {
        string OutTimt = null;
        switch (tyep)
        {
            case NumTime.年:
                OutTimt = System.DateTime.Now.Year.ToString();
                break;
            case NumTime.月:
                OutTimt = System.DateTime.Now.Month.ToString();
                break;
            case NumTime.日:
                OutTimt = System.DateTime.Now.Day.ToString();
                break;
            case NumTime.小时:
                OutTimt = System.DateTime.Now.Hour.ToString();
                break;
            case NumTime.分:
                OutTimt = System.DateTime.Now.Minute.ToString();
                break;
            case NumTime.秒:
                OutTimt = System.DateTime.Now.Second.ToString();
                break;
            case NumTime.现在:
                OutTimt = System.DateTime.Now.ToString();
                break;
            case NumTime.时分:
                OutTimt = System.DateTime.Now.ToString("t");
                break;
        }
        return OutTimt;
    }
    public static string Get(ChineseTime tyep)
    {
        string OutTimt = null;
        switch (tyep)
        {
            case ChineseTime.现在:
                OutTimt = System.DateTime.Now.ToString("f");
                break;
            case ChineseTime.年月:
                OutTimt = System.DateTime.Now.ToString("y");
                break;
            case ChineseTime.月日:
                OutTimt = System.DateTime.Now.ToString("m");
                break;
            case ChineseTime.今天:
                OutTimt = System.DateTime.Now.Date.ToShortDateString();
                break;
            case ChineseTime.明天:
                OutTimt = System.DateTime.Now.AddDays(1).ToShortDateString();
                break;
            case ChineseTime.昨天:
                OutTimt = System.DateTime.Now.AddDays(-1).ToShortDateString();
                break;
        }
        return OutTimt;
    }
}

