using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 外部ファイルjsoを読み込んで、指定TimeCodeで実行
/// </summary>
public class TimelineProcess_Fileload : MonoBehaviour
{
    [SerializeField] string settingFile = "setting.json";
    [SerializeField] TimelineProcess timelineProcess = null;

    string settingPath => Application.dataPath + "/../../_Externals/";
    TimelineProcess.Setting LoadSetting(string settingFile)
    {
        string path = System.IO.Path.Combine(settingPath, settingFile);
        Debug.Log("path: " + path);
        string jsonStr = FileUtils.Read(path);
        if (!string.IsNullOrEmpty(jsonStr))
        {
            return JsonUtility.FromJson<TimelineProcess.Setting>(jsonStr);
        }
        return null;
    }

    void ApplySetting(TimelineProcess.Setting setting)
    {
        timelineProcess.ApplySetting(setting);
    }

    [ContextMenu("SaveSetting")]
    void SaveSetting()
    {
        string path = System.IO.Path.Combine(settingPath, settingFile);
        string jsonStr = JsonUtility.ToJson(timelineProcess.GetSetting(), true);
        FileUtils.Write(path, jsonStr);
    }
    // Start is called before the first frame update
    void Awake()
    {
        TimelineProcess.Setting loadSetting = LoadSetting(settingFile);
        if (loadSetting != null)
        {
            ApplySetting(loadSetting);
        }
    }

    private void Start()
    {
        timelineProcess.eventTimeCode += TimelineProcess_eventTimeCode;
    }

    public void Process(float timeSec)
    {
        timelineProcess.Process(timeSec);
    }

    private void TimelineProcess_eventTimeCode(TimelineProcess.Timecode timeCode, float timeSec)
    {
        Debug.Log($"Action: {timeCode.ToString()} / timeSec: {timeSec}");
    }

}
