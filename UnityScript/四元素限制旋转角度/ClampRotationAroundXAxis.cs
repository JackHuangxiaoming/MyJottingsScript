    /// 限定一个四元素的x值 的范围 
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
