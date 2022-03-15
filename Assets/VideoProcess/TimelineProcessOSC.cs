using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uOSC;

public class TimelineProcessOSC : MonoBehaviour
{
    [SerializeField] TimelineProcess timelineProcess = null;
    [SerializeField] uOscClient oscSender = null;
    [SerializeField] SimpleLogGUI logGui = null;

    [SerializeField] string oscAddress = "/id";

    // Start is called before the first frame update
    void Start()
    {
        timelineProcess.eventTimeCode += TimelineProcess_eventTimeCode;
    }

    public void SetSetting(string ip, int port)
    {
        oscSender.address = ip;
        oscSender.port = port;
    }

    private void TimelineProcess_eventTimeCode(TimelineProcess.Timecode timeCode, float timeSec)
    {
        string param = timeCode.action.param;
        SendOsc(oscAddress, timeSec, param);
    }

    void SendOsc(string address, float timeSec, params object[] values)
    {
        logGui.AddLog($"send: {timeSec.ToString("0.00")} sec > osc: {address} [{string.Join(",", values)}]");
        oscSender.Send(address, values);
    }

    public void DrawGUI()
    {
        GUILayout.Label(this.gameObject.name);
        GUILayout.Label($"Timecode event OSC送信先 [ {oscSender.address} : {oscSender.port} ]");
    }
}
