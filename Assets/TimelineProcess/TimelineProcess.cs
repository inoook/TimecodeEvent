using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指定タイムコードでイベント実行 Actionを含むTimeCodeを発火する
/// </summary>
public class TimelineProcess : MonoBehaviour
{
    public static float ToTime(string timeCode)
    {
        return (float)(System.TimeSpan.Parse(timeCode).TotalSeconds);
    }
    public static string ToTimecode(float time)
    {
        return System.TimeSpan.FromSeconds(time).ToString(@"hh\:mm\:ss\.fff");
    }

    [System.Serializable]
    public class Action
    {
        public string type = "osc";
        public string param = "";
        public override string ToString()
        {
            return $"type: {type} / {param}";
        }
    }

    [System.Serializable]
    public class Timecode
    {
        public string timecode = "0:00:00.000";
        public Action action = null;

        public override string ToString()
        {
            return $"time: {timecode} / {action.ToString()}";
        }
    }

    //
    public delegate void TimeCodeHandler(Timecode timeCode, float timeSec);
    public event TimeCodeHandler eventTimeCode;

    public event KeyTimePlayer.ScenarioEventHandler eventComplete;
    //
    [System.Serializable]
    public class Setting
    {
        [SerializeField] public Timecode[] timecodes = null;
    }

    [SerializeField] bool autoApplySetting = true;
    [SerializeField] Setting setting = null;

    [SerializeField] KeyTimePlayer keyTimePlayer = null;

    Dictionary<KeyTimePlayer.KeyTime, Timecode> timecodeDic = new Dictionary<KeyTimePlayer.KeyTime, Timecode>();

    public KeyTimePlayer KeyTimePlayer => keyTimePlayer;
    public Setting GetSetting()
    {
        return setting;
    }

    public void ApplySetting(Setting setting)
    {
        this.setting = setting;
        //
        if (setting == null || setting.timecodes == null || setting.timecodes.Length <= 0) { return; }
        //
        List<KeyTimePlayer.KeyTime> keyCodeList = new List<KeyTimePlayer.KeyTime>();
        
        foreach (var timeCode in setting.timecodes)
        {
            float timeSec = ToTime(timeCode.timecode);
            KeyTimePlayer.KeyTime key = new KeyTimePlayer.KeyTime(timeSec);
            keyCodeList.Add(key);

            timecodeDic.Add(key, timeCode);
        }
        keyTimePlayer.Init(keyCodeList);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (autoApplySetting)
        {
            ApplySetting(setting);
        }

        keyTimePlayer.eventScenario += KeyTimePlayer_eventScenario;
        keyTimePlayer.eventScenarioComplete += KeyTimePlayer_eventScenarioComplete;
    }

    private void KeyTimePlayer_eventScenario(KeyTimePlayer.KeyTime key, float timeSec)
    {
        Timecode tCode = timecodeDic[key];
        eventTimeCode?.Invoke(tCode, timeSec);

    }
    private void KeyTimePlayer_eventScenarioComplete()
    {
        eventComplete?.Invoke();
    }

    public void Process(float timeSec)
    {
        keyTimePlayer.Process(timeSec);
    }

    public void Init()
    {
        keyTimePlayer.Init();
    }

    public float EndTimeSec => keyTimePlayer.EndTimeSec;
    public bool IsComplete() { return keyTimePlayer.IsComplete();}

    public void DrawGUI()
    {

    }
}
