using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineProcess_Test : MonoBehaviour
{
    [SerializeField] TimelineProcess timelineProcess = null;

    // Start is called before the first frame update
    void Start()
    {
        timelineProcess.eventTimeCode += TimelineProcess_eventTimeCode;
        timelineProcess.eventComplete += TimelineProcess_eventComplete;
    }

    private void TimelineProcess_eventTimeCode(TimelineProcess.Timecode timeCode, float timeSec)
    {
        Debug.LogWarning($"{timeCode.ToString()} / timeSec: {timeSec}");
    }

    private void TimelineProcess_eventComplete()
    {
        Debug.LogWarning($"complete");
    }


    [SerializeField] float timeSec = 0;
    void Update()
    {

        if (!isPause)
        {
            if (!timelineProcess.IsComplete())
            {
                timeSec += Time.deltaTime;
            }
            else
            {
                timeSec = timelineProcess.EndTimeSec;
            }
            timelineProcess.Process(timeSec);
        }
    }

    public void Rest()
    {
        timeSec = 0;
        timelineProcess.Init();
    }

    bool isPause = false;
    public void DrawGUI()
    {
        GUILayout.Label(this.gameObject.name);
        if (GUILayout.Button("Rest"))
        {
            Rest();
        }
        if (GUILayout.Button("Puase"))
        {
            isPause = !isPause;
        }

        GUILayout.Label(timeSec.ToString("0.00"));

        timeSec = GUILayout.HorizontalSlider(timeSec, 0, timelineProcess.EndTimeSec);
    }

    [SerializeField] Rect drawRect = new Rect(10, 10, 400, 400);
    
    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        DrawGUI();
        GUILayout.EndArea();
    }
}
