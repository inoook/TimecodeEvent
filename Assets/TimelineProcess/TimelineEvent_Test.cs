using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineEvent_Test : MonoBehaviour
{
    [SerializeField] TimelineProcess timelineProcess = null;
    [SerializeField] SimpleLogGUI logGui = null;

    private void Start()
    {
        timelineProcess.eventTimeCode += TimelineProcess_eventTimeCode;
    }

    private void TimelineProcess_eventTimeCode(TimelineProcess.Timecode timeCode, float timeSec)
    {
        string log = $"Action: {timeCode.ToString()} / timeSec: {timeSec}";
        Debug.Log(log);
        logGui.AddLog(log);
    }
}
