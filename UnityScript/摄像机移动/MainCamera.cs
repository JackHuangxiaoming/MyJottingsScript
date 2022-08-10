using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : Singleton<MainCamera>
{
    private Camera Camera;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (GameMarager.Single.State != State.Game) return;

        //摄像机的旋转
        Quaternion CameraRotation = Camera.transform.localRotation;
        //自己+鼠标的Y值 *= 
        CameraRotation *= Quaternion.Euler(InputMarager.Vertical * PlayerColtroller.Single.RotationSpeed * Time.deltaTime, 0, 0);
        //限定Y值的范围
        CameraRotation = ClampRotationAroundXAxis(CameraRotation);
        //赋值
        Camera.transform.localRotation = CameraRotation;
    }

    /// <summary>
    /// 限定一个四元素 的范围 
    /// 这儿是-65 -- 65
    /// </summary>
    /// <param name="Q"></param>
    /// <returns></returns>
    private Quaternion ClampRotationAroundXAxis(Quaternion Q)
    {
        //得到 xyz 的弧度
        Q.x /= Q.w;
        Q.y /= Q.w;
        Q.z /= Q.w;
        Q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(Q.x);

        angleX = Mathf.Clamp(angleX, -65, 65);

        Q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return Q;
    }
}
