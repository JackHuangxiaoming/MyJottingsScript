using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMarager : MonoBehaviour
{
    /// <summary>
    /// 鼠标移动的速度倍率
    /// </summary>
    public float MouseSpeed = 1f;

    /// <summary>
    /// 键盘的按键
    /// </summary>
    public static bool Escape { get; private set; }
    /// <summary>
    /// 鼠标X轴的值
    /// </summary>
    public static float Horizontal { get; private set; }
    /// <summary>
    /// 鼠标Y轴的值
    /// </summary>
    public static float Vertical { get; private set; }
    /// <summary>
    /// 向前和向后
    /// </summary>
    public static float ForwardBack { get; private set; }
    /// <summary>
    /// 向左和向右
    /// </summary>
    public static float LeftRight { get; private set; }
    /// <summary>
    /// 交互UI展示的按键  E
    /// </summary>
    private static bool use;
    public static bool Use
    {
        get
        {
            bool b = use;
            use = false;
            return b;
        }
        private set { use = value; }
    }
    /// <summary>
    /// 空格跳跃 
    /// </summary>
    public static bool Space { get; private set; }



    void Update()
    {
        if (GameMarager.Single.State == State.StartMenu) return;

        LeftRight = 0;
        ForwardBack = 0;

        Horizontal = Input.GetAxis("Mouse X") * MouseSpeed;
        Vertical = -Input.GetAxis("Mouse Y") * MouseSpeed;
        Escape = Input.GetKeyDown(KeyCode.Escape);
        Space = Input.GetKeyDown(KeyCode.Space);
        Use = Input.GetKeyDown(KeyCode.E);

        if (Input.GetKey(KeyCode.W)) ForwardBack = 1;
        if (Input.GetKey(KeyCode.S)) ForwardBack = -1;
        if (Input.GetKey(KeyCode.A)) LeftRight = -1;
        if (Input.GetKey(KeyCode.D)) LeftRight = 1;
    }
}
