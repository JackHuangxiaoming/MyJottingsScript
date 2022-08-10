using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;
/// <summary>
/// ÉãÏñ»ú¸úËæÄ¿±ê
/// </summary>
//[RequireComponent(typeof(RectTransform))]
public class CameraFollowPlayer : MonoBehaviour
{
    RectTransform rectTransform;
    public Transform FollowTarget;
    public float OffsetW, OffsetH;
    private Vector2 OffsetVector;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        OffsetVector = new Vector2(OffsetW, OffsetH);
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 Player2DPosition = Camera.main.WorldToScreenPoint(FollowTarget.position);
        rectTransform.position = Player2DPosition + OffsetVector;
    
    }
}