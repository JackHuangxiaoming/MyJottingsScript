using UnityEngine;

public interface IMoveCtrl
{
    Vector3 GetPointByTime(ref bool isReachFinishline,float time);

}
