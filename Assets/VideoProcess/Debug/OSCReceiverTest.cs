using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uOSC;

public class OSCReceiverTest : MonoBehaviour
{
    [SerializeField] uOscServer oscReceiver = null;
    [SerializeField] SimpleLogGUI simpleLogGUI = null;

    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.onDataReceived.AddListener(OnDataReceived);
    }
    void OnDataReceived(Message message)
    {
        if(message.address == "/id")
        {
            string id = (string)message.values[0];

            simpleLogGUI.AddLog($"receive: {id}");
        }
    }

    [SerializeField] Rect drawRect = new Rect(10,10,200,200);

    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        GUILayout.BeginVertical("box");
        simpleLogGUI.DrawGUI();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
