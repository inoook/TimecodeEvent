using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBehav : MonoBehaviour
{

    [SerializeField] SimpleLogGUI logGui = null;
    [SerializeField] Rect drawRect = new Rect(10,10,200,200);

    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        GUILayout.BeginVertical("box");
        logGui.DrawGUI();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
