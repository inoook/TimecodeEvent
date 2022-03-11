using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineToVideoBinder : MonoBehaviour
{
    [SerializeField] TimelineProcess timelineProcess = null;
    [SerializeField] VideoRenderUGUI videoRenderUGUI = null;

    // Update is called once per frame
    void Update()
    {
        timelineProcess.Process(videoRenderUGUI.CurrentTimeSec);
    }
}
