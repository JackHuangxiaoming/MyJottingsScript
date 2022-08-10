using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FairyGUIGizmos : MonoBehaviour
{
    Rect trct = new Rect();
    GUIStyle style = new GUIStyle();
    private void Start()
    {
        trct.Set(0, 0, Screen.width, 20);
        style.fontSize = 60;
        style.normal.textColor = Color.white;
    }

    private void OnGUI()
    {
        if (GRoot.inst.touchTarget != null)
        {            
            GUI.TextArea(trct, GRoot.inst.touchTarget.name, style);
        }
    }
}
