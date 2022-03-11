using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLogGUI : MonoBehaviour
{

    string logStr = "";
    List<string> logs = new List<string>();
    [SerializeField] int maxLog = 10;

    public void AddLog(string str)
    {
        logs.Insert(0, $"[{System.DateTime.Now}] {str}");
        if (logs.Count > maxLog)
        {
            logs.RemoveAt(logs.Count - 1);
        }
        logStr = string.Join(System.Environment.NewLine, logs);
    }

    void Clear()
    {
        logs.Clear();
        logStr = "";
    }

    [SerializeField] Vector2 scrollPos = Vector2.zero;
    [SerializeField] float scrollHeight = 150;
    public void DrawGUI()
    {
        GUILayout.Label(this.gameObject.name);
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.Height(scrollHeight));
        GUILayout.Label(logStr);
        GUILayout.EndScrollView();
        if (GUILayout.Button("Clear"))
        {
            Clear();
        }
    }
}
